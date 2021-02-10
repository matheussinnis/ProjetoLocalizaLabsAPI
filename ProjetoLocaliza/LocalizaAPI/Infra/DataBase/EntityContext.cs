using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizaAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LocalizaAPI.Infra.DataBase
{
    public class EntityContext : DbContext
    {

        public EntityContext(DbContextOptions<EntityContext> options) : base(options) { }

        public EntityContext() { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
        //    optionsBuilder.UseSqlServer(jAppSettings["ConnectionString"].ToString());
        //}

        public DbSet<Usuario> Usuario { get; set; }
    }
}
