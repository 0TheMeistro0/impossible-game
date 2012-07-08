using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This is the main type for your game
    /// </summary>
    public class ImpossibleGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private IPlayer _player;

        private const int _speed = 5;
        private const int LeftBound = 100;
        private const int RightBound = 500;

        private readonly Color _color = new Color(41, 121, 130);
        private bool _gameStarted = false;

        private IEnemy Enemy1, Enemy2, Enemy3;

        public ImpossibleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            InitGraphicsMode(600, 400, false);

            var bounds = new Vector2(LeftBound, RightBound);

            var texture = Content.Load<Texture2D>("Player");
            _player = new Player(this, ref texture, new Vector2(200, 300),
                new Vector2(texture.Width, texture.Height), _speed);

            var container = new IoCContainer();
            container.Player = _player;

            IoC.Container = container;

            texture = Content.Load<Texture2D>("Water");
            Enemy1 = new Water(this, ref texture,
                new Vector2(1000, 320), new Vector2(20, 20), bounds);
            Enemy2 = new Water(this, ref texture,
                new Vector2(1020, 320), new Vector2(20, 20), bounds);
            texture = Content.Load<Texture2D>("Triangle");
            Enemy3 = new Triangle(this, ref texture,
                new Vector2(1200, 300), new Vector2(20, 20), bounds);

            Components.Add(IoC.Container.Player);
            Components.Add(Enemy1);
            Components.Add(Enemy2);
            Components.Add(Enemy3);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        /// checking for collisions, gathering input, and playing audio.
        {
            // Allows the game to exit
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.Escape)
                || Enemy1.Collision(IoC.Container.Player)
                || Enemy2.Collision(IoC.Container.Player)
                || Enemy3.Collision(IoC.Container.Player))
            {
                this.Exit();
            }
            
            if (Enemy1.Position.X <= 0) 
            {
                Enemy1.Position = new Vector2(1000, Enemy1.Position.Y);
            }
            if (Enemy2.Position.X <= 0)
            {
                Enemy2.Position = new Vector2(1000, Enemy2.Position.Y);
            }
            if (Enemy3.Position.X <= 0)
            {
                Enemy3.Position = new Vector2(1000, Enemy3.Position.Y);
            }
            
                        
            if (gameTime.TotalGameTime.Milliseconds % 1 == 0)
            {
                Enemy1.Position = new Vector2(
                    Enemy1.Position.X - _speed,
                    Enemy1.Position.Y);
                Enemy2.Position = new Vector2(
                    Enemy2.Position.X - _speed,
                    Enemy2.Position.Y);
                Enemy3.Position = new Vector2(
                    Enemy3.Position.X - _speed,
                    Enemy3.Position.Y);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(41, 121, 130));

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(Content.Load<Texture2D>("Line"),
                 new Vector2(50, 310), null, Color.White);
            base.Draw(gameTime);
            _spriteBatch.End();
        }

        /// <summary>
        /// Attempt to set the display mode to the desired resolution.  Itterates through the display
        /// capabilities of the default graphics adapter to determine if the graphics adapter supports the
        /// requested resolution.  If so, the resolution is set and the function returns true.  If not,
        /// no change is made and the function returns false.
        /// </summary>
        /// <param name="iWidth">Desired screen width.</param>
        /// <param name="iHeight">Desired screen height.</param>
        /// <param name="bFullScreen">True if you wish to go to Full Screen, false for Windowed Mode.</param>
        private bool InitGraphicsMode(int iWidth, int iHeight, bool bFullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (bFullScreen == false)
            {
                if ((iWidth <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (iHeight <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    _graphics.PreferredBackBufferWidth = iWidth;
                    _graphics.PreferredBackBufferHeight = iHeight;
                    _graphics.IsFullScreen = bFullScreen;
                    _graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == iWidth) && (dm.Height == iHeight))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        _graphics.PreferredBackBufferWidth = iWidth;
                        _graphics.PreferredBackBufferHeight = iHeight;
                        _graphics.IsFullScreen = bFullScreen;
                        _graphics.ApplyChanges();
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
