﻿using System;
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
    class Bird : Sprite
    {
        private List<SpriteReader.SpriteInfo> _birdInfo;
        private int _frameSpeed = 75;
        private int _lastFrame;
        private int _frameIndex = 0;

        private const int WIDTH = 200;
        private const int HEIGHT = 100;

        public Bird(Game game, SpriteBatch spriteBatch) : base( game, spriteBatch )
        {
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
            this.Position = new Vector2((game.GraphicsDevice.Viewport.Width - WIDTH) / 2f, game.GraphicsDevice.Viewport.Height - HEIGHT );
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

        public override void Update(GameTime gameTime)
        {
            _lastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_lastFrame > _frameSpeed)
            {
                _lastFrame = 0;
                _frameIndex++;
                if (_frameIndex >= _birdInfo.Count - 2)     //dont include banking images in animation loop
                {
                    _frameIndex = 0;
                }
            }
        }


        public override void Draw()
        {
            Rectangle dest = new Rectangle((int)Position.X - WIDTH / 2, (int)Position.Y - HEIGHT / 2, WIDTH, HEIGHT);
            Rectangle source = _birdInfo[_frameIndex].Position;
            base._spriteBatch.Draw(Resources.Instance.GetTexture( "BirdSprite" ), dest, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
        

    }
}