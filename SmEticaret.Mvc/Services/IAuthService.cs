using SmEticaret.Models.Dto;
using SmEticaret.Shared.ServiceResult;

namespace SmEticaret.Mvc.Services
{
    public interface IAuthService
    {
        Task<IServiceResult<string>> GetToken(LoginDto loginDto);
        Task<IServiceResult> CreateUser(RegisterDto registerDto);
    }

    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public HttpClient Client => _clientFactory.CreateClient("api");

        public async Task<IServiceResult> CreateUser(RegisterDto registerDto)
        {
            var uri = "https://localhost:7251/api/Auth/register";
            var response = await Client.PostAsJsonAsync(uri, registerDto);

            return response.IsSuccessStatusCode
                ? ServiceResult.Success()
                : ServiceResult.Fail("Api request failed.");
        }

        public async Task<IServiceResult<string>> GetToken(LoginDto loginDto)
        {
            var uri = "https://localhost:7251/api/Auth/login";
            var response = await Client.PostAsJsonAsync(uri, loginDto);

            if (!response.IsSuccessStatusCode)
            {
                return ServiceResult.Fail<string>("Api request failed.");
            }

            var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>();

            return ServiceResult.Success(tokenDto.Token);

        }
    }
}
