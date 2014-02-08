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
    public class Bird : Sprite
    {
        enum Animate
        {
            NONE,
            Flap,
            Left,
            Right
        };

        private List<SpriteReader.SpriteInfo> _birdInfo;
        private Animate _animate = Animate.NONE;
        private int _frameSpeed = 75;
        private int _lastFrame;
        private int _frameIndex = 0;
        private int _timer = 0;
        private int _delay = 0;
        private int _animateTime = 100;
        private float velocity = 0;

        private const int WIDTH = 200;
        private const int HEIGHT = 100;

        public Bird(Game game, SpriteBatch spriteBatch) : base( game, spriteBatch )
        {
            Name = "Bird";
            this._birdInfo = new List<SpriteReader.SpriteInfo>();
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird1"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird2"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird3"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird4"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird5"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird6"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird7"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("Bird8"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("left"));
            _birdInfo.Add(Resources.Instance.GetSpriteInfo("right"));
            this.Position = new Vector2((game.GraphicsDevice.Viewport.Width) / 2f, game.GraphicsDevice.Viewport.Height / 2  );
            _timer = _animateTime;
            velocity = 0;
            
        }
        
        public override bool IsColliding(Sprite sprite)
        {
            //TODO
            return false;
        }

        public override void HandleCollision(Sprite sprite)
        {
            
            //TODO
            return;
        }               

        public void MoveDown()
        {
            if (!Resources.Instance.GameOver)
            {
                Position = new Vector2(Position.X, Position.Y + 2);
            }
        }

        public void MoveLeft(float speed)
        {
            if (!Resources.Instance.GameOver)
            {
                float x = MathHelper.Clamp(Position.X - speed, Resources.Instance.BorderDensity, _game.GraphicsDevice.Viewport.Width - Resources.Instance.BorderDensity);
                Position = new Vector2(x, Position.Y + 1);
                _animate = Animate.Left;
                _delay = _animateTime;
            }
        }

        public void MoveRight(float speed)
        {
            if (!Resources.Instance.GameOver)
            {
                float x = MathHelper.Clamp(Position.X + speed, Resources.Instance.BorderDensity, _game.GraphicsDevice.Viewport.Width - Resources.Instance.BorderDensity);
                Position = new Vector2(x, Position.Y + 1);
                _animate = Animate.Right;
                _delay = _animateTime;
            }
        }

        public void MoveUp(float speed)
        {
            if (!Resources.Instance.GameOver)
            {
                velocity -= speed;
                _animate = Animate.Flap;
                _timer = _animateTime;
            }
        }

        public override void Update(GameTime gameTime)
        {
            int elapsedMillis = gameTime.ElapsedGameTime.Milliseconds;
            _lastFrame += elapsedMillis;
            _timer -= elapsedMillis;
            _delay -= elapsedMillis;
            if ( _animate == Animate.Flap )
            {
                if (_lastFrame > _frameSpeed && (_frameIndex < _birdInfo.Count - 2))
                {
                    _lastFrame = 0;
                    _frameIndex++;
                    if (_frameIndex >= _birdInfo.Count - 2)     //dont include banking images in animation loop
                    {
                        _frameIndex = 0;
                    }
                }
            }
            else if (_animate == Animate.Left)
            {
                if (_delay < 0 )
                {
                    _frameIndex = 0;
                    _animate = Animate.NONE;
                }
                else
                    _frameIndex = 8;
            }
            else if (_animate == Animate.Right )
            {
                if (_delay < 0 )
                {
                    _frameIndex = 0;
                    _animate = Animate.NONE;
                }
                else
                    _frameIndex = 9;
            }
            else
            {
                _frameIndex = 0;
            }
            
            //decrease velocity ( remember: it's negative to move up! )
            if (_timer < 0)
            {
                velocity = velocity * 0.9f;
                if( velocity > -0.05 )_animate = Animate.NONE;
                _timer += _animateTime;
            }

            CheckBottomBorder();
            velocity = MathHelper.Clamp(velocity, -10, 2);
            float y = MathHelper.Clamp(Position.Y + velocity + 4, HEIGHT / 2, _game.GraphicsDevice.Viewport.Height * 2);
            Position = new Vector2(Position.X, y);
        }


        public override void Draw()
        {
            Rectangle dest = new Rectangle((int)Position.X - WIDTH / 2, (int)Position.Y - HEIGHT / 2, WIDTH, HEIGHT);
            Rectangle source = _birdInfo[_frameIndex].Position;
            base._spriteBatch.Draw(Resources.Instance.GetTexture("BirdSprite"), dest, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        private void CheckBottomBorder()
        {
            if (Position.Y - HEIGHT / 2 > _game.GraphicsDevice.Viewport.Height)
            {
                Resources.Instance.GameOver = true;
                Resources.Instance.Reset = false;
                Position = new Vector2(0, -5 * HEIGHT);
                velocity = 0;
            }
        }
    }
}
