using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core.Interfaces;
using TelegramBot.Other;
using ToDoListManagement;

namespace TelegramBot.Core.Commads;

internal class FinishTaskCommand : ICommandBot
{
    private readonly ToDoListManager _toDoListManager;

    public FinishTaskCommand(ToDoListManager toDoListManager)
    {
        _toDoListManager = toDoListManager;
    }

    public TelegramMessageType Type => TelegramMessageType.FinishTask;

    public async Task CommandEx(string nickName, ITelegramBotClient botClient, Chat chat, string message,
        InlineKeyboardMarkup replyKeyboardMarkup)
    {
        var username = nickName;
        if (int.TryParse(message, out var indexTask))
            _toDoListManager.СompleteTask(username, indexTask);
    }
}