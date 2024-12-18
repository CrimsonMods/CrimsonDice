using CrimsonDice;
using CrimsonDice.Services;
using CrimsonDice.Structs;
using CrimsonDice.Utilities;
using ProjectM;
using ProjectM.Network;
using Unity.Mathematics;
using Unity.Transforms;
using VampireCommandFramework;

namespace CrimsonDice.Commands;

[CommandGroup("dice")]
internal static class DiceCommands
{
    [Command(name: "roll", shortHand: "r", description: "Roll dice", adminOnly: false)]
    public static void Roll(ChatCommandContext ctx, string dies = "1d20")
    {
        var channel = ctx.Event.Type;
        if(channel.Equals(ChatMessageType.Whisper))
        {
            ctx.Reply("Dice rolling is not allowed in Whispers.");
            return;
        }

        if (channel.Equals(ChatMessageType.Global) && !Settings.AllowInGlobal.Value)
        {
            ctx.Reply("Dice rolling is disabled in Global chat.");
            return;
        }

        if (!DiceService.ValidateHandOfDice(dies, out int die, out int amount))
        {
            ctx.Reply("Invalid dice format. Use the format '{count}d{dieSides}' from the follow options: d4, d6, d8, d10, d12, d20, d100");
            return;
        }

        int result = DiceService.RollDice(die, amount);

        User user = ctx.User;

        string message = $"{user.CharacterName} rolled {amount}d{die} = <color={Settings.ResultColor.Value}>{result}</color>.";

        if (channel != ChatMessageType.Global)
        {
            string prefix = "[{channel.ToString()}] ";
            message = prefix + message;

            switch (channel)
            {
                case ChatMessageType.Local:
                    float3 position = ctx.Event.SenderCharacterEntity.Read<LocalToWorld>().Position;
                    ChatUtilities.SystemSendLocal(user, position, message);
                    break;
                case ChatMessageType.Team:
                    ChatUtilities.SystemSendTeam(user, message);
                    break;
            }

            ServerChatUtils.SendSystemMessageToClient(Core.EntityManager, user, $"You rolled <color={Settings.ResultColor.Value}>{result}</color>.");
        }
        else
        {
            ServerChatUtils.SendSystemMessageToAllClients(Core.EntityManager, message);
        }

        ctx.Event.Cancel();
    }
}