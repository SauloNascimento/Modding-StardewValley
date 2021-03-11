using StardewModdingAPI;

using StardewValley;
using StardewValley.TerrainFeatures;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace GreenhouseUpgrades.Framework
{
    /// <summary>Move the Fruit Trees in the greenhouse when needed. </summary>
    class FruitTreeMover
    {
        /// <summary>Whether the greenhouse just upgraded.</summary>
        private static bool justUpgraded;

        /// <summary>Encapsulates monitoring and logging.</summary>
        private static IMonitor Monitor;

        /// <summary>Setup Monitor.</summary>
        /// <param name="monitor">Encapsulates monitoring and logging.</param>
        public static void SetUp(IMonitor monitor)
        {
            Monitor = monitor;
        }

        /// <summary>Notifies FruitTreeMover that the greenhouse has been updated.</summary>
        public static void Upgraded()
        {
            justUpgraded = true;
        }

        /// <summary>Move the Fruit Trees in the greenhouse if an upgrade happened.</summary>
        /// <param name="newLevel">The upgrade level of the greenhouse after the upgrade.</param>
        public static void MoveTrees(int newLevel)
        {
            if (justUpgraded)
            {
                justUpgraded = false;

                var greenhouse = Game1.getLocationFromName("Greenhouse");
                if (greenhouse != null)
                {
                    // Defining the offset to be applied for bottom and right side trees.
                    Vector2 bottomOffset = (newLevel == 1) ? new Vector2(0, 5) : new Vector2(0, 6);
                    Vector2 rightOffset = (newLevel == 1) ? new Vector2(3, 0) : new Vector2(6, 0);

                    // List of bottom and right side trees to move.
                    List<FruitTree> moveBottom = new List<FruitTree>();
                    List<FruitTree> moveRight = new List<FruitTree>();

                    // Finding the fruit trees.
                    foreach (var terrain in greenhouse.terrainFeatures.Values)
                    {
                        if (terrain is FruitTree fruitTree)
                        {
                            // Checking if the tree is on the bottom or right side then
                            // add it to the respective list.
                            if (BottomSideTree(newLevel, fruitTree))
                            {
                                moveBottom.Add(fruitTree);
                            }
                            else if (RightSideTree(newLevel, fruitTree))
                            {
                                moveRight.Add(fruitTree);
                            }
                        }
                    }

                    // Moving bottom side trees.
                    foreach (var fruitTree in moveBottom)
                    {
                        Vector2 newTile = fruitTree.currentTileLocation + bottomOffset;
                        greenhouse.terrainFeatures.Remove(fruitTree.currentTileLocation);
                        greenhouse.terrainFeatures.Add(newTile, fruitTree);
                    }

                    // Moving right side trees.
                    foreach (var fruitTree in moveRight)
                    {
                        Vector2 newTile = fruitTree.currentTileLocation + rightOffset;
                        greenhouse.terrainFeatures.Remove(fruitTree.currentTileLocation);
                        greenhouse.terrainFeatures.Add(newTile, fruitTree);
                    }
                }
                else
                {
                    Monitor.Log("Unable to find Greenhouse", LogLevel.Warn);
                }
            }
        }

        /// <summary>
        /// Checks if a FruitTree is localized on the bottom side of the greenhouse based on the new upgrade level.
        /// </summary>
        /// <param name="newLevel">The new upgrade level.</param>
        /// <param name="fruitTree">The fruit tree to check.</param>
        /// <returns></returns>
        private static bool BottomSideTree(int newLevel, FruitTree fruitTree)
        {
            float x = fruitTree.currentTileLocation.X;
            float y = fruitTree.currentTileLocation.Y;

            switch (newLevel)
            {
                case 1:
                    return (x >= 3 && x <= 16) && (y >= 21 && y <= 22);
                case 2:
                    return (x >= 3 && x <= 19) && (y >= 26 && y <= 27);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks if a FruitTree is localized on the right side of the greenhouse based on the new upgrade level.
        /// </summary>
        /// <param name="newLevel">The new upgrade level.</param>
        /// <param name="fruitTree">The fruit tree to check.</param>
        /// <returns></returns>
        private static bool RightSideTree(int newLevel, FruitTree fruitTree)
        {
            float x = fruitTree.currentTileLocation.X;
            float y = fruitTree.currentTileLocation.Y;

            switch (newLevel)
            {
                case 1:
                    return (x >= 17 && x <= 18) && (y >= 9 && y <= 22);
                case 2:
                    return (x >= 20 && x <= 21) && (y >= 9 && y <= 27);
                default:
                    return false;
            }
        }
    }
}
