using Common.Entities;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

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
        public List<AttributesUser> ObtenerUsuarios(string filtro = "")
        {
            using (var context = new RSContext())
            {
                var query = context.Usuarios
                    .Where(u => u.Activo);

                if (!string.IsNullOrEmpty(filtro))
                {
                    // EF maneja Comparisons si lo pasas así
                    query = query.Where(u => u.Nombre.Contains(filtro));
                }

                return query.ToList();
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
        //Listar empresas sin verificar
        public List<AttributesUser> ObtenerEmpresasSinVerificar()
        {
            using (var context = new RSContext())
            {
                return context.Usuarios
                    .Where(u => u.Rol == "Empresa" && u.Verificado == false && u.Activo == true)
                    .ToList();
            }
        }
        //Cambio de estado de verificar empresa, para aprobar su inserción al software
        public bool VerificarEmpresa(int idEmpresa)
        {
            try
            {
                using (var context = new RSContext())
                {
                    var empresa = context.Usuarios
                                         .FirstOrDefault(u => u.IdUsuario == idEmpresa && u.Rol == "Empresa");

                    if (empresa == null)
                        return false;

                    empresa.Verificado = true;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        //Obtener empresas que hacen parte del sistema.
        public List<AttributesUser> ObtenerEmpresasVerificadas()
        {
            using (var context = new RSContext())
            {
                return context.Usuarios
                    .Where(u => u.Rol == "Empresa" && u.Verificado == true)
                    .ToList();
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

                    existente.Nombre = usuario.Nombre;
                    existente.Correo = usuario.Correo;
                    existente.ClaveHash = usuario.ClaveHash;
                    existente.Rol = usuario.Rol;
                    existente.Activo = usuario.Activo;
                    existente.FotoPerfil = usuario.FotoPerfil;
                    existente.CiudadResidencia = usuario.CiudadResidencia;
                    existente.ProgramaAcademico = usuario.ProgramaAcademico;
                    existente.AnioGraduacion = usuario.AnioGraduacion;
                    existente.EmpresaActual = usuario.EmpresaActual;
                    existente.CargoActual = usuario.CargoActual;
                    existente.LinkedIn = usuario.LinkedIn;
                    existente.SitioWebPersonal = usuario.SitioWebPersonal;
                    existente.Biografia = usuario.Biografia;
                    existente.Verificado = usuario.Verificado;
                    existente.TelefonoContacto = usuario.TelefonoContacto;
                    existente.SectorIndustria = usuario.SectorIndustria;
                    existente.PersonaRepresentante = usuario.PersonaRepresentante;

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
        public void ActualizarFotoPerfil(int idUsuario, string rutaFoto)
        {
            using (var context = new RSContext())
            {
                var usuario = context.Usuarios.Find(idUsuario);
                if (usuario != null)
                {
                    usuario.FotoPerfil = rutaFoto;
                    context.SaveChanges();
                }
            }
        }

        // Obtener usuarios aleatorios para sugerencias (excluyendo al usuario actual)
        public List<AttributesUser> ObtenerUsuariosAleatorios(int idUsuarioActual, int cantidad = 4)
        {
            using (var context = new RSContext())
            {
                return context.Usuarios
                    .Where(u => u.Activo && 
                               u.IdUsuario != idUsuarioActual &&
                               (u.Rol == "Empresa" || u.Rol == "Egresado" || u.Rol == "Estudiante"))
                    .OrderBy(u => Guid.NewGuid()) // Orden aleatorio
                    .Take(cantidad)
                    .ToList();
            }
        }

    }
}
