using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Domain;
using TodoListProject.Models;

namespace TodoListProject.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Todo, TodoViewModel>()
                .ReverseMap();
            });
        }
    }
}