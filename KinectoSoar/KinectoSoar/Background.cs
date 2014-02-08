// Background.cs
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KinectoSoar
{
    class Background
    {
        // The image representing the background
        Texture2D texture;

        // An array of positions of the background
        Vector2[] positions;

        // The speed which the background i smovint
        int speed;

        public void Initialize(Texture2D texture, int screenHeight, int speed)
        {
            this.texture = texture;

            this.speed = speed;

            positions = new Vector2[screenHeight / texture.Height + 1];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(0, i * texture.Height);
            }
        }

        public void Update()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].Y += speed;

                // if the speed is moving up
                if (speed <= 0)
                {
                    if (positions[i].Y <= -texture.Height)
                    {
                        positions[i].Y = texture.Height * (positions.Length - 1);
                    }
                }
                else
                {
                    if (positions[i].Y >= texture.Height * (positions.Length - 1))
                    {
                        positions[i].Y = -texture.Height;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }
        }
    }
}
