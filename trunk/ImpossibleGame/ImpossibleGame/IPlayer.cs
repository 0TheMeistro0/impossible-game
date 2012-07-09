using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ImpossibleGame
{
    /// <summary>
    /// This is the player interface
    /// </summary>
    public class IPlayer : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /// <summary>
        /// Here contains sprite
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Here contains position of player
        /// </summary>
        /// 
        public Vector2 Position { get; set; }

        /// <summary>
        /// Here contains size of player
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Gets rectangle where player is located
        /// </summary>
        public Rectangle PlayerRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,
                (int)(Size.X), (int)(Size.Y));
            }
        }
        
        public JumpState StateJumpState = JumpState.NotJumped;

        /// <summary>
        /// Create new instance of IPlayer
        /// </summary>
        /// <param name="game">Current game</param>
        /// <param name="texture">Player image</param>
        /// <param name="position">Start position</param>
        /// <param name="size">Player's size</param>
        protected IPlayer(Game game, ref Texture2D texture, Vector2 position, Vector2 size)
            : base(game)
        {
            Texture = texture;
            Position = position;
            Size = size;
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
