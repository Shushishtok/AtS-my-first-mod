# Getting Started

Hello there, viceroy! So you decided to try your hand in modding Against The Storm? The Queen will surely be pleased! Be prepared to embark in an incredible journey, where you will settle the land, the sea and the sky!

In all seriousness, we are glad that you decided to join our modding community and add your little twists to the already incredible game. It can be adding new stuff that don't exist, rebalancing of existing contents in the game, or maybe completely changing how the game works. Everything is possible, though some tasks may be easier than others. Remember to start simple and go from there.

Before you begin the process, if you aren't in the AtS modding Discord yet, feel free to join us by clicking on [this Discord invite](www.google.com).

This guide assumes you have a Windows PC (preferably Windows 10 or 11).

# Prerequisites:

In order to start modding, there are a few things that you need, which are listed below. 

**NOTE:** You need to know how to code in at least basic level. Without any programming knowledge, it will be tough to follow the original's game code and tweak it to your liking. 

## Against The Storm
You must have a copy of Against The Storm. This guide assumes you have a copy of it from Steam, though it might work similarly if you got it from other sources such as GamePass or GoG. Feel free to discuss this with the community in the Discord.

## Visual Studio 2022
As the code is written in C# and managed via a C# project, Visual Studio 2022 is an excellent IDE (programming tool) for modding Against The Storm. You will write C# as well as be able to look at the game's code. 

