using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    public static class Menu
    {
        private static Texture2D pacmanLogo;
        private static Rectangle pacmanLogoPos = new Rectangle(13, 40, 4530/7, 1184/7);
        private static SpriteFont basicFont;
        private static Vector2 basicFontPos = new Vector2(150, 400);

        public static SpriteFont setBasicFont
        {
            set { basicFont = value; }
        }

        public static Texture2D setPacmanLogo
        {
            set { pacmanLogo = value; }
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Enter))
            {
                Game1.gameController.gameState = Controller.GameState.Normal;
                MySounds.game_start.Play();
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(basicFont, "PRESS ENTER TO PLAY", basicFontPos, Color.Red);
            spriteBatch.Draw(pacmanLogo, pacmanLogoPos, Color.White);
        }
    }
}
