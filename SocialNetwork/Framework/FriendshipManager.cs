using StardewModdingAPI;

using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Framework
{
    class FriendshipManager
    {

        private Dictionary<string, int> state;

        private ModConfig config;

        private IMonitor Monitor;

        private bool settedUp = false;

        public static Dictionary<string, int> GetStateForPlayer(Farmer player)
        {
            Dictionary<string, int> newState = new Dictionary<string, int>();
            foreach (string character in player.friendshipData.Keys)
            {
                newState[character] = player.getFriendshipHeartLevelForNPC(character);
            }
            return newState;
        }

        public FriendshipManager(ModConfig modConfig, IMonitor monitor)
        {
            this.config = modConfig;

            this.Monitor = monitor;

            this.state = new Dictionary<string, int>();
        }

        public void SetUp()
        {
            this.state = FriendshipManager.GetStateForPlayer(Game1.player);
            this.settedUp = true;
            Monitor.Log(state.ToString(), LogLevel.Info);
        }

        public void Update()
        {
            if (settedUp)
            {
                Dictionary<string, int> oldState = new Dictionary<string, int>(this.state);
                foreach (string character in this.config.Network.Keys)
                {
                    oldState.TryGetValue(character, out int oldLevel);
                    int newLevel = Game1.player.getFriendshipHeartLevelForNPC(character);
                    if (newLevel != oldLevel)
                    {
                        Monitor.Log("Friendship for " + character + " changed from " + oldLevel +
                            " to " + newLevel, LogLevel.Info);
                        state[character] = newLevel;
                        this.Propagate(character, newLevel - oldLevel);

                    }

                }
            }
        }

        public void Propagate(string name, int times)
        {
            if (this.config.Network.TryGetValue(name, out List<string> scope))
            {
                foreach (string character in scope)
                {
                    var npc = Game1.getCharacterFromName(name);
                    int qnt = times * this.config.Bonus;
                    Game1.player.changeFriendship(qnt, npc);
                    if (this.config.TrackNetwork)
                    {
                        string info = npc.displayName + ": " + (qnt > 0 ? "+" : "") + qnt;
                        Monitor.Log(info, LogLevel.Info);
                    }
                }
            }
        }
    }
}
