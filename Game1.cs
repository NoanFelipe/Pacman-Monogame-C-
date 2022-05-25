using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    public enum Dir
    {
        Down,
        Up,
        Left,
        Right,
        None
    }

    public class Game1 : Game
    {
        public static Controller gameController;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Texture2D GeneralSprites1;
        public static Texture2D GeneralSprites2;

        public static Texture2D debuggingDot;
        public static Texture2D debugLineX;
        public static Texture2D debugLineY;
        public static Texture2D playerDebugLineX;
        public static Texture2D playerDebugLineY;
        public static Texture2D pathfindingDebugLineX;
        public static Texture2D pathfindingDebugLineY;

        public static SpriteSheet spriteSheet1;
        public static SpriteSheet spriteSheet2;

        public static int scoreOffSet = 27;
        public static int windowHeight = 744 + scoreOffSet;
        public static int windowWidth = 672;

        public static float gameStartSongLength;
        public static float gameStartTimer;

        public static Text text;

        Rectangle backgroundRect = new Rectangle(684,0,672,744);

        Inky inky;
        Blinky blinky;
        Clyde clyde;
        Pinky pinky;

        Player Pacman;

        public static int score;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            MySounds.credit = Content.Load<SoundEffect>("Sounds/credit");
            MySounds.death_1 = Content.Load<SoundEffect>("Sounds/death_1");
            MySounds.death_2 = Content.Load<SoundEffect>("Sounds/death_2");
            MySounds.eat_fruit = Content.Load<SoundEffect>("Sounds/eat_fruit");
            MySounds.eat_ghost = Content.Load<SoundEffect>("Sounds/eat_ghost");
            MySounds.extend = Content.Load<SoundEffect>("Sounds/extend");
            MySounds.game_start = Content.Load<SoundEffect>("Sounds/game_start");
            MySounds.intermission = Content.Load<SoundEffect>("Sounds/intermission");

            MySounds.munch = Content.Load<SoundEffect>("Sounds/munch");
            MySounds.munchInstance = MySounds.munch.CreateInstance();
            MySounds.munchInstance.Volume = 0.5f;
            MySounds.munchInstance.IsLooped = true;

            MySounds.retreating = Content.Load<SoundEffect>("Sounds/retreating");
            MySounds.siren_1 = Content.Load<SoundEffect>("Sounds/siren_1");
            MySounds.siren_2 = Content.Load<SoundEffect>("Sounds/siren_2");
            MySounds.siren_3 = Content.Load<SoundEffect>("Sounds/siren_3");
            MySounds.siren_4 = Content.Load<SoundEffect>("Sounds/siren_4");
            MySounds.siren_5 = Content.Load<SoundEffect>("Sounds/siren_5");

            GeneralSprites1 = Content.Load<Texture2D>("SpriteSheets/GeneralSprites1");
            GeneralSprites2 = Content.Load<Texture2D>("SpriteSheets/GeneralSprites2");
            debuggingDot = Content.Load<Texture2D>("SpriteSheets/debuggingDot");
            debugLineX = Content.Load<Texture2D>("SpriteSheets/debugLineX");
            debugLineY = Content.Load<Texture2D>("SpriteSheets/debugLineY");
            playerDebugLineX = Content.Load<Texture2D>("SpriteSheets/playerDebugLineX");
            playerDebugLineY = Content.Load<Texture2D>("SpriteSheets/playerDebugLineY");
            pathfindingDebugLineX = Content.Load<Texture2D>("SpriteSheets/pathfindingDebugLineX");
            pathfindingDebugLineY = Content.Load<Texture2D>("SpriteSheets/pathfindingDebugLineY");
            spriteSheet1 = new SpriteSheet(GeneralSprites1);
            spriteSheet2 = new SpriteSheet(GeneralSprites2);

            text = new Text(new SpriteSheet(Content.Load<Texture2D>("Spritesheets/TextSprites")));

            gameController = new Controller();
            gameController.createGrid();

            inky = new Inky(11, 14, gameController.tileArray);
            blinky = new Blinky(13, 11, gameController.tileArray);
            pinky = new Pinky(13, 14, gameController.tileArray);
            clyde = new Clyde(15, 14, gameController.tileArray);

            Pacman = new Player(13, 23, gameController.tileArray);

            MySounds.game_start.Play();
            gameStartSongLength = 4.23f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // checks if starting song finished, if false returns
            if (gameStartSongLength > gameStartTimer) 
            {
                gameStartTimer += dt;
                base.Update(gameTime);
                return;
            }

            Pacman.updatePlayerTilePosition(gameController.tileArray); 
            Pacman.Update(gameTime, gameController.tileArray);
            gameController.updateGhosts(inky, blinky, pinky, clyde, gameTime, gameController, Pacman.CurrentTile, Pacman.Direction, blinky.CurrentTile);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            spriteSheet1.drawSprite(_spriteBatch, backgroundRect, new Vector2(0,scoreOffSet)); 
            text.draw(_spriteBatch, "score - " + score, new Vector2(3,3), 24);
            foreach (Snack snack in gameController.snackList)
            {
                snack.Draw(_spriteBatch);
            }
            Pacman.Draw(_spriteBatch, spriteSheet1);
            gameController.drawGhosts(inky, blinky, pinky, clyde, _spriteBatch, spriteSheet1);

            //gameController.drawGridDebugger(_spriteBatch);

            //gameController.drawPathFindingDebugger(_spriteBatch, inky.PathToPacMan);
            //gameController.drawPathFindingDebugger(_spriteBatch, blinky.PathToPacMan);
            //gameController.drawPathFindingDebugger(_spriteBatch, pinky.PathToPacMan);
            //gameController.drawPathFindingDebugger(_spriteBatch, clyde.PathToPacMan);

            //gameController.drawPacmanGridDebugger(_spriteBatch);
            //Pacman.debugPacmanPosition(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