Get it for free at [the official Visual Studio website](https://visualstudio.microsoft.com/downloads/). Be sure to download the Community version which is always free.

## dnSpy
This is a tool that can look at assemblies and make readable code from them. Assemblies are the result of building an entire codebase, resuling in a single file with the .dll extension. This will make it possible to see Against The Storm's codebase in order to tweak it or add to it. 

Get it at [dnSpy's GitHub](https://github.com/dnSpy/dnSpy/releases). You would usually want to get the *dnSpy-net-win64.zip* file in the latest release. Extract the zip's contents into a folder that you can easily get to.

## Thunderstore Mod Manager
Though this application is technically not required in order to mod, I highly recommend using this. This allows you to automatically switch between modded and vanilla versions of the game (if you ever want to just play it without mods), and it can manage additional mods or libraries, making sure they're up to date and are set in the correct place. This guide assumes that you use this. If you don't want to, you can discuss with our modding community in Discord for steps regarding this.

Get it at [the official Thunderstore Mod Manager website](https://www.overwolf.com/app/thunderstore-thunderstore_mod_manager).

## Modding Template

This template is excellent for setting up common procedures across all mods, and is used as a great starting point. It is recommended to use it, as you can get right to modding after doing slight tweaks on it to match your mod.

Get it at [ATS Mod Template Github page](https://github.com/ats-mods/ModTemplate). You can take it by clicking on the green "Code" button, then clicking "Download ZIP". Extract it to a folder of your choice. This is where you'll work on your mod.

# First Steps
Once everything's installed, we want to make a few things to prepare our modding codebase. Once we'll have a good base, we can go from there and start adding our stuff.

There are a few things to tweak here, but you only need to do those once, so bear with me.

## Setting Up Thunderstore Mod Manager

Open the Thunderstore Mod Manager. The first time it opens, it will show a list of games and a search bar. Type "Against The Storm" in the search bar so it will narrow the list of games to the one we want, then hover on it and click on "Select Game" that appears. 

You will be moved to a profile selection page for this game, which has a profile named "Default". You can use this one or create a profile for modding. Regardless, click on the desired profile, then click on the "Select Profile" button.

You are now in the page for managing Against The Storm. Here, click on "Get mods", which will give you a list of all existing mods in Against The Storm. You should find and download the **BepInExPack** mod and the **API** mod. The former enables modding capabilities on Against The Storm's codebase, while the latter adds a powerful library for managing all contents in the game. You will also be notified when said mods are updated and be able to update it easily.

**NOTE:** mods are downloaded to `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\<your profile name>`. You will find the BepInEx folder in it.

## Setting Up the Modding Template

Open Visual Studio 2022, then click on "Open a local folder". Find the folder where you downloaded the Modding Template to, and select it. This will load the folder into Visual Studio.

Find the "Solution Explorer" window - which by default starts on the right side of the screen. This lists all files in the folder. 

Inside the Solution Explorer, find the `ModTemplate.csproj` file and double click on it. This opens a file with properties that look like XML that describe the project and its behavior. If you don't know XML is, it is recommended to take a short read at the [W3School XML Tutorial](https://www.w3schools.com/xml/).

Find the `<PropertyGroup>` field, which houses a few properties in it. We will work inside it to define a few properties about your mod.

Find the `<AssemblyName>` field and replace its value from `ModTemplate` to what you want your mod's name to be called. Make sure to give it a unique name, as it will be used to differentiate between other mods used by players in the future.

Find the `<Description>` field and replace its value from `Example Mod for Against The Storm` to anything that would describe your mod.

Add a new line inside the `<PropertyGroup>` field. In it, add the line `<StormPath>C:Program Files (x86)\\Steam\\steamapps\\common\\Against the Storm</StormPath>`. If the path written in the value does not match where your copy of Against The Storm is installed, change it accordingly to match. This will tell your compiler where your Against The Storm's codebase is in.

Add another line inside the `<PropertyGroup>` field. In it, add the line `<BepInExPath>C:\\Users\\<your_windows_user>\\AppData\\Roaming\\Thunderstore Mod Manager\\DataFolder\\AgainstTheStorm\\profiles\\<your_profile_name></BepInExPath>`. You'll have to replace `<your_windows_user>` with your PC user (name of the user you use to log in) and `<your_profile_name>` with your Thunderstore Mod Manager profile name. If you didn't create a profile, this would be "Default". This is where we installed BepInEx at the previous step.

Lastly, find the field `<Target Name="Deploy" AfterTargets="Build">`. Inside of it, find the two `<Copy>` fields. Replace `$(StormPath)` with `$(BepInExPath)`. We need to do this because the Mod Template assumed we'll install BepInEx in the same place as Against The Storm, but we installed it via the Thunderstore Mod Manager. Doing this correctly adjusts the path to where BepInEx is installed.

You are now finished. To build this, click on File -> Open -> Project/Solution, then browse and find `ModTemplate.csproj`. Select it and click "OK". This will open the `ModTemplate` project in Visual Studio, where before we only viewed the folder. You can now press Shift-F6, or click on Build -> Build ModTemplate. If everything went well, you should see output at the bottom which would look like this:

```text
1>Done building project "ModTemplate.csproj".
========== Build All: 1 succeeded, 0 failed, 0 skipped ==========
========== Build completed at 19:36 and took 08.716 seconds ==========
```

If there are errors here, retract the steps above and fix accordingly. If the errors persist, feel free to request assistance in our modding Discord.

Otherwise, if there are no errors, then you should be able to go to where BepInEx is installed (by default, at `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\<your profile name>`), then go into the BepInEx -> plugins folder. Inside, you should see a .dll file with your chosen mod's name (e.g. `MyFirstMod.dll`). If you do, you have successfully set up the project!

## Setting up BepInEx console

By default, BepInEx hides its console, as most people use it to play with mods. When developing, it is beneficial to show it, as it shows logs from the Against The Storm, unity and the API library that we will be using.

Go to your Thunderstore Mod Manager folder (by default: `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\<your profile name>`), then go to BepInEx -> config folder. Locate the `BepInEx.cfg` file, right click it and open it with any text application such as Notepad. Find the following configuration:

```text
## Enables showing a console for log output.
# Setting type: Boolean
# Default value: false
Enabled = false
```

Change `Enabled = false` to `Enabled = true` to enable the console. From now on, whenever BepInEx runs, it will also open a console that you can use to look for issues and track your code.

## Testing The Mod

Now we are ready to check if Against The Storm is ready to perform with our mod. Open Thunderstore Mod Manager app and navigate to your game. If you click on the blue "Modded" button, the game will run with BepInEx, which will apply your mod. You should see two windows open the same time - one for Against The Storm itself, and one for BepInEx console. 

As the template has already set up some prints, we can immediately check to see if they are properly loaded. If you can see the log `Plugin <your mod's name> is loaded!`, it worked! You have now modded the game.

**NOTE** - Closing the BepInEx console also closes the game, and vice versa. Be careful to not accidentally close the game while it's saving, as it can corrupt your save. Prefer to use a separate Against The Storm profile (in the game itself) to prevent accidentally losing your vanilla game progress. To be extra safe, exit the game like you would normally.


## Adding Dependencies

In order for your mod to be usable and reference code from Against The Storm's codebase, your project uses dependencies. The Mod Template already adds one dependency by default, which can be visible from Visual Studio's `Dependencies` expandable list. We would like to add other libraries that we care about for modding, which are divided into two types in total: Unity libraries and the ATS API library.

### Unity Libraries

As the game is developed in Unity, it makes sense that the codebase uses Unity based libraries. Modding usually means accessing those types when we override or add to the codebase, and therefore eventually Visual Studio will not know what those types are. To prevent it, add the Unity based libraries to your dependencies.

Right click on the `Dependencies` expandable list in Visual Studio, then click Add Project Reference. On the window that opens, click on the "Browse..." button. Navigate to where your Against The Storm copy is installed, then go into the `Against the Storm_Data` folder. Inside, open the `Managed` folder. Hold CTRL and select all the following DLL files that start with the words:

* Sirenix
* UniRx
* Unity
* UnityEngine

When all of those dll files are selected, click on "Add", then "OK". Visual Studio should now display all those DLLs in the Dependency list.

### API Library

The API library, developed by a fellow modder, is an expanding and developing library intended to make our modding lives easier by abstracting actions commonly done in modding. It has excellent support for adding new goods, traders, perks and cornerstones, and will add more as the time goes. [This wiki](https://github.com/JamesVeug/AgainstTheStormAPI/blob/master/ATS_API/WIKI/WIKI.md) can provide more info on existing capabilities if you are interested.

Regardless, no harm in setting up the dependency now in case you will want to use it. Right click on the `Dependencies` expandable list in Visual Studio, then click Add Project Reference. Click on "Browse..." to browse to Thunderstore Mod Manager (by default: `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\<your profile name>`), then go to BepInEx -> plugins -> ATS_API_Devs-API. Choose the API.dll and press "Add".


## Understanding BepInEx and Harmony

One of the main things you want to do when modding Against The Storm is changing existing code in some way. This will be done either by preventing functions from running, doing something right before they run, or doing something after they run. For example, if we wanted the a certain blueprint to always appear in the first draw, we'll have to find the function that generates random blueprints for the game, and then override it with our function which will return the blueprint we want instead of the random ones.

This is where BepInEx and Harmony come together. BepInEx uses Harmony as a tool to allow it to control the flow of the existing codebase. You can read the [official Harmony documentation](https://harmony.pardeike.net/articles/intro.html) for the full information, but instead, let's look at two main use cases:

### Harmony Prefix

A prefix is a function that gets run before (pre) a certain method. Whenever that method is called, that function gets called instead. It must always return a boolean result, which determines whether the original method will be run after the function ends, or gets completely skipped and ignored. It can be useful to skip functions if you plan to completely override it.

For example, let's say that we want to change the duration of the seasons. I want seasons to be much shorter. After a bit of looking around the existing codebase (which I will explain later in this guide how to do so effectively), we found the following function:

```cs
protected void SetUpSeasons(BiomeModel biome)
{
	this.Conditions.drizzleDuration = biome.seasons.DrizzleTime;
	this.Conditions.clearanceDuration = biome.seasons.ClearanceTime;
	this.Conditions.stormDuration = biome.seasons.StormTime;
}
```

This seems to get a biome, which has a `seasons` key, that hold the `DrizzleTime`, `ClearanceTime` and `StormTime`. Those variables are assigned to something called conditions, probably conditions of a game run. This is a method in the a class named `BaseConditionsCreator`. We could override it, so it would instead take our values.

Each Harmony related function must have two annotations which describe its behavior. First, the `HarmonyPatch` annotation, which describes which method is patching, or overriding in our case. Second, the `HarmonyPrefix` annotation, which denotes that this is a *prefix*, which would happen *before* `SetUpSeason` gets to run.

```cs
[HarmonyPatch(typeof(BaseConditionsCreator), nameof(BaseConditionsCreator.SetUpSeason))]
[HarmonyPrefix]
private static bool SetUpSeasons_PrePatch(BaseConditionsCreator __instance)
{
	return false;
}
```

In the above example, the first annotation, `HarmonyPatch`, it is described to take the `BaseConditionsCreator` class and find a method named `SetUpSeason`. The second annotation denotes it is a prefix. Then the function signature itself is a static function that returns a boolean. 

It is also important to note that Harmony provides the unique `__instance` parameter, which is used as the actual instance of the class being overridden. This is useful if we want to assign or fetch values from it.

Currently, this prefix does nothing except returning false. What that means is that the original method, `SetUpSeason` will not run, but also, it was overridden by nothing. This is likely to cause issues, as the game expects to know how long the seasons are going to last for. Let's fix that by adding our own values:

```cs
[HarmonyPatch(typeof(BaseConditionsCreator), nameof(BaseConditionsCreator.SetUpSeason))]
[HarmonyPrefix]
private static bool SetUpSeasons_PrePatch(BaseConditionsCreator __instance)
{
	__instance.Conditions.drizzleDuration = 30f;
	__instance.Conditions.clearanceDuration = 30f;
	__instance.Conditions.stormDuration = 20f;

	return false;
}
```

We're now basically doing what the original method did, but with a few differences. First, we completely ignore the biome; we instead use our own hardcoded values. And second, we use `__instance` instead of `this` to reference the actual instance of the class running this method.

If we would start a new settlement in Against The Storm, our seasons should be a lot shorter now than usual.

**NOTE:** Be careful of using this method. If the game is updated and the logic changes, your prefix will not have the changes applied, as it overrides whatever the code in the original function has. This can cause the game to crash or error out after an update. Handle with caution.

### Harmony Postfix

A postfix is a function that runs after (post) a certain method, but before other functions that called it can get its result. It is mostly useful for two things: doing things in addition to what the original code does, or changing the result of the function before it can be returned.

The `Plugin.cs` file in the Mod Template comes out of the box with a couple of postfixes. In the function named `HookEveryGameStart`, we can see a Postfix annotation using a class named `GameController`, patching the `StartGame` method inside of it. This is an excellent use case of it, as that means we can now listen to every instance of a player starting a game, and do something right when the game starts, such as granting a bonus effect or giving them goods.

Let's take an additional example though. Let's say that we want all perks in the game to have a description that starts with "[MODDED]". For the sake of the example, I'm ignoring localization, pretending there's only English to worry about.

Scanning the code, I can find that all perks are effects using the `EffectModel` class. Every effect has a method named `GetFullDescription`, which returns a string with the effect's description. We can patch that and edit the result after it's determined.

Like with the Prefix, we use similar annotations, like this:

```cs
[HarmonyPatch(typeof(EffectModel), nameof(EffectModel.GetFullDescription))]
[HarmonyPostfix]
private static void EffectModel_GetFullDescription_Postfix(ref string __result)
{

}
```

A few differences between prefixes and postfixes. First, postfixes use the `[HarmonyPostFix]` annotation. Secondly, it does not return a boolean value; instead, it is set to void. And finally, we also use a `ref string __result` parameter, which is provided by Harmony. We can use this parameter to override the result. So let's do that:

```cs
[HarmonyPatch(typeof(EffectModel), nameof(EffectModel.GetFullDescription))]
[HarmonyPostfix]
private static void EffectModel_GetFullDescription_Postfix(ref string __result)
{
	__result = "[MODDED] " + __result;
}
```

As `__result` holds the full description, we can assign it a new string made from the word "[MODDED]" and then appending the actual description. This will trigger every time the game would try to get the description of an effect.


# TO BE CONTINUED WHEN I GET THE TIME!

Subjects to cover:

* Coding your own changes (changes to game logic, utility functions)
* Doing changes to existing stuff (goods, effects, seasons)
* Using dnSpy to search for code - and exporting the codebase to a proper project file (and loading it in VS)
* Creating new effects with API library
	* Simple non-hooked effect (Honeytraps)
    * Simple Hooked effect (Modding Tools)
	* Multi-Layered effect (Bonding Time)
	* Retroactive/Progressive effect (Humble Bundles)
	* Using references to existing models in effects (Steel Boots, Humble Bundles)
	