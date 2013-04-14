﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hero.Sample.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ToDosContext : DbContext
    {
        public ToDosContext()
            : base("ToDosConnection")
        {
        }
        public DbSet<ToDo> Items { get; set; }   
    }
}