using System;
using System.Collections.Generic;
using System.Linq;
using TankWorld.Core;

namespace TankWorld.GameManager
{
    /// <summary>
    /// Creates players.
    /// </summary>
    public static class PlayerFactory
    {
        private static string[] playerRoster;
        public static string[] PlayerRoster {  get { return playerRoster; } }

        /// <summary>
        /// You must call this before use this class.
        /// </summary>
        public static void Initialise()
        {

            //add external players
            playerDict = GetExternalPlayers();
            //Add internal players
            playerDict.Add("Wandering Player", typeof(WanderingPlayer));
            playerDict.Add("Flag Remover", typeof(FlagRemovingPlayer));
            playerRoster = playerDict.Keys.ToArray();
        }

        /// <summary>
        /// key - the player name
        /// value - the player type
        /// </summary>
        static Dictionary<string, Type> playerDict = new Dictionary<string, Type>();


        public static IPlayer Create(string displayName)
        {
            Type t = playerDict[displayName];
            return Create(t);
        }

        private static IPlayer Create(Type t)
        {
            return (IPlayer)Activator.CreateInstance(t);
        }

        private static Dictionary<string, Type> GetExternalPlayers()
        {
            Dictionary<string, Type> players = new Dictionary<string, Type>();
            string dir = System.Environment.CurrentDirectory + "\\Plugins";
            string[] files = System.IO.Directory.GetFiles(dir, "*.dll");
            foreach (string dllFile in files)
            {
                var assem = System.Reflection.Assembly.LoadFile(dllFile);
                Type[] types = assem.GetTypes();
                foreach (Type t in types)
                {
                    if (t.GetInterfaces().Contains(typeof(IPlayer)))
                    {
                        PlayerAttribute attr = (PlayerAttribute)t.GetCustomAttributes(typeof(PlayerAttribute), false).First();

                        players.Add(attr.DisplayName, t);
                    }
                }
            }

            return players;
        }
    }
}
