using Telegram.Bot;
using Telegram.Bot.Types;

public partial class UpdateHandler
{
    public Task HandleEditMessageUpdateAsync(ITelegramBotClient botClient, Message editedMessage, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Received edited message from {userId}. New text: {text}",
            editedMessage.Chat.Id,
            editedMessage.Text);

        return Task.CompletedTask;
    }
}