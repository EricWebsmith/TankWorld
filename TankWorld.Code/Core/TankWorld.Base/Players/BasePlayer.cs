using System;

namespace TankWorld.Core
{
    /// <summary>
    /// A basic abstract player.
    /// a concreate player class either inherits this or implements IPlayer.
    /// The name of an implementation must ends with <b>Player</b>.
    /// </summary>
    public abstract class BasePlayer : IPlayer
    {
        protected Map map;
        /// <summary>
        /// The flag of the player
        /// </summary>
        public Flag Flag { get; private set; }
        /// <summary>
        /// The first tank of the player
        /// </summary>
        public Tank Tank1 { get; private set; }
        /// <summary>
        /// The second tank of the player
        /// </summary>
        public Tank Tank2 { get; private set; }

        /// <summary>
        /// The enemy.
        /// </summary>
        public IPlayer Opponent { get; private set; }

        /// <summary>
        /// Shows the map to the player
        /// </summary>
        /// <param name="map">The map to be shown to the player.</param>
        public void ShowMap(Map map)
        {
            this.map = map;
        }

        /// <summary>
        /// You must assign a flag and two tanks to the player before the player plays.
        /// That will create two-way binds between the player and game elements, namely the flag and tanks.
        /// </summary>
        /// <param name="myflag">The flag to be assigned to this player</param>
        /// <param name="myTank1">The first tank to be assigned to this player</param>
        /// <param name="myTank2">The second tank to be assigned to this player</param>
        public void Assign(Flag flag, Tank tank1, Tank tank2)
        {
            flag.Owner = this;
            tank1.Owner = this;
            tank2.Owner = this;
            this.Flag = flag;
            this.Tank1 = tank1;
            this.Tank2 = tank2;
        }

        /// <summary>
        /// Introduce this opponent to the player.
        /// </summary>
        /// <param name="opponent">the opponent of the player.</param>
        public void HandShake(IPlayer opponent)
        {
            this.Opponent = opponent;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Prepare()
        {
            //do nothing.
        }

        /// <summary>
        /// The player makes PlayerAction.
        /// Concreate Player class must override this.
        /// </summary>
        /// <param name="tank">The tank to be manipulated.</param>
        /// <returns>The PlayerAction</returns>
        public abstract PlayerAction Play(Tank tank);


    }
}