using StardewModdingAPI;
using StardewModdingAPI.Events;

using StardewValley;
using StardewValley.Buildings;
using StardewValley.Menus;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;

using ContentPatcher;
using Harmony;
using GreenhouseUpgrades.Framework;

using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenhouseUpgrades
{
    class ModEntry : Mod
    {
        /// <summary>The key for the upgrade level on GreenhouseBuilding modData</summary>
        public static readonly String DataKey = "Cecidelus.GreenhouseUpgrades/upgrade-level";

        /// <summary>The mod settings.</summary>
        private ModConfig Config;

        /// <summary>Determines the current Greenhouse upgrade level.</summary>
        /// <param name="greenhouse">The Farm's Greenhouse Building.</param>
        /// <returns>A value from 0 (No Upgrade done) to 2 (Maximum Upgrade Level).</returns>
        public static int GetUpgradeLevel(GreenhouseBuilding greenhouse)
        {
            return greenhouse.modData.TryGetValue(DataKey, out string currentLevel) ?
                int.Parse(currentLevel) : 0;
        }

        public override void Entry(IModHelper helper)
        {
            // Read Config
            this.Config = helper.ReadConfig<ModConfig>();

            // Starting Localisation
            I18n.Init(helper.Translation);

            // Starting Harmony
            UpgradePatches.SetUp(Monitor, helper, Config.MoveFruitTrees);
            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(Building), nameof(Building.dayUpdate)),
                prefix: new HarmonyMethod(typeof(UpgradePatches), nameof(UpgradePatches.Upgrade_Prefix))
            );

            FruitTreeMover.SetUp(Monitor);

            // Events Hooks
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.Display.MenuChanged += this.OnMenuChanged;
        }

        /// <summary>The event called after the first game update, once all mods are loaded.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            // Getting ContentPatcher API
            var api = this.Helper.ModRegistry.GetApi<IContentPatcherAPI>("Pathoschild.ContentPatcher");

            if (api != null)
            {
                // Registering Tokens
                api.RegisterToken(this.ModManifest, "UpgradeLevel", () =>
                {
                    if (Context.IsWorldReady)
                    {
                        var greenhouse = Game1.getFarm().buildings.OfType<GreenhouseBuilding>().FirstOrDefault();
                        return new[] { GetUpgradeLevel(greenhouse).ToString() };
                    }
                    else
                    {
                        return new[] { "0" };
                    }
                });

                api.RegisterToken(this.ModManifest, "CropsReady", () =>
                {
                    if (Context.IsWorldReady)
                    {
                        bool cropsReady = this.GreenhouseCropsReady();
                        return new[] { cropsReady.ToString() };
                    }
                    else
                    {
                        return new[] { "False" };
                    }
                });
            }
        }

        /// <summary>The event called when a new day begins.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            var greenhouse = Game1.getFarm().buildings.OfType<GreenhouseBuilding>().FirstOrDefault();
            int currentLevel = GetUpgradeLevel(greenhouse);

            FruitTreeMover.MoveTrees(currentLevel);

            if (currentLevel == 2)
                this.WaterGreenhouse();
        }

        /// <summary>The event called after an active menu is opened or closed.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMenuChanged(object sender, MenuChangedEventArgs e)
        {
            // Checked if on Robin's menu and adds the blueprint if available
            if (!Context.IsWorldReady ||
                !Game1.getFarm().greenhouseUnlocked)
                return;

            if (e.NewMenu is CarpenterMenu &&
                !this.Helper.Reflection.GetField<bool>(e.NewMenu, "magicalConstruction").GetValue())
            {
                var greenhouse = Game1.getFarm().buildings.OfType<GreenhouseBuilding>().FirstOrDefault();
                int currentLevel = GetUpgradeLevel(greenhouse);

                if (currentLevel < 2)
                {
                    IList<BluePrint> blueprints = this.Helper.Reflection
                        .GetField<List<BluePrint>>(e.NewMenu, "blueprints")
                        .GetValue();
                    blueprints.Add(this.GetUpgradeBluePrint(currentLevel));
                }
            }
        }

        /// <summary>Determines the Blueprint of the next Greenhouse Upgrade to be done.</summary>
        /// <param name="currentLevel">The current upgrade level of the Greenhouse.</param>
        /// <returns>A BluePrint for the next (or last) Greenhouse Upgrade.</returns>
        private BluePrint GetUpgradeBluePrint(int currentLevel)
        {
            string name;
            string desc;
            int money;
            Dictionary<int, int> items;

            if (currentLevel == 0)
            {
                name = I18n.Upgrade1_Name();
                desc = I18n.Upgrade1_Description();
                money = this.Config.UpgradePrice1;
                items = this.Config.UpgradeMaterials1;
            }
            else
            {
                name = I18n.Upgrade2_Name();
                desc = I18n.Upgrade2_Description();
                money = this.Config.UpgradePrice2;
                items = this.Config.UpgradeMaterials2;
            }

            return new BluePrint("Greenhouse")
            {
                itemsRequired = items,
                displayName = name,
                description = desc,
                moneyRequired = money,
                nameOfBuildingToUpgrade = "Greenhouse",
                blueprintType = "Upgrade",
                daysToConstruct = 2
            };
        }


        /// <summary>Waters all Hoed Dirt and Garden Pots in the Greenhouse.</summary>
        private void WaterGreenhouse()
        {
            var greenhouse = Game1.getLocationFromName("Greenhouse");
            if (greenhouse != null)
            {
                // Water Soil
                foreach (var terrain in greenhouse.terrainFeatures.Values)
                {
                    if (terrain is HoeDirt hoeDirt)
                    {
                        hoeDirt.state.Value = HoeDirt.watered;
                    }
                }

                // Water Garden pots
                foreach (IndoorPot pot in greenhouse.objects.Values.OfType<IndoorPot>())
                {
                    pot.hoeDirt.Value.state.Value = HoeDirt.watered;
                    pot.showNextIndex.Value = true;
                }
            }
            else
            {
                Monitor.Log("Unable to find Greenhouse", LogLevel.Warn);
            }
        }

        /// <summary>Determines if any crop in the Greenhouse (on Soil or Garden Pot) is ready for harvest.</summary>
        /// <returns>Returns true if any ready for harvest crop was found, false otherwise.</returns>
        private bool GreenhouseCropsReady()
        {
            bool soilCrops = Game1.getFarm().getTotalGreenhouseCropsReadyForHarvest() > 0;

            var greenhouse = Game1.getLocationFromName("Greenhouse");
            var pots = greenhouse.objects.Values.OfType<IndoorPot>();
            bool potCrops = pots.Select(p => p.hoeDirt.Value.readyForHarvest()).Contains(true);
            return soilCrops || potCrops;
        }
    }
}
