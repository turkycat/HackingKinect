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
        private int speed;
        private int _yOne;
        private int _yTwo;

        public Background( Game game, SpriteBatch spriteBatch ) : base( game, spriteBatch )
        {
            GameProperties.Instance.BorderDensity = (game.GraphicsDevice.Viewport.Width / (90f * _game.GraphicsDevice.Viewport.Width / 900)) * 7.5f;
            this._game = game;
            this.speed = 4;
            _yOne = 0;
            _yTwo = -_game.GraphicsDevice.Viewport.Height;
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
            _yOne += speed;
            _yTwo += speed;
            if (_yOne >= _game.GraphicsDevice.Viewport.Height)
                _yOne = -_game.GraphicsDevice.Viewport.Height;
            if (_yTwo >= _game.GraphicsDevice.Viewport.Height)
                _yTwo = -_game.GraphicsDevice.Viewport.Height;
        }

        public override void Draw()
        {
            Rectangle _drawRect1 = new Rectangle(0, _yOne, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
            Rectangle _drawRect2 = new Rectangle(0, _yTwo, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
            _spriteBatch.Draw(Resources.Instance.GetTexture("Background"), _drawRect1, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(Resources.Instance.GetTexture("Background"), _drawRect2, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
