using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ToDoListBot.DAL;
using ToDoListManagement;
using ToDoListManagement.Storage;

namespace TelegramBot;

internal class Program
{
    private static IHost host;

    private static readonly ITelegramBotClient bot =
        new TelegramBotClient("5978132221:AAHCDW7mVeMHZ9Z-Z7p-n3B5ou49ruGbLeM");

    private static readonly ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
    {
        new KeyboardButton[] {"Посмотреть задачи", "Добавить задачу", "Удалить задачу", "Завершить задачу"}
    })
    {
        ResizeKeyboard = true
    };

    private static TelegramMessageType msgMessageType = TelegramMessageType.Main;

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (msgMessageType == TelegramMessageType.Main)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));

            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!",
                        replyMarkup: replyKeyboardMarkup);
                    return;
                }

                if (message.Text.ToLower() == "посмотреть задачи")
                {
                    await CommandGetAllTasks(update.Message?.From.Username, botClient, message);
                    return;
                }

                if (message.Text.ToLower() == "добавить задачу")
                {
                    msgMessageType = TelegramMessageType.AddTask;
                    await botClient.SendTextMessageAsync(message.Chat, "Ведите задачу",
                        replyMarkup: replyKeyboardMarkup);
                    return;
                }

                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!", replyMarkup: replyKeyboardMarkup);
            }
        }

        if (msgMessageType == TelegramMessageType.AddTask)
        {
            if (update.Type == UpdateType.Message)
            {
                var username = update.Message.From.Username;
                var message = update.Message;
                host.Services.GetService<ToDoListManager>().Add(username, message.Text);
                await botClient.SendTextMessageAsync(message.Chat, "Задача добавлена", replyMarkup: replyKeyboardMarkup);
            }

            msgMessageType = TelegramMessageType.Main;
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }

    private static async Task CommandGetAllTasks(string nickName, ITelegramBotClient botClient, Message? message)
    {
        string tasksString;
        var index = 0;
        var listTask = host.Services.GetService<ToDoListManager>().GetAllTasks(nickName);
        if (listTask is not null)
        {
            tasksString = "Список задач\n";
            foreach (var item in listTask)
            {
                tasksString += index + "." + item.Description + "\n";
                index++;
            }
        }
        else
            tasksString = "У вас нет задач";

        await botClient.SendTextMessageAsync(message.Chat, tasksString,
            replyMarkup: replyKeyboardMarkup);
    }

    private static void Main(string[] args)
    {
        host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<ApplicationContext>();
                services.AddScoped<IStorageTasks, StorageTasks>();
                services.AddSingleton<ToDoListManager>();
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