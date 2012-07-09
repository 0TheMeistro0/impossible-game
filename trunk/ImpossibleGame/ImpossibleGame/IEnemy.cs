using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ImpossibleGame
{
    /// <summary>
    /// This is the enemy interface
    /// </summary>
    public class IEnemy : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /// <summary>
        /// Here contains sprite
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Here contains position of enemy
        /// </summary>
        /// 
        public Vector2 Position { get; set; }

        /// <summary>
        /// Here contains size of enemy
        /// </summary>
        public Vector2 Size { get; set; }

        public Vector2 Bounds { get; private set; }


        /// <summary>
        /// Creates the new instance of IEnemy
        /// </summary>
        /// <param name="game">Current game</param>
        /// <param name="texture">Image of enemy</param>
        /// <param name="position">Start position of enemy</param>
        /// <param name="size">Size of enemy</param>
        /// <param name="bounds">Left and right bounds, in which enemy will be drawn</param>
        protected IEnemy(Game game, ref Texture2D texture, Vector2 position, 
            Vector2 size, Vector2 bounds) : base(game)
        {
            Texture = texture;
            Position = position;
            Size = size;
            Bounds = bounds;
        }

        /// <summary>
        /// Gets rectangle where Enemy is located
        /// </summary>
        public Rectangle EnemyRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,
                (int)(Size.X), (int)(Size.Y));
            }
        }

        /// <summary>
        /// Checks if enemy collide with player
        /// </summary>
        /// <param name="player">Current Player</param>
        public virtual bool Collision(IPlayer player)
        {
            //if (EnemyRectangle.Intersects(player.PlayerRectangle))
            //{
            //    return true;
            //}
            //else
            //{
                if (EnemyRectangle.Contains(player.PlayerRectangle.X, 
                    player.PlayerRectangle.Y))
                {
                    return true;
                }

                if (EnemyRectangle.Contains(player.PlayerRectangle.X + player.PlayerRectangle.Width,
                    player.PlayerRectangle.Y))
                {
                    return true;
                }

                if (EnemyRectangle.Contains(player.PlayerRectangle.X,
                    player.PlayerRectangle.Y + player.PlayerRectangle.Height))
                {
                    return true;
                }

                if (EnemyRectangle.Contains(player.PlayerRectangle.X + player.PlayerRectangle.Width,
                    player.PlayerRectangle.Y + player.PlayerRectangle.Height))
                {
                    return true;
                }
            //}
            return false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
