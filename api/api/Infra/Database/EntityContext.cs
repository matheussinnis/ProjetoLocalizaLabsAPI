using System;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using api.Domain.Entities;

namespace api.Infra.Database
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options): base(options){}

        public EntityContext(){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            optionsBuilder.UseSqlServer(jAppSettings["ConnectionString"].ToString());
        }
        
        public DbSet<User> Users { get; set; }
    }
}
