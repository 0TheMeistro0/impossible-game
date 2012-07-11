using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ImpossibleGame
{

    /// <summary>
    /// Creates map - object which contains all enemies in the map
    /// </summary>
    public class RandomMap : IMap
    {
        /// <summary>
        /// List of all enemies in this map
        /// </summary>
        public List<IEnemy> Enemies { get; private set; }

        /// <summary>
        /// Create an instanse of the map. Random generate it
        /// </summary>
        /// <param name="countObjects">How many objects will be placed in the map</param>
        /// <param name="bounds">Bounds in which objects will be drawn</param> 
        /// /// <param name="game">Current game</param> 
        public RandomMap(int countObjects, Game game, Vector2 bounds)
        {
            Enemies = new List<IEnemy>();

            Create(countObjects, game, bounds);
        }

        /// <summary>
        /// Moves all enemies in N points
        /// </summary>
        /// <param name="points">How many pixels will be decreased</param>
        public void ChangeXPosition(int points)
        {
            foreach (var t in Enemies)
            {
                t.Position = new Vector2(t.Position.X - points, t.Position.Y);
            }
        }

        /// <summary>
        /// Check, if nearest enemy colides with player
        /// </summary>
        /// <param name="player">Current player</param>
        /// <returns>True if colides, otherwise - false</returns>
        public bool Colision(IPlayer player)
        {
            var nearestEnemies =
                Enemies.Select(x => x).Where(x => Math.Abs(player.Position.X - x.Position.X) <= player.Size.X);
            var result = nearestEnemies.Any(nearestEnemy => nearestEnemy.Collision(player));
            if (result == true)
            {
                return result;
            }
            return false;
        }

        public void Regenerate()
        {
            var minusObjects = Enemies.Select(x => x).Where(x => x.Position.X < 0);
            int i = 0;
            foreach (var enemies in minusObjects)
            {
                int positionStart = 0;
                int positionEnd = 0;
                var random = new Random(System.Environment.TickCount + i);
                var positionDifference = random.Next(1, 3);

                var lesserEnemies = Enemies.Select(x => x).Where(x => x.Position.X < 1000).ToList();

                switch (positionDifference)
                {
                    case 1:

                        if (lesserEnemies.Count >= 2)
                        {
                            for (int k = 0; i < lesserEnemies.Count - 1; k++)
                            {
                                for (int j = k + 1; j < lesserEnemies.Count; j++)
                                {
                                    if (Math.Abs(lesserEnemies[k].Position.X - lesserEnemies[j].Position.X) <= 400)
                                    {
                                        goto case 2;
                                    }
                                }
                            }
                        }

                        positionStart = 20;
                        positionEnd = 30;

                        break;

                    case 2:

                        positionStart = 400;
                        positionEnd = 550;

                        break;
                }

                int positionBounds = random.Next(positionStart, positionEnd);
                enemies.Position = new Vector2(random.Next(700, 1500) + positionBounds, enemies.Position.Y);
                i++;
            }
        }

        /// <summary>
        /// Generate objects
        /// </summary>
        /// <param name="countOjects">Count of objects</param>
        /// <param name="bounds">Bounds in which objects will be drawn</param> 
        /// /// <param name="game">Current game</param> 
        private void Create(int countOjects, Game game, Vector2 bounds)
        {

            var texture = game.Content.Load<Texture2D>("Triangle");
            IEnemy enemy = new Triangle(game, ref texture, new Vector2(1100, 300), new Vector2(20, 20), bounds);
            Enemies.Add(enemy);

            texture = game.Content.Load<Texture2D>("Triangle");
            enemy = new Triangle(game, ref texture, new Vector2(1120, 300), new Vector2(20, 20), bounds);
            Enemies.Add(enemy);

            texture = game.Content.Load<Texture2D>("Triangle");
            enemy = new Triangle(game, ref texture, new Vector2(700, 300), new Vector2(20, 20), bounds);
            Enemies.Add(enemy);

            texture = game.Content.Load<Texture2D>("Triangle");
            enemy = new Triangle(game, ref texture, new Vector2(500, 300), new Vector2(20, 20), bounds);
            Enemies.Add(enemy);

            texture = game.Content.Load<Texture2D>("Water");
            enemy = new Water(game, ref texture, new Vector2(900, 320), new Vector2(20, 20), bounds);
            Enemies.Add(enemy);

            enemy = new Water(game, ref texture, new Vector2(920, 320), new Vector2(20, 20), bounds);
            Enemies.Add(enemy);

            //for (int i = 1; i < countOjects; i++)
            //{
            //    var randomType = new Random(System.Environment.TickCount + i);
            //    var randomPosition = new Random(System.Environment.TickCount - i);
            //    var randomPositionDifference = new Random(System.Environment.TickCount + i*i);
            //    var random = new Random(System.Environment.TickCount + i);

            //    int positionStart = 0;
            //    int positionEnd = 0;
            //    int positionDifference = 0;
            //    int positionBounds = 0;
            //    int type = 0;
            //    Vector2 position;

            //    positionDifference = random.Next(1, 3);


            //    switch (positionDifference)
            //    {
            //        case 1:

            //            //if (Enemies[Enemies.Count - 1].Position.X - Enemies[Enemies.Count - 2].Position.X < 250)
            //            //{
            //            //    goto case 2;
            //            //}

            //            //positionStart = 20;
            //            //positionEnd = 30;

            //            //break;
            //            goto case 2;

            //        case 2:

            //            positionStart = 400;
            //            positionEnd = 550;

            //            break;
            //    }

            //    positionBounds = random.Next(positionStart, positionEnd);

            //    type = random.Next(1, 3);

            //    switch (type)
            //    {
            //        case 1:


            //            position = new Vector2(Enemies.Last().Position.X + positionBounds, 300);

            //            texture = game.Content.Load<Texture2D>("Triangle");
            //            enemy = new Triangle(game, ref texture, position, new Vector2(20, 20), bounds);

            //            break;
            //        case 2:

            //            position = new Vector2(Enemies.Last().Position.X + positionBounds, 320);

            //            texture = game.Content.Load<Texture2D>("Water");
            //            enemy = new Water(game, ref texture, position, new Vector2(20, 20), bounds);

            //            break;
            //    }

            //    Enemies.Add(enemy);
            //}
        }
    }
}
