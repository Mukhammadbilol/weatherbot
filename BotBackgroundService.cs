using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

public class BotBackgroundService : BackgroundService
{
    private readonly ILogger<BotBackgroundService> logger;
    private readonly ITelegramBotClient telegramBotClient;
    private readonly IUpdateHandler updateHandler;

    public BotBackgroundService(
        ILogger<BotBackgroundService> logger,
        ITelegramBotClient telegramBotClient,
        IUpdateHandler updateHandler)
    {
        this.logger = logger;
        this.telegramBotClient = telegramBotClient;
        this.updateHandler = updateHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var me = await telegramBotClient.GetMeAsync(stoppingToken);
        logger.LogInformation("Bot {username} started at {time}", me.Username, DateTime.UtcNow);

        telegramBotClient.StartReceiving(
            updateHandler: updateHandler,
            receiverOptions: new ReceiverOptions
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage
                },
                ThrowPendingUpdates = true
            },
            cancellationToken: stoppingToken);
    }
}