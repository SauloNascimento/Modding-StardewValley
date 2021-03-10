using StardewModdingAPI;

using StardewValley.Buildings;

using System;

namespace GreenhouseUpgrades.Framework
{
    /// <summary>Defines patches to be used by Harmony</summary>
    class UpgradePatches
    {
        /// <summary>Encapsulates monitoring and logging.</summary>
        private static IMonitor Monitor;
        /// <summary>Provides simplified APIs for writing mods.</summary>
        private static IModHelper Helper;

        private static bool MoveEnabled;

        /// <summary>Setup Monitor and Helper.</summary>
        /// <param name="monitor">Encapsulates monitoring and logging.</param>
        /// <param name="modHelper">Provides simplified APIs for writing mods.</param>
        public static void SetUp(IMonitor monitor, IModHelper modHelper, bool moveFruitTrees)
        {
            Monitor = monitor;
            Helper = modHelper;
            MoveEnabled = moveFruitTrees;
        }

        /// <summary>Stores the upgrade level on GreenhouseBuilding modData.</summary>
        /// <param name="__instance">The Building Upgraded</param>
        /// <returns></returns>
        public static bool Upgrade_Prefix(GreenhouseBuilding __instance)
        {
            try
            {
                if (__instance.buildingType == "Greenhouse")
                {
                    if (__instance.daysUntilUpgrade.Value == 1)
                    {
                        __instance.daysUntilUpgrade.Value = 0;
                        int newLevel = (ModEntry.GetUpgradeLevel(__instance) + 1);
                        __instance.modData[ModEntry.DataKey] = newLevel.ToString();
                        if (MoveEnabled)
                        {
                            Monitor.Log("Signaling update.", LogLevel.Info);
                            FruitTreeMover.Upgraded();
                        }
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed in {nameof(Upgrade_Prefix)}:\n{ex}", LogLevel.Error);
                return true;
            }
        }
    }
}
