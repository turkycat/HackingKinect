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

namespace KinectoSoar
{
    using Microsoft.Kinect;
    using System.IO;
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Soar : Microsoft.Xna.Framework.Game
    {
        #region Class Member Variables

        public static float Height;
        public static float Width;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ScreenManager.ScreenManager _screenManager;
        private Controllers.SoarKinect _kinect;

        #endregion

        #region constructor
        public Soar()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //set window preferences
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 900;

            Resources.Instance.SetSpriteReader(new SpriteReader.SpriteReader(Content.RootDirectory + @"\", "BirdSprite.xml"));
            Resources.Instance.SetSpriteBlackReader(new SpriteReader.SpriteReader(Content.RootDirectory + @"\", "BlackBirdSprite.xml"));


            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            // Add the screen manager component used to control the 
            // flow of screens throughout the game.
            _screenManager = new ScreenManager.ScreenManager(this);
            _kinect = new Controllers.SoarKinect(this);
            this.Components.Add(_screenManager);
            this.Components.Add(_kinect);

            base.Initialize();
        }

        #endregion

        #region Load/Unload Content
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Resources.Instance.AddFont("Cooper", Content.Load<SpriteFont>("Cooper"));
            Resources.Instance.AddFont("TitleFont", Content.Load<SpriteFont>("TitleFont"));
            Resources.Instance.AddTexture("Background", Content.Load<Texture2D>(@"Sprites/Background"));
            Resources.Instance.AddTexture("BirdSprite", Content.Load<Texture2D>(@"Sprites/BirdSprite"));
            Resources.Instance.AddTexture("BlackBirdSprite", Content.Load<Texture2D>(@"Sprites/BlackBirdSprite"));
            Resources.Instance.AddTexture("Water", Content.Load<Texture2D>(@"Sprites/Water"));
            Resources.Instance.AddTexture("Fish", Content.Load<Texture2D>(@"Sprites/Fish"));
            Resources.Instance.AddTexture("Ready", Content.Load<Texture2D>(@"Sprites/Ready"));

            Resources.Instance.AddSound("EagleCry", Content.Load<SoundEffect>(@"Sounds/EagleCry"));
            Resources.Instance.AddSound("EagleCollide", Content.Load<SoundEffect>(@"Sounds/EagleCollide"));
            Resources.Instance.AddSound("EagleEat", Content.Load<SoundEffect>(@"Sounds/EagleEat"));
            Resources.Instance.AddSound("EagleFlap", Content.Load<SoundEffect>(@"Sounds/EagleFlap"));
            Resources.Instance.AddSound("EagleTurn", Content.Load<SoundEffect>(@"Sounds/EagleTurn"));
            Resources.Instance.AddSound("RockMusic", Content.Load<SoundEffect>(@"Sounds/RockMusic"));
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
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            base.Draw(gameTime);
            spriteBatch.End();

        }

        #endregion

        #region helper methods



        #endregion
    }
}
