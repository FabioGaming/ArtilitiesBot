using Discord;

namespace ArtilitiesBot.Utils
{
    public class Templates
    {
        public class loadingEmbed
        {
            public EmbedBuilder _loadingEmbed = new EmbedBuilder();
            public loadingEmbed()
            {
                _loadingEmbed.Color = Color.Purple;
                _loadingEmbed.Title = "Loading...";
                _loadingEmbed.Description = "Your command is being processed...";
                _loadingEmbed.Footer = new EmbedFooterBuilder
                {
                    Text = "Thank you for your patience!"
                };
                _loadingEmbed.ThumbnailUrl = "https://cdn.discordapp.com/attachments/1013504907812278342/1029367231664095252/output-onlinegiftools.gif";
            }
        }
    }
}
