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
    class GameScreen : Screen
    {
        private KeyboardState _currState;
        private SpriteManager.SpriteManager _spriteManager;
        private Controllers.SoarKeyboard _keyboard;
        private Controllers.SoarKinect _kinect;
        private SoundEffectInstance _backgroundMusic;

        public GameScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            this._spriteManager = new SpriteManager.SpriteManager(game);
            this._keyboard = new Controllers.SoarKeyboard(game);
            this._kinect = new Controllers.SoarKinect(game);

            SpriteManager.Bird bird = new SpriteManager.Bird(game, spriteBatch);
            Resources.Instance.setBird(bird);

            this._spriteManager.AddSprite(new SpriteManager.Background(game, spriteBatch));
            this._spriteManager.AddSprite(bird);

            game.Components.Add(_spriteManager);
            game.Components.Add(_keyboard);
            game.Components.Add(_kinect);
        }

        public override void Draw()
        {
            base._spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "game screen", new Vector2(20, 20), Color.White);
            if (Resources.Instance.GameOver)
            {
                Vector2 fontDimensions = Resources.Instance.GetFont("Cooper").MeasureString("Game Over") * 0.5f;
                Vector2 screenSize = new Vector2(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
                _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "Game Over", new Vector2(screenSize.X / 2 - fontDimensions.X, screenSize.Y * 0.3f - fontDimensions.Y), Color.Yellow);
            }
        }

        public override void Update(ref Screen activeScreen)
        {
            if (_backgroundMusic == null)
            {
                _backgroundMusic = Resources.Instance.GetSound("RockMusic").CreateInstance();
                _backgroundMusic.IsLooped = true;
                _backgroundMusic.Volume = 0.5f;
                _backgroundMusic.Play();
            }
            _currState = Keyboard.GetState();
            if (Resources.Instance.GameOver && _currState.IsKeyDown(Keys.Enter))
            {
                _game.Components.Remove(_kinect);
                _game.Components.Remove(_keyboard);
                _game.Components.Remove(_spriteManager);
                _backgroundMusic.Stop();
                Resources.Instance.GameOver = false;
                activeScreen = new StartScreen(_game, _spriteBatch);
            }
        }
    }
}
