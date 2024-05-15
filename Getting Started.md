# Getting Started

Hello there, viceroy! So you decided to try your hand in modding Against The Storm? The Queen will surely be pleased! Be prepared to embark in an incredible journey, where you will settle the land, the sea and the sky!

In all seriousness, we are glad that you decided to join our modding community and add your little twists to the already incredible game. It can be adding new stuff that don't exist, rebalancing of existing contents in the game, or maybe completely changing how the game works. Everything is possible, though some tasks may be easier than others. Remember to start simple and go from there.

Before you begin the process, if you aren't in the AtS modding Discord yet, feel free to join us by clicking on [this Discord invite](https://discord.com/invite/AdzCf5uzNv).

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

### Using dnSpy to Export AtS Codebase

Whenever you want to make a change, you first need to understand how things currently work. For that, you'll need to be able to effectively look at the current Against The Storm's codebase and search in it. We'll use dnSpy to both open the compiled C# code and export it as a project that we can load into Visual Studio.

Open dnSpy.exe that you downloaded in the above section. The application opens. Click on File -> Open, and navigate to your Against The Storm's installation folder. Go into `Against the Storm_Data` -> `Managed` folder, then find and select `Assembly-CSharp.dll` to open it. This will load the codebase into the DLL, and if you'll expand it you'll be able to see the code in decompiled form.

However, from my experience, the search in dnSpy is not great, so we will defer to Visual Studio's search instead. Click File -> Export to Project. Select a folder to export it to (not inside your mod's folder!). Uncheck the "Solution" checkbox. Set the Visual Studio to VS2019. Keep everything else the same and press Export.

When it finishes, you can close dnSpy. You'll find a new folder named `Assembly-CSharp` in the place you selected. Open your mod in Visual Studio, then click on File -> Add -> Existing Project. Navigate to the `Assembly-CSharp` folder and find the file `Assembly-CSharp.csproj`. Open it to have both projects open at the same time. 

You can now expand the `Assembly-CSharp` project in Visual Studio's Solution Explorer and see the entire codebase for the game. This is a bit of a massive codebase, so make to ease into it to not be overwhelmed.

Now, if you'll press CTRL+SHIFT+F, this will open the "Find in Files" window, which will find all instances of your search parameters across the entire codebase. Try searching a few values such as "drizzle" - you should be able to see a ton of search results, which can help slowly figuring out the codebase in relevant areas. This will definitely come in handy.

### Adding New Game Mechanics

Let's imagine we want to make a new simple mechanic where you will always start a game with a Wildcard blueprint that you can choose. For that we'll have to search for "wildcard" and look for something that gives a wildcard - as we know there's a cornerstone that does it in the game, so there's probably logic for it somewhere. If we'd look at the functions in the search results, there's one that sounds suspiciously like what we want: `public void GrantWildcardPick(int amount)`.

That sounds like a name of a method that grants X amount of wildcard picks. That's pretty good! Let's try to use this. Now we just need to find the appropriate place to put it. Since we want the logic to apply when the game starts, we will have to look for the logic that starts a game. Fortunately, this comes with the template! Open `plugin.cs` and find the function named `HookEveryGameStart`. Seems like it's a Harmony Postfix that runs after the game starts. Exactly what we need. Seems like it also comes with a `isNewGame` check, which we can guess that returns `true` if we started a new settlement, or `false` if we continue from an existing settlement. So let's make a simple if check:

```cs
if (isNewGame)
{
    // Grant wildcard
}
```

Now we need to call the `GrantWildcardPick` function we found before. It seems to belong to the `EffectsService` class. Services are classes initialized by the game which hold information, references and methods to control the game logic in various ways. During an active game (even a game that just started), they are held in a class named `SO`, which we can access from mostly anywhere. This class grants us access all services that were initialized as part of the game. From it we can access `EffectsService`, which holds the `GrantWildcardPick` method that we want to call.

Let's add it to our code:

