using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ImpossibleGame
{
    /// <summary>
    /// Jump Statements
    /// </summary>
    public enum JumpState { NotJumped, InProgressGrow, InHighest, InProgressLoose };

    /// <summary>
    /// This is the player class
    /// </summary>
    public class Player : IPlayer
    {
        private const float RotationAngle = (float)-22.5;
        private const int JumpPixels = 8;
        private const float RoundingDimension = MathHelper.Pi * 2;
        private const int MilisecondsToChangeJumpState = 50;

        private Game _game;
        private int _milisecondsPassed = 0;
        private double _miliseconds = 0;
        private int _jumpStatesCounter = 0;
        private bool _finishedJumping = false;

        /// <summary>
        /// Current angle of player (when jumping)
        /// </summary>
        public float Angle { get; private set; }

        /// <summary>
        /// Create new instance of Player
        /// </summary>
        /// <param name="game">Current game</param>
        /// <param name="texture">Player image</param>
        /// <param name="position">Start position</param>
        /// <param name="size">Player's size</param>
        public Player(Game game, ref Texture2D texture, Vector2 position, Vector2 size)
            : base(game, ref texture, position, size)
        {
            _game = game;
            Angle = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            _milisecondsPassed = (int)(gameTime.TotalGameTime.TotalMilliseconds - _miliseconds);

            if (StateJumpState != JumpState.NotJumped
                && _milisecondsPassed > MilisecondsToChangeJumpState)
            {
                if (_jumpStatesCounter < 4 && !_finishedJumping && Angle == 0
                    && StateJumpState == JumpState.InProgressGrow)
                {
                    Position = new Vector2(Position.X, Position.Y - JumpPixels);
                    _jumpStatesCounter++;
                    StateJumpState = JumpState.InProgressGrow;
                }

                else if (Angle >= -45 && !_finishedJumping
                    && StateJumpState == JumpState.InProgressGrow)
                {
                    Position = new Vector2(Position.X, Position.Y - JumpPixels);
                    _jumpStatesCounter = 0;
                    Angle += RotationAngle;
                    if (Angle == -45) StateJumpState = JumpState.InProgressLoose;
                }

                else if (_jumpStatesCounter < 3 && !_finishedJumping && Angle == -90
                    && StateJumpState == JumpState.InProgressLoose)
                {
                    _finishedJumping = true;
                    Position = new Vector2(Position.X, Position.Y + JumpPixels);
                    Angle = 0;
                }


                else if (Angle >= -90 && !_finishedJumping
                    && StateJumpState == JumpState.InProgressLoose)
                {
                    Position = new Vector2(Position.X, Position.Y + JumpPixels);
                    _jumpStatesCounter = 0;
                    Angle += RotationAngle;
                }

                else if (_jumpStatesCounter < 3 && _finishedJumping)
                {
                    Position = new Vector2(Position.X, Position.Y + JumpPixels);
                    _jumpStatesCounter++;
                }

                if (_jumpStatesCounter == 3 && _finishedJumping)
                {
                    StateJumpState = JumpState.NotJumped;
                    //Position = new Vector2(Position.X, Position.Y + JumpPixels);
                    _jumpStatesCounter = 0;
                }

                _miliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space)
                && StateJumpState == JumpState.NotJumped)
            {
                StateJumpState = JumpState.InProgressGrow;
                _miliseconds = gameTime.TotalGameTime.TotalMilliseconds;
                _finishedJumping = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Position.X >= Game.Window.ClientBounds.Width)
            {
                Position = new Vector2(30, Position.Y);
            }

            var sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            // Draw the player

            sBatch.Draw(Texture, Position, null, Color.White, Angle % (MathHelper.Pi * 2),
                new Vector2(Size.X / 2, Size.Y / 2), 1.0f, SpriteEffects.None, 0f);
            base.Draw(gameTime);
        }
    }
}
