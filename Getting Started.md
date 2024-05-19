<!-- TOC start (generated with https://github.com/derlin/bitdowntoc) -->

- [Getting Started](#getting-started)
- [Prerequisites:](#prerequisites)
    * [Against The Storm](#against-the-storm)
    * [Visual Studio 2022](#visual-studio-2022)
    * [dnSpy](#dnspy)
    * [Thunderstore Mod Manager](#thunderstore-mod-manager)
    * [Modding Template](#modding-template)
- [First Steps](#first-steps)
    * [Setting Up Thunderstore Mod Manager](#setting-up-thunderstore-mod-manager)
    * [Setting Up the Modding Template](#setting-up-the-modding-template)
    * [Setting up BepInEx console](#setting-up-bepinex-console)
    * [Testing The Mod](#testing-the-mod)
    * [Adding Dependencies](#adding-dependencies)
        + [Unity Libraries](#unity-libraries)
        + [API Library](#api-library)
    * [Understanding BepInEx and Harmony](#understanding-bepinex-and-harmony)
        + [Harmony Prefix](#harmony-prefix)
        + [Harmony Postfix](#harmony-postfix)
        + [Using dnSpy to Export AtS Codebase](#using-dnspy-to-export-ats-codebase)
        + [Adding New Game Mechanics](#adding-new-game-mechanics)
        + [Changing Existing Stuff](#changing-existing-stuff)
        + [Hands-On Challenge: Give Yourself a Perk](#hands-on-challenge-give-yourself-a-perk)
        + [Hands-On Challenge Solution](#hands-on-challenge-solution)
- [Using The ATS API](#using-the-ats-api)
    * [Effects](#effects)
        + [Simple Effect](#simple-effect)
        + [Simple Hooked Effect](#simple-hooked-effect)
        + [Retroactive/Progressive Hooked Effect](#retroactiveprogressive-hooked-effect)
        + [Multi-Layered Hooked Effect](#multi-layered-hooked-effect)
- [Publishing The Mod](#publishing-the-mod)
    * [Package Setup](#package-setup)
        + [Your Mod file](#your-mod-file)
        + [A Manifest file](#a-manifest-file)
        + [An Icon](#an-icon)
        + [A Readme file](#a-readme-file)
        + [A Changelog file](#a-changelog-file)
        + [Zip it](#zip-it)
    * [Uploading the Mod](#uploading-the-mod)
    * [Uploading a New Version](#uploading-a-new-version)
    * [References](#references)
- [More subjects to come soon!](#more-subjects-to-come-soon)

<!-- TOC end -->

# Getting Started

Hello there, viceroy! So you decided to try your hand in modding Against The Storm? The Queen will surely be pleased! Be prepared to embark in an incredible journey, where you will settle the land, the sea and the sky!

In all seriousness, we are glad that you decided to join our modding community and add your little twists to this already incredible game. It can be adding new stuff that don't exist yet, rebalancing existing contents in the game, or maybe completely changing how the game works. Everything is possible, though some tasks may be easier than others. Remember to start simple and go from there.

Before you begin the process, if you aren't in the AtS modding Discord yet, feel free to join us by clicking on [this Discord invite](https://discord.com/invite/AdzCf5uzNv).

This guide assumes you have a Windows PC (preferably Windows 10 or 11).

# Prerequisites:

In order to start modding, there are a few things that you need, which are listed below. 

**NOTE:** You need to know how to code in at least basic level. Without any programming knowledge, it will be tough to follow the original's game code and tweak it to your liking. 

## Against The Storm
You must have a copy of Against The Storm. This guide assumes you have a copy of it from Steam, though it might work similarly if you got it from other sources such as GamePass or GoG. Feel free to discuss this with the community in the Discord.

## Visual Studio 2022
As the code is written in C# and managed via a C# project, Visual Studio 2022 is an excellent IDE (programming tool) for modding Against The Storm. You will write C# as well as be able to look at the game's code by using this tool. 

Get it for free at [the official Visual Studio website](https://visualstudio.microsoft.com/downloads/). Be sure to download the Community version which is always free.

## dnSpy
This is a tool that can look at assemblies and make readable code from them. Assemblies are the result of building an entire codebase, resuling in a single file with the .dll extension. This will make it possible to see Against The Storm's codebase in order to tweak it or add to it. Eventually we will export Against The Storm's code by using its export feature.

Get it at [dnSpy's GitHub](https://github.com/dnSpy/dnSpy/releases). You would usually want to get the *dnSpy-net-win64.zip* file in the latest release. Extract the zip's contents into a folder that you can easily get to later on.

## Thunderstore Mod Manager
Though this application is technically not required in order to mod, we highly recommend using this. This allows you to automatically switch between modded and vanilla versions of the game (if you ever want to just play it without mods), and it can manage additional mods or libraries, making sure they're up to date and are set in the correct place. This guide assumes that you use this manager. If you don't want to, you can discuss with our modding community in Discord for steps regarding this.

Get it at [the official Thunderstore Mod Manager website](https://www.overwolf.com/app/thunderstore-thunderstore_mod_manager).

## Modding Template

This template is excellent for setting up common procedures across all mods, and is used as a great starting point. It is recommended to use it, as you can get right to modding after doing slight tweaks on it to match your mod's properties.

Get it at [ATS Mod Template Github page](https://github.com/ats-mods/ModTemplate). You can download it by clicking on the green "Code" button, then clicking "Download ZIP". Extract it to a folder of your choice. This is where you'll work on your mod.

# First Steps
Once everything's installed, we want to do a few things to prepare our modding codebase. Once we'll have a good base, we can go from there and start adding our stuff.

There are a few things to tweak here, but you only need to do those once, so bear with us.

## Setting Up Thunderstore Mod Manager

Open the Thunderstore Mod Manager. The first time it opens, it will show a list of games and a search bar. Type "Against The Storm" in the search bar so it will narrow the list of games to the one we want, then hover on it and click on the "Select Game" button that appears. 

You will be moved to a profile selection page for this game, which has a profile named "Default". You can use this one or create a profile for modding. Regardless, click on the desired profile, then click on the "Select Profile" button. For simplicity's sake, this guide will assume you are using the "Default" profile.

You are now in the page for managing Against The Storm. Here, click on the "Get mods" tab on the left side of the screen, which will give you a list of all existing mods in Against The Storm. You should find and download the **BepInExPack** mod and the **API** mod. The former enables modding capabilities on Against The Storm's codebase, while the latter adds a powerful library for managing all contents in the game. You will also be notified when said mods are updated and be able to update it easily.

**NOTE:** mods are downloaded to `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\Default`. You will find the BepInEx folder in it.

## Setting Up the Modding Template

Open Visual Studio 2022, then click on "Open a local folder". Find the folder where you downloaded the Modding Template to, and select it. This will load the folder into Visual Studio.

Find the "Solution Explorer" window - which by default starts on the right side of the screen. This lists all files in the folder. 

Inside the Solution Explorer, find the `ModTemplate.csproj` file and double click on it. This opens a file with properties that look like XML that describe the project and its behavior. If you don't know what XML is, it is recommended to take a short read at the [W3School XML Tutorial](https://www.w3schools.com/xml/).

Find the `<PropertyGroup>` field, which houses a few properties in it. We will work inside it to define a few properties about your mod.

Find the `<AssemblyName>` field and replace its value from `ModTemplate` to what you want your mod's name to be called. Make sure to give it a unique name, as it will be used to differentiate between other mods used by players in the future.

Find the `<Description>` field and replace its value from `Example Mod for Against The Storm` to anything that would describe your mod.

Add a new line inside the `<PropertyGroup>` field. In it, add the line `<StormPath>C:\\Program Files (x86)\\Steam\\steamapps\\common\\Against the Storm</StormPath>`. If the path written in the value does not match where your copy of Against The Storm is installed, change it accordingly to match. This will tell your compiler where your Against The Storm's codebase is in.

Add another line inside the `<PropertyGroup>` field. In it, add the line `<BepInExPath>C:\\Users\\<your_windows_user>\\AppData\\Roaming\\Thunderstore Mod Manager\\DataFolder\\AgainstTheStorm\\profiles\\Default</BepInExPath>`. You'll have to replace `<your_windows_user>` with your PC user, which is the name of the user you use to log in to your PC. This is where we installed BepInEx at a previous step.

Lastly, find the field `<Target Name="Deploy" AfterTargets="Build">`. Inside of it, find the two `<Copy>` fields. Replace the string `$(StormPath)` with `$(BepInExPath)` in each of those values. We need to do this because the Mod Template assumed we'll install BepInEx in the same place as Against The Storm, but we installed it via the Thunderstore Mod Manager. Doing this correctly adjusts the path to where BepInEx is installed.

You are now finished. To build this, click on File -> Open -> Project/Solution, then browse and find `ModTemplate.csproj`. Select it and click "OK". This will open the `ModTemplate` project in Visual Studio, where before we only viewed the folder. You can now press Shift-F6, or click on Build -> Build ModTemplate. If everything went well, you should see output at the bottom which would look like this:

```text
1>Done building project "ModTemplate.csproj".
========== Build All: 1 succeeded, 0 failed, 0 skipped ==========
========== Build completed at 19:36 and took 08.716 seconds ==========
```

If there are errors here, retract the steps above and fix accordingly. If the errors persist, feel free to request assistance in our modding Discord.

Otherwise, if there are no errors, then you should be able to go to where BepInEx is installed, which by default, is at `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\Default`. Then go into the BepInEx -> plugins folder. Inside, you should see a .dll file with your chosen mod's name (e.g. `MyFirstMod.dll`). If you do, you have successfully set up the project!

## Setting up BepInEx console

By default, BepInEx hides its console, as most people use it to play with mods. When developing, it is beneficial to show it, as it shows logs from the Against The Storm, unity and the API library that we will be using.

Go to your Thunderstore Mod Manager folder (by default: `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\<your profile name>`), then go to BepInEx -> config folder. Locate the `BepInEx.cfg` file, right click it and open it with any text application such as Notepad. Find the following configuration:

```text
## Enables showing a console for log output.
# Setting type: Boolean
# Default value: false
Enabled = false
```

Change `Enabled = false` to `Enabled = true` to enable the console and save. From now on, whenever BepInEx runs, it will also open a console that you can use to look for issues and track your code.

## Testing The Mod

Now we are ready to check if Against The Storm is ready to perform with our mod. Open Thunderstore Mod Manager app and navigate to your game. If you click on the blue "Modded" button at the top section of the application, the game will run with BepInEx, which will apply your mod. You should see two windows open the same time - one for Against The Storm itself, and one for BepInEx console. 

As the template has already set up some prints, we can immediately check to see if they are properly loaded. If you can see the log `Plugin <your mod's name> is loaded!`, it worked! You have now modded the game.

**NOTE** - Closing the BepInEx console also closes the game, and vice versa. Be careful to not accidentally close the game while it's saving, as it can corrupt your save. Prefer to use a separate Against The Storm profile (in the game itself) to prevent accidentally losing your vanilla game progress. To be extra safe, exit the game like you would normally.


## Adding Dependencies

In order for your mod to be usable and reference code from Against The Storm's codebase, your project uses dependencies. The Mod Template already adds one dependency by default, which can be visible from Visual Studio's `Dependencies` expandable list on the Solution Explorer. We would like to add other libraries that we care about for modding, which are divided into two types in total: Unity libraries and the ATS API library.

### Unity Libraries

As the game is developed in Unity, it makes sense that the codebase uses Unity based libraries. Modding usually means accessing those types when we override or add to the codebase, and therefore eventually Visual Studio will not know what those types are. To prevent it, add the Unity based libraries to your dependencies.

Right click on the `Dependencies` expandable list in Visual Studio, then click Add Project Reference. On the window that opens, click on the "Browse..." button. Navigate to where your Against The Storm copy is installed, then go into the `Against the Storm_Data` folder. Inside, open the `Managed` folder. Hold CTRL and select all the following DLL files that start with the words:

* Sirenix
* UniRx
* Unity
* UnityEngine

When all of those dll files are selected, click on "Add", then "OK". Visual Studio should now display all those DLLs in the Dependency list.

### API Library

The API library, developed by a fellow modder [James](https://github.com/JamesVeug), is an expanding and developing library intended to make our modding lives easier by abstracting actions commonly done in modding. It has excellent support for adding new goods, traders, perks and cornerstones, and will add more as the time goes. [This wiki](https://github.com/JamesVeug/AgainstTheStormAPI/blob/master/ATS_API/WIKI/WIKI.md) can provide more info on existing capabilities if you are interested.

Regardless, no harm in setting up the dependency now in case you will want to use it. Right click on the `Dependencies` expandable list in Visual Studio, then click Add Project Reference. Click on "Browse..." to browse to Thunderstore Mod Manager (by default: `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\Default`), then go to BepInEx -> plugins -> ATS_API_Devs-API. Choose the `API.dll` file and press "Add".


## Understanding BepInEx and Harmony

One of the main things you want to do when modding Against The Storm is changing existing code in some way. This will be done either by preventing functions from running, doing something right before they run, or doing something after they run. For example, if we wanted a certain blueprint to always appear in the first draw, we'll have to find the function that generates random blueprints for the game, and then override it with our function which will return the blueprint we want instead of the random ones.

This is where BepInEx and Harmony come together. BepInEx uses Harmony as a tool to allow it to control the flow of the existing codebase. You can read the [official Harmony documentation](https://harmony.pardeike.net/articles/intro.html) for the full information if you are interested. For this guide, we will focus on two main use cases:

### Harmony Prefix

A prefix is a function that gets run before (**pre**) a certain method. Whenever that method is called, that function gets called instead. It must always return a boolean result, which determines whether the original method will be run after the function ends, or gets completely skipped and ignored. It can be useful to skip functions if you plan to completely override it.

For example, let's say that we want to change the duration of the seasons. I want seasons to be much shorter. After a bit of looking around the existing codebase (which we will explain later in this guide how to do so effectively), we found the following function:

```cs
protected void SetUpSeasons(BiomeModel biome)
{
	this.Conditions.drizzleDuration = biome.seasons.DrizzleTime;
	this.Conditions.clearanceDuration = biome.seasons.ClearanceTime;
	this.Conditions.stormDuration = biome.seasons.StormTime;
}
```

This seems to get a biome, which has a `seasons` key, that hold the `DrizzleTime`, `ClearanceTime` and `StormTime`. Those variables are assigned to something called conditions, probably conditions of a game run. This is a method in the a class named `BaseConditionsCreator`. We could override it, so it would instead take our values.

Each Harmony related function must have two annotations which describe its behavior. First, the `HarmonyPatch` annotation, which describes which method it is patching, or overriding in our case. Second, the `HarmonyPrefix` annotation, which denotes that this is a *prefix*, which would happen *before* `SetUpSeason` gets to run.

```cs
[HarmonyPatch(typeof(BaseConditionsCreator), nameof(BaseConditionsCreator.SetUpSeason))]
[HarmonyPrefix]
private static bool SetUpSeasons_PrePatch(BaseConditionsCreator __instance)
{
	return false;
}
```

In the above example, the first annotation, `HarmonyPatch`, is configured to access the `BaseConditionsCreator` class and patch a method named `SetUpSeason`. The second annotation denotes it is a prefix. Lastly, the function signature itself, which is a static function that returns a boolean. 

It is also important to note that Harmony provides the unique `__instance` parameter, which is the actual instance of the class being overridden. This is useful if we want to assign or fetch values from it.

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

**NOTE:** Be careful of using this method. If the game is updated and the logic changes, your prefix will not have the changes applied, as it overrides whatever the code is in the original function. This can cause the game to crash or error out after an update. Handle with caution.

### Harmony Postfix

A postfix is a function that runs after (**post**) a certain method, but before other functions that called it can get its result. It is mostly useful for two things: doing things in addition to what the original code does, or changing the result of the function before it can be returned.

The `Plugin.cs` file in the Mod Template comes out of the box with a couple of postfixes. In the function named `HookEveryGameStart`, we can see a Postfix annotation using a class named `GameController`, patching the `StartGame` method inside of it. This is an excellent use case of it, as that means we can now listen to every instance of a player starting a game, and do something right when the game starts, such as granting a bonus effect or giving them goods.

Let's take an additional example. Let's say that we want all perks in the game to have a description that starts with "[MODDED]". For the sake of the example, we will be ignoring localization, pretending there's only the English language to worry about.

Scanning Against The Storm's codebase, we can find that all perks are effects using the `EffectModel` class. Every effect has a method named `GetFullDescription`, which returns a string with the effect's description. We can patch that and edit the result after it's determined.

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

Controlling Harmony properly allows you to have a strong control over the game's behavior with your mods.

### Using dnSpy to Export AtS Codebase

Whenever you want to make a change, you first need to understand how things currently work. For that, you'll need to be able to effectively look at the current Against The Storm's codebase and search in it. We'll use dnSpy to both open the compiled C# code and export it as a project that we can load into Visual Studio.

Open the `dnSpy.exe` file that you downloaded in the above section. The application opens. Click on File -> Open, and navigate to your Against The Storm's installation folder. Go into `Against the Storm_Data` -> `Managed` folder, then find and select `Assembly-CSharp.dll` to open it. This will load the codebase from the DLL, and if you'll expand it you'll be able to see the code in decompiled form.

However, from our experience, the search in dnSpy is not great, so we will defer to Visual Studio's search instead. Click File -> Export to Project. Select a folder to export it to (not inside your mod's folder!). Uncheck the "Solution" checkbox. Set the Visual Studio to VS2019. Keep everything else the same and press Export.

When it finishes, you can close dnSpy. You'll find a new folder named `Assembly-CSharp` in the place you selected. Open your mod in Visual Studio, then click on File -> Add -> Existing Project. Navigate to the `Assembly-CSharp` folder and find the file `Assembly-CSharp.csproj`. Open it to have both projects open at the same time. 

You can now expand the `Assembly-CSharp` project in Visual Studio's Solution Explorer and see the entire codebase for the game. This is a bit of a massive codebase, so make sure to ease into it to not be overwhelmed.

Now, if you'll press CTRL+SHIFT+F in Visual Studio, this will open the "Find in Files" window, which will find all instances of your search parameters across the entire codebase. Try searching a few values such as "drizzle" - you should be able to see a ton of search results, which can help slowly figuring out the codebase in relevant areas. This will definitely come in handy.

### Adding New Game Mechanics

Let's imagine we want to make a new simple mechanic where the player will always start a game with a Wildcard blueprint that you can choose. For that we'll have to search for "wildcard" and look for something that gives a wildcard - as we know there's a cornerstone that does it in the game, so there's probably logic for it somewhere. If we'd look at the functions in the search results, there's one that sounds suspiciously like what we want: `public void GrantWildcardPick(int amount)`.

That sounds like a name of a method that grants X amount of wildcard picks. That's pretty good! Let's try to use this. Now we just need to find the appropriate place to put it. Since we want the logic to apply when the game starts, we will have to look for the logic that starts a game. Fortunately, this comes with the template! Open `plugin.cs` and find the function named `HookEveryGameStart`. Seems like it's a Harmony Postfix that runs after the game starts. Exactly what we need. Seems like it also comes with a `isNewGame` check, which we can guess that returns `true` if we started a new settlement, or `false` if we continue from an existing settlement. So let's make a simple if check:

```cs
if (isNewGame)
{
    // Grant wildcard
}
```

Now we need to call the `GrantWildcardPick` function we found before. It seems to belong to the `EffectsService` class. Services are classes initialized by the game which hold information, references and methods to control the game logic in various ways. During an active game (even a game that just started), they are held in a class named `SO`, which we can access from mostly anywhere. This class grants us access to all services that were initialized as part of the game. From it we can access `EffectsService`, which holds the `GrantWildcardPick` method that we want to call.

Let's add it to our code:

```cs
if (isNewGame)
{
    SO.EffectsService.GrantWildcardPick(1)`
}
```

We can also put a log which would make it easier to see when it triggered. We can use our plugin's `Logger` to do so. However, because `HookEveryGameStart` is a static function, we'll have to access the instance from a static parameter.

Our final code should look like this:

```cs
if (isNewGame)
{
    SO.EffectsService.GrantWildcardPick(1);
    Plugin.Instance.Logger.LogInfo("New wildcard pick granted for new game");
}
```

When triggered, the log should appear in the BepInEx console, right after the existing log that checks if this is a new game. 

Now build your project with Shift + F6 and launch it through Thunderstore Mod Manager in Modded mode. If you are currently in a settlement, abandon it; as stated above, we only grant the wildcard when starting a new game. Then start a new settlement. Check the logs to see if your log triggered, and check ingame to see if you can now pick any blueprint once. If you see the wildcard and the logs, you were successful in adding a new game mechanic!

Feel free to mess around with the code, but try to stay simple for now - it is easy to be overwhelmed if we try to do complex ideas right away. Practice first and progress slowly while becoming more familiar with Eremite's codebase.

### Changing Existing Stuff

A common theme in modding is rebalancing the existing content and maybe mixing things up. This is done by finding existing content and tweaking their values before they take effect in a game.

Before we begin - a word of caution. If you change things too much, saved games will fail to load and will corrupt saves that were loaded with previous values. Make sure to start a new settlement when values change. When your mod is published, make sure to let your players know to end their current settlement, if any, before they update your mod to a newer version that changes values around.

Let's say we want to make a simple change where Shelters only cost 5 Wood to construct instead of the usual 10 Wood. For that we'll have to find the Shelter's building configuration - or as they're called in the code - models. A model is a bunch of configurations that belong to a single entity, like a building in our case, that you can make infinite copies of, such as Shelters.

First, we need to find out the Shelter's internal ID. This will allow us to be able to reference it. The simplest way to do it is to print each building as a log and find the one we care about. 

Open `plugin.cs` and find the method `HookMainControllerSetup`. It is an Harmony Postfix that triggers after `MainController.OnServicesReady` finishes running. This is a good time to add such changes, as we'll need the game's content to be initialized and stored in the game's various services.

Unlike `SO` that we referenced before, we will now reference the `MB` class. It holds all of the game's configurations and settings, without it being related to a specific run.

Inside of `HookMainControllerSetup`, at the end of the method, add the following:

```cs
foreach(BuildingModel buildingModel in MB.Settings.Buildings)
{
    Instance.Logger.LogInfo("Found building with name: " + buildingModel.name + " and display name " + buildingModel.displayName.Text);
}
```

Note: `name` is the internal name (or ID) used to identify a specific model, whereas `display name` is the name of the building, localized to a language based on your game's language settings.

If we'll build and run the game now, we'll get a log for each building model in the game. For example:

`[Info   :MyFirstMod] Found building with name: Sealed Biome Shrine and display name Beacon Tower`

We'll look at the logs until we find the log that we want:

`[Info   :MyFirstMod] Found building with name: Shelter and display name Shelter`

Now that we know the internal name of the building, we can request that specific model and access it. Then we'll be able to directly manipulate its values. So what exactly are we doing? We want to find the configuration that determines the build cost of the Shelter. Looking at the `BuildingModel` class, we can find the following public field: `public GoodRef[] requiredGoods;`. It seems to be a list of GoodRefs (references to actual instances of goods). We can assume that if we'll log them, we'll find a single GoodRef of "10 wood", as it is the only good required a Shelter. So for Shelters to cost 5 Wood, we just need to tweak this reference. Let's try it out:

```cs
BuildingModel shelterModel = MB.Settings.GetBuilding("Shelter");
GoodRef woodRef = shelterModel.requiredGoods[0]; // we know there's only one, so it's definitely the reference to an instance of "X Wood".
woodRef.amount = 5;
```

We have now made it so the reference to a good is an instance of "5 wood" - which is used here as the cost of the Shelter in order to construct it.

Build the project as usual and launch the game. Go into a settlement and check if Shelters now cost 5 wood. If so, success!

It's important to note that the end code is usually very short and simple - but the real effort comes from searching and understanding how the game processes those entities.

### Hands-On Challenge: Give Yourself a Perk

Even though this is a guide, I think the best way to get things to connect is to actually try and do something for yourself. If you wish, do the following challenge. Otherwise, skip to the solution in the next section. This will be useful later for debugging perks.

To complete the challenge, give the player on game start the perk "Woodcutter's Song". Its internal name (ID) is `Resolve for Glade`.

Hints:
* No Harmony overrides are necessary for this challenge.
* Use the internal ID to fetch the perk configuration. Think where it would reside.
* Remember: perks are a type of effect, and is bundled in with other types of effects in the code.
* Check the model of the effect. It might contain a useful method to complete the challenge!

### Hands-On Challenge Solution

<details>
	<summary>Click here to expand and reveal the solution.</summary>

First, because we want to give us a perk at the start of the game, we want to find that `HookEveryGameStart` function we used before to listen where games begin. This is where we'll put our code.

We want to fetch the effect from the settings list, like we did when we edited the Shelter in the above example. The internal name of the effect is known to us, so we can use it to fetch it:

```cs
EffectModel resolveEffect = SO.Settings.GetEffect("Resolve for Glade");                	
```

Next, we will look at the EffectModel class. If we search for "perk" in this class, we can find a useful function to allow us to apply this effect as a perk, which is what we need. Let's also add a log for it as well.

```cs
EffectModel resolveEffect = SO.Settings.GetEffect("Resolve for Glade");                
resolveEffect.AddAsPerk();
Instance.Logger.LogInfo("Got the Resolve for Glade perk.");
```

After building and running this, you should have the perk Woodcutter's Song as a perk on the bottom left of the screen when you begin a new settlement.

</details>

# Using The ATS API

Until now we didn't use the ATS API library at all. We referenced actual code, overridden or added some parts of the game's code. This is great when we want to tweak the game's logic or change existing content.

The ATS API library is intended to make some actions abstract, by taking care of the handling of a bunch of complicated actions. As it grows, new things will be added to it. At the time of writing this guide, its major part is by helping us creating new content, such as perks, cornerstones, goods, traders, and more! If you have any ideas or requests, feel free to tell us in the modding Discord.

## Effects

Almost anything that modifies the game in any way is an Effect. Forest Mysteries, Glade Events (both consequences and working effects), cornerstones and perks are all effects that the player gets in various ways. Some are instant, while some are continuous effects, applying their logic constantly.

In this section, we'll learn how to create new effects from scratch by using the ATS API. Note that we'll need icons in size of 128x128 pixels. If you don't have any, feel free to take them from the [Asset Folder](https://github.com/Shushishtok/AtS-my-first-mod/tree/master/Assets) of this repository.

New effects must be created before the services are initialized but after references are initialized, so the best place to put them are in the following Harmony method:

```cs
[HarmonyPatch(typeof(MainController), nameof(MainController.InitReferences))]
[HarmonyPostfix]
private static void PostSetupMainController()
{

}
```

Copy this method and put it somewhere in `Plugin.cs`. Write the examples below inside it.

### Simple Effect

Let's begin with a simple effect. We will need two things for this effect to work. 

First, we need an Effect Model. Eremite has a lot of predefined Effect Models that handle logic so we won't have to, which is nice. The list of models can be found [here](https://github.com/JamesVeug/AgainstTheStormAPI/blob/master/ATS_API/WIKI/EFFECTS.md). Most of those Effect Models' behavior is self explanatory from their name. For some, we'll still need to test and see how they work. For now, let's pick an easy one: the `GoodsPerMinEffectModel` model. As its name might suggest, it gives the player X goods every minute. We decide how many and what good when we define the model.

Second, we need a reference to the Good that we want to get every minute. The reference will define what Good will be given to the player and its amount.

We want to make an effect named "Honeytraps". It will give you 5 Insects per minute.

First, let's begin with some easy variables:

```cs
string effectName = "Honeytraps";
string effectIconPath = "Honeytraps.jpg";
int amount = 5;
```

We will use the corresponding image `Honeytraps.jpg` from the Assets Folder in the project. Create it if you don't have it yet - it will automatically be processed during the project build process into the mod.

Next, let's create the Good Reference. We will attach it to the effect once we're doing creating it. We'll have to first fetch the model (configuration) of the Good we want to give the player:

```cs
GoodModel insectGoodModel = MB.Settings.GetGood(GoodsTypes.Insects.ToName());
GoodRef insectGoodRef = new() { good = insectGoodModel, amount = amount };
```

Note that we used an enum named `GoodTypes`, which comes from the ATS API, and is used to make our lives easier so we won't have to guess the internal name of the Good - in this case - Insects.

Next, we will create an "effect builder". This is where the ATS API comes in - this is a custom builder that manages most of the properties of the effects for us. When we create an effect builder, we must tell it what type of effect we're building. So we'll do that as follows:

```cs
EffectBuilder<GoodsPerMinEffectModel> builder = new(PluginInfo.PLUGIN_GUID, effectName, effectIconPath);
```

There are a few things to note here. First, the EffectBuilder is of type `GoodsPerMinEffectModel`, which is the effect model we wanted the builder to use. Its constructor accepts 3 parameters: the mod's name (which is automatically stored in `PluginInfo.PLUGIN_GUID`), the effect's name, and the path to the effect's image (relative to the Assets folder in your project).

Our effect's name will actually be `<Your mod name>_<Your effect name>` when the builder will create it. This is done to prevent name collisions in case either Eremite or other modders add effects with the same name. For example, if your mod's name is MyFirstMod, then this effect will be named `MyFirstMod_Honeytraps` behind the scenes.

Next, we will use the builder to set up some properties for the effect:

```cs
builder.SetRarity(EffectRarity.Legendary); // sets it as a legendary perk
builder.SetPositive(true); // sets it as a positive perk
builder.SetDrawLimit(1); // makes it possible to get this perk via randoms a maximum of 1 time
builder.SetAvailableInAllBiomesAndSeasons(); // adds the perk to all biomes' configuration
builder.SetObtainedAsCornerstone(); // adds the perk to the list of cornerstones that you can obtain every year
```

We'll also set up some simple text for the English version of our mod.
```cs
builder.SetLabel("Modded Perk"); // sets the text of the perk's "category"
builder.SetDisplayName(effectName); // sets the text in English for the perk to be displayed to players.
builder.SetDescription($"Gain {amount} insects every minute."); // sets the description of the perk, shown when picking the perk or when hovering over it.
```

Lastly, we'll attach the Good Reference of 5 insects that we made at the start of the proces to the Effect Model of this builder. In the case of the `GoodsPerMinEffectModel`, it expects a Good Reference to use to provide the good.

```cs
builder.EffectModel.good = insectGoodRef;
```

**Note:** Don't forget to make the game add the perk by default in game start. You will do so by referencing the full name of the effect, as created by the builder:

```cs
EffectModel effectModel = SO.Settings.GetEffect($"{PluginInfo.PLUGIN_GUID}_Honeytraps");
effectModel.AddAsPerk();
```

Build the project and test to see if your effect works as intended. This is a simple effect that doesn't need a lot of configuration.

### Simple Hooked Effect

Now that we learned how to make a simple effect of a certain type, let's learn how to make a very common type of an effect that makes up most of the effects in Against The Storm.

A Hooked Effect is an effect that attaches hooks to itself. Hooks are conditioned triggers that, when their condition applies, apply some kind of effect. The list of hooks can be found [here](https://github.com/JamesVeug/AgainstTheStormAPI/blob/master/ATS_API/WIKI/HOOKS.md), and their name implies their conditions. For example, the `GameTimePassedHook` hook triggers after a certain amount of game time passed (taking into account the game speed, of course).

The effect that the hook grants when triggered is immediately applied and becomes active, though it is hidden to the player by default. A copy (or stack) of that reward is given each time the hook trigger applies - for instance, you can make an effect that grants Global Resolve cumulatively apply every 5 minutes. Those effects can be either positive or negative to the player.

Hooked Effects are very powerful not only in their capabilities to apply further effects, but also in their capabilities to format the descriptions it displays to the player in dynamic ways.

For the example of this hooked effect, we will make an effect named **Modding Tools**. This effect triggers whenever a total of 8 new villagers are added to the village over time for any reason, and grants you a bonus of 1 Global Resolve. We will also use the ATS API library for support on creating this Hooked Effect.

First, let's define the effect and the builder class:

```cs
string effectName = "Modding Tools";
string effectIconPath = "ModdingTools.png";

HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, effectName, effectPath);
```

Note that we used the `HookedEffectBuilder` builder this time; this will give us a builder that we can use for building and configuring Hooked Effects.

Next, let's add its generic details:

```cs
builder.SetPositive(true);
builder.SetRarity(EffectRarity.Rare);
builder.SetObtainedAsCornerstone();
builder.SetAvailableInAllBiomesAndSeasons();
builder.SetDrawLimit(1);
builder.SetDisplayName(effectName);
builder.SetLabel("Modded Perk");
```

Nothing new here. But here is where things start to get interesting:

```cs
builder.AddHook(HookFactory.AfterXNewVillagers(8));
```

First, we use the `HookFactory`, another class introduced by the ATS API. This stores a bunch of easy to create hooks by introducing methods that handle them for you. One such method is the `AfterXNewVillagers`, which returns a hook that triggers after a number of villagers arrive - in this case, 8 villagers.

Second, we use the `AddHook` method of the builder to attach the hook produced by the hook factory to the perk.

Note that this alone doesn't do much. While it's hooked to a condition, it doesn't trigger any effect when it is fulfilled. So let's add an effect: 1 permanent Global Resolve each time the hook is fulfilled:

```cs
builder.AddHookedEffect(EffectFactory.AddHookedEffect_IncreaseResolve(builder, 1, ResolveEffectType.Global));
```

Similar to adding hooks, we use the `EffectFactory` introduced by the ATS API to add effects. Like the `HookFactory` class, it stores a bunch of methods to easily set up and produce effects. Note that this one takes the `builder` as argument - which it uses to give the effect a unique name to make sure there are no name collisions between different effects.

Also, note that it takes two more arguments: the amount of resolve to grant, and the type of resolve to grant (global, racial, need based, etc.). We have set it to grant 1 Global Resolve.

Then we use the `AddHookedEffect` method to attach the effect to the hook. This means that this effect is added to the player each time the hook triggers, which turns it into a *hooked effect*.

**NOTE:** A single Hooked Effect can host multiple hooks and effects, which can be used to apply complex effects and rewards. We'll see an example of that later on.

Lastly, we want to add two kinds of descriptions to the perk: a regular description that describes what the effect does, and a "preview" description that shows at the bottom of an active perk. We will make them dynamic to adhere to the values of the hooks and hooked effects we've attached to this effect.

Let's start with the description:

```cs
builder.SetDescription("Modders have assembled new tools that bring in new talent. " +
                        "Every {0} new Villagers gain +{1} Global Resolve.");
```

As you can, the description is straightforward, but it has two specific instances where there are placeholder values - `{0}` and `{1}`. We want to replace those with dynamic values. So let's do that:

```cs
builder.SetDescriptionArgs(
	(SourceType.Hook, TextArgType.Amount, 0),
	(SourceType.HookedEffect, TextArgType.Amount, 0));
```

Description args, or arguments, instructs the game what to fill in dynamically for the text. It takes 3 parameters:
* Source Type - where the value comes from? From what type of structure? This can be a hook, a hooked effect, or other types of sources supported by Hooked Effects.
* Text Argument Type - which field is the value stored in? This usually refers to a common field.
* Source Index - if you have multiple sources of the same type (e.g. 2 hooks), what is the index, starting from 0, the desired source is in? For example, if you added two hooks, then the first hook is assigned to Source Index 0 while the other is assigned to Source Index 1.

In order for the dynamic texts to be assigned to the values we want, we need to set them up in order.

The first value will replace `{0}`, which should be `Every 8 new Villagers..`. The `8` is defined in the hook, as it listens to the amount of villagers added, so the Source Type should be `SourceType.Hook`. The number `8` comes from the amount field of the **hook**, which is provided in the Hook Factory method. This means the Text Argument Type should be `TextArgType.Amount`. And lastly, we only have one hook, which should be at index 0, so that would be Source Index `0`.

Similarly, we do the same for the `{1}`, which should be substituted to `gain +1 Global Resolve.`. The number `1` comes from the amount field of the **effect** that we produced using the Effect Factory, so this time we need to use `SourceType.HookedEffect`. For the source index, we use `0` since this is the first Hooked Effect stored in this effect, so it gets stored in Source Index `0`.

This should set up our regular description. But we also want to include preview descriptions, which are shown each time we hover over a perk that we have. So let's do that:

```cs
builder.SetPreviewDescription("+{0} Global Resolve");
```

In this case, we want the text to show how much Global Resolve we got, in total, from all instances of the hook firing. This will count up all instances (stacks) of the hooked effect and calculate how much Global Resolve they give the player in total. Note that this value is an `Integer` - it cannot be `3.5` Global Resolve for instance. This is important in order to make the dynamic value show up properly.

Let's add the arguments to the preview to set up the dynamic values:

```cs
builder.SetPreviewDescriptionArgs(
	(HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
);
```

Similarly to the Description Argument, we need to provide certain arguments:
* HookedStateTextArg - This is a collection of possible ways to instruct the system to return a calculated value.
* Source Index - Similar to description args. Controls which hooked effect to look at, starting from the first effect hook at index 0.

In this case, we want it to calculate the total Global Resolve bonus from all instances of the first Hooked Effect. We can use the `TotalGainIntFromHooked` method and instruct it to look at Source Index 0.

**Note:** Each method has a `float` variance (such as `TotalGainFloatFromHooked`). If you choose the wrong numeric type (int/float), it will always default the value to 0.

This simple hooked effect is now done! Build and test it out as we did with the Simple Effect before.

### Retroactive/Progressive Hooked Effect

Now that we know how to use a simple hooked effect, let's introduce another one with a few small twists. We will now make a perk named Humble Bundles, in which you will get 3 Fabrics, Bricks and Planks each time you sell goods worth a total of 20 Amber.

There are a few challenges to tackle in this example. First, we will use a new type of hook and hooked effect. The hook is somewhat straightforward, but the effect is a little more complex - since we want it to use specific goods. Additionally, we want this perk to be retroactive, and reward the player for what they have sold so far until getting this perk. And lastly, we want to show the player how many more goods they have to sell before getting another instance of the bonus of 3 Fabrics, Bricks and Planks.

Let's get the simple things out of the way with setting up the builder:

```cs
string effectName = "Humble Bundles";
string effectIconPath = "HumbleBundles.jpg";

HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, cornerstoneName, cornerstoneIconPath);
builder.SetAvailableInAllBiomesAndSeasons();
builder.SetObtainedAsCornerstone();
builder.SetDrawLimit(1);
builder.SetPositive(true);
builder.SetRarity(EffectRarity.Rare);
builder.SetDisplayName(effectName);
builder.SetLabel("Modded Perk");
```

Nothing new here. Now let's add the `TraderValueSoldHook` hook, which triggers when items are sold at a certain value over time. However, since the ATS API is still new, at the time of writing this guide it doesn't support this hook in the Hook Factory - this is expected to be introduced in future versions of it. We will simply construct it manually for now:

```cs
TraderValueSoldHook traderValueSoldHook = Activator.CreateInstance<TraderValueSoldHook>();
traderValueSoldHook.amount = 20;
traderValueSoldHook.startWithCurrentValue = true;
builder.AddHook(traderValueSoldHook);
```

As you can see, we created an instance of the hook. Then we set its `amount` value to 20, which indicates how much the player has to sell for it trigger. And lastly, we also set the `startWithCurrentValue` property to true. This makes it so the effect becomes *retroactive*, as it starts with the total accumulated worth of value sold since the start of the game. If this was set to false, then it would simply start with a value of `0`, which meant the player has to start selling items from the moment this perk was obtained. In our case, we want this perk to be retroactive so you'll get the bonus for past deals with the traders.

Then the builder adds this hook to itself.

Next, we have to make the hooked effect - the bonus that triggers when the hook accumulates enough worth - to grant us the desired goods. Let's first set up those goods first:

```cs
int amountToGet = 3;
Settings settings = MB.Settings;
GoodModel fabricGoodModel = settings.GetGood(GoodsTypes.Fabric.ToName());
GoodModel brickGoodModel = settings.GetGood(GoodsTypes.Bricks.ToName());
GoodModel plankGoodModel = settings.GetGood(GoodsTypes.Planks.ToName());
GoodModel[] goodsToReceive = [fabricGoodModel, brickGoodModel, plankGoodModel];
```

We've set the amount to 3 and fetched from `MB.Settings` the model, or configuration, of each good we care about. This is similar to what we did in the `Simple Effect` section above.

Next, we want each of those models to be generated a GoodRef, and assign it to a hooked effect:

```cs
foreach (GoodModel goodModel in goodsToReceive)
{
    GoodRef goodRef = new()
    {
        good = goodModel,
        amount = amountToGet
    };

    GoodsEffectModel goodEffectsModel = EffectFactory.NewHookedEffect<GoodsEffectModel>(builder);
    goodEffectsModel.good = goodRef;
    builder.AddHookedEffect(goodEffectsModel);
}
```

For every good type that we care about, we got a model. And for each model, we created a GoodRef that will represent 3 of that good (e.g. 3 fabrics). We will then use the Effect Factory to create a new effect of type `GoodsEffectModel`, which grants goods based on the Good Reference attached to it. Then, each effect is attached to the builder.

In total, we now have 1 Hook and 3 Hooked Effects in this effect, each giving a single bundle of 3 items, respectively. In other words, each time the hook triggers, the 3 hooked effects apply, granting the bonus goods.

Let's also add the description:

```cs
builder.SetDescription("Traders like to throw in small extras in their deals with you. " + 
						"When selling goods worth {0} Amber to traders and trade routes, gain {1} Fabrics, Bricks and Planks" +    
    					"(The bonus is added retroactively).");
builder.SetDescriptionArgs(
    (SourceType.Hook, TextArgType.Amount, 0),
    (SourceType.HookedEffect, TextArgType.Amount, 0)
);
```

The description is similar to the Simple Hooked Effect example. We want to get the amount of goods to sell to replace the placeholder text `{0}`, and the amount of items to gain to replace the placeholder text `{1}`. Since the value gained from each hooked effect is the same for all goods, we can simply take it from the first Hooked Effect.

As mentioned before, this perk is retroactive - we have set the hook as such. So we want to add some text to the description of the text that will describe the retroactive bonus. This retroactive perk description shows up before you gain the perk, such as when looking at the description of the perk from order rewards or from a cornerstone selection.

```cs
builder.SetRetroactiveDescription("Expected Gain: {0}");
builder.SetRetroactiveDescriptionArgs(
    (HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
);
```

In this case, it will append the text `"Expected Gain:"`, and substitute the `{0}` with the calculated amount of bonuses which *will be gained* from the hooked effect upon obtaining this perk. For example, if the player already sold goods worth 105 Amber in total, then it would trigger 5 times (105/20), each giving 3 fabrics, bricks and plank - for a total of 15 of each item. In that case, the text will show `Expected Gain: 15`.

**Note:** The retroactive text is always encased in brackets, e.g. `(Expected Gain: 15)`. This is controlled by the system and can be removed by overriding the relevant function - but is out of the scope of this guide. Feel free to override it if you don't want it - but note that it will change all existing perks as well.

Lastly, we would like to show the player their progress towards getting another instance of the goods, so they know how many more goods they need to sell to get the bonus. This is typically done in the preview text, like this:

```cs
builder.SetPreviewDescription("PROGRESS: {0}/{1}. GAINED: {2}");
builder.SetPreviewDescriptionArgs(
   (HookedStateTextArg.HookedStateTextSource.ProgressFloat, 0),
   (HookedStateTextArg.HookedStateTextSource.HookAmountInt, 0),
   (HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
);

```

This is similar to the Simple Hooked Effect's preview in that it shows the total bonuses gained from this perk. However, we added the `PROGRESS {0}/{1}` to also denote the current progress.

In this case, `{0}` is replaced with the `ProgressFloat` to show how much worth of items, and `{1}` is replaced with the `HookAmountInt`, which is the amount `20` that we've set in the hook. Remember that each item is worth its value in decimals, e.g. 0.14, so that's why we are counting progress in float rather than integers.

This will show something along the lines of `PROGRESS: 8.42/20. GAINED: 0`, which is excellent for seeing the progress of this perk. 

### Multi-Layered Hooked Effect

The last example of this guide is going to be a somewhat complex perk to create, but it also really emphasizes how powerful and robust is the effect system set up by Eremite.

We are going to make an effect named `Bonding Season`. Its definition is somewhat simple: each time the season changes, we gain 5 Global Resolve which lasts for 120 seconds.

So what makes it complex? Think of the previous Hooked Effects that we made before. Those had hooks that, when triggered, gave a permanent bonus that lasts for the entire game. However, this time we want the bonus to be timed and remove itself after 120 in-game seconds.

So let's first discuss what we can do. Like Hooked Effect have the `AddHook` function, they also have the `AddRemovalHook` function. The way those hooks work is the same as `AddHook`, however instead of applying an effect for triggering them, they remove the perk that they are attached to. So we can make a perk that has a `GameTimePassedHook` removal hook that is set to 120 in-game seconds.

However, the application is not quite what we want - because we don't want to lose the perk that triggers this on every season; we only want to lose the global resolve bonus after the time elapses. It should still apply again on the next season change, after all. So we need to apply a little more thought. We'll get back to this later.

Additionally, like there are Hooked Effects, there are also Instant Effects. While Hooked Effect wait for the hook to trigger to be applied, Instant Effects apply immediately when obtaining the perk. For instance, imagine a perk that gives you +1 to meat production immediately, and the bonus also increased by +1 (hooked effect) for every 70 Skewers produced (hook). This is useful when designing perks that we might want to apply without having to wait for a hook - and only once per copy.

Taking into consideration all of those capabilities, let's think what we want to do to create the Bonding Season perk:

* We want to make a permanent effect that has the `SeasonChangeHook` hook. Whenever the hook triggers, we want to give an effect, which is its own instance of a perk.

* That effect should have an Instant Effect of giving out resolve from the moment it is applied. In addition, it should also have a removal hook that counts seconds

* Whenever that removal hook triggers, it would only take away that particular instance of that resolve bonus perk, not the seasons change hook perk.

This is a little tricky, but that generally means that we have a Hooked Effect that grants another Hooked Effect when it triggers; the secondary hooked effect removes itself with its own hook when it triggers.

Let's see how it translates into code. Starting with the regular stuff that we already know:

```cs
string effectName = "Bonding Season";
string effectIconPath = "BondingSeason.jpg";

HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, effectName, effectIconPath);
builder.SetPositive(true);
builder.SetRarity(EffectRarity.Epic);
builder.SetObtainedAsCornerstone();
builder.SetAvailableInAllBiomesAndSeasons();
builder.SetDrawLimit(1);
builder.SetDisplayName(effectName);
builder.SetLabel("Modded Perk");
```

Nothing special here. Let's add a hook for whenever the season changes, which the API supports:

```cs
builder.AddHook(HookFactory.OnNewSeason(SeasonTypes.All, 1)); // every season, every year
```

Let's do some preparation for the hooked effect. First, we want it to show up a separate perk when it triggers - this is typically what Eremite do in this kind of perks. This is already the case for hooked effects, but they are hidden by default. So let's make them visible on the player's HUD as a perk:

```cs
builder.EffectModel.showHookedRewardsAsPerks = true;
```

Now the player would see two effects - the permanent one and the timed one that grants the global resolve bonus.

Let's prepare the removal hook for the timed perk:

```cs
GameTimePassedHook gameTimePassedRemovalHook = Activator.CreateInstance<GameTimePassedHook>();
gameTimePassedRemovalHook.startWithCurrentValue = false; // not retroactive, start from the current time
gameTimePassedRemovalHook.seconds = 120f;
```

Next, we need to prepare the timed perk, the same way we do on the permanent perk:

```cs
HookedEffectBuilder resolveBonusBuilder = new(PluginInfo.PLUGIN_GUID, effectName + "_resolveHookedEffect", effectIconPath);
resolveBonusBuilder.SetPositive(true);
resolveBonusBuilder.SetRarity(EffectRarity.Epic);
resolveBonusBuilder.SetDisplayName(effectNameName);
resolveBonusBuilder.SetLabel("Timed Bonus - Modded Perk");
```

This timed perk has an instant effect, immediately giving its resolve bonus as long as it lives, so let's add that first:

```cs
resolveBonusBuilder.AddInstantEffect(EffectFactory.AddHookedEffect_IncreaseResolve(builder, 5));
```

And it also has a removal hook, which will remove that perk when the time elapses:

```cs
// TODO: remove when API supports it
resolveBonusBuilder.EffectModel.hooks = [gameTimePassedRemovalHook]; x
resolveBonusBuilder.AddRemovalHook(gameTimePassedRemovalHook);
```

Let's add some descriptions to the timed perk to make it clear what it does:

```cs
resolveBonusBuilder.SetDescription("It's time to create bonds! Global Resolve increased by +{0} for {1} seconds.");
resolveBonusBuilder.SetDescriptionArgs(
    (SourceType.InstantEffect, TextArgType.Amount, 0),
    (SourceType.RemovalHook, TextArgType.Amount, 0)
);
```

Note the source types in each argument used to reference the proper values, referencing the instant effect and the removal hook, respectively.

We also need to include a few properties: those are currently done manually, but will be supported by the API in a later date and handled behind the scenes:

```cs
resolveBonusBuilder.EffectModel.hasRemovalHooks = true;
resolveBonusBuilder.EffectModel.hasNestedAmount = true;
resolveBonusBuilder.EffectModel.nestedAmount = new() { source = SourceType.InstantEffect, type = TextArgType.Amount, sourceIndex = 0 };
```

The gist of those changes are:
* We are marking the timed Hooked Effect as having removal hooks, so the game will listen to it.
* We are marking the timed Hooked Effect as having nested amount, so the game will check for it.
* We are setting the nested amount to the amount of resolve given by the Instant Effect that grants Global Resolve bonus.

The nested amount is set in order to show a number at the bottom part of the perk icon which would make it easy for the player to see how much resolve you get from it without having to hover over it.

Another thing we will do is add a timer to the timed perk until it gets removed by adding the following parameters:

```cs
// TODO: remove when API supports it
resolveBonusBuilder.EffectModel.hasRemovalDynamicStatePreview = true;
resolveBonusBuilder.EffectModel.removalDynamicPreviewText = LocalizationManager.ToLocaText(PluginInfo.PLUGIN_GUID, resolveBonusBuilder.Name, "preview", "TIME LEFT: {0}");
resolveBonusBuilder.EffectModel.removalStatePreviewArgs = [new() { asTime = true, sourceIndex = 0, source = HookedStateTextArg.HookedStateTextSource.RemovalProgressLeftFloat }];
```

This allows to add a timer to the preview, replacing the `{0}` with the remaining time. It is set as time by adding the `asTime = true` property to the arguments. The API currently doesn't support it for now, which is why it looks a little messy. 

Finally, we finished the timed perk. Now all we need to do is connect it to the parent perk, as an effect:

```cs
builder.AddHookedEffect(resolveBonusBuilder.EffectModel);
```

Lastly, we want to set up the descriptions of the parent perk:

```cs
builder.SetDescription("The strong smell in the air creates unbreakable bonds in the settlements. When the season changes, gain +{0} Global Resolve for {1} seconds.");
builder.SetDescriptionArgs(
    (SourceType.HookedEffect, TextArgType.NestedArg0, 0),
    (SourceType.HookedEffect, TextArgType.NestedArg1, 0)
);
```

Note the `NestedArgs0` and `NestedArgs1` values here. This will go into the first Hooked Effect (Source index 0), and check their first (NestedArg0) and second (NestedArg1) dynamic descriptions arguments and return their values. This lets you dive deeper into a nested effect - in this case it's the timed effect - and get their values for the description, which you can put into the parent effect to show the expected values.

Since this Hooked Effect is complex, feel free to go to [the full example](https://github.com/Shushishtok/AtS-my-first-mod/blob/master/CustomCornerstones.cs#L54-L107) to see all the pieces.

You should now be able to build and run this perk to see it at work.

This should be a proper introduction into effects and their powers. Feel free to discuss with us on the our modding Discord if you need any assistance.

# Publishing The Mod

Once you are happy with your Mod, it is time to publish it to the world. This example will show you how to publish your Mod to the [Thunderstore ATS Mod Page](https://thunderstore.io/c/against-the-storm/).

## Package Setup

To publish your Mod, you will have to prepare a zip package with the following content:

- Your mod .dll file (e.g. `MyFirstMod.dll`)
- A Manifest file (`manifest.json`)
- An icon of your choice, which will be displayed with the Mod name for others to see (`icon.png`)
- A Readme file (`README.md`)

Optionally you can add the following files:
- A Changelog file (`CHANGELOG.md`)

Note, that the exact filenames are required. If you encounter issue, the Thunderstore page will let you know what's missing.

You can also download the default files from this repository here [/MyFirstModPackage](https://github.com/Shushishtok/AtS-my-first-mod/tree/master/MyFirstModPackage).

### Your Mod file

If you followed this guide for creating your Mod, you can find it in the BepInEx/plugins folder at `C:\Users\<your username>\AppData\Roaming\Thunderstore Mod Manager\DataFolder\AgainstTheStorm\profiles\Default\BepInEx\plugins`.

### A Manifest file

The manifest file (`manifest.json`) contains general information about your mod which will be displayed in the Thunderstore.

A default manifest looks like this:

```json
{
  "name": "MyFirstMod",
  "version_number": "1.0.0",
  "website_url": "https://website.com",
  "description": "My first mod with BepInEx",
  "dependencies": [
    "BepInEx-BepInExPack-5.4.2100"
  ]
}
```
### An Icon

The Icon (`icon.png`) must be 256x256px. Since it will be quite small, make sure to keep it simple, so that people can identify it with your Mod.

### A Readme file

The Readme file (`README.md`) can contain a longer description of your mod. It will only be displayed on the Website though.

If you are new to Markdown (`.md`) files, here is a short guide to get you started: [Markdown Basics](https://www.markdownguide.org/basic-syntax/)

### A Changelog file

If you want to, you can add a Changelog file (`CHANGELOG.md`) to document the changes you made with each version. An example could look like this:

```markdown
# 1.0.1
- Fixed this and that

# 1.0.0
- Added my first Mod. It lives!
```

This will also be displayed on the Website.

### Zip it

Lastly, place all files into one folder and zip it.

## Uploading the Mod

Now you can visit the [Thunderstore Upload Page](https://thunderstore.io/c/against-the-storm/create/) and select your zip to upload. Note you **must login** before being able to access the upload page.
You can use a Github, Discord or Overwolf Account for the login.

The website will ask you to select a Team. If this is your first time publishing on Thunderstore, you will have to create a Team, which will be shown as the Author of the Mod. Simply follow the instructions on the Website. (If you cannot find it, here is the link to [create a Team on Thunderstore](https://thunderstore.io/settings/teams/)).

Now, once created navigate back to the Upload Page and choose the created Team.

You can also upload your zip file now by clicking on `Choose or drag file here`.

Next, you will be asked to select a Community, choose `Against the Storm`.

Make sure to select the tags that apply. For your first Mod it probably is simply `Mods`, but feel free to select more if they fit.

Check `Contains NSFW content` if it applies as well.

Once you are done, hit `Submit` and once the link to the Mod page is shown, you are successful in publishing your first Mod, congrats! Should there be any issues, the Website will let you know what's wrong.

Note, that it might take a few minutes for your Mod to show up in the App, but as soon as it's on the Website, you can sit back and relax.

## Uploading a New Version

Sometimes you want to update your Mod because of a recent fix or a new feature. Since you published your Mod already, you will have to publish an Update for it as well.
To do so, gather your package files as described in the [Package Setup](#package-setup).

First, adjust the `manifest.json` version_number to a higher value. **Important:** Do not change the name, or it will not be recognized as a new version!

Second, if you want to, you can create a Changelog file (`CHANGELOG.md`) or adjust the existing one to let people know what you changed.

Now gather your changed .dll file and zip your files like before.

Lastly, head over to the [Thunderstore Upload Page](https://thunderstore.io/c/against-the-storm/create/), make sure to select the same Team, Community and Categories as before and hit submit.
The link shown will lead you to the same Mod Page as before, but when you look under Versions, you will see the newly created one. Congrats, you now published your first update!

## References

Here are some references to help you troubleshoot, should you encounter issues along the way:

- https://h3vr-modding.github.io/wiki/creating/thunderstore/uploading.html
- https://github.com/nayr31/TSGen.Check

# More subjects to come soon!

This might take some time as we are figuring out how things work though. If you have any requests, let us know.

We plan to eventually release guides for:

* Configuring Existing Traders
* Custom Traders
* Custom Goods
* Configuring Goods
* Custom Buildings
* Custom Glade Events
* ...and more!