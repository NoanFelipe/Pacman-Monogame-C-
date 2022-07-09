using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Text
    {
        public enum Color { White, Red };

        private SpriteSheet textSprite;
        public static int textSize = 21;
        private Rectangle C = new Rectangle(51, 0, textSize, textSize);
        private Rectangle E = new Rectangle(99, 0, textSize, textSize);
        private Rectangle I = new Rectangle(195, 0, textSize, textSize);
        private Rectangle L = new Rectangle(267, 0, textSize, textSize);
        private Rectangle O = new Rectangle(339, 0, textSize, textSize);
        private Rectangle R = new Rectangle(51, 24, textSize, textSize);
        private Rectangle S = new Rectangle(75, 24, textSize, textSize);
        private Rectangle V = new Rectangle(147, 24, textSize, textSize);
        private Rectangle n0 = new Rectangle(3, 48, textSize, textSize);
        private Rectangle n1 = new Rectangle(27, 48, textSize, textSize);
        private Rectangle n2 = new Rectangle(51, 48, textSize, textSize);
        private Rectangle n3 = new Rectangle(75, 48, textSize, textSize);
        private Rectangle n4 = new Rectangle(99, 48, textSize, textSize);
        private Rectangle n5 = new Rectangle(123, 48, textSize, textSize);
        private Rectangle n6 = new Rectangle(147, 48, textSize, textSize);
        private Rectangle n7 = new Rectangle(171, 48, textSize, textSize);
        private Rectangle n8 = new Rectangle(195, 48, textSize, textSize);
        private Rectangle n9 = new Rectangle(219, 48, textSize, textSize);
        private Rectangle slash = new Rectangle(267, 48, textSize, textSize);

        private Rectangle G_RED = new Rectangle(147, 96, textSize, textSize);
        private Rectangle A_RED = new Rectangle(3, 96, textSize, textSize);
        private Rectangle M_RED = new Rectangle(291, 96, textSize, textSize);
        private Rectangle E_RED = new Rectangle(99, 96, textSize, textSize);
        private Rectangle O_RED = new Rectangle(339, 96, textSize, textSize);
        private Rectangle V_RED = new Rectangle(147, 120, textSize, textSize);
        private Rectangle R_RED = new Rectangle(51, 120, textSize, textSize);
        private Rectangle exclamationPointRed = new Rectangle(267, 120, textSize, textSize);

        public Text(SpriteSheet newSprite)
        {
            textSprite = newSprite;
        }

        public void draw(SpriteBatch spriteBatch, string text, Vector2 position, int spaceBetweenLetters, Color color)
        {
            int indexOffSet = 0;
            foreach (char c in text)
            {
                if (color == Color.White)
                {
                    switch (c)
                    {
                        case '-':
                            textSprite.drawSprite(spriteBatch, slash, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '0':
                            textSprite.drawSprite(spriteBatch, n0, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '1':
                            textSprite.drawSprite(spriteBatch, n1, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '2':
                            textSprite.drawSprite(spriteBatch, n2, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '3':
                            textSprite.drawSprite(spriteBatch, n3, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '4':
                            textSprite.drawSprite(spriteBatch, n4, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '5':
                            textSprite.drawSprite(spriteBatch, n5, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '6':
                            textSprite.drawSprite(spriteBatch, n6, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '7':
                            textSprite.drawSprite(spriteBatch, n7, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '8':
                            textSprite.drawSprite(spriteBatch, n8, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '9':
                            textSprite.drawSprite(spriteBatch, n9, new Vector2(position.X + indexOffSet, position.Y));
                            break;


                        case 'c':
                            textSprite.drawSprite(spriteBatch, C, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'e':
                            textSprite.drawSprite(spriteBatch, E, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'i':
                            textSprite.drawSprite(spriteBatch, I, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'l':
                            textSprite.drawSprite(spriteBatch, L, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'o':
                            textSprite.drawSprite(spriteBatch, O, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'r':
                            textSprite.drawSprite(spriteBatch, R, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 's':
                            textSprite.drawSprite(spriteBatch, S, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'v':
                            textSprite.drawSprite(spriteBatch, V, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                    }
                } else if (color == Color.Red)
                {
                    switch (c)
                    {
                        case 'g':
                            textSprite.drawSprite(spriteBatch, G_RED, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'a':
                            textSprite.drawSprite(spriteBatch, A_RED, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'm':
                            textSprite.drawSprite(spriteBatch, M_RED, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'e':
                            textSprite.drawSprite(spriteBatch, E_RED, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'o':
                            textSprite.drawSprite(spriteBatch, O_RED, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'v':
                            textSprite.drawSprite(spriteBatch, V_RED, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case 'r':
                            textSprite.drawSprite(spriteBatch, R_RED, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                        case '!':
                            textSprite.drawSprite(spriteBatch, exclamationPointRed, new Vector2(position.X + indexOffSet, position.Y));
                            break;
                    }
                }
                indexOffSet += spaceBetweenLetters;
            }
        }

        public void draw(SpriteBatch spriteBatch, string text, Vector2 position, int spaceBetweenLetters, Color color, float Scale)
        {
            int indexOffSet = 0;
            foreach (char c in text)
            {
                if (color == Color.White)
                {
                    switch (c)
                    {
                        case '-':
                            textSprite.drawSprite(spriteBatch, slash, new Vector2(position.X + indexOffSet, position.Y), 3f);
                            break;
                        case '0':
                            textSprite.drawSprite(spriteBatch, n0, new Vector2(position.X + indexOffSet, position.Y), 3f);
                            break;
                        case '1':
                            textSprite.drawSprite(spriteBatch, n1, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '2':
                            textSprite.drawSprite(spriteBatch, n2, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '3':
                            textSprite.drawSprite(spriteBatch, n3, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '4':
                            textSprite.drawSprite(spriteBatch, n4, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '5':
                            textSprite.drawSprite(spriteBatch, n5, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '6':
                            textSprite.drawSprite(spriteBatch, n6, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '7':
                            textSprite.drawSprite(spriteBatch, n7, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '8':
                            textSprite.drawSprite(spriteBatch, n8, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '9':
                            textSprite.drawSprite(spriteBatch, n9, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;


                        case 'c':
                            textSprite.drawSprite(spriteBatch, C, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'e':
                            textSprite.drawSprite(spriteBatch, E, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'i':
                            textSprite.drawSprite(spriteBatch, I, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'l':
                            textSprite.drawSprite(spriteBatch, L, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'o':
                            textSprite.drawSprite(spriteBatch, O, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'r':
                            textSprite.drawSprite(spriteBatch, R, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 's':
                            textSprite.drawSprite(spriteBatch, S, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'v':
                            textSprite.drawSprite(spriteBatch, V, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                    }
                }
                else if (color == Color.Red)
                {
                    switch (c)
                    {
                        case 'g':
                            textSprite.drawSprite(spriteBatch, G_RED, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'a':
                            textSprite.drawSprite(spriteBatch, A_RED, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'm':
                            textSprite.drawSprite(spriteBatch, M_RED, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'e':
                            textSprite.drawSprite(spriteBatch, E_RED, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'o':
                            textSprite.drawSprite(spriteBatch, O_RED, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'v':
                            textSprite.drawSprite(spriteBatch, V_RED, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case 'r':
                            textSprite.drawSprite(spriteBatch, R_RED, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                        case '!':
                            textSprite.drawSprite(spriteBatch, exclamationPointRed, new Vector2(position.X + indexOffSet, position.Y), Scale);
                            break;
                    }
                }
                indexOffSet += spaceBetweenLetters;
            }
        }
    }
}
