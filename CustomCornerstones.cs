﻿using ATS_API.Effects;
using ATS_API.Helpers;
using ATS_API.Localization;
using Eremite;
using Eremite.Model;
using Eremite.Model.Effects;
using Eremite.Model.Effects.Hooked;
using MyFirstMod.CustomHooks;
using System;
using TextArgType = Eremite.Model.Effects.Hooked.TextArgType;

namespace MyFirstMod
{
    internal class CustomCornerstones
    {
        public static void CreateNewCornerstones() 
        {
            ATS_API.Plugin.Log.LogInfo("Starting to creating custom cornerstones");
            CreateCornerstoneModdingTools();
            CreateCornerstoneBondingSeason();
            CreateCornerstoneHumbleBundles();
            CreateCornerstoneSteelBoots();
            CreateCornerstoneHoneytraps();
            CreateCornerstoneJoyOfCreation();
            CreateCornerstoneBotanyKnowledge();
            ATS_API.Plugin.Log.LogInfo("All custom cornerstones created successfully!");
        }

        private static void CreateCornerstoneModdingTools()
        {
            string cornerstoneName = "Modding Tools";
            string cornerstoneIconPath = "ModdingTools.png";

            HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, cornerstoneName, cornerstoneIconPath);
            builder.SetPositive(true);
            builder.SetRarity(EffectRarity.Rare);
            builder.SetObtainedAsCornerstone();
            builder.SetAvailableInAllBiomesAndSeasons();
            builder.SetDrawLimit(1);
            builder.SetDisplayName(cornerstoneName);
            builder.SetLabel("Modded by Shush");
            builder.SetDescription("Modders have assembled new tools that bring in new talent. " +
                                    "Every {0} new Villagers gain +{1} Global Resolve.");
            builder.SetDescriptionArgs((SourceType.Hook, TextArgType.Amount, 0), (SourceType.HookedEffect, TextArgType.Amount, 0));
            builder.SetPreviewDescription("+{0} Global Resolve");
            builder.SetPreviewDescriptionArgs((HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0));

            builder.AddHook(HookFactory.AfterXNewVillagers(8));
            builder.AddHookedEffect(EffectFactory.AddHookedEffect_IncreaseResolve(builder));

            ATS_API.Plugin.Log.LogInfo($"Cornerstone {builder.Name} created");
        }

        private static void CreateCornerstoneBondingSeason()
        {
            string cornerstoneName = "Bonding Season";
            string cornerstoneIconPath = "BondingSeason.jpg";

            HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, cornerstoneName, cornerstoneIconPath);
            builder.SetPositive(true);
            builder.SetRarity(EffectRarity.Epic);
            builder.SetObtainedAsCornerstone();
            builder.SetAvailableInAllBiomesAndSeasons();
            builder.SetDrawLimit(1);
            builder.SetDisplayName(cornerstoneName);
            builder.SetDescription("The strong smell in the air creates unbreakable bonds in the settlements. When the season changes, gain +{0} Global Resolve for {1} seconds.");
            builder.SetDescriptionArgs(
                (SourceType.HookedEffect, TextArgType.NestedArg0, 0),
                (SourceType.HookedEffect, TextArgType.NestedArg1, 0)
            );
            builder.SetLabel("Modded by Shush");
            builder.AddHook(HookFactory.OnNewSeason(SeasonTypes.All, 1));
            builder.EffectModel.showHookedRewardsAsPerks = true;

            GameTimePassedHook gameTimePassedRemovalHook = Activator.CreateInstance<GameTimePassedHook>();
            gameTimePassedRemovalHook.startWithCurrentValue = false;
            gameTimePassedRemovalHook.seconds = 120f;

            HookedEffectBuilder resolveBonusBuilder = new(PluginInfo.PLUGIN_GUID, cornerstoneName + "_resolveHookedEffect", cornerstoneIconPath);
            resolveBonusBuilder.EffectModel.hooks = [gameTimePassedRemovalHook];
            resolveBonusBuilder.SetPositive(true);
            resolveBonusBuilder.SetRarity(EffectRarity.Epic);
            resolveBonusBuilder.SetDisplayName(cornerstoneName);
            resolveBonusBuilder.SetLabel("Timed Bonus - Modded by Shush");
            resolveBonusBuilder.SetDescription("It's time to create bonds! Global Resolve increased by +{0} for {1} seconds.");
            resolveBonusBuilder.SetDescriptionArgs(
                (SourceType.InstantEffect, TextArgType.Amount, 0),
                (SourceType.RemovalHook, TextArgType.Amount, 0)
            );

            resolveBonusBuilder.AddRemovalHook(gameTimePassedRemovalHook);
            resolveBonusBuilder.AddInstantEffect(EffectFactory.AddHookedEffect_IncreaseResolve(builder, 5));
            resolveBonusBuilder.EffectModel.hasRemovalHooks = true;
            resolveBonusBuilder.EffectModel.amountText = "+";
            resolveBonusBuilder.EffectModel.hasNestedAmount = true;
            resolveBonusBuilder.EffectModel.nestedAmount = new() { source = SourceType.InstantEffect, type = TextArgType.Amount, sourceIndex = 0 };

