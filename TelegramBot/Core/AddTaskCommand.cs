using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core.Interfaces;
using ToDoListManagement;

namespace TelegramBot.Core;

public class AddTaskCommand : ICommandBot
{
    private readonly ToDoListManager _toDoListManager;

    public AddTaskCommand(ToDoListManager toDoListManager)
    {
        _toDoListManager = toDoListManager;
    }

    public TelegramMessageType Type => TelegramMessageType.AddTask;

    public async Task CommandEx(string nickName, ITelegramBotClient botClient, Message? message,
        ReplyKeyboardMarkup replyKeyboardMarkup)
    {
        var username = nickName;
        _toDoListManager.Add(username, message.Text);
        await botClient.SendTextMessageAsync(message.Chat, "Задача добавлена", replyMarkup: replyKeyboardMarkup);
    }
}