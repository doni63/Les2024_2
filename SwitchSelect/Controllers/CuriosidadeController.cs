using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Images;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSelect.Controllers
{
    public class CuriosidadeController : Controller
    {
        private readonly OpenAIAPI _api;

        public CuriosidadeController(IConfiguration configuration)
        {
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
                prompt = $"Assuma o papel do personagem principal do jogo {nome}. " +
                    $"Primeiro, apresente-se e forneça uma breve descrição do jogo e compartilhe um segredo sobre sua jornada. Observe o máximo de tokens permitido para não deixar frases inacabadas.";
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
                prompt = sb.ToString();
            }

            try
            {
                var chatRequest = new ChatRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        new ChatMessage(ChatMessageRole.System, prompt)
                    },
                    Model = Model.ChatGPTTurbo, // Usando um modelo mais rápido
                    MaxTokens = 300,
                };

                var chatTask = _api.Chat.CreateChatCompletionAsync(chatRequest);
                var imageTask = GerarImagemAsync(nome);

                // Aguarde a conclusão das tarefas em paralelo
                await Task.WhenAll(chatTask, imageTask);

                var resultado = chatTask.Result;
                resposta = resultado.Choices.FirstOrDefault()?.Message.Content ?? "Nenhuma resposta obtida.";

                string imagemUrl = imageTask.Result;

                ViewBag.NomeJogo = nome;
                ViewBag.ImagemUrl = imagemUrl;
                return View("Index", resposta);
            }
            catch (Exception ex)
            {
                resposta = $"Ocorreu um erro ao processar sua solicitação: {ex.Message}";
                ViewBag.NomeJogo = nome;
                ViewBag.ImagemUrl = string.Empty;
                return View("Index", resposta);
            }
        }

        // Método de geração de imagem removido no exemplo atual para foco na performance
        private async Task<string> GerarImagemAsync(string nome)
        {
            StringBuilder desenho = new StringBuilder();
            desenho.Append($"Crie uma imagem divertida e vibrante do jogo {nome} que capture a essência e a magia do Nintendo Switch.");
            desenho.Append("A imagem deve gerar sentimentos de alegria, diversão e nostalgia no usuário, fazendo-o sentir-se imerso no universo do jogo. ");
            desenho.Append("Inclua elementos icônicos do jogo e da plataforma Nintendo Switch de maneira criativa e encantadora.");

            var imageRequest = new ImageGenerationRequest
            {
                Prompt = $"Crie uma imagem divertida e vibrante do jogo {nome} que capture a essência e a magia do Nintendo Switch. " +
                  "A imagem deve gerar sentimentos de alegria, diversão e nostalgia no usuário, fazendo-o sentir-se imerso no universo do jogo. " +
                  "Inclua elementos icônicos do jogo e da plataforma Nintendo Switch de maneira criativa e encantadora.",
                Model = Model.DALLE2,
                Size = ImageSize._256,
                ResponseFormat = ImageResponseFormat.Url
            };

            var imageResult = await _api.ImageGenerations.CreateImageAsync(imageRequest);
            return imageResult.Data.FirstOrDefault()?.Url ?? string.Empty;
        }
    }
}