            resolveBonusBuilder.EffectModel.hasRemovalDynamicStatePreview = true;
            resolveBonusBuilder.EffectModel.removalDynamicPreviewText = LocalizationManager.ToLocaText(PluginInfo.PLUGIN_GUID, resolveBonusBuilder.Name, "preview", "TIME LEFT: {0}");
            HookedStateTextArg removalPreviewArgs = new() { asTime = true, sourceIndex = 0, source = HookedStateTextArg.HookedStateTextSource.RemovalProgressLeftFloat };

            resolveBonusBuilder.EffectModel.removalStatePreviewArgs = [removalPreviewArgs];

            builder.AddHookedEffect(resolveBonusBuilder.EffectModel);

            ATS_API.Plugin.Log.LogInfo($"Cornerstone {builder.Name} created");
        }

        private static void CreateCornerstoneHumbleBundles()
        {
            ATS_API.Plugin.Log.LogInfo("Starting to build humble bundle cornerstone");
            string cornerstoneName = "Humble Bundles";
            string cornerstoneIconPath = "HumbleBundles.jpg";
            int amountToGet = 3;
            Settings settings = MB.Settings;
            GoodModel fabricGoodModel = settings.GetGood(GoodsTypes.Fabric.ToName());
            GoodModel brickGoodModel = settings.GetGood(GoodsTypes.Bricks.ToName());
            GoodModel plankGoodModel = settings.GetGood(GoodsTypes.Planks.ToName());
            GoodModel[] goodsToReceive = [fabricGoodModel, brickGoodModel, plankGoodModel];

            ATS_API.Plugin.Log.LogInfo("builder start");
            HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, cornerstoneName, cornerstoneIconPath);
            builder.SetAvailableInAllBiomesAndSeasons();
            builder.SetObtainedAsCornerstone();
            builder.SetDrawLimit(1);
            builder.SetPositive(true);
            builder.SetRarity(EffectRarity.Rare);
            builder.SetDisplayName(cornerstoneName);
            builder.SetLabel("Modded by Shush");
            ATS_API.Plugin.Log.LogInfo("setting a description");
            builder.SetDescription("Traders like to throw in small extras in their deals with you. When selling goods worth {0} Amber to traders and trade routes, gain {1} " +
                Utils.GetGoodIconAndName(fabricGoodModel) + ", " +
                Utils.GetGoodIconAndName(brickGoodModel) + " and " +
                Utils.GetGoodIconAndName(plankGoodModel) + ". " +
                "(The bonus is added retroactively).");
            builder.SetDescriptionArgs(
                (SourceType.Hook, TextArgType.Amount, 0),
                (SourceType.HookedEffect, TextArgType.Amount, 0)
            );
            builder.SetPreviewDescription("PROGRESS: {0}/{1}. GAINED: {2}");
            builder.SetPreviewDescriptionArgs(
               (HookedStateTextArg.HookedStateTextSource.ProgressFloat, 0),
               (HookedStateTextArg.HookedStateTextSource.HookAmountInt, 0),
               (HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
            );

            builder.SetRetroactiveDescription("Expected Gain: {0}");
            builder.SetRetroactiveDescriptionArgs(
                (HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
            );

            TraderValueSoldHook traderValueSoldHook = Activator.CreateInstance<TraderValueSoldHook>();
            traderValueSoldHook.amount = 20;
            traderValueSoldHook.startWithCurrentValue = true;
            builder.AddHook(traderValueSoldHook);

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

            ATS_API.Plugin.Log.LogInfo($"Cornerstone {builder.Name} created");
        }
        private static void CreateCornerstoneSteelBoots()
        {
            string cornerstoneName = "Steel Boots";
            string cornerstoneIconPath = "SteelBoots.jpg";

            HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, cornerstoneName, cornerstoneIconPath);
            builder.SetAvailableInAllBiomesAndSeasons();
            builder.SetObtainedAsCornerstone();
            builder.SetDrawLimit(1);
            builder.SetPositive(true);
            builder.SetRarity(EffectRarity.Epic);
            builder.SetDisplayName(cornerstoneName);
            builder.SetLabel("Modded by Shush");
            builder.SetDescription("Incredibly sturdy steel boots that are perfect for exploring the forest. Scouts move {0} faster.");
            builder.SetDescriptionArgs((SourceType.InstantEffect, TextArgType.Amount, 0));

            ProfessionSpeedEffectModel professionSpeedEffectModel = EffectFactory.NewHookedEffect<ProfessionSpeedEffectModel>(builder);
            professionSpeedEffectModel.amount = 0.25f;
            professionSpeedEffectModel.profession = MB.Settings.GetProfessionModel(Professions.Scout.ToName());
            builder.AddInstantEffect(professionSpeedEffectModel);

            builder.EffectModel.hooks = [];

            ATS_API.Plugin.Log.LogInfo($"Cornerstone {builder.Name} created");
        }

        private static void CreateCornerstoneHoneytraps()
        {
            string cornerstoneName = "Honeytraps";
            string cornerstoneIconPath = "Honeytraps.jpg";
            int amount = 5;

            GoodModel insectGoodModel = MB.Settings.GetGood(GoodsTypes.Insects.ToName());
            GoodRef insectGoodRef = new() { good = insectGoodModel, amount = amount };

            EffectBuilder<GoodsPerMinEffectModel> builder = new(PluginInfo.PLUGIN_GUID, cornerstoneName, cornerstoneIconPath);
            builder.SetRarity(EffectRarity.Legendary);
            builder.SetPositive(true);
            builder.SetDrawLimit(1);
            builder.SetAvailableInAllBiomesAndSeasons();
            builder.SetObtainedAsCornerstone();
            builder.SetLabel("Modded by Shush");
            builder.SetDisplayName(cornerstoneName);
            builder.SetDescription($"Gain {amount} {Utils.GetGoodIconAndName(insectGoodModel)} every minute.");
            builder.EffectModel.good = insectGoodRef;
        }

        private static void CreateCornerstoneJoyOfCreation()
        {
            string cornerstoneName = "Joy of Creation";
            string cornerstoneIconPath = "Joy of Creation.jpg";

            HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID ,cornerstoneName, cornerstoneIconPath);
            builder.SetPositive(true);
            builder.SetRarity(EffectRarity.Rare);
            builder.SetObtainedAsCornerstone();
            builder.SetAvailableInAllBiomesAndSeasons();
            builder.SetDrawLimit(1);
            builder.SetDisplayName(cornerstoneName);
            builder.SetLabel("Modded by Shush");
            builder.SetDescription("Your village is filled with a little bit of hope with every new structure erected. " +
                                    "Gain {0} resolve any time {1} buildings are constructed. " +
                                    "(The bonus is added retroactively)");
            builder.SetDescriptionArgs((SourceType.HookedEffect, TextArgType.Amount, 0), (SourceType.Hook, TextArgType.Amount, 0));
            builder.SetPreviewDescription("PROGRESS: {0}/{1}. GAINED: {2}");
            builder.SetPreviewDescriptionArgs(
               (HookedStateTextArg.HookedStateTextSource.ProgressInt, 0),
               (HookedStateTextArg.HookedStateTextSource.HookAmountInt, 0),
               (HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
            );
            builder.SetRetroactiveDescription("Expected Gain: {0}");
            builder.SetRetroactiveDescriptionArgs(
                (HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
            );

            BuildingCompletedHook buildingCompletedHook = Activator.CreateInstance<BuildingCompletedHook>();
            buildingCompletedHook.amount = 3;
            buildingCompletedHook.startWithCurrentValue = true;
            buildingCompletedHook.ignoreDecorationBuildings = true;
            buildingCompletedHook.ignoreRoads = true;

            builder.AddHook(buildingCompletedHook);
            builder.AddHookedEffect(EffectFactory.AddHookedEffect_IncreaseResolve(builder, 1));
        }

        private static void CreateCornerstoneBotanyKnowledge()
        {
            string cornerstoneName = "Botany Knowledge";
            string cornerstoneIconPath = "Botany Knowledge.jpg";

            HookedEffectBuilder builder = new(PluginInfo.PLUGIN_GUID, cornerstoneName, cornerstoneIconPath);
            builder.SetPositive(true);
            builder.SetRarity(EffectRarity.Legendary);
            builder.SetObtainedAsCornerstone();
            builder.SetAvailableInAllBiomesAndSeasons();
            builder.SetDrawLimit(1);
            builder.SetDisplayName(cornerstoneName);
            builder.SetLabel("Modded by Shush");
            builder.SetDescription("An ever increasing understanding of plants pushes the potential of handling roots. " +
                "Root production increases by {0} every {1} times it's produced.");
            builder.SetDescriptionArgs(
                    (SourceType.HookedEffect, TextArgType.Amount, 0),
                    (SourceType.Hook, TextArgType.Amount, 0)
                );

            builder.SetPreviewDescription("PROGRESS: {0}/{1}. GAINED: {2}");
            builder.SetPreviewDescriptionArgs(
                    (HookedStateTextArg.HookedStateTextSource.ProgressInt, 0),
                    (HookedStateTextArg.HookedStateTextSource.HookAmountInt, 0),
                    (HookedStateTextArg.HookedStateTextSource.TotalGainIntFromHooked, 0)
                );

            GoodModel rootGoodModel = MB.Settings.GetGood(GoodsTypes.Roots.ToName());
            GoodRef rootGoodRefForHook = new() { good = rootGoodModel, amount = 25 };
            GoodRef rootGoodRefForEffect = new() { good = rootGoodModel, amount = 1 };

            GoodProducedHook goodProducedHook = Activator.CreateInstance<GoodProducedHook>();
            goodProducedHook.good = rootGoodRefForHook;
            goodProducedHook.cycles = true;
            builder.AddHook(goodProducedHook);

            GoodsRawProductionEffectModel goodsRawProductionEffect = EffectFactory.NewHookedEffect<GoodsRawProductionEffectModel>(builder);
            goodsRawProductionEffect.good = rootGoodRefForEffect;
            builder.AddHookedEffect(goodsRawProductionEffect);
        }
    }
}
