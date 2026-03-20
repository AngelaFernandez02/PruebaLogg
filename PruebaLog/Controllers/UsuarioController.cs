using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaLog.Models;

namespace PruebaLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly PruebaLogContext _baseDatos;

        public UsuarioController(PruebaLogContext basesDatos)
        {
            _baseDatos = basesDatos;
        }




        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario request)
        {
            // Validar campos vacíos
            if (string.IsNullOrEmpty(request.Usuario1) || string.IsNullOrEmpty(request.Contrasenia))
            {
                return BadRequest(new { mensaje = "Usuario y contraseña son obligatorios" });
            }

            // Verificar si ya existe el usuario
            var existe = await _baseDatos.Usuarios
                .AnyAsync(u => u.Usuario1 == request.Usuario1);

            if (existe)
            {
                return BadRequest(new { mensaje = "El usuario ya existe" });
            }

            // Hashear contraseña
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Contrasenia);

            var nuevoUsuario = new Usuario
            {
                Usuario1 = request.Usuario1,
                Contrasenia = passwordHash
            };

            await _baseDatos.Usuarios.AddAsync(nuevoUsuario);
            await _baseDatos.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario registrado correctamente"
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Models.LoginRequest request)
        {
            // Validar datos vacíos
            if (string.IsNullOrEmpty(request.Usuario) || string.IsNullOrEmpty(request.Contrasenia))
            {
                return BadRequest(new { mensaje = "Usuario y contraseña son obligatorios" });
            }

            var usuario = await _baseDatos.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == request.Usuario);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Usuario no encontrado" });
            }

            bool passwordValido;

            try
            {
                // Si está hasheada
                passwordValido = BCrypt.Net.BCrypt.Verify(request.Contrasenia, usuario.Contrasenia);
            }
            catch
            {
                // Si NO está hasheada (fallback temporal)
                passwordValido = request.Contrasenia == usuario.Contrasenia;
            }

            if (!passwordValido)
            {
                return Unauthorized(new { mensaje = "Contraseña incorrecta" });
            }

            return Ok(new
            {
                mensaje = "Login exitoso",
                usuario = new
                {
                    usuario.IdUsuario,
                    usuario.Usuario1
                }
            });
        }
    }
}