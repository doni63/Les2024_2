using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwitchSelect.Dto;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSelect.Controllers
{
    public class FreteController : Controller
    {
        private readonly HttpClient _httpClient;

        public FreteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> CalcularFrete([FromBody] CalcularFreteRequest request)
        {
            var apiUrl = "https://www.melhorenvio.com.br/api/v2/me/shipment/calculate";
            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiOTczYzRhMWQxMGUyNDc1YjQ0ZGQwMjlmNTc4MGExZGUxNTQzMjM2ZDc4MzdhZGE0YWMwMGQ1NTA4Y2M2Y2NkMDExYWQ5MGE2NTI2MDdlNmUiLCJpYXQiOjE3MTU5OTQ0MTYuMTQwOTk4LCJuYmYiOjE3MTU5OTQ0MTYuMTQxLCJleHAiOjE3NDc1MzA0MTYuMTMwOTg0LCJzdWIiOiI5YmVkMjVkZi0yZDg5LTQxZWItODViNS1mZDYwNDc4ZTZhNDAiLCJzY29wZXMiOlsic2hpcHBpbmctY2FsY3VsYXRlIl19.1fcQQXBtW1zXlNgHKCSPnWuKvYpIwd2DmsB1wJeLIioBDAwBuK1hk3zfG-ruehbsOwkEYQIjp9ZkzFjf7CNXwb68-BITYQs_a2X1LS4JyBhrm-MlrlqghfCoBK42PGFKk6cTr9cLH3tpfkTcMdkb7B0_0ybvzqZ8-Pg7yvD40Z3-pb4jnC2EqcsYPADXA9A5jYxzWPAoIYJGHY2YtB-mSr6Q7t-p-Rc8jY0rG5fGn_SS86O4AczMuWlw4Zl3bzeuJKojI24Z8y-dEmZbC_8WuwUVsYEefUZdVNtBVQIZ-LDDf0x9Bf8qrWcFTmj0SQv4GmFLN9QkGq25_cLvRMGG_889dov9mAAYRSnG0sfJ89Qn8NmitNV17wdX62hbsZSDf9OQHZ_7FR155i4xJ6DjLdLMkJr8LqjzwMQIaVJqPweTPenuF1TCKB5VcURvoJ_q9Omoc4Fx-u4TPDczoXXuxMC2tP_8luxZpArSj7ME4doMZN1-delk3klX0Kp3NkZk96x1WcP48_dvAzgp7htZJ2uR9Fq71q5QKqrrJdmep0MlKESyToyJubxCq9-ZX7IykWbvYRvxz2ZI1ba4EskusRybn4BpLpsK5FXzhrnUmkKMNUCSpSXoB1Qyj7W0hR9HWGLK8ucER-udezzeZ6C64Vx6GIept3HhA4cFo1_SN58"; // meu token

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var freteRequest = new FreteRequest
            {
                from = new From { postal_code = "08772000" },
                to = new To { postal_code = request.cep },
                package = new Package
                {
                    height = 10,
                    width = 15,
                    length = 20,
                    weight = 0.5
                }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(freteRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var freteResponse = JsonConvert.DeserializeObject<List<FreteResponse>>(responseData);

                if (freteResponse != null && freteResponse.Count > 0)
                {

                    var valorFreteString = freteResponse[1].price;
                    var valorFrete = (Convert.ToDecimal(valorFreteString))/100;

                    return Json(new { success = true, valorFrete = valorFrete });
                }
            }

            return Json(new { success = false });
        }
    }
}
