using TASK_2.Common;
using TASK_2.DTOs;
using TASK_2.Models;
using TASK_2.Repositories;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TASK_2.models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TASK_2.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly AuthService _authService;

        public RegistrationService(IRegistrationRepository registrationRepository, AuthService authService)
        {
            _registrationRepository = registrationRepository;
            _authService = authService;
        }

        public async Task<OperationResult<RegistrationDto>> RegisterAsync(RegistrationRequestDto registrationRequest)
        {
            // Validate input data
            if (string.IsNullOrEmpty(registrationRequest.Username) ||
                string.IsNullOrEmpty(registrationRequest.Password) ||
                string.IsNullOrEmpty(registrationRequest.Email))
            {
                return new OperationResult<RegistrationDto>(400, "Username, password, and email are required.");
            }

            if (!IsValidUsername(registrationRequest.Username))
            {
                return new OperationResult<RegistrationDto>(400, "Username is invalid.");
            }

            if (!IsValidEmail(registrationRequest.Email))
            {
                return new OperationResult<RegistrationDto>(400, "Invalid email format.");
            }

            if (registrationRequest.Password.Length < 8)
            {
                return new OperationResult<RegistrationDto>(400, "Password must be at least 8 characters long.");
            }

            if (!IsValidPassword(registrationRequest.Password))
            {
                return new OperationResult<RegistrationDto>(400, "Password must include uppercase, lowercase, digit, and special character.");
            }

            if (await _registrationRepository.UsernameExistsAsync(registrationRequest.Username))
            {
                return new OperationResult<RegistrationDto>(409, "Username already exists.");
            }

            if (await _registrationRepository.EmailExistsAsync(registrationRequest.Email))
            {
                return new OperationResult<RegistrationDto>(409, "Email already in use.");
            }

            var registration = new models.User
            {
                Username = registrationRequest.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registrationRequest.Password),
                Email = registrationRequest.Email,
                CreatedAt = DateTime.Now
            };

            await _registrationRepository.AddAsync(registration);

            var registrationDto = new RegistrationDto
            {
                Username = registration.Username,
                Email = registration.Email
            };

            return new OperationResult<RegistrationDto>(200, "Registration successful.", registrationDto);
        }

        public OperationResult<string> Login(LoginModel loginModel)
        {
            var user = _registrationRepository.GetByUsername(loginModel.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
            {
                return new OperationResult<string>(401, "Invalid username or password.");
            }

            var token = _authService.GenerateToken(user.Id);
            return new OperationResult<string>(200, "Login successful.", token);
        }


        private bool IsValidUsername(string username)
        {
            bool isAlphanumeric = Regex.IsMatch(username, @"^[a-zA-Z0-9]+$");

            bool containsArabic = Regex.IsMatch(username, @"[\u0600-\u06FF\u0750-\u077F]");

            return isAlphanumeric && !containsArabic && username.Length >= 4 && username.Length <= 20;
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"[A-Z]") && 
                   Regex.IsMatch(password, @"[a-z]") && 
                   Regex.IsMatch(password, @"[0-9]") && 
                   Regex.IsMatch(password, @"[\W_]");   
        }
    }
}
