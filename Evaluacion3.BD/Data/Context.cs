using Evaluacion3.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion3.BD.Data
{
    //Esta clase representara dentro del programa la base de datos
    public class Context : DbContext
    {
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        
        
        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
