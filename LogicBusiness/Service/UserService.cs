using Common.Entities;
using DataAccess.ConnectionDB;
using DataAccess.Repository;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using LogicBusiness.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

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

            // Encriptamos con las funciones previamente creadas
            usuario.ClaveHash = SecurityHelper.HashPassword(usuario.ClaveHash);


            return _userRepository.RegistrarUsuario(usuario);
        }
        //Login
        public AttributesUser Login(string correo, string clave)
        {
            string claveHash = SecurityHelper.HashPassword(clave);

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
                // Verifica usando el hash guardado (con sal)
                bool esValido = SecurityHelper.VerifyPassword(clave, usuario.ClaveHash);
                if (esValido)
                    return usuario;
            }
            return null;
        }
        

    }
}
