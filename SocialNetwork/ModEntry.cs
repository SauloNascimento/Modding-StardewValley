using StardewModdingAPI;
using StardewModdingAPI.Events;

using SocialNetwork.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    class ModEntry : Mod
    {
        /// <summary>The mod settings.</summary>
        private ModConfig Config;

        /// <summary>The Friendship manager.</summary>
        private FriendshipManager manager;

        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();

            this.manager = new FriendshipManager(Config, Monitor);

            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        /// <summary>The event called after a save slot is loaded.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            this.manager.SetUp();
        }

        /// <summary>The event called when the game updates (roughly sixty times per second).</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (Context.IsWorldReady)
            {
                this.manager.Update();
            }
        }
    }
}
