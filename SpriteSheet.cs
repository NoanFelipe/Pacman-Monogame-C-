using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class SpriteSheet
    {
        private Texture2D spriteSheet;

        public SpriteSheet(Texture2D newSpriteSheet)
        {
            spriteSheet = newSpriteSheet;
        }

        public void drawSprite(SpriteBatch spriteBatch, Rectangle sourceRectangle, Vector2 position)
        {
            spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White);
        }
    }
}
