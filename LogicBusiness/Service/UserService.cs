using Common.Entities;
using DataAccess.Repository;
using LogicBusiness.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicBusiness.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        // Registrar usuario (encriptando clave antes de guardar)
        public bool RegistrarUsuario(AttributesUser usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre) ||
                string.IsNullOrWhiteSpace(usuario.Correo) ||
                string.IsNullOrWhiteSpace(usuario.ClaveHash))
            {
                return false;
            }

            // Validar dominio institucional
            if (usuario.Rol == "Egresado")
            {
                if (!usuario.Correo.EndsWith("@pascualbravo.edu.co", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }


            // Verificar si el correo ya existe
            var existente = _userRepository.ObtenerUsuarioPorCorreo(usuario.Correo);
            if (existente != null)
            {
                // El correo ya está registrado
                return false;
            }


            // Encriptamos con las funciones previamente creadas
            usuario.ClaveHash = SecurityHelper.HashPassword(usuario.ClaveHash);


            return _userRepository.RegistrarUsuario(usuario);
        }

        public bool RegistrarEmpresa(AttributesUser usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre) ||
                string.IsNullOrWhiteSpace(usuario.Nit) ||
                string.IsNullOrWhiteSpace(usuario.Correo) ||
                string.IsNullOrWhiteSpace(usuario.ClaveHash))
            {
                return false;
            }
            //Verificar que si entre como no verificada en primera instancia
            if(usuario.Rol == "Empresa")
            {
                usuario.Verificado = false;
            }
            // Verificar si el correo ya existe
            var existente = _userRepository.ObtenerUsuarioPorCorreo(usuario.Correo);
            if (existente != null)
            {
                // El correo ya está registrado
                return false;
            }
            // Encriptamos con las funciones previamente creadas
            usuario.ClaveHash = SecurityHelper.HashPassword(usuario.ClaveHash);

            return _userRepository.RegistrarUsuario(usuario);
        }
        //Login
        public AttributesUser Login(string correo, string clave)
        {
            var usuario = _userRepository.ObtenerUsuarioPorCorreo(correo);

            if (usuario == null)
                return null;

            bool valido = SecurityHelper.VerifyPassword(clave, usuario.ClaveHash);

            if (!valido)
                return null;

            return usuario;
        }


        // Obtener lista de usuarios activos
        public List<AttributesUser> ObtenerUsuarios()
        {
            return _userRepository.ObtenerUsuarios();
        }

        // Obtener un usuario por Id
        public AttributesUser ObtenerUsuarioPorId(int id)
        {
            return _userRepository.ObtenerUsuarioPorId(id);
        }

        public List<AttributesUser> ObtenerUsuariosPorIds(List<int> ids)
        {
            return _userRepository.ObtenerUsuarios()
                .Where(u => ids.Contains(u.IdUsuario))
                .ToList();
        }

        // Actualizar
        public bool ActualizarUsuario(AttributesUser usuario)
        {
            return _userRepository.ActualizarUsuario(usuario);
        }

        // Eliminación por campo activo
        public bool EliminarUsuario(int id)
        {
            return _userRepository.EliminarUsuario(id);
        }

        public AttributesUser ValidarUsuario(string correo, string clave)
        {
            var usuario = _userRepository.ObtenerUsuarioPorCorreo(correo);
            if (usuario != null)
            {
                // Verifica usando el hash guardado (con sal)
                bool esValido = SecurityHelper.VerifyPassword(clave, usuario.ClaveHash);
                if (esValido)
                    return usuario;
            }
            return null;
        }
        // Listar empresas sin verificar
        public List<AttributesUser> ObtenerEmpresasSinVerificar()
        {
            return _userRepository.ObtenerEmpresasSinVerificar();
        }

        public List<AttributesUser> ObtenerEmpresasVerificadas()
        {
            return _userRepository.ObtenerEmpresasVerificadas();
        }

        //Verificar o cambiar de estado para aprobar el ingreso de la empresa a el sistema.
        public bool VerificarEmpresa(int idEmpresa)
        {
            return _userRepository.VerificarEmpresa(idEmpresa);
        }


        public string ObtenerFotoPerfil(int idUsuario)
        {
            var usuario = _userRepository.ObtenerUsuarioPorId(idUsuario);
            return string.IsNullOrEmpty(usuario?.FotoPerfil)
                ? "~/Resources/perfiles/default.png"
                : usuario.FotoPerfil;
        }

        public bool ActualizarFotoPerfil(int idUsuario, string nuevaFotoUrl)
        {
            var usuario = _userRepository.ObtenerUsuarioPorId(idUsuario);
            if (usuario == null) return false;
            usuario.FotoPerfil = nuevaFotoUrl;
            return _userRepository.ActualizarUsuario(usuario);
        }

        // Obtener usuarios aleatorios para sugerencias
        public List<AttributesUser> ObtenerUsuariosAleatorios(int idUsuarioActual, int cantidad = 10)
        {
            return _userRepository.ObtenerUsuariosAleatorios(idUsuarioActual, cantidad);
        }


    }
}
