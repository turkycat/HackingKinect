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
    public class StartScreen : Screen
    {
        private KeyboardState _currentState;
        private KeyboardState _prevState;

        public StartScreen(Game game, SpriteBatch sprite)
            : base(game, sprite)
        {
            _currentState = Keyboard.GetState();
            _prevState = _currentState;
        }

        public override void Update(ref Screen activeScreen)
        {
            _currentState = Keyboard.GetState();
            if (_currentState != _prevState && _currentState.IsKeyDown(Keys.Enter))
            {
                activeScreen = new GameScreen(_game, _spriteBatch);
            }
            _prevState = _currentState;
        }
        public override void Draw()
        {
            Vector2 fontDimensions = Resources.Instance.GetFont("TitleFont").MeasureString("Soar") * 0.5f;
            Vector2 screenSize = new Vector2(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
            _spriteBatch.DrawString(Resources.Instance.GetFont("TitleFont"), "Soar", new Vector2(screenSize.X / 2 - fontDimensions.X, screenSize.Y * 0.3f - fontDimensions.Y), Color.Black);

            Rectangle background = new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y);
            _spriteBatch.Draw(Resources.Instance.GetTexture("Background"), background, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);
        }

    }
}
