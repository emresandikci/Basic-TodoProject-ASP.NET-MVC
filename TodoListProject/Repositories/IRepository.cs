using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListProject.Repositories
{
    public interface IRepository<T>
    {
        int Create(T item);
        int Update(T item);
        int Delete(string Id);
        ICollection<T> getAll();
        T getByItemId(string Id);
    }
}