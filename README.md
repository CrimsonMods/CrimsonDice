# CrimsonDice
`Server side only` mod for dice rolling

Features:
- Roll Dice in Chat! (Local, Clan, and Global (config to disable Global))

COMING SOON FEATURES:
- Liar's Dice
- Farkle
- Pig 
- And more! (Send me suggestions in the community discord?)

## Want to learn how to mod? 
CrimsonDice was actually created for the purpose of teaching people how to mod V Rising as a tutorial project. 

The tutorial will be coming soon and the Template package can be found on NuGet now. 

https://www.nuget.org/packages/VRising.ModTemplatePlus/

Let me know any feedback or suggestions on the template project; as it is still partially a work in progress. 

## Installation
* Install [BepInEx](https://v-rising.thunderstore.io/package/BepInEx/BepInExPack_V_Rising/)
* Extract CrimsonDice.dll_ into _(VRising server folder)/BepInEx/plugins_
* Start server to generate the config file, modify it, and restart server

## Config

```
## If true the mod will be usable; otherwise it will be disabled.
# Setting type: Boolean
# Default value: true
Toggle = true

## The color displayed for the player's roll sum.
# Setting type: String
# Default value: #34c6eb
ResultColor = #34c6eb

## If set to true, people will be able to roll dice in Global chat.
# Setting type: Boolean
# Default value: false
AllowInGlobal = false
```
This mod is rather straight-forward and so are the configs. 

The only thing that might be less than clear is ResultColor. This is a hexx color code that will be used to display the result of the dice roll in chat.

Use the following url if you're unsure how to find hex color codes: https://g.co/kgs/DxWQHt4


## Support

Want to support my V Rising Mod development? 

Donations Accepted

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/skytech6)

Or buy/play my games! 

[Train Your Minibot](https://store.steampowered.com/app/713740/Train_Your_Minibot/) 

[Boring Movies](https://store.steampowered.com/app/1792500/Boring_Movies/) **FREE TO PLAY**

**If you are looking to hire someone to make a mod for any Unity game reach out to me on Discord! (skytech6)**