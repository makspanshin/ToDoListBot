using TelegramBot.Other;

namespace TelegramBot.Core.Interfaces;

public interface IResolverCommand
{
    ICommandBot Get(TelegramMessageType telegramMessageType);
}