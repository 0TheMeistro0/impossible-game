using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ImpossibleGame
{
    public class Water : IEnemy
    {
        public Water(Game game, ref Texture2D texture, Vector2 position, Vector2 size, Vector2 bounds)
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
    }
}
