using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
namespace SwitchSelect.Controllers;


public class CuriosidadeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public CuriosidadeController(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> ObterCompletions(string nome, string pergunta)
    {
        string resposta = string.Empty;
        //variavel configurada localmente no sistema operacional
        string? apiKey = Environment.GetEnvironmentVariable("OpenaiApiKey");
        if(apiKey == null)
        {
            resposta = "Chave apikey vazia";
            return View("Index", resposta);
        }
        else if(pergunta == null)
        {
            string prompt = $"Se apresente como o personagem principal do jogo {nome} e fale um segredo";

            var cliente = new OpenAIAPI(apiKey);

            var chat = cliente.Chat.CreateConversation();

            chat.AppendSystemMessage(prompt);

            resposta = await chat.GetResponseFromChatbotAsync();

            ViewBag.NomeJogo = nome;

            return View("Index", resposta);
        }
        else
        {
            string prompt = $"Responda a pergunta {pergunta}. Se a pergunta for de assuntos diferente de jogos, direcione a pessoa a falar sobre jogos, se for citado outro jogo diferente de {nome} responda a verdade, mas demonstrando ciúmes e mostrando a vantagem do jogo {nome}";

            var cliente = new OpenAIAPI(apiKey);

            var chat = cliente.Chat.CreateConversation();

            chat.AppendSystemMessage(prompt);

            resposta = await chat.GetResponseFromChatbotAsync();

            ViewBag.NomeJogo = nome;

            return View("Index", resposta);
        }
    }

}


