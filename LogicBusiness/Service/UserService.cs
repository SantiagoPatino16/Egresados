using Common.Entities;
using DataAccess.Repository;
using LogicBusiness.Security;
using System;
using System.Collections.Generic;

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
            if (string.IsNullOrWhiteSpace(usuario.NombreCompleto) ||
                string.IsNullOrWhiteSpace(usuario.Correo) ||
                string.IsNullOrWhiteSpace(usuario.ClaveHash))
            {
                return false;
            }

            // Verificar si el correo ya existe
            var existente = _userRepository.ObtenerUsuarioPorCorreo(usuario.Correo);
            if (existente != null)
            {
                // El correo ya está registrado
                return false;
            }

            // Encriptamos con SHA256 antes de guardar
            usuario.ClaveHash = SecurityHelper.GetSHA256(usuario.ClaveHash);

            return _userRepository.RegistrarUsuario(usuario);
        }
        //Login
        public AttributesUser Login(string correo, string clave)
        {
            string claveHash = SecurityHelper.GetSHA256(clave);

            return _userRepository.Login(correo, claveHash);
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
                // Compara el hash ingresado con el hash guardado
                string hashIngresado = SecurityHelper.GetSHA256(clave);
                if (usuario.ClaveHash == hashIngresado)
                    return usuario;
            }
            return null;
        }
    }
}
