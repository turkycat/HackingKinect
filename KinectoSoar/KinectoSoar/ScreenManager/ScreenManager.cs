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

namespace KinectoSoar.ScreenManager
{
    /// <summary>
    /// Game component for managing screens. This uses a state-machine
    /// pattern for easy addition and removal of screens. Each class is 
    /// responsible for loading its screens based off of user input. This
    /// class mainly just holds the active screen and calls its update
    /// and Draw methods.
    /// </summary>
    public class ScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Class Member Variables

        private SpriteBatch _spriteBatch;
        private Screen _activeScreen;

        #endregion

        #region Main Game Methods and Constructor

        public ScreenManager(Game game)
            : base(game)
        {
            _spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            // Sets initial screen to start screen since that is 
            // the first screen that is present in the game

            // TODO: set first screen
            _activeScreen = new StartScreen(game, _spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            // Gives the active screen a reference to the current active
            // screen so it can change it based off of input
            _activeScreen.Update(ref _activeScreen, gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _activeScreen.Draw();
            base.Draw(gameTime);
        }

        #endregion
    }
}
