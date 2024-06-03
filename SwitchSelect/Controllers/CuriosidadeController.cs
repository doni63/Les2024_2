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
            string prompt = $"Assuma o papel do personagem principal do jogo {nome}. Primeiro, apresente-se e forneça uma breve descrição do jogo, incluindo o gênero e o desenvolvedor. Em seguida, forneça um resumo do enredo e compartilhe um segredo sobre sua jornada.";


            var cliente = new OpenAIAPI(apiKey);

            var chat = cliente.Chat.CreateConversation();

            chat.AppendSystemMessage(prompt);

            resposta = await chat.GetResponseFromChatbotAsync();

            ViewBag.NomeJogo = nome;

            return View("Index", resposta);
        }
        else
        {
            string prompt = $"Responda à pergunta '{pergunta}'. Se a pergunta for sobre assuntos não relacionados a jogos, gentilmente redirecione a conversa para falar sobre jogos. Se a pergunta mencionar um jogo diferente de {nome}, responda honestamente, mas demonstre ciúmes e destaque as vantagens únicas de {nome}. Certifique-se de manter um tom envolvente e divertido.";

            var cliente = new OpenAIAPI(apiKey);

            var chat = cliente.Chat.CreateConversation();

            chat.AppendSystemMessage(prompt);

            resposta = await chat.GetResponseFromChatbotAsync();

            ViewBag.NomeJogo = nome;

            return View("Index", resposta);
        }
    }

}


