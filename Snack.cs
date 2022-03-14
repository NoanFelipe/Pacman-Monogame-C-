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
        private Rectangle smallSnackRect = new Rectangle(33, 33, 6, 6);
        private int radiusOffSet = 3;

        public Vector2 Position
        {
            get { return gridPosition; }
        }

        public Snack(SnackType newSnackType, Vector2 newPosition, int[] newGridTile)
        {
            snackType = newSnackType;
            gridPosition = newPosition;
            gridTile = newGridTile;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.spriteSheet1.drawSprite(spriteBatch, smallSnackRect, new Vector2(gridPosition.X + Controller.tileWidth/2 - radiusOffSet, gridPosition.Y + Controller.tileHeight/2 - radiusOffSet));
        }
    }
}
