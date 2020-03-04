using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotC.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("Ping")]
        [Description("Erhalte eine Kuhle Nachricht")]
        public async  Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong du Muchkind").ConfigureAwait(false);
            Console.WriteLine("Command + wurde verwendet");
        }

        [Command("+")]
        [Description("Addiere eine Zahl mit der anderen")]
        /*[RequireRoles(RoleCheckMode.All, "» Project Lead", "» Projektmanager")]  Wenn man den Command für spezielle Rollen nur haben will*/
        public async Task Add(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString()).ConfigureAwait(false);
            Console.WriteLine("Command + wurde verwendet");

        }

        [Command("-")]
        [Description("Subtrahiere eine Zahl mit der anderen")]
        /*[RequireRoles(RoleCheckMode.All, "» Project Lead", "» Projektmanager")]  Wenn man den Command für spezielle Rollen nur haben will*/
        public async Task Sub(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne - numberTwo).ToString()).ConfigureAwait(false);
            Console.WriteLine("Command - wurde verwendet");

        }
        [Command("response")]
        [Description("Lass den Bot deine Nachricht wiederholen")]
        public async Task Response(CommandContext ctx)
            {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);


        }

        [Command("forum")]
        [Description("Erhalte den Forumlink")]
        public async Task Forum(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("https://millenium-games.de").ConfigureAwait(false);
            Console.WriteLine("Command forum wurde verwendet");


        }


        [Command("poll")]
        [Description("Lass den Bot Reaktionen zählen. BSP: !poll 1s [1s = 1 Sekunden] :D :/")]

        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
        {

            var interactivity = ctx.Client.GetInteractivity();
            var options = emojiOptions.Select(x => x.ToString());

            var pollembed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollembed).ConfigureAwait(false);


            foreach(var option in emojiOptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }

    }
}
