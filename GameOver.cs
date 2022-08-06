using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    public static class GameOver
    {
        private static SpriteFont basicFont;
        private static Vector2 basicFontPos = new Vector2(93, 400);
        public static SpriteFont setBasicFont
        {
            set { basicFont = value; }
        }

        public static void Update()
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Space))
            {
                Game1.gameController.gameState = Controller.GameState.Menu;
            }
        }

        public static void Draw(SpriteBatch spriteBatch, Text text)
        {
            text.draw(spriteBatch, "game over!", new Vector2(100, 321), 48, Text.Color.Red, 2f);
            spriteBatch.DrawString(basicFont, "PRESS SPACE TO GO TO MENU", basicFontPos, Color.Red);
        }
    }
}
