using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Snack
    {
        public enum SnackType { Small, Big };
        private SnackType snackType;
        private Vector2 gridPosition;
        private int[] gridTile;
        public int scoreGain;
        private Rectangle smallSnackRect = new Rectangle(33, 33, 6, 6);
        private Rectangle bigSnackRect = new Rectangle(24, 72, 24, 24);
        private int radiusOffSet;

        public Vector2 Position
        {
            get { return gridPosition; }
        }

        public Snack(SnackType newSnackType, Vector2 newPosition, int[] newGridTile)
        {
            snackType = newSnackType;
            if (newSnackType == SnackType.Big)
            {
                scoreGain = 50;
                radiusOffSet = 12;
            }
            else
            {
                scoreGain = 10;
                radiusOffSet = 3;
            }
                
            gridPosition = newPosition;
            gridTile = newGridTile;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (snackType == SnackType.Small)
                Game1.spriteSheet1.drawSprite(spriteBatch, smallSnackRect, new Vector2(gridPosition.X + Controller.tileWidth/2 - radiusOffSet, gridPosition.Y + Controller.tileHeight/2 - radiusOffSet));
            else
                Game1.spriteSheet1.drawSprite(spriteBatch, bigSnackRect, new Vector2(gridPosition.X + Controller.tileWidth / 2 - radiusOffSet, gridPosition.Y + Controller.tileHeight / 2 - radiusOffSet));
        }
    }
}
