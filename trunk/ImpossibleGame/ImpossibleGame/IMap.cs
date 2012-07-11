using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImpossibleGame
{
    public interface IMap
    {

        /// <summary>
        /// List of all enemies in this map
        /// </summary>
        List<IEnemy> Enemies { get; }
        
        /// <summary>
        /// Moves all enemies in N points
        /// </summary>
        /// <param name="points">How many pixels will be decreased</param>
        void ChangeXPosition(int points);

        /// <summary>
        /// Check, if nearest enemy colides with player
        /// </summary>
        /// <param name="player">Current player</param>
        /// <returns>True if colides, otherwise - false</returns>
        bool Colision(IPlayer player);

        void Regenerate();
    }
}
