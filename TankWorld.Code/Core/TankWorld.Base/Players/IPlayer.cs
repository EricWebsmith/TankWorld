namespace TankWorld.Core
{
    /// <summary>
    /// The interface of the player.
    /// The name of an implementation must ends with <b>Player</b>.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The flag of the player
        /// </summary>
        Flag Flag { get; }
        /// <summary>
        /// The first tank of the player
        /// </summary>
        Tank Tank1 { get; }
        /// <summary>
        /// The second tank of the player
        /// </summary>
        Tank Tank2 { get; }

        /// <summary>
        /// The enemy.
        /// </summary>
        IPlayer Opponent { get; }

        /// <summary>
        /// Shows the map to the player
        /// </summary>
        /// <param name="map">The map to be shown to the player.</param>
        void ShowMap(Map map);

        /// <summary>
        /// You must assign a flag and two tanks to the player before the player plays.
        /// That will create two-way binds between the player and game elements, namely the flag and tanks.
        /// </summary>
        /// <param name="myflag">The flag to be assigned to this player</param>
        /// <param name="myTank1">The first tank to be assigned to this player</param>
        /// <param name="myTank2">The second tank to be assigned to this player</param>
        void Assign(Flag myflag, Tank myTank1, Tank myTank2);

        /// <summary>
        /// Introduce this opponent to the player.
        /// </summary>
        /// <param name="opponent">the opponent of the player.</param>
        void HandShake(IPlayer opponent);

        /// <summary>
        /// The player prepares for the match.
        /// Any thing the player wish to do before the match begins.
        /// </summary>
        void Prepare();

        /// <summary>
        /// The player makes PlayerAction.
        /// </summary>
        /// <param name="tank">The tank to be manipulated.</param>
        /// <returns>The PlayerAction</returns>
        PlayerAction Play(Tank tank);
    }
}
