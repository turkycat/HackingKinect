
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

namespace KinectoSoar.SpriteManager
{
    /// <summary>
    /// This is the sprite manager (Component) that is used to hold all sprites
    /// and update and draw them. This uses a customized version of
    /// the observor pattern to pass information along for collisions. 
    /// The other classes are notified if a collision occurs and then
    /// they are responsible to handle it. I did it this way to make it 
    /// more modular and easily expandable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Class Member Variables

        // Holds a list of all active sprites. This is  
        // iterrated through continuously to draw, update,
        // and check for collisions.
        private List<Sprite> _sprites;

        #endregion

        #region Constructor and Game Component Overridden Methods

        public SpriteManager(Game game)
            : base(game)
        {
            _sprites = new List<Sprite>();
        }

        public override void Update(GameTime gameTime)
        {
            if (!GameProperties.Instance.GameOver)
            {
                // As long as the game is not over or paused then process the sprite information,
                // and check and notify classes of collisions.
                foreach (Sprite sprite in _sprites)
                {
                    sprite.Update(gameTime);
                    if (sprite.Name == "Bird")
                    {
                        foreach (Sprite check in _sprites)
                        {
                            if (check.IsColliding(sprite))
                            {
                                check.HandleCollision(sprite);
                            }
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Sprite sprite in _sprites)
            {
                // Calls each sprites draw method
                sprite.Draw();
            }
            base.Draw(gameTime);
        }

        #endregion

        #region Other Public Methods

        // This is used to add a sprite to be monitored and notified to the 
        // active sprites.
        public void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
        }

        #endregion
    }
}
