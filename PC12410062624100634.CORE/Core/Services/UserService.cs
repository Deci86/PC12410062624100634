using System;
using System.Collections.Generic;
using System.Text;
using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.CORE.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;

        public UserService(IUserRepository userRepository, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<UserDTO> SignIn(string email, string password)
        {
            var user = await _userRepository.SignIn(email, password);
            var token = _jwtService.GenerateJWToken(user); // Aquí deberías generar un token JWT o similar para autenticación

            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };

        }

        public async Task<int> Signup(UserCreateDTO userCreateDTO)
        {
            var user = new User
            {
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName,
                DateOfBirth = userCreateDTO.DateOfBirth,
                Email = userCreateDTO.Email,
                Password = userCreateDTO.Password, // En producción, asegúrate de hashear la contraseña
                Country = userCreateDTO.Country,
                Address = userCreateDTO.Address,
                Type = userCreateDTO.Type,
                IsActive = true//Valor por defecto para indicar que el usuario está activo
            };
            return await _userRepository.Signup(user);
        }
    }
}
