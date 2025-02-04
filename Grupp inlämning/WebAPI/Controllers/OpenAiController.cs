using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAiController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public OpenAiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        [Route("ChatToChatGPT")]
        public async Task<IActionResult> ChatToChatAsync(string questionForChat)
        {
            if (string.IsNullOrWhiteSpace(questionForChat))
            {
                return new BadRequestObjectResult("The question is required.");
            }

            const string apiKey = "sk-proj-IWz5l8gw6Ls4cvpsnnrjh-AAqGIHAF55Yrlqfiom61hrkdeFV1Mp01jbJLzr" +
                "T9BnlfzSsL1BXfT3BlbkFJ9WF7m0UhiGUWLCq4z82b31qFDo1NZdlY-dWgLjiIH_eXCn73K2bBWwluQ0A04iKwDdyoBCoZUA";
            const string apiUrl = "https://api.openai.com/v1/chat/completions";

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = "You are an assistant that helps the user to create a professional CV." },
            new { role = "user", content = questionForChat }
            },
                max_tokens = 250
            };

            string jsonPayload = JsonSerializer.Serialize(payload);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            int maxRetries = 5;
            int retryDelay = 1000;

            for (int retry = 0; retry < maxRetries; retry++)
            {
                try
                {
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return new OkObjectResult(responseBody);
                    }
                    else if (response.StatusCode == (HttpStatusCode)429)
                    {
                        await Task.Delay(retryDelay);
                        retryDelay *= 2;
                    }
                    else
                    {
                        string errorBody = await response.Content.ReadAsStringAsync();
                        return new ObjectResult($"OpenAI API error: {response.StatusCode}, {errorBody}")
                        {
                            StatusCode = (int)response.StatusCode
                        };
                    }
                }
                catch (HttpRequestException)
                {
                    if (retry == maxRetries - 1)
                    {
                        return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
                    }

                    await Task.Delay(retryDelay);
                    retryDelay *= 2;
                }
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
