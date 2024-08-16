using TASK_2.DTOs;
using System.Threading.Tasks;
using TASK_2.Common;
using TASK_2.Models;

namespace TASK_2.Services
{
    public interface IRegistrationService
    {
        Task<OperationResult<RegistrationDto>> RegisterAsync(RegistrationRequestDto registrationRequest);
        OperationResult<string> Login(LoginModel loginModel);
    }
}
