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
    class Background : Sprite
    {
        private Vector2[] _positions;
        private int _textureHeight;

        //TODO
        //not sure if we'll use this?
        private int speed;

        public Background( Game game, SpriteBatch spriteBatch ) : base( game, spriteBatch )
        {
            this._game = game;
            this.speed = 5;
        }

        public override bool IsColliding(Sprite sprite)
        {
            return false;
        }


        public override void HandleCollision(Sprite sprite)
        {
            return;
        }

        public override void Update(GameTime gameTime)
        {
            if (_positions == null)
            {
                initializePositions();
            }


            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i].Y += speed;

                // if the speed is moving up
                if (speed < 0)
                {
                    if (_positions[i].Y <= -_textureHeight)
                    {
                        _positions[i].Y = _textureHeight * (_positions.Length - 1);
                    }
                }
                else
                {
                    if (_positions[i].Y >= _textureHeight * (_positions.Length - 1))
                    {
                        _positions[i].Y = -_textureHeight + speed;
                    }
                }
            }
        }

        public override void Draw()
        {
            if (_positions == null)
            {
                return;
            }

            Texture2D tex = Resources.Instance.GetTexture("Background");
            for (int i = 0; i < _positions.Length; i++)
            {
                //draw the background at the far plane
                base._spriteBatch.Draw( tex, _positions[i], null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
            }
        }



        private void initializePositions()
        {
            this._textureHeight = Resources.Instance.GetTexture("Background").Height;
            this._positions = new Vector2[_game.GraphicsDevice.Viewport.Height / _textureHeight + 1];

            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i] = new Vector2(0, i * _textureHeight);
            }
        }
    }
}
