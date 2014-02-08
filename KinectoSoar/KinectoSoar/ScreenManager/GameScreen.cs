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
        private KeyboardState _prevState;
        private SpriteManager.SpriteManager _spriteManager;


        public GameScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            this._currState = Keyboard.GetState();
            this._prevState = _currState;
            this._spriteManager = new SpriteManager.SpriteManager(game);
            this._spriteManager.AddSprite(new SpriteManager.Background(game, spriteBatch));
            this._spriteManager.AddSprite(new SpriteManager.Bird(game, spriteBatch));
            game.Components.Add(_spriteManager);
        }

        public override void Draw()
        {
            base._spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "game screen", new Vector2(20, 20), Color.White);
        }

        public override void Update(ref Screen activeScreen)
        {
            _currState = Keyboard.GetState();
            if (_currState != _prevState && _currState.IsKeyDown(Keys.Enter))
            {
                activeScreen = new ScreenTwo(_game, _spriteBatch);
            }
            _prevState = _currState;
        }
    }
}
