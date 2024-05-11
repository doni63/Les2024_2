namespace SwitchSelect.Service;

public class CepService
{
    private readonly HttpClient _httpClient;

    public CepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ConsultarCep(string cep)
    {
        // Construa a URL da consulta de CEP
        string url = $"https://brasilapi.com.br/api/cep/v1/{cep}";

        // Faça a solicitação HTTP GET
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        // Verifique se a solicitação foi bem-sucedida
        if (response.IsSuccessStatusCode)
        {
            // Leia o conteúdo da resposta
            string json = await response.Content.ReadAsStringAsync();

            // Retorne os dados do CEP como uma string JSON
            return json;
        }
        else
        {
            // Se a solicitação falhar, retorne null ou uma mensagem de erro adequada
            return null;
        }
    }
}
