using TelegramBot.Core.Interfaces;

namespace TelegramBot.Core;

public class ResolverCommand : IResolverCommand
{
    private readonly Dictionary<TelegramMessageType, ICommandBot> _commands;

    public ResolverCommand(IEnumerable<ICommandBot> commands)
    {
        _commands = commands.ToDictionary(x => x.Type);
    }

    public ICommandBot Get(TelegramMessageType telegramMessageType)
    {
        return _commands[telegramMessageType];
    }
}