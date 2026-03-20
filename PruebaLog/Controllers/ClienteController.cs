using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaLog.Models;

namespace PruebaLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly PruebaLogContext _baseDatos;

        //Metodo para incializar la base de datos
        public ClienteController(PruebaLogContext basesDatos)
        {
            _baseDatos = basesDatos;
        }

        //METODO GET
        [HttpGet] //TIPO DE PETICION
        [Route("ListaClientes")] //Personalizar ruta
        //FUNCION PARA CONSULTAR LA LISTA DE EMPLEADOS
        public async Task<IActionResult> lista()
        {
            //DECLARAMOS VARIABLE Y CONSULTAMOS EN BD Y LO TRAEMOS EN LISTA
            var listaCliente = await _baseDatos.Clientes.ToListAsync();
            //MOSTRAMOS LA LISTA
            return Ok(listaCliente);
        }


        [HttpPost] // PETICION DE TIPO POST PARA CREAR
        [Route("AgregarCliente")]

        public async Task<IActionResult> Agregar([FromBody] Cliente request)
        {
            //SE LLAMA EL MODELO CLIENTE Y ATRAVES DE LA FUNCION ADD SE AGREGA LO QUE EL USUARIO MANDO EN FRONT COMO REQUEST
            await _baseDatos.Clientes.AddAsync(request);
            //SE GUARDAN CAMBIOS
            await _baseDatos.SaveChangesAsync();
            //REGRESA UN 200 CON TODOS LOS DATOS GUARDADOS
            return Ok(request);
        }


        [HttpPut]
        [Route("EditarCliente/{idCliente:int}")]

        public async Task<IActionResult> Modificar(int idCliente, [FromBody] Cliente request)
        {
            var cienteModificar = await _baseDatos.Clientes.FindAsync(idCliente);

            if(cienteModificar == null)
            {
                return NotFound("No existe el cliente");

            }
            cienteModificar.Nombre = request.Nombre;
            cienteModificar.Telefono = request.Telefono;
            cienteModificar.CorreoElectronico = request.CorreoElectronico;
            cienteModificar.Estatus = request.Estatus;

            try
            {
                await _baseDatos.SaveChangesAsync();
                return Ok(cienteModificar);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "Error al actualizar el cliente");
            }
  
 
        }

        [HttpDelete]
        [Route("EliminarCliente/{idCliente:int}")]
        public async Task<IActionResult> Eliminar(int idCliente)
        {
            var clienteEliminar = await _baseDatos.Clientes.FindAsync(idCliente);

            if (clienteEliminar == null)
            {
                return NotFound("No existe el cliente");
            }

            _baseDatos.Clientes.Remove(clienteEliminar);

            try
            {
                await _baseDatos.SaveChangesAsync();
                return Ok(new { Mensaje = "cliente eliminado correctamente" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "error al eliminar el cliente");
            }
        }

    }
}
