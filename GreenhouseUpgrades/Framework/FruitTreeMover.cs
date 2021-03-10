using StardewModdingAPI;

using StardewValley;
using StardewValley.TerrainFeatures;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace GreenhouseUpgrades.Framework
{
    class FruitTreeMover
    {
        private static bool justUpgraded;

        /// <summary>Encapsulates monitoring and logging.</summary>
        private static IMonitor Monitor;

        public static void SetUp(IMonitor monitor)
        {
            Monitor = monitor;
        }

        public static void Upgraded()
        {
            justUpgraded = true;
        }

        public static void MoveTrees(int newLevel)
        {
            if (justUpgraded)
            {
                justUpgraded = false;

                Monitor.Log("Starting the move process.", LogLevel.Info);

                var greenhouse = Game1.getLocationFromName("Greenhouse");
                if (greenhouse != null)
                {
                    int countTrees = 0;
                    int bottomMoved = 0;
                    int rightMoved = 0;

                    Vector2 bottomOffset = (newLevel == 1) ? new Vector2(0, 5) : new Vector2(0, 6);
                    Vector2 rightOffset = (newLevel == 1) ? new Vector2(3, 0) : new Vector2(6, 0);

                    List<FruitTree> moveBottom = new List<FruitTree>();
                    List<FruitTree> moveRight = new List<FruitTree>();

                    Monitor.Log(String.Join(" ", "Bottom | Right offsets:", bottomOffset, "|", rightOffset), LogLevel.Info);

                    foreach (var terrain in greenhouse.terrainFeatures.Values)
                    {
                        if (terrain is FruitTree fruitTree)
                        {
                            countTrees++;
                            if (BottomSideTree(newLevel, fruitTree))
                            {
                                bottomMoved++;
                                moveBottom.Add(fruitTree);
                            }
                            else if (RightSideTree(newLevel, fruitTree))
                            {
                                rightMoved++;
                                moveRight.Add(fruitTree);
                            }
                        }
                    }

                    foreach (var fruitTree in moveBottom)
                    {
                        Vector2 newTile = fruitTree.currentTileLocation + bottomOffset;
                        Monitor.Log(String.Join(" ", "Moving from:", fruitTree.currentTileLocation, "to", newTile), LogLevel.Info);
                        greenhouse.terrainFeatures.Remove(fruitTree.currentTileLocation);
                        greenhouse.terrainFeatures.Add(newTile, fruitTree);
                    }

                    foreach (var fruitTree in moveRight)
                    {
                        Vector2 newTile = fruitTree.currentTileLocation + rightOffset;
                        Monitor.Log(String.Join(" ", "Moving from:", fruitTree.currentTileLocation, "to", newTile), LogLevel.Info);
                        greenhouse.terrainFeatures.Remove(fruitTree.currentTileLocation);
                        greenhouse.terrainFeatures.Add(newTile, fruitTree);
                    }

                    Monitor.Log(countTrees + " fruit trees found.", LogLevel.Info);
                    Monitor.Log(bottomMoved + " bottom trees moved.", LogLevel.Info);
                    Monitor.Log(rightMoved + " right trees moved.", LogLevel.Info);
                }
                else
                {
                    Monitor.Log("Unable to find Greenhouse", LogLevel.Warn);
                }
            }
        }

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
