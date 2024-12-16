using CrimsonDice;
using CrimsonDice.Services;
using CrimsonDice.Structs;
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
        if (channel.Equals(ChatMessageType.Global) && !Settings.AllowInGlobal.Value)
        {
            ctx.Reply("Dice rolling is disabled in Global chat.");
            return;
        }

        if (!DiceService.ValidateHandOfDice(dies, out int die, out int amount))
        {
            ctx.Reply("Invalid dice format. Use the format 'AMOUNTdDIE' (e.g. 1d20).");
            return;
        }

        int result = DiceService.RollDice(die, amount);

        User user = ctx.User;
        string prefix = "";
        if (channel != ChatMessageType.Global) prefix = $"[{channel}]";

        string message = $"{prefix} {user.CharacterName} rolled {amount}d{die} = <color={Settings.ResultColor.Value}>{result}</color>.";

        float3 position = ctx.Event.SenderCharacterEntity.Read<LocalToWorld>().Position;

        ctx.Event.Cancel();

        Extensions.LocalSystem(user, position, message);
        ServerChatUtils.SendSystemMessageToClient(Core.EntityManager, user, $"You rolled <color={Settings.ResultColor.Value}>{result}</color>.");
    }
}