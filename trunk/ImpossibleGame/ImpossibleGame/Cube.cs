using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ImpossibleGame
{
    public class Cube : IEnemy
    {
        /// <summary>
        /// Creates the new instance of CubeEnemy
        /// </summary>
        /// <param name="game">Current game</param>
        /// <param name="texture">Image of enemy</param>
        /// <param name="position">Start position of enemy</param>
        /// <param name="size">Size of enemy</param>
        /// <param name="bounds">Left and right bounds, in which enemy will be drawn</param>
        public Cube(Game game, ref Texture2D texture, Vector2 position, Vector2 size, Vector2 bounds)
            : base(game, ref texture, position, size, bounds)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw enemy (only if it's located in the screen bounds 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        public override void Draw(GameTime gameTime)
        {
            if (Position.X <= Bounds.Y
                && Position.X >= Bounds.X)
            {

                var sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
                // Draw the enemy

                sBatch.Draw(Texture, Position, null, Color.White, 0,
                    new Vector2(Size.X / 2, Size.Y / 2), 1.0f, SpriteEffects.None, 0f);
                base.Draw(gameTime);
            }
        }

        public override bool Collision(IPlayer player)
        {
            if (player.PlayerRectangle.X >= EnemyRectangle.Left && player.PlayerRectangle.X <= EnemyRectangle.Right
                || player.PlayerRectangle.X + player.PlayerRectangle.Width >= EnemyRectangle.Left && player.PlayerRectangle.X + player.PlayerRectangle.Width <= EnemyRectangle.Right)
            {
                player.StateJumpState = JumpState.NotJumped;
                return false;
            }

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

            return false;
        }
    }
}
