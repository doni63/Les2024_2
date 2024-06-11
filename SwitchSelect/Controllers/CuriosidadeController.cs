using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Text;

namespace SwitchSelect.Controllers
{
    public class CuriosidadeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly OpenAIAPI _api;

        public CuriosidadeController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;

            string? apiKey = Environment.GetEnvironmentVariable("OpenaiApiKey");
            if (apiKey == null)
            {
                throw new InvalidOperationException("Chave API não configurada");
            }
            _api = new OpenAIAPI(apiKey);
        }

        public async Task<IActionResult> ObterCompletions(string nome, string pergunta)
        {
            string resposta = string.Empty;
            string prompt = string.Empty;

            if (string.IsNullOrEmpty(pergunta))
            {
                prompt = $"Assuma o papel do personagem principal do jogo {nome}. Primeiro, apresente-se e forneça uma breve descrição do jogo, incluindo o gênero e o desenvolvedor. Em seguida, forneça um resumo do enredo e compartilhe um segredo sobre sua jornada.";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"Responda à pergunta '{pergunta}' com base nas respostas anteriores sobre o jogo {nome}, seguindo estas orientações:");
                sb.Append($"1. Se a pergunta for sobre jogos para Nintendo Switch, responda diretamente e sempre destaque as vantagens únicas de jogar {nome} no Nintendo Switch.");
                sb.Append($"2. Se a pergunta mencionar um jogo diferente de {nome}, responda honestamente, mas demonstre um pouco de ciúmes e mostre por que {nome} é uma experiência imperdível.");
                sb.Append($"3. Se a pergunta for sobre um jogo em outra plataforma, responda honestamente, mas destaque as vantagens e a experiência única de jogar {nome} no Nintendo Switch.");
                sb.Append($"4. Se a pergunta não for relacionada a jogos, redirecione gentilmente a conversa de volta para o universo dos jogos Nintendo Switch.");
                sb.Append($"5. Mantenha sempre um tom envolvente, divertido e cativante, sem responder perguntas maliciosas ou grosseiras.");
                sb.Append($"Vamos continuar essa jornada emocionante! Estou aqui para qualquer coisa que você precisar sobre {nome}.");
                prompt = sb.ToString();
            }

            var chatRequest = new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage(ChatMessageRole.System, prompt)
                },
                Model = Model.ChatGPTTurbo, // Ou qualquer modelo adequado que você esteja usando
                MaxTokens = 500,
            };

            var resultado = await _api.Chat.CreateChatCompletionAsync(chatRequest);
            resposta = resultado.Choices.FirstOrDefault()?.Message.Content ?? "Nenhuma resposta obtida.";

            ViewBag.NomeJogo = nome;
            return View("Index", resposta);
        }
    }
}
