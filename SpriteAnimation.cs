using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class SpriteAnimation
    {
        private float timer = 0;
        private float threshold;
        private Rectangle[] sourceRectangles;
        private int animationIndex = 2;

        public SpriteAnimation(float newThreshold, Rectangle[] newSourceRectangles)
        {
            threshold = newThreshold;
            sourceRectangles = newSourceRectangles;
        }

        public void setSourceRects(Rectangle[] newSourceRects)
        {
            sourceRectangles = newSourceRects;
        }

        public Rectangle[] SourceRectangles
        {
            get { return sourceRectangles; }
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > threshold)
            {
                timer -= threshold;
                if (animationIndex < sourceRectangles.Length - 1)
                {
                    animationIndex++;
                }
                else
                {
                    animationIndex = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet, Vector2 position)
        {
            spriteSheet.drawSprite(spriteBatch, sourceRectangles[animationIndex], position);
        }
    }
}
