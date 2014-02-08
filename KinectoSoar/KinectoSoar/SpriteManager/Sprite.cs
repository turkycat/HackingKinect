
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
    /// The is the base class for all sprites within the game. It 
    /// contains some basic information all sprites use and methods 
    /// that are needed for collisions. I used this polymorphic 
    /// architecture here to develop a contract so all sprites could 
    /// be easily updated, drawn, and have collsion checks within a 
    /// simple iteration.
    /// </summary>
    public abstract class Sprite
    {
        #region Class Member Variables

        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Size { get; set; }

        // Holds the game reference mainly so graphics
        // device is easily accessible
        protected Game _game;
        // Uses this reference to main game sprite batch to 
        // prevent issues with multiple sprite batches
        protected SpriteBatch _spriteBatch;

        #endregion

        #region Constructor and Abstract Methods

        public Sprite(Game game, SpriteBatch spriteBatch)
        {
            _game = game;
            _spriteBatch = spriteBatch;
        }

        /// <summary>
        /// Check is a sprite is colliding with this sprite.
        /// </summary>
        /// <param name="sprite">Sprite to check collision against</param>
        /// <returns>True for collion and false otherwise</returns>
        public abstract bool IsColliding(Sprite sprite);

        /// <summary>
        /// Method used to handle collisions. This is where it would update 
        /// itself or the item is collided with.
        /// </summary>
        /// <param name="sprite">Sprite this collided with</param>
        public abstract void HandleCollision(Sprite sprite);

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();

        #endregion
    }
}
