using Evaluacion3.BD.Data.Entity;
using Evaluacion3.BD.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion3.SERVER.Controllers
{
    [ApiController]
    [Route("api/Personas")]
    public class PersonasControllers : ControllerBase
    {
        private readonly Context context;
        public PersonasControllers (Context context)
        {
            this.context = context;
        }

        [HttpGet] //Es como el select 
        public async Task<ActionResult<List<Persona>>> Get() // es de manera asincrona para optimizar el funcionamiento de las capas
        {
            return await context.Personas.ToListAsync();
        }

        [HttpGet("{Id:int}")] //api/Personas/1
        public async Task<ActionResult<Persona>> Get(int Id)
        {
            var Evaluacion3 = await context.Personas
                                    .FirstOrDefaultAsync(x => x.Id == Id);

            if (Evaluacion3 == null)
            {
                return NotFound();
            }
            return Evaluacion3;
        }

        [HttpGet("GetByCod/{Nombre}")] //api/Personas/GetByNombre/Nombre
        public async Task<ActionResult<Persona>> GetByNombre(string Nombre)
        {
            Persona? Evaluacion3 = await context.Personas
                                    .FirstOrDefaultAsync(x => x.Nombre == Nombre);

            if (Evaluacion3 == null)
            {
                return NotFound();
            }
            return Evaluacion3;
        }

        [HttpGet("Existe/{Id:int}")] //api/Personas/existe/1

        public async Task<ActionResult<bool>> Existe(int Id)
        {

            var existe = await context.Personas.AnyAsync(x => x.Id == Id);
            return existe;
        }



        [HttpPost] //es un metodo de insert
        public async Task<ActionResult<int>> Post(Persona entidad)
        {
            try
            {
                context.Personas.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        [HttpPut("{Id:int")] //api/Personas/1 //Metodo que sirve para modificar un registro
        public async Task<ActionResult> Put(int Id, [FromBody] TipoDocumento entidad)
        {
            {

                if (Id != entidad.Id)
                {
                    return BadRequest("Datos Incorrectos");
                }
                var Evaluacion3 = await context.Personas
                                .Where(e => e.Id == Id)
                                .FirstOrDefaultAsync();

                if (Evaluacion3 == null)
                {
                    return NotFound("No existe la persona buscada");
                }

                Evaluacion3.NumDoc = entidad.Codigo;
                Evaluacion3.Nombre = entidad.Nombre;

                try
                {
                    context.Personas.Update(Evaluacion3);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);

                }
            }
        }

        [HttpDelete("{Id:int}")] //api/Personas/1 

        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Personas.AnyAsync(x => x.Id == Id); //metodo para ver si existe 

            if (!existe)
            {
                return NotFound($"La persona {Id} no existe");
            }
            TipoDocumento EntidadBorrar = new TipoDocumento(); //instancio la entidad que quiero borrar
            EntidadBorrar.Id = Id;

            context.Remove(EntidadBorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }

}
   