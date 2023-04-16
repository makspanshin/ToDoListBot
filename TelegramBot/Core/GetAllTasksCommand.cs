using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core.Interfaces;
using ToDoListManagement;

namespace TelegramBot.Core;

internal class GetAllTasksCommand : ICommandBot
{
    private readonly ToDoListManager _toDoListManager;

    public GetAllTasksCommand(ToDoListManager toDoListManager)
    {
        _toDoListManager = toDoListManager;
    }

    public TelegramMessageType Type => TelegramMessageType.GetAllTasks;

    public async Task CommandEx(string nickName, ITelegramBotClient botClient, Chat chat, string message,
        InlineKeyboardMarkup replyKeyboardMarkup)
    {
        string tasksString;
        var index = 0;
        var listTask = _toDoListManager.GetAllTasks(nickName);
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
        {
            tasksString = "У вас нет задач";
        }

        await botClient.SendTextMessageAsync(chat, tasksString,
            replyMarkup: replyKeyboardMarkup);
    }
}