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
    class Pond : Sprite
    {
        private Vector2 fish;
        private int _speed;
        private bool _empty;

        private int WIDTH = 335;
        private int HEIGHT = 200;
        private int fishWidth = 64;
        private int fishHeight = 60;
        
        public Pond(Game game, SpriteBatch spriteBatch) : base( game, spriteBatch )
        {
            this._speed = 4;
            Set();
        }



        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X, Position.Y + _speed);
            fish = new Vector2(fish.X, fish.Y + _speed);
            if (Position.Y > _game.GraphicsDevice.Viewport.Height)
            {
                Set();
            }
        }


        public void randomizePosition()
        {
            int x = Resources.Instance.Rand.Next((int)GameProperties.Instance.BorderDensity, (int)(_game.GraphicsDevice.Viewport.Width - WIDTH - GameProperties.Instance.BorderDensity));
            int y = -Resources.Instance.Rand.Next(HEIGHT * 2, 3000);
            Position = new Vector2(x, y);
        }



        public override bool IsColliding(Sprite sprite)
        {
            Vector2 fishMid = new Vector2(fish.X + (fishWidth / 2f), fish.Y + (fishHeight / 2f));
            float distance = Vector2.Distance(sprite.Position, fishMid);
            if (!_empty && distance <= 100 && GameProperties.Instance.Screech)
            {
                return true;
            }
            return false;
        }

        public override void HandleCollision(Sprite sprite)
        {
            _empty = true;
            GameProperties.Instance.Multiplier += 1;
            GameProperties.Instance.Score += (15 * GameProperties.Instance.Multiplier);
            return;
        }


        public void Set()
        {
            randomizePosition();
            if (Resources.Instance.Rand.Next(2) == 0)
            {
                _empty = true;
            }
            else
            {
                _empty = false;
                fish = new Vector2(Position.X + ( ( WIDTH - fishWidth ) / 2f ), Position.Y + ((HEIGHT - fishHeight) / 2f));
            }
        }

        public override void Draw()
        {
            _spriteBatch.Draw(Resources.Instance.GetTexture("Water"), Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .9f);
            if (!_empty)
            {
                _spriteBatch.Draw(Resources.Instance.GetTexture("Fish"), fish, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .8f);
            }
        }
    }
}
