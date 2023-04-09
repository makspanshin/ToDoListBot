using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Core.Interfaces;

public interface ICommandBot
{
    TelegramMessageType Type { get; }

    Task CommandEx(string nickName, ITelegramBotClient botClient, Message? message,
        ReplyKeyboardMarkup replyKeyboardMarkup);
}