
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
    /// This class is used as the base class for screens. I used 
    /// this OOP style architecture here so it could be 
    /// a simple polymorphic call to create screens. I also wanted to
    /// make sure update and draw were made for each screen and easily
    /// called by the Screen manager state machine.
    /// </summary>
    public abstract class Screen
    {
        #region Class Member Variables

        // A reference to the game to get information such as the graphics device
        protected Game _game;
        // A reference to the sprite batch so do not have deal with problems
        // of using multiple
        protected SpriteBatch _spriteBatch;

        #endregion

        #region Constructor and Abstract methods

        // Should be pretty self explanatory

        public Screen(Game game, SpriteBatch spriteBatch)
        {
            this._game = game;
            this._spriteBatch = spriteBatch;
        }

        public abstract void Draw();

        // Passes reference so can change the screen based
        // off user input.
        public abstract void Update(ref Screen activeScreen, GameTime gameTime);

        #endregion
    }
}
