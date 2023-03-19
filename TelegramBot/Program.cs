using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ToDoListManagement;
using ToDoListManagement.Models;
using ToDoListManagement.Storage;

namespace TelegramBot;

internal class Program
{
    private static readonly ITelegramBotClient bot =
        new TelegramBotClient("5978132221:AAHCDW7mVeMHZ9Z-Z7p-n3B5ou49ruGbLeM");

    private static readonly ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
    {
        new KeyboardButton[] {"Посмотреть задачи", "Добавить задачу", "Удалить задачу", "Завершить задачу"}
    })
    {
        ResizeKeyboard = true
    };

    private static ToDoListManager manager = new(new StorageTasks());
    private static TelegramMessageType msgMessageType = TelegramMessageType.Main;
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (msgMessageType == TelegramMessageType.Main)
        {
            // Некоторые действия
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
                    string tasksString = "";
                    int index = 0;
                    manager.GetAllTasks();
                    foreach (var item in manager.GetAllTasks())
                    {
                        tasksString += (index + "." + item.ShortName + "-" + item.Description + "\n");
                        index++;
                    }
                    await botClient.SendTextMessageAsync(message.Chat, tasksString,
                        replyMarkup: replyKeyboardMarkup);
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
                var message = update.Message;
                manager.Add(new ToDoItem(message.Text, message.Text));
            }

            msgMessageType = TelegramMessageType.Main;
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }


    private static void Main(string[] args)
    {
        Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
        manager.Add(new ToDoItem("rrrrrr","ssssssssssssss"));
        manager.Add(new ToDoItem("2222222", "ssssssssssssss"));
        manager.Add(new ToDoItem("33333", "ddddddddddddddddddd"));
        manager.Add(new ToDoItem("44444", "ssssssssssssss"));
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