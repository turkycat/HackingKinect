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

namespace KinectoSoar.SpriteManager
{
    public class BlackBird : Sprite
    {
        private List<SpriteReader.SpriteInfo> _birdInfo;
        private int _frameSpeed = 75;
        private int _lastFrame;
        private int _frameIndex = 0;

        private const int WIDTH = 300;
        private const int HEIGHT = 150;

        private int _lastWaitFrame = 0;
        private int _waitTime;
        private bool _wait;

        private Rectangle tempBird;

        public BlackBird(Game game, SpriteBatch spriteBatch) : base( game, spriteBatch )
        {
            tempBird = new Rectangle(0, 0, 0, 0);

            this._birdInfo = new List<SpriteReader.SpriteInfo>();
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird1"));
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird2"));
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird3"));
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird4"));
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird5"));
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird6"));
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird7"));
            _birdInfo.Add(Resources.Instance.GetSpriteBlackInfo("Bird8"));
            this.Position = new Vector2((game.GraphicsDevice.Viewport.Width - WIDTH) / 2f, (game.GraphicsDevice.Viewport.Height - HEIGHT) / 2f);
            _waitTime = Resources.Instance.Rand.Next(1000, 6000);
            _wait = true;
            SetRandomPosition();
        }
        
        public override bool IsColliding(Sprite sprite)
        {
            Rectangle thisBird = new Rectangle((int)Position.X - WIDTH / 4, (int)Position.Y - HEIGHT / 2 + 55, WIDTH / 2, HEIGHT - 110);
            Rectangle bird = new Rectangle((int)(sprite.Position.X - 50), (int)(sprite.Position.Y - 15 ), 100, 30);
            tempBird = bird;
            if (thisBird.Intersects(bird))
                return true;
            return false;
        }

        public override void HandleCollision(Sprite sprite)
        {
            GameProperties.Instance.Reset = false;
            GameProperties.Instance.Start = false;
            GameProperties.Instance.GameOver = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!_wait)
            {
                _lastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (_lastFrame > _frameSpeed)
                {
                    _lastFrame = 0;
                    _frameIndex++;
                    if (_frameIndex >= _birdInfo.Count)
                    {
                        _frameIndex = 0;
                    }
                }
                Position = new Vector2(Position.X, Position.Y + 2);
                if (Position.Y > _game.GraphicsDevice.Viewport.Height + HEIGHT)
                {
                    GameProperties.Instance.Score += (5 * GameProperties.Instance.Multiplier);
                    SetRandomPosition();
                }
            }
            else
            {
                _lastWaitFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (_lastWaitFrame > _waitTime)
                {
                    _wait = false;
                    _lastWaitFrame = 0;
                }
            }
        }

        public override void Draw()
        {
            Rectangle dest = new Rectangle((int)Position.X - WIDTH / 2, (int)Position.Y - HEIGHT / 2, WIDTH, HEIGHT);
            Rectangle source = _birdInfo[_frameIndex].Position;
            base._spriteBatch.Draw(Resources.Instance.GetTexture( "BlackBirdSprite" ), dest, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
            //DrawRect(new Rectangle((int)Position.X - WIDTH / 4, (int)Position.Y - HEIGHT / 2 + 55, WIDTH / 2, HEIGHT - 110));
            //DrawRect(tempBird);
        }

        private void SetRandomPosition()
        {
            int screenWidth = _game.GraphicsDevice.Viewport.Width;
            int x = Resources.Instance.Rand.Next(screenWidth / 4, screenWidth / 4 * 3);
            _waitTime = Resources.Instance.Rand.Next(1000, 6000);
            Position = new Vector2(x, -1 * HEIGHT);
            _wait = true;
        }

        private void DrawRect(Rectangle rect)
        {
            Texture2D line = Resources.Instance.GetTexture("LineTexture");
            _spriteBatch.Draw(line, new Rectangle(rect.X, rect.Y, rect.Width, 5), Color.White); // top line
            _spriteBatch.Draw(line, new Rectangle(rect.X, rect.Y, 5, rect.Height), Color.White); // left line
            _spriteBatch.Draw(line, new Rectangle(rect.X, rect.Height - 5 + rect.Y, rect.Width, 5), Color.White); // bottom line
            _spriteBatch.Draw(line, new Rectangle(rect.Width - 5 + rect.X, rect.Y, 5, rect.Height), Color.White); // right line
        }
    }
}
