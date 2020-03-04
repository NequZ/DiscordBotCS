using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotC.Commands
{
    public class TeamCommands : BaseCommandModule
    {
        [Command("join")]
        [Description("Trete der Testergruppe bei!")]
        public async  Task Join(CommandContext ctx)
        {
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Möchtest du beitreten?",
                ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                Color = DiscordColor.Green
            };

           var joinMessage =  await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

          var reactionResult =  await interactivity.WaitForReactionAsync(x => x.Message == joinMessage && x.User == ctx.User && (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji)).ConfigureAwait(false);

               if(reactionResult.Result.Emoji == thumbsUpEmoji)
            {
                var role = ctx.Guild.GetRole(617088703352799241);
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
                Console.WriteLine("Eine Rolle wurde vergeben");
            }
            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }



    }
}
