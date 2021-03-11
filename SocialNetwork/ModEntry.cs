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

        private ModConfig Config;

        private FriendshipManager manager;

        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();

            this.manager = new FriendshipManager(Config, Monitor);

            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            this.manager.SetUp();
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (Context.IsWorldReady)
            {
                this.manager.Update();
            }
        }
    }
}
