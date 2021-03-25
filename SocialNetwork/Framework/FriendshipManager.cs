using StardewModdingAPI;

using StardewValley;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Framework
{
    class FriendshipManager
    {
        /// <summary>Stores the heart level of a character friendship.</summary>
        private Dictionary<string, int> state;

        /// <summary>The mod settings.</summary>
        private ModConfig config;

        /// <summary>Encapsulates monitoring and logging.</summary>
        private IMonitor Monitor;

        /// <summary>Whether the FriendshipManager was settedup.</summary>
        private bool settedUp = false;

        /// <summary>Get the friendship heart level state for a given player.</summary>
        /// <param name="player">The intended player.</param>
        /// <returns>A dictionary in which keys are NPCs names and values are friendship heart level.</returns>
        public static Dictionary<string, int> GetStateForPlayer(Farmer player)
        {
            Dictionary<string, int> newState = new Dictionary<string, int>();
            foreach (string character in player.friendshipData.Keys)
            {
                newState[character] = player.getFriendshipHeartLevelForNPC(character);
            }
            return newState;
        }

        /// <summary>Initialize a new instance of the class.</summary>
        /// <param name="modConfig">The mod settings.</param>
        /// <param name="monitor">The mod monitor.</param>
        public FriendshipManager(ModConfig modConfig, IMonitor monitor)
        {
            this.config = modConfig;

            this.Monitor = monitor;

            this.state = new Dictionary<string, int>();
        }

        /// <summary>Set up the manager, getting the current state of player's friendships.</summary>
        public void SetUp()
        {
            this.state = FriendshipManager.GetStateForPlayer(Game1.player);
            this.settedUp = true;
        }

        /// <summary>
        /// Checks if friendship heart level change. When change, affects related characters friendship.
        /// </summary>
        public void Update()
        {
            if (settedUp)
            {
                // Save current state
                Dictionary<string, int> oldState = new Dictionary<string, int>(this.state);

                foreach (string character in this.config.Network.Keys)
                {
                    oldState.TryGetValue(character, out int oldLevel);
                    int newLevel = Game1.player.getFriendshipHeartLevelForNPC(character);
                    if (newLevel != oldLevel)
                    {
                        state[character] = newLevel;
                        this.Propagate(character, newLevel - oldLevel);

                    }

                }
            }
        }

        /// <summary>
        /// Applies the bonus points to related NPCs of the given character by name.
        /// </summary>
        /// <param name="name">The NPC name to search for relatives.</param>
        /// <param name="times">The multiplier for the bonus value.</param>
        private void Propagate(string name, int times)
        {
            // Do nothing if Leo is on the island (he is not friends with anyone yet).
            if (name == Characters.Leo && !Game1.MasterPlayer.mailReceived.Contains("leoMoved"))
            {
                return;
            }

            // Affecting related characters if found.
            if (this.config.Network.TryGetValue(name, out List<string> scope))
            {
                foreach (string character in scope)
                {
                    // Don't affect Kent if he didn't come back.
                    if (character != Characters.Kent || Game1.year > 1)
                    {
                        // Look for the character
                        var npc = Game1.getCharacterFromName(character);
                        if (npc != null)
                        {
                            int qnt = times * this.config.Bonus;
                            Game1.player.changeFriendship(qnt, npc);
                            if (this.config.TrackNetwork)
                            {
                                string info = npc.displayName + ": " + (qnt > 0 ? "+" : "") + qnt;
                                Color color = qnt > 0 ? Color.SpringGreen : Color.Firebrick;
                                HUDMessage message = new HUDMessage(info, color, 3500f);
                                message.noIcon = true;
                                Game1.addHUDMessage(message);
                            }
                        }
                        else
                        {
                            Monitor.Log($"{character} not found!", LogLevel.Warn);
                        }
                    }
                }
            }
        }
    }
}
