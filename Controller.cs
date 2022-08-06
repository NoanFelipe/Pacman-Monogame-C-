using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Controller
    {
        private int[,] mapDesign = new int[,]{
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,3,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,3,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,0,1},
            { 1,0,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,0,1},
            { 1,0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,1},
            { 1,1,1,1,1,1,0,1,1,1,1,1,5,1,1,5,1,1,1,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,1,1,1,5,1,1,5,1,1,1,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,5,5,5,5,5,5,5,5,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,2,2,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,2,2,2,2,2,2,1,5,1,1,0,1,1,1,1,1,1},
            { 0,0,0,0,0,0,0,5,5,5,1,2,2,2,2,2,2,1,5,5,5,0,0,0,0,0,0,0},
            { 1,1,1,1,1,1,0,1,1,5,1,2,2,2,2,2,2,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,1,1,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,5,5,5,5,5,5,5,5,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,1,1,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,1,1,1,1,1,0,1,1,5,1,1,1,1,1,1,1,1,5,1,1,0,1,1,1,1,1,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,0,1,1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,1,1,0,1,1,1,1,0,1},
            { 1,3,0,0,1,1,0,0,0,0,0,0,0,5,5,0,0,0,0,0,0,0,1,1,0,0,3,1},
            { 1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1},
            { 1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1},
            { 1,0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,1},
            { 1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,0,1},
            { 1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1,1,1,1,1,1,1,1,1,0,1},
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        public enum GameState { Normal, GameOver, Menu };
        public GameState gameState = GameState.Menu;

        public static int numberOfTilesY;
        public static int numberOfTilesX;
        public static int tileWidth;
        public static int tileHeight;
        public Tile[,] tileArray;
        public List<Snack> snackList = new List<Snack>();

        public Enemy.EnemyState enemiesState = Enemy.EnemyState.Scatter;

        public float ghostInitialTimer;
        public float ghostInitialTimerLength = 2f;

        public float ghostTimerScatter;
        public float ghostTimerScatterLength = 15f;
        public float ghostTimerChaser;
        public float ghostTimerChaserLength = 20f;

        public bool eatenBigSnack = false;

        public bool startPacmanDeathAnim = false;
        public Vector2 pacmanDeathPosition;

        public int ghostScoreMultiplier = 1;
        public Controller()
        {
            numberOfTilesX = 28;
            numberOfTilesY = 31;
            tileWidth = Game1.windowWidth / numberOfTilesX;
            tileHeight = (Game1.windowHeight - Game1.scoreOffSet) / numberOfTilesY;
            tileArray = new Tile[numberOfTilesX, numberOfTilesY];
        }

        public void createGrid() // creates grid that contains Tile objects, which represent 24x24 pixels squares in the game, all with types such as walls, snacks and etc.
        {
            for (int y = 0; y < numberOfTilesY; y++)
            {
                for (int x = 0; x < numberOfTilesX; x++)
                {
                    if (mapDesign[y, x] == 0) // small snack
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Snack);
                        tileArray[x, y].isEmpty = false;
                        snackList.Add(new Snack(Snack.SnackType.Small, new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), new int[] { x, y}));
                    }
                    else if (mapDesign[y, x] == 1) // wall collider
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Wall);
                        tileArray[x, y].isEmpty = false;
                    }
                    else if (mapDesign[y, x] == 2) //  ghost house
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.GhostHouse);
                        tileArray[x, y].isEmpty = false;
                    }
                    else if (mapDesign[y, x] == 3) // big snack
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Snack);
                        tileArray[x, y].isEmpty = false;
                        snackList.Add(new Snack(Snack.SnackType.Big, new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), new int[] { x, y }));
                    }
                    else if (mapDesign[y, x] == 5) // empty
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet));
                    }
                }
            }
        }

        // creates snacks again for when the player eats all snacks on the screen
        public void createSnacks()
        {
            for (int y = 0; y < numberOfTilesY; y++)
            {
                for (int x = 0; x < numberOfTilesX; x++)
                {
                    if (mapDesign[y, x] == 0) // small snack
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Snack);
                        tileArray[x, y].isEmpty = false;
                        snackList.Add(new Snack(Snack.SnackType.Small, new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), new int[] { x, y }));
                    }
                    else if (mapDesign[y, x] == 3) // big snack
                    {
                        tileArray[x, y] = new Tile(new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), Tile.TileType.Snack);
                        tileArray[x, y].isEmpty = false;
                        snackList.Add(new Snack(Snack.SnackType.Big, new Vector2(x * tileWidth, y * tileHeight + Game1.scoreOffSet), new int[] { x, y }));
                    }
                }
            }
        }

        public void win(Inky i, Blinky b, Pinky p, Clyde c, Player pacman)
        {
            createSnacks();
            resetGhosts(i,b,p,c);

            ghostTimerChaser = 0;
            ghostTimerScatter = 0;
            ghostInitialTimer = 0;

            eatenBigSnack = false;

            pacman.Position = new Vector2(tileArray[13,23].Position.X + 14, tileArray[13, 23].Position.Y);
            pacman.CurrentTile = new Vector2(13, 23);
            pacman.PlayerAnim.setSourceRects(Player.rectsRight);
            pacman.PlayerAnim.setAnimIndex(2);
            pacman.Direction = Dir.Right;

            MySounds.munchInstance.Stop();
            MySounds.power_pellet_instance.Stop();
            MySounds.retreatingInstance.Stop();
        }

        public void gameOver(Inky i, Blinky b, Pinky p, Clyde c, Player pacman)
        {
            gameState = GameState.GameOver;

            Game1.hasPassedInitialSong = false;
            Game1.score = 0;
            Game1.pacmanDeathAnimation.IsPlaying = false;
            Game1.gamePauseTimer = Game1.gameStartSongLength;
            pacman.ExtraLives = 4;

            createSnacks();
            resetGhosts(i, b, p, c);

            ghostTimerChaser = 0;
            ghostTimerScatter = 0;
            ghostInitialTimer = 0;

            eatenBigSnack = false;

            pacman.Position = new Vector2(tileArray[13, 23].Position.X + 14, tileArray[13, 23].Position.Y);
            pacman.CurrentTile = new Vector2(13, 23);
            pacman.PlayerAnim.setSourceRects(Player.rectsRight);
            pacman.PlayerAnim.setAnimIndex(2);
            pacman.Direction = Dir.Right;

            MySounds.munchInstance.Stop();
            MySounds.power_pellet_instance.Stop();
            MySounds.retreatingInstance.Stop();
        }

        public void killPacman(Inky i, Blinky b, Pinky p, Clyde c, Player pacman)
        {
            pacman.ExtraLives -= 1;
            startPacmanDeathAnim = true;
            pacmanDeathPosition = new Vector2(pacman.Position.X - Player.radiusOffSet / 2, pacman.Position.Y - Player.radiusOffSet / 2 + 1);
            MySounds.death_1.Play(); //Length = 2.78
            Game1.gamePauseTimer = 4f;

            resetGhosts(i, b, p, c);

            ghostTimerChaser = 0;
            ghostTimerScatter = 0;
            ghostInitialTimer = 0;

            eatenBigSnack = false;

            pacman.Position = new Vector2(tileArray[13, 23].Position.X + 14, tileArray[13, 23].Position.Y);
            pacman.CurrentTile = new Vector2(13, 23);
            pacman.PlayerAnim.setSourceRects(Player.rectsRight);
            pacman.PlayerAnim.setAnimIndex(2);
            pacman.Direction = Dir.Right;

            MySounds.munchInstance.Stop();
            MySounds.power_pellet_instance.Stop();
            MySounds.retreatingInstance.Stop();
        }

        public void drawGridDebugger(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < numberOfTilesX; x++)
            {
                for (int y = 0; y < numberOfTilesY; y++)
                {
                    Vector2 dotPosition = tileArray[x, y].Position;
                    spriteBatch.Draw(Game1.debugLineX, dotPosition, Color.White);
                    spriteBatch.Draw(Game1.debugLineY, dotPosition, Color.White);
                    
                }
            }
        }

        public void drawGhosts(Inky i, Blinky b, Pinky p, Clyde c, SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            i.Draw(spriteBatch, spriteSheet);
            b.Draw(spriteBatch, spriteSheet);
            p.Draw(spriteBatch, spriteSheet);
            c.Draw(spriteBatch, spriteSheet);
        }

        public void updateGhosts(Inky i, Blinky b, Pinky p, Clyde c, GameTime gameTime, Player Pacman, Vector2 blinkyPos)
        {
            if (eatenBigSnack)
            {
                eatenBigSnack = false;
                setGhostStates(i, b, p, c, Enemy.EnemyState.Frightened);
                MySounds.power_pellet_instance.Play();
            }

            if (i.state != Enemy.EnemyState.Frightened && b.state != Enemy.EnemyState.Frightened && p.state != Enemy.EnemyState.Frightened && c.state != Enemy.EnemyState.Frightened)
            { 
                MySounds.power_pellet_instance.Stop();
                ghostScoreMultiplier = 1;
            }
            if (i.state != Enemy.EnemyState.Eaten && b.state != Enemy.EnemyState.Eaten && p.state != Enemy.EnemyState.Eaten && c.state != Enemy.EnemyState.Eaten)
                MySounds.retreatingInstance.Stop();

            if (ghostInitialTimer < ghostInitialTimerLength)
            {
                ghostInitialTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                c.EnemyAnim.Update(gameTime);
                i.EnemyAnim.Update(gameTime);
            }
            if (ghostInitialTimer > ghostInitialTimerLength / 2 && ghostInitialTimer < ghostInitialTimerLength)
            {
                i.Update(gameTime, this, Pacman.CurrentTile, Pacman.Direction, blinkyPos);
            }
            else if (ghostInitialTimer > ghostInitialTimerLength) // When Initial timer ends, starts the timers to switch from scatter to chaser
            {
                c.Update(gameTime, this, Pacman.CurrentTile, Pacman.Direction, blinkyPos);
                i.Update(gameTime, this, Pacman.CurrentTile, Pacman.Direction, blinkyPos);
                switchBetweenStates(i, b, p, c, gameTime);
            }

            p.Update(gameTime, this, Pacman.CurrentTile, Pacman.Direction, blinkyPos);
            b.Update(gameTime, this, Pacman.CurrentTile, Pacman.Direction, blinkyPos);

            if (i.colliding == true || b.colliding == true || p.colliding == true || c.colliding == true)
            {
                killPacman(i, b, p, c, Pacman);
                i.colliding = false;
                b.colliding = false;
                p.colliding = false;
                c.colliding = false;
            }
        }

        public void switchBetweenStates(Inky i, Blinky b, Pinky p, Clyde c, GameTime gameTime)
        {
            if (i.state == Enemy.EnemyState.Frightened || i.state == Enemy.EnemyState.Eaten ||
                b.state == Enemy.EnemyState.Frightened || b.state == Enemy.EnemyState.Eaten ||
                c.state == Enemy.EnemyState.Frightened || c.state == Enemy.EnemyState.Eaten ||
                p.state == Enemy.EnemyState.Frightened || p.state == Enemy.EnemyState.Eaten)
                return;

            if (enemiesState == Enemy.EnemyState.Scatter)
            {
                ghostTimerScatter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ghostTimerScatter > ghostTimerScatterLength)
                {
                    ghostTimerScatter = 0;
                    enemiesState = Enemy.EnemyState.Chase;
                    setGhostStates(i, b, p, c, Enemy.EnemyState.Chase);
                }
            }else if (enemiesState == Enemy.EnemyState.Chase)
            {
                ghostTimerChaser += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ghostTimerChaser > ghostTimerChaserLength)
                {
                    ghostTimerChaser = 0;
                    enemiesState = Enemy.EnemyState.Scatter;
                    setGhostStates(i, b, p, c, Enemy.EnemyState.Scatter);
                }
            }
        }

        public void setGhostStates(Inky i, Blinky b, Pinky p, Clyde c, Enemy.EnemyState eState)
        {
            if(eState == Enemy.EnemyState.Chase || eState == Enemy.EnemyState.Scatter || eState == Enemy.EnemyState.Eaten)
            {
                i.speed = i.normalSpeed;
                b.speed = b.normalSpeed;
                p.speed = p.normalSpeed;
                c.speed = c.normalSpeed;
            }
            else
            {
                if (i.state != Enemy.EnemyState.Eaten)
                    i.speed = i.frightenedSpeed;
                if (b.state != Enemy.EnemyState.Eaten)
                    b.speed = b.frightenedSpeed;
                if (p.state != Enemy.EnemyState.Eaten)
                    p.speed = p.frightenedSpeed;
                if (c.state != Enemy.EnemyState.Eaten)
                    c.speed = c.frightenedSpeed;
            }
            if (i.state != Enemy.EnemyState.Eaten)
                i.state = eState;
            if (b.state != Enemy.EnemyState.Eaten)
                b.state = eState;
            if (p.state != Enemy.EnemyState.Eaten)
                p.state = eState;
            if (c.state != Enemy.EnemyState.Eaten)
                c.state = eState;
        }

        public void drawPacmanGridDebugger(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < numberOfTilesX; x++)
            {
                for (int y = 0; y < numberOfTilesY; y++)
                {
                    Vector2 dotPosition = tileArray[x, y].Position;
                    if (tileArray[x, y].tileType == Tile.TileType.Player)
                    {
                        spriteBatch.Draw(Game1.playerDebugLineX, dotPosition, Color.White);
                        spriteBatch.Draw(Game1.playerDebugLineY, dotPosition, Color.White);
                        spriteBatch.Draw(Game1.playerDebugLineX, new Vector2(dotPosition.X, dotPosition.Y + 24), Color.White);
                        spriteBatch.Draw(Game1.playerDebugLineY, new Vector2(dotPosition.X + 24, dotPosition.Y), Color.White);
                    }
                }
            }
        }

        public void resetGhosts(Inky i, Blinky b, Pinky p, Clyde c)
        {
            ghostInitialTimer = 0;

            setGhostStates(i, b, p, c, Enemy.EnemyState.Scatter);

            i.EnemyAnim.setSourceRects(i.rectsUp);
            b.EnemyAnim.setSourceRects(b.rectsLeft);
            p.EnemyAnim.setSourceRects(p.rectsDown);
            c.EnemyAnim.setSourceRects(c.rectsUp);

            i.timerFrightened = 0;
            b.timerFrightened = 0;
            p.timerFrightened = 0;
            c.timerFrightened = 0;

            i.PathToPacMan = new List<Vector2>();
            b.PathToPacMan = new List<Vector2>();
            p.PathToPacMan = new List<Vector2>();
            c.PathToPacMan = new List<Vector2>();

            i.Position = new Vector2(tileArray[11, 14].Position.X+12, tileArray[11, 14].Position.Y);
            b.Position = new Vector2(tileArray[13, 11].Position.X + 12, tileArray[13, 11].Position.Y);
            p.Position = new Vector2(tileArray[13, 14].Position.X + 12, tileArray[13, 14].Position.Y); 
            c.Position = new Vector2(tileArray[15, 14].Position.X + 12, tileArray[15, 14].Position.Y); 
        }

        public void drawPathFindingDebugger(SpriteBatch spriteBatch, List<Vector2> path)
        {
            if (path == null) return;
            foreach (Vector2 gridPos in path)
            {
                Vector2 pos = tileArray[(int)gridPos.X, (int)gridPos.Y].Position;
                spriteBatch.Draw(Game1.pathfindingDebugLineX, pos, Color.White);
                spriteBatch.Draw(Game1.pathfindingDebugLineY, pos, Color.White);
                spriteBatch.Draw(Game1.pathfindingDebugLineX, new Vector2(pos.X, pos.Y + 24), Color.White);
                spriteBatch.Draw(Game1.pathfindingDebugLineY, new Vector2(pos.X + 24, pos.Y), Color.White);
            }
        }

        public bool isNextTileAvailable(Dir dir, Vector2 tile)
        { // tile != new int[2] {0, 14} && tile != new int[2] {numberOfTilesX-1 ,14}
            if (tile.Equals(new Vector2( 0, 14 )) || tile.Equals(new Vector2(numberOfTilesX - 1 ,14)))
            {
                if (dir == Dir.Right || dir == Dir.Left)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                switch (dir)
                {
                    case Dir.Right:
                        if (tileArray[(int)tile.X + 1, (int)tile.Y].tileType == Tile.TileType.Wall || tileArray[(int)tile.X + 1, (int)tile.Y].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Left:
                        if (tileArray[(int)tile.X - 1, (int)tile.Y].tileType == Tile.TileType.Wall || tileArray[(int)tile.X - 1, (int)tile.Y].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Down:
                        if (tileArray[(int)tile.X, (int)tile.Y + 1].tileType == Tile.TileType.Wall || tileArray[(int)tile.X, (int)tile.Y + 1].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                    case Dir.Up:
                        if (tileArray[(int)tile.X, (int)tile.Y - 1].tileType == Tile.TileType.Wall || tileArray[(int)tile.X, (int)tile.Y - 1].tileType == Tile.TileType.GhostHouse)
                        {
                            return false;
                        }
                        break;
                }
                return true;
            }
        }

        public bool isNextTileAvailableGhosts(Dir dir, Vector2 tile)
        { // tile != new int[2] {0, 14} && tile != new int[2] {numberOfTilesX-1 ,14}
            if (tile.Equals(new Vector2(0, 14)) || tile.Equals(new Vector2(numberOfTilesX - 1, 14)))
            {
                if (dir == Dir.Right || dir == Dir.Left)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                switch (dir)
                {
                    case Dir.Right:
                        if (tileArray[(int)tile.X + 1, (int)tile.Y].tileType == Tile.TileType.Wall)
                        {
                            return false;
                        }
                        break;
                    case Dir.Left:
                        if (tileArray[(int)tile.X - 1, (int)tile.Y].tileType == Tile.TileType.Wall)
                        {
                            return false;
                        }
                        break;
                    case Dir.Down:
                        if (tileArray[(int)tile.X, (int)tile.Y + 1].tileType == Tile.TileType.Wall)
                        {
                            return false;
                        }
                        break;
                    case Dir.Up:
                        if (tileArray[(int)tile.X, (int)tile.Y - 1].tileType == Tile.TileType.Wall)
                        {
                            return false;
                        }
                        break;
                }
                return true;
            }
        }

        public static Dir returnOppositeDir(Dir dir)
        {
            switch (dir)
            {
                case Dir.Up:
                    return Dir.Down;
                case Dir.Down:
                    return Dir.Up;
                case Dir.Right:
                    return Dir.Left;
                case Dir.Left:
                    return Dir.Right;
            }
            return Dir.None;
        }

        public int findSnackListPosition(Vector2 snackGridPos)
        {
            int listPosition = -1;
            foreach (Snack snack in snackList)
            {
                if (snack.Position == snackGridPos)
                {
                    listPosition = snackList.IndexOf(snack);
                }
            }

            return listPosition;
        }

        public bool checkTileType(Vector2 gridIndex, Tile.TileType tileType)
        {
            bool tile = false;
            if (tileArray[(int)gridIndex.X, (int)gridIndex.Y].tileType == tileType)
            {
                tile = true;
            }
            return tile;
        }
    }
}
