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
        private SoundEffectInstance _backgroundMusic;

        private int _score;
        private int _lastFrame;
        private int _time = 1000;

        public GameScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            _score = 0;

            Resources.Instance.GameOver = false;
            this._spriteManager = new SpriteManager.SpriteManager(game);
            this._keyboard = new Controllers.SoarKeyboard(game);

            SpriteManager.Bird bird = new SpriteManager.Bird(game, spriteBatch);
            Resources.Instance.setBird(bird);

            this._spriteManager.AddSprite(new SpriteManager.Background(game, spriteBatch));
            this._spriteManager.AddSprite(new SpriteManager.BlackBird(game, spriteBatch));
            this._spriteManager.AddSprite(new SpriteManager.BlackBird(game, spriteBatch));
            this._spriteManager.AddSprite(new SpriteManager.BlackBird(game, spriteBatch));
            this._spriteManager.AddSprite(new SpriteManager.Pond(game, spriteBatch));
            this._spriteManager.AddSprite(bird);

            game.Components.Add(_spriteManager);
            game.Components.Add(_keyboard);
        }

        public override void Draw()
        {
            if (Resources.Instance.GameOver)
            {
                Vector2 fontDimensions = Resources.Instance.GetFont("Cooper").MeasureString("Game Over") * 0.5f;
                Vector2 screenSize = new Vector2(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
                _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "Game Over", new Vector2(screenSize.X / 2 - fontDimensions.X, screenSize.Y * 0.3f - fontDimensions.Y), Color.Yellow);
            }
            Vector2 scoreSize = Resources.Instance.GetFont("Cooper").MeasureString("Score: " + _score.ToString());
            _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "Score: " + _score.ToString(), new Vector2((_game.GraphicsDevice.Viewport.Width - scoreSize.X) / 2, 10), Color.Yellow);
        }

        public override void Update(ref Screen activeScreen, GameTime gameTime)
        {
            if (!Resources.Instance.GameOver)
            {
                _lastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (_lastFrame > _time)
                {
                    _score += 1;
                    _lastFrame = 0;
                }
            }
            else
            {
                if (_score > Resources.Instance.HighScore)
                {
                    Resources.Instance.HighScore = _score;
                }
            }

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
                _game.Components.Remove(_keyboard);
                _game.Components.Remove(_spriteManager);
                _backgroundMusic.Stop();
                Resources.Instance.GameOver = false;
                activeScreen = new StartScreen(_game, _spriteBatch);
            }
            if (Resources.Instance.GameOver && Resources.Instance.Reset)
            {
                Resources.Instance.Reset = false;
                _game.Components.Remove(_keyboard);
                _game.Components.Remove(_spriteManager);
                _backgroundMusic.Stop();
                Resources.Instance.GameOver = false;
                activeScreen = new StartScreen(_game, _spriteBatch);
            }
        }
    }
}
