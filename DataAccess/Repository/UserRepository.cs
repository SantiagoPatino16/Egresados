using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using DataAccess.ConnectionDB;

namespace DataAccess.Repository
{
    public class UserRepository
    {
        // Crear (Register)
        public bool RegistrarUsuario(AttributesUser usuario)
        {
            try
            {
                using (var context = new RSContext())
                {
                    usuario.FechaRegistro = DateTime.Now;
                    usuario.Activo = true;

                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Leer todos (solo activos)
        public List<AttributesUser> ObtenerUsuarios()
        {
            using (var context = new RSContext())
            {
                return context.Usuarios
                             .Where(u => u.Activo)
                             .ToList();
            }
        }

        // Leer por Id
        public AttributesUser ObtenerUsuarioPorId(int id)
        {
            using (var context = new RSContext())
            {
                return context.Usuarios.FirstOrDefault(u => u.IdUsuario == id && u.Activo);
            }
        }

        //Obtener usuario por correo
        public AttributesUser ObtenerUsuarioPorCorreo(string correo)
        {
            using (var context = new RSContext())
            {
                return context.Usuarios.FirstOrDefault(u => u.Correo == correo);
            }
        }


        // Actualizar
        public bool ActualizarUsuario(AttributesUser usuario)
        {
            try
            {
                using (var context = new RSContext())
                {
                    var existente = context.Usuarios.Find(usuario.IdUsuario);
                    if (existente == null) return false;

                    existente.NombreCompleto = usuario.NombreCompleto;
                    existente.Correo = usuario.Correo;
                    existente.ClaveHash = usuario.ClaveHash;
                    existente.Rol = usuario.Rol;
                    existente.Activo = usuario.Activo;

                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Eliminación por campo activo
        public bool EliminarUsuario(int id)
        {
            try
            {
                using (var context = new RSContext())
                {
                    var usuario = context.Usuarios.Find(id);
                    if (usuario == null) return false;

                    usuario.Activo = false;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Login
        public AttributesUser Login(string correo, string claveHash)
        {
            using (var context = new RSContext())
            {
                return context.Usuarios
                    .FirstOrDefault(u => u.Correo == correo && u.ClaveHash == claveHash && u.Activo);
            }
        }

        //validar usuario
        public AttributesUser ValidarUsuario(string correo, string clave)
        {
            using (var context = new RSContext())
            {
                // Busca el usuario por correo
                var usuario = context.Usuarios.FirstOrDefault(u => u.Correo == correo);

                if (usuario != null)
                {
                    // Aquí comparamos el hash real con el ingresado
                    // Por ahora ejemplo simple: comparación directa
                    if (usuario.ClaveHash == clave)
                    {
                        return usuario;
                    }
                }

                return null; // No existe o clave incorrecta
            }
        }
    }
}
