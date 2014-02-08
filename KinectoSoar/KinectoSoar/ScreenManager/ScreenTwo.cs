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
    class ScreenTwo : Screen
    {
        private KeyboardState _currState;
        private KeyboardState _prevState;

        public ScreenTwo(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            _currState = Keyboard.GetState();
            _prevState = _currState;
        }

        public override void Draw()
        {
            base._spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "screen two", new Vector2(20, 20), Color.White);
        }

        public override void Update(ref Screen activeScreen)
        {
            _currState = Keyboard.GetState();
            if (_currState != _prevState && _currState.IsKeyDown(Keys.Enter))
            {
                activeScreen = new GameScreen(_game, _spriteBatch);
            }
            _prevState = _currState;
        }
    }
}
