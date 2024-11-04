using Evaluacion3.BD.Data;
using Evaluacion3.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion3.SERVER.Controllers
{
    [ApiController]
    [Route("api/TipoDocumentos")]
    public class TipoDocumentosControllers : ControllerBase
    {
        private readonly Context context;
        public TipoDocumentosControllers(Context context)
        {
            this.context = context;
        }

        [HttpGet] //Es como el select en sql server
        public async Task<ActionResult<List<TipoDocumento>>> Get() // es de manera asincrona para optimizar el funcionamiento de las capas
        {
            return await context.TipoDocumentos.ToListAsync();  
        }

        [HttpGet("{Id:int}")] //api/TipoDocumentos/1
        public async Task<ActionResult<TipoDocumento>> Get(int Id)
        { 
            var Evaluacion3 = await context.TipoDocumentos
                                    .FirstOrDefaultAsync(x=>x.Id == Id);

            if (Evaluacion3 == null)
            {
                return NotFound();
            }
            return Evaluacion3;
        }

        [HttpGet("GetByCod/{cod}")] //api/TipoDocumentos/GetByCod/DNI
        public async Task<ActionResult<TipoDocumento>> GetByCod(string cod)
        {
            var Evaluacion3 = await context.TipoDocumentos
                                    .FirstOrDefaultAsync(x => x.Codigo == cod);

            if (Evaluacion3 == null)
            {
                return NotFound();
            }
            return Evaluacion3;
        }

        [HttpGet("Existe/{Id:int}")] //api/TipoDocumentos/existe/1

        public async Task<ActionResult<bool>> Existe(int Id)
        {

            var existe = await context.TipoDocumentos.AnyAsync(x => x.Id == Id);
            return existe;
        }



        [HttpPost] //es un metodo de insert
        public async Task<ActionResult<int>> Post(TipoDocumento entidad)
        {
            try
            {
                context.TipoDocumentos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        [HttpPut("{Id:int")] //api/TipoDocumentos/1 //Metodo que sirve para modificar un registro
        public async Task<ActionResult> Put(int Id, [FromBody] TipoDocumento entidad)
        {
            {

                if (Id != entidad.Id)
                {
                    return BadRequest("Datos Incorrectos");
                }
                var Evaluacion3 = await context.TipoDocumentos
                                .Where(e => e.Id == Id)
                                .FirstOrDefaultAsync();

                if (Evaluacion3 == null)
                {
                    return NotFound("No existe el tipo de documento buscado");
                }

                Evaluacion3.Codigo = entidad.Codigo;
                Evaluacion3.Nombre = entidad.Nombre;

                try
                {
                    context.TipoDocumentos.Update(Evaluacion3);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);

                }
            }
        }

        [HttpDelete("{Id:int}")] //api/TipoDocumentos/1 

        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.TipoDocumentos.AnyAsync(x => x.Id==Id); //metodo para ver si existe 

            if (!existe) 
            {
                return NotFound($"El tipo de documento {Id} no existe");
            }
            TipoDocumento  EntidadBorrar = new TipoDocumento(); //instancio la entidad que quiero borrar
            EntidadBorrar.Id = Id;

            context.Remove(EntidadBorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }

}