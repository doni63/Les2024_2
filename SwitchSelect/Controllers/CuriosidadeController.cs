﻿using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> ObterCompletions(string nome)
    {
        string apiKey = Environment.GetEnvironmentVariable("OpenaiApiKey");


        string prompt = $"Escreva no máximo 100 tokens sobre um segredo do jogo {nome}";

        var cliente = new OpenAIAPI(apiKey);

        var chat = cliente.Chat.CreateConversation();

        chat.AppendSystemMessage(prompt);

        string resposta = await chat.GetResponseFromChatbotAsync();

        return View("Index", resposta);

    }

}


