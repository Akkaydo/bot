using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var botClient = new TelegramBotClient("6526703995:AAHL-MXQBAe42zEGhixCPfMGEKCacgmMzsw");

using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
    if (messageText == "проверка")
    {
        await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Повреждений нет!",
        cancellationToken: cancellationToken);
    }
    if (messageText == "привет")
    {
        await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Здорова, танкист!",
        cancellationToken: cancellationToken);
    }
    if (messageText == "чем занимаешься?")
    {
        await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Я занимаюсь танками",
        cancellationToken: cancellationToken);
    }
    if (messageText == "картинка")
    {
        message = await botClient.SendPhotoAsync(
    chatId: chatId,
    photo: InputFile.FromUri("https://www.iguides.ru/upload/medialibrary/3be/cltfix7293dh4ctulz5qufi09e4qke5o.png"),
    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
    parseMode: ParseMode.Html,
    cancellationToken: cancellationToken);
    }
    if (messageText == "видео")
    {
        message = await botClient.SendVideoAsync(
     chatId: chatId,
     video: InputFile.FromUri("https://github.com/Akkaydo/bot/raw/main/document_5296777531712617438%20%E2%80%94%20%D0%BA%D0%BE%D0%BF%D0%B8%D1%8F.mp4"),
     thumbnail: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg"),
     supportsStreaming: true,
     cancellationToken: cancellationToken);
    }
    if (messageText == "стикер")
    {
        Message message1 = await botClient.SendStickerAsync(
        chatId: chatId,
        sticker: InputFile.FromUri("https://raw.githubusercontent.com/Akkaydo/bot/main/sticker%20(1).webp"),
        cancellationToken: cancellationToken);
    }
}


Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
};
