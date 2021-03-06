using System.Collections.Generic;

namespace GreenhouseUpgrades.Framework
{
    /// <summary>The mod configuration model.</summary>
    class ModConfig
    {
        /// <summary>Whether the fruit trees in greenhouse should be moved after an upgrade.</summary>
        public bool MoveFruitTrees { get; set; } = true;

        /// <summary>First upgrade price</summary>
        public int UpgradePrice1 { get; set; } = 200000;

        /// <summary>Second upgrade price</summary>
        public int UpgradePrice2 { get; set; } = 500000;

        /// <summary>First upgrade materials required</summary>
        public Dictionary<int, int> UpgradeMaterials1 { get; set; } = new Dictionary<int, int>()
        {
            [390] = 300,
            [335] = 20,
            [330] = 50
        };

        /// <summary>Second upgrade materials required</summary>
        public Dictionary<int, int> UpgradeMaterials2 { get; set; } = new Dictionary<int, int>()
        {
            [390] = 500,
            [787] = 30,
            [645] = 25
        };
    }
}
