namespace ViaCep.com.br.ViaCep.Handlers
{
    public class LoggingHandler(ILogger<LoggingHandler> logger) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                logger.LogInformation("Request: {Body}", requestBody);
            }
            else
            {
                logger.LogInformation("Request has no body.");
            }

            var response = await base.SendAsync(request, cancellationToken);

            logger.LogInformation("StatusCode: {StatusCode}", response.StatusCode);

            if (response.Content != null)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                logger.LogInformation("Content: {Content}", responseContent);
            }
            else
            {
                logger.LogInformation("Response has no body.");
            }

            return response;
        }
    }

}
