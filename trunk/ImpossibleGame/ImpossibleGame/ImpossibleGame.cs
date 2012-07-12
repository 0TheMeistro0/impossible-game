using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private IPlayer _player;

        private const int Speed = 5;
        private const int Width = 800;
        private const int Height = 600;
        private const bool FullScreenMode = false;
        private const int LeftBound = 100;
        private const int RightBound = Width - 100;
        private double _seconds = 0;
        private double _lastSeonds = 0;

        private readonly Color _color = new Color(41, 121, 130);
        private bool _gameStarted = false;

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

            InitGraphicsMode(Width, Height, FullScreenMode);

            var texture = Content.Load<Texture2D>("Player");
            _player = new Player(this, ref texture, new Vector2(200, 300),
                new Vector2(texture.Width, texture.Height));

            var container = new IoCContainer();
            container.Player = _player;

            IoC.Container = container;

            Start();

            Components.Add(IoC.Container.Player);

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
        {

            // Checking for collisions, gathering input, and playing audio.
            // Allows the game to exit
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.Escape))
            {
                this.Exit();
            }

            if (IoC.Container.Map.Colision(IoC.Container.Player))
            {
                Components.Clear();
                Components.Add(IoC.Container.Player);
                Thread.Sleep(2000);
                Start(gameTime.TotalGameTime.TotalSeconds);
                return;
            }


            if (gameTime.TotalGameTime.Milliseconds % 1 == 0)
            {
                IoC.Container.Map.ChangeXPosition(Speed);
            }
            

            _seconds = gameTime.TotalGameTime.TotalSeconds - _lastSeonds;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_color);

            // TODO: Add your drawing code here

            var courierNew = Content.Load<SpriteFont>("SpriteFont1");

            string time = String.Format(CultureInfo.CurrentCulture, "{0:0.00}",  _seconds);

            var fontOrigin = courierNew.MeasureString(time) / 2;
            var fontPos = new Vector2(Width / 2, 50);

            _spriteBatch.Begin();
            _spriteBatch.Draw(Content.Load<Texture2D>("Line"),
                 new Vector2(50, 310), new Rectangle(0, 0, RightBound, 2), Color.White);
            // Draw the string
            _spriteBatch.DrawString(courierNew, time, fontPos, Color.LightGreen, 0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            base.Draw(gameTime);
            _spriteBatch.End();
        }

        private void Start(double seconds = 0)
        {
            var bounds = new Vector2(LeftBound, RightBound);

            IoC.Container.Map = new RandomMap(1000, this, new Vector2(LeftBound, RightBound));
            
            foreach (var enemies in IoC.Container.Map.Enemies)
            {
                Components.Add(enemies);
            }

            _seconds = 0;
            _lastSeonds = seconds;
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
