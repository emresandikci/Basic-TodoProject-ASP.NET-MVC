using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TodoListProject.Domain;
using TodoListProject.Models;
using AutoMapper;
using Newtonsoft.Json;

namespace TodoListProject.Controllers
{
    public class TodoesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Todoes
        public TodoesController()
        {
        }
        [Route("api/todo/list")]
        [HttpGet]
        public IHttpActionResult GetTodos()
        {
            var result = db.Todos;
            if (result == null)
                return NotFound();
            // return Ok(result);
            //Mapper.Map<ICollection<Todo>, ICollection<TodoViewModel>>(result)
            //return Ok(JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            //{
            //    PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //}));Mapper.Map<ICollection<Todo>, ICollection<TodoViewModel>>(result.ToList())
            return Ok(result.ToList());
        }

        // GET: api/Todoes/5
        [Route("api/todo/getbyId/{itemId}")]
        [ResponseType(typeof(Todo))]
        public async Task<IHttpActionResult> GetTodo(string itemId)
        {
            Todo todo = await db.Todos.FindAsync(itemId);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/Todoes/5
        [Route("api/todo/put/{itemId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTodo(string itemId, Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (itemId != todo.Id)
            {
                return BadRequest();
            }

            db.Entry(todo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(itemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Todoes
        [Route("api/todo/post")]
        [ResponseType(typeof(Todo))]
        public async Task<IHttpActionResult> PostTodo(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Todos.Add(todo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TodoExists(todo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todoes/5
        [Route("api/todo/delete/{itemId}")]
        [ResponseType(typeof(Todo))]
        public async Task<IHttpActionResult> DeleteTodo(string itemId)
        {
            Todo todo = await db.Todos.FindAsync(itemId);
            if (todo == null)
            {
                return NotFound();
            }

            db.Todos.Remove(todo);
            await db.SaveChangesAsync();

            return Ok(todo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoExists(string id)
        {
            return db.Todos.Count(e => e.Id == id) > 0;
        }
    }
}