```cs
if (isNewGame)
{
    SO.EffectsService.GrantWildcardPick(1)`
}
```

We can also put a log which would make it easier to see when it triggered. We can use our plugin's `Logger` to do so. However, because `HookEveryGameStart` is a static function, we'll have to access the instance from a static parameter:

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

Before we begin - a word of caution. If you change things too much, saved games will fail to load and will corrupt a save that was loaded with previous values. Make sure to start a new settlement when values change. When your mod is published, make sure to let your players know to end their current settlement, if any, before they update your mod to a newer version that changes values around.

Let's say we want to make a simple change where Shelters only cost 5 Wood to construct instead of the usual 10 Wood. For that we'll have to find the Shelter's building configuration - or as they're called in the code - models. A model is a bunch of configurations that belong to a single entity, like a building in our case, that you can make infinite copies of, such as Shelters.

First, we need to find out the Shelter's internal ID. This will allow us to be able to reference it. The simplest way to do it is to print each building as a log and find the one we care about. 

Open `plugin.cs` and find the method `HookMainControllerSetup`. It is an Harmony Postfix that triggers after `MainController.OnServicesReady` finishes running. This is a good time to add such changes, as we'll need the game's content to be initialized.

Unlike `SO` that we referenced before, we will now reference the `MB` class. It holds all of the game's configurations and settings, without it being related to a specific run.

Inside of `HookMainControllerSetup`, at the end of the method, add the following:

```cs
foreach(BuildingModel buildingModel in MB.Settings.Buildings)
{
    Instance.Logger.LogInfo("Found building with name: " + buildingModel.name + " and display name " + buildingModel.displayName.Text);
}
```

Note: `name` is the internal name (or ID) used to identify a specific model, which `display name` is the localized (to your game's language) name.

If we'll build and run the game now, we'll get a log for each building model in the game. For example:

`[Info   :MyFirstMod] Found building with name: Sealed Biome Shrine and display name Beacon Tower`

We can make the code only print things that have "Shelter" in their name, but for now, let's skip ahead and have the log that we want:

`[Info   :MyFirstMod] Found building with name: Shelter and display name Shelter`

Easy enough. Now that we know the internal name of the building, we can request that specific model and access it. Then we'll be able to directly manipulate its values. So what exactly are we doing? We want to find the configuration that determines the build cost of the Shelter. Looking at the BuildingModel class, we can find the following public field: `public GoodRef[] requiredGoods;`. It seems to be a list of GoodRefs (references to actual instances of goods). We can assume that if we'll log them, we'll find a single GoodRef of "10 wood", as it is the only good required to construct the building. So for the building to cost 5 Wood, we just need to tweak this reference. Let's try it out:

```cs
BuildingModel shelterModel = MB.Settings.GetBuilding("Shelter");
GoodRef woodRef = shelterModel.requiredGoods[0]; // we know there's only one, so it's definitely the reference to wood
woodRef.amount = 5;
```

Basically, we made it so the reference to a good is an instance of "5 wood" - which is used here as the price of the building.

Build the project as usual and launch the game. Go into a settlement and check if Shelters now cost 5 wood. If so, success!

It's important to note that the end code is usually very short and simple - but the real effort comes from searching and understanding how the game processes those entities.

### Hand-On Challenge: Give Yourself a Perk

Even though this is a guide, I think the best way to get things to connect is to actually try and do something for yourself. If you wish, do the following challenge. Otherwise, skip to the solution in the next section. This will be useful later for debugging effects.

To complete the challenge, give the player on game start the perk "Woodcutter's Song". Its internal name (ID) is `Resolve for Glade`.

Hints:
* No Harmony overrides are necessary for this challenge.
* Use the internal ID to fetch the perk configuration. Think where it would reside.
* Remember: perks are actually just effects behind the scenes.
* Check the model of the effect. It might contain a useful method to complete the challenge!

### Hang-On Challenge Solution

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

After building and running this, you should have the perk Woodcutter's Song as a perk on the bottom left of the screen.

</details>

# Using The ATS API

Until now we didn't use the ATS API library at all. We referenced actual code, overridden or added some parts of the game's code. This is great when we want to tweak the game's logic or change existing content.

The ATS API library is intended to make some actions abstract, by taking care of the handling of a bunch of complicated actions. As it grows, new things will be added to it. At the time of writing this guide, its major part is by helping us creating new content, such as perks, cornerstones, goods, traders, and more! If you have any ideas or requests, feel free to tell us in the modding Discord!

## Effects

Almost anything that modifies the game in any way is an Effect. Forest Mysteries, Glade Events (both consequences and working effects), cornerstones and perks are all effects that the player gets in various ways. Some are instant, while some are continuous effects, applying their logic constantly.

In this section, we'll learn how to create new effects from scratch by using the ATS API. Note that we'll need icons in size of 128x128. If you don't have any, feel free to take them from the [Asset Folder](https://github.com/Shushishtok/AtS-my-first-mod/tree/master/Assets) of this repository.

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

First, we need an Effect Model. Eremite has a lot of predefined Effect Models that handle logic so we won't have to, which is nice. The list of models can be found [here](https://github.com/JamesVeug/AgainstTheStormAPI/blob/master/ATS_API/WIKI/EFFECTS.md). Most of those Effect Model's behavior is self explanatory from their name. For some, we'll still need to test and see how they work. For now, let's pick an easy one: the `GoodsPerMinEffectModel` model. As its name might suggest, it gives the player X goods every minute. We decide how many and what good when we define the model.

Second, we need a reference to the Good that we want to get every minute. The reference will define what Good will be given to the player and its amount.

We want to make an effect named "Honeytraps". It will give you 5 Insects per minute.

First, let's begin with some easy variables:

```cs
string effectName = "Honeytraps";
string effectIconPath = "Honeytraps.jpg";
int amount = 5;
```

We will use the corresponding image Honeytraps.jpg from the Assets Folder in the project. Create it if you don't have it yet - it will automatically be processed during the project build process into the mod.

Next, let's create the Good Reference. We will attach it to the effect once we're doing creating it. We'll have to first fetch the model (configuration) of the Good we want to give the player:

```cs
GoodModel insectGoodModel = MB.Settings.GetGood(GoodsTypes.Insects.ToName());
GoodRef insectGoodRef = new() { good = insectGoodModel, amount = amount };
```

Note that we used an enum named `GoodTypes`, which comes from the ATS API, and is used to make our lives easier so we won't have to guess the internal name of the Good - in this case - Insects.

Next, we will create an "effect builder". This is where the ATS API comes in - this is a custom builder that manages most of the properties of the effects easily. When we create an effect builder, we must tell it what type of effect we're building. So we'll do that as follows:

```cs
EffectBuilder<GoodsPerMinEffectModel> builder = new(PluginInfo.PLUGIN_GUID, effectName, effectIconPath);
```

There are a few things to note here. First, the EffectBuilder is of type `GoodsPerMinEffectModel`, which is the effect model we wanted to use. Its constructor accepts 3 parameters: the mod's name (which is automatically stored in `PluginInfo.PLUGIN_GUID`), the effect's name, and the path to the effect's image (relative to the Assets folder in your project).

Our effect's name will actually be `<Your mod name>_<Your effect name>` when the builder will create it. This is done to prevent name collisions in case either Eremite or other modders add effects with the same name.

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
EffectModel effectModel = SO.Settings.GetEffect($"{PluginInfo.PLUGIN_GUID}_Joy of Creation");
effectModel.AddAsPerk();
```

Build the project and test to see if your effect works as intended. This is a simple effect that doesn't need a lot of configuration.

# TO BE CONTINUED WHEN I GET THE TIME!

Subjects to cover:

* Creating new effects with API library	
    * Simple Hooked effect (Modding Tools)
	* Retroactive/Progressive effect (Humble Bundles)
	* Multi-Layered effect (Bonding Time)	
	