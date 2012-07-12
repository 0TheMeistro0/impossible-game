using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ImpossibleGame
{
    public class Triangle : IEnemy
    {

        private delegate int Line(int x, int y);

        /// <summary>
        /// Creates the new instance of TriangleEnemy
        /// </summary>
        /// <param name="game">Current game</param>
        /// <param name="texture">Image of enemy</param>
        /// <param name="position">Start position of enemy</param>
        /// <param name="size">Size of enemy</param>
        /// <param name="bounds">Left and right bounds, in which enemy will be drawn</param>
        public Triangle (Game game, ref Texture2D texture, Vector2 position, Vector2 size, Vector2 bounds)
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

        public override bool Collision(IPlayer player)
        {
            //var highestPoint = new Vector2(Position.X + Texture.Width/2, Position.Y + Texture.Width);
            //var leftPoint = new Vector2(Position.X, Position.Y);
            //var rightPoint = new Vector2(Position.X + Texture.Width, Position.Y);

            //if (player.Position.Y >= highestPoint.Y && player.Position.Y + Texture.Height >= highestPoint.Y)
            //{
            //    return false;
            //}
            
            //Line vLine = (int x, int y) => (int)((leftPoint.Y - highestPoint.Y)*x - (leftPoint.X - highestPoint.X)*y +
            //                                     (leftPoint.X*highestPoint.Y - highestPoint.X*leftPoint.Y));

            //if (vLine((int)player.Position.X, (int)player.Position.Y) < 0) return false;
            ////if (vLine((int)player.Position.X + (int)player.Texture.Width, (int)player.Position.Y) < 0) return false;
            //if (vLine((int)player.Position.X + (int)player.Texture.Width, (int)player.Position.Y + (int)player.Texture.Height) < 0) return false;
            ////if (vLine((int)player.Position.X, (int)player.Position.Y + (int)player.Texture.Height) < 0) return false;

            //vLine = (int x, int y) => (int)((rightPoint.Y - highestPoint.Y) * x - (rightPoint.X - rightPoint.X) * y +
            //                                     (rightPoint.X * highestPoint.Y - highestPoint.X * rightPoint.Y));

            ////if (vLine((int)player.Position.X, (int)player.Position.Y) < 0) return false;
            //if (vLine((int)player.Position.X + (int)player.Texture.Width, (int)player.Position.Y) < 0) return false;
            ////if (vLine((int)player.Position.X + (int)player.Texture.Width, (int)player.Position.Y + (int)player.Texture.Height) < 0) return false;
            //if (vLine((int)player.Position.X, (int)player.Position.Y + (int)player.Texture.Height) < 0) return false;


            //return true;

            return base.Collision(player);
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
    }
}
