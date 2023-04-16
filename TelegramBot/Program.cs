using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core;
using TelegramBot.Core.Interfaces;
using ToDoListBot.DAL;
using ToDoListManagement;
using ToDoListManagement.Storage;

namespace TelegramBot;

internal class Program
{
    private static IHost host;

    private static readonly ITelegramBotClient bot =
        new TelegramBotClient("5978132221:AAHCDW7mVeMHZ9Z-Z7p-n3B5ou49ruGbLeM");

    private static readonly InlineKeyboardMarkup inlineKeyboard = new(new[]
    {
        // first row
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Посмотреть задачи", "GetAllTasks"),
            InlineKeyboardButton.WithCallbackData("Добавить задачу", "AddTask"),
            InlineKeyboardButton.WithCallbackData("Удалить задачу", "DeleteTask"),
            InlineKeyboardButton.WithCallbackData("Завершить задачу", "FinishTask")
        }
    });

    private static TelegramMessageType msgMessageType = TelegramMessageType.Main;

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        # region first message

        if (update.Message?.Text.ToLower() == "/start")
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, "Добро пожаловать на борт, добрый путник!",
                replyMarkup: inlineKeyboard);
            return;
        }

        #endregion

        #region MainCycl

        if (msgMessageType == TelegramMessageType.Main)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));

            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackQuery = update.CallbackQuery;

                if (Enum.Parse<TelegramMessageType>(callbackQuery.Data) == TelegramMessageType.AddTask)
                {
                    msgMessageType = TelegramMessageType.AddTask;
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, "Ведите задачу",
                        replyMarkup: null);
                    return;
                }

                await host.Services.GetRequiredService<IResolverCommand>()
                    .Get(Enum.Parse<TelegramMessageType>(callbackQuery.Data))
                    .CommandEx(callbackQuery.From.Username, botClient,
                        callbackQuery.Message.Chat, callbackQuery.Message.Text,
                        inlineKeyboard);

                return;
            }
        }

        #endregion

        #region AddTaskCycl

        if (msgMessageType == TelegramMessageType.AddTask)
        {
            if (update.Type == UpdateType.Message)
            {
                var username = update.Message.From.Username;
                var message = update.Message;
                await host.Services.GetRequiredService<IResolverCommand>()
                    .Get(TelegramMessageType.AddTask)
                    .CommandEx(message.From.Username, botClient,
                        message.Chat, message.Text,
                        inlineKeyboard);
                await botClient.SendTextMessageAsync(message.Chat, "Задача добавлена", replyMarkup: inlineKeyboard);
            }

            msgMessageType = TelegramMessageType.Main;
            return;
        }

        #endregion


        await botClient.SendTextMessageAsync(update.Message.Chat, "Меню", replyMarkup: inlineKeyboard);
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }

    private static void Main(string[] args)
    {
        host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<ApplicationContext>();
                services.AddScoped<IStorageTasks, StorageTasks>();
                services.AddSingleton<ToDoListManager>();
                services.AddScoped<ICommandBot, AddTaskCommand>();
                services.AddScoped<ICommandBot, GetAllTasksCommand>();
                services.AddScoped<IResolverCommand, ResolverCommand>();
            })
            .Build();

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions();
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}