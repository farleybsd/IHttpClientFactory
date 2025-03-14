using System.Net.Http;
using System.Net.Http.Json;
using ViaCep.com.br.Dominio;
using ViaCep.com.br.ViaCep.Models;

namespace ViaCep.com.br.ViaCep.Service;

public class ViaCepGateway(HttpClient httpClient) : IviaCepGateway
{

    public async Task<CepResponse> BuscarCepAsync(CepRequest cep)
    {
        if (httpClient.BaseAddress == null)
        {
            throw new InvalidOperationException("O HttpClient foi injetado sem um BaseAddress configurado.");
        }
        var httpResponse = await httpClient.GetAsync($"{cep.Cep}/json/");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Erro ao buscar o CEP {cep}. Código: {httpResponse.StatusCode}");
        }

        var response = await httpResponse.Content.ReadFromJsonAsync<CepResponse>();

        return response ?? throw new InvalidOperationException($"Resposta nula ao buscar o CEP {cep}");
    }
}
