using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Other;

namespace TelegramBot.Core.Interfaces;

public interface ICommandBot
{
    TelegramMessageType Type { get; }

    Task CommandEx(string nickName, ITelegramBotClient botClient, Chat chat, string message,
        InlineKeyboardMarkup replyKeyboardMarkup);
}