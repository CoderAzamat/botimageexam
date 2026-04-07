using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramImageBot.Services;

namespace TelegramImageBot.Handlers
{
    public class ImageHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ImageService _imageService;

        public ImageHandler(ITelegramBotClient botClient, ImageService imageService)
        {
            _botClient = botClient;
            _imageService = imageService;
        }

        public async Task HandleImageRequestAsync(Message message, CancellationToken ct)
        {
            List<string> imageUrls = await _imageService.SearchImagesAsync(message.Text, 3);

            if (imageUrls.Count > 0)
            {
                var album = new List<IAlbumInputMedia>();

                foreach (var url in imageUrls)
                {
                    album.Add(new InputMediaPhoto(InputFile.FromUri(url)));
                }

                await _botClient.SendMediaGroup(
                    chatId: message.Chat.Id,
                    media: album,
                    cancellationToken: ct
                );
            }
            else
            {
                await _botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Kechirasiz, bu so'z bo'yicha rasm topilmadi. Boshqa so'z yozib ko'ring.",
                    cancellationToken: ct
                );
            }
        }
    }
}