namespace ViaCep.com.br.ViaCep.Models
{
    public record CepResponse(
    string Cep,
    string Logradouro,
    string Complemento,
    string Unidade,
    string Bairro,
    string Localidade,
    string Uf,
    string Estado,
    string Regiao,
    long Ibge,
    long Gia,
    long Ddd,
    long Siafi
    );
}
