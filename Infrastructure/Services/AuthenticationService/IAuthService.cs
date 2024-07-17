using Domain.DTOs.AuthDto;
using Domain.Responses;

namespace Infrastructure.Services.AuthenticationService;

public interface IAuthService
{
    Task<Response<string>> Login(LoginDto loginDto);
    Task<Response<string>> Register(RegisterDto registerDto);
    Task<Response<string>> ResetPassword(ResetPasswordDto resetPasswordDto);
    Task<Response<string>> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
    Task<Response<string>> ChangePassword(ChangePasswordDto changePasswordDto, int id);
    Task<Response<string>> DeleteAccount(int id);
}