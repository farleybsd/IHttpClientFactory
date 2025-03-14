using Microsoft.AspNetCore.Mvc;
using ViaCep.com.br.Dominio;
using ViaCep.com.br.ViaCep.Models;
using ViaCep.com.br.ViaCep.Service;

namespace ViaCep.com.br.Controllers
{
    [ApiController]
    [Route("buscar-cep")]
    public class ViaCepController : ControllerBase
    {
        private readonly IviaCepGateway _viaCepGateway;

        public ViaCepController(IviaCepGateway viaCepGateway)
        {
            _viaCepGateway = viaCepGateway;
        }

        [HttpPost]
        public async Task<IActionResult> BuscarCep([FromBody] CepRequest request)
        {
            var response = await _viaCepGateway.BuscarCepAsync(request);
            return response is not null ? Ok(response) : NotFound("CEP não encontrado.");
        }
    }
}
