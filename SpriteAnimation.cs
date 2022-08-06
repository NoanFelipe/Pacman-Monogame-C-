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
        private int animationIndex = 0;
        private bool isLooped = true;
        private bool isPlaying;

        public int AnimationIndex
        {
            get { return animationIndex; }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set { isPlaying = value; }
        }

        public SpriteAnimation(float newThreshold, Rectangle[] newSourceRectangles)
        {
            threshold = newThreshold;
            sourceRectangles = newSourceRectangles;
            isPlaying = true;
        }

        public SpriteAnimation(float newThreshold, Rectangle[] newSourceRectangles, int startingAnimIndex)
        {
            threshold = newThreshold;
            sourceRectangles = newSourceRectangles;
            animationIndex = startingAnimIndex;
            isPlaying = true;
        }

        public SpriteAnimation(float newThreshold, Rectangle[] newSourceRectangles, int startingAnimIndex, bool newIsLooped, bool newIsPlaying)
        {
            threshold = newThreshold;
            sourceRectangles = newSourceRectangles;
            animationIndex = startingAnimIndex;
            isLooped = newIsLooped;
            isPlaying = newIsPlaying;
        }

        public void setAnimIndex (int newAnimIndex)
        {
            animationIndex = newAnimIndex;
        }

        public void setSourceRects(Rectangle[] newSourceRects)
        {
            if (newSourceRects.Length != sourceRectangles.Length)
                animationIndex = 0;
            sourceRectangles = newSourceRects;
        }

        public void start()
        {
            isPlaying = true;
            animationIndex = 0;
        }

        public Rectangle[] SourceRectangles
        {
            get { return sourceRectangles; }
        }

        public void Update(GameTime gameTime)
        {
            if (isLooped)
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
                return;
            }
            // if not looped, plays animation once and then stops (by setting isPlaying to false)
            else
            {
                if (isPlaying)
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
                            isPlaying = false;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet, Vector2 position)
        {
            if (isPlaying)
                spriteSheet.drawSprite(spriteBatch, sourceRectangles[animationIndex], position);
        }
    }
}
