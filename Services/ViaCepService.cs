using ConsultorioApi.Models;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace ConsultorioApi.Services
{
    public class ViaCepService
    {
       private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        public async Task<ViaCepResponse?> ObterEnderecoPorAsync(string cep)
        {
            var endereco = await _httpClient.GetFromJsonAsync<ViaCepResponse>($"{cep}/json/");
            return endereco;
        }
    }
}
