using ViaCep.com.br.ViaCep.Models;

namespace ViaCep.com.br.Dominio
{
    public interface IviaCepGateway
    {
        Task<CepResponse> BuscarCepAsync(CepRequest cep);
    }
}
