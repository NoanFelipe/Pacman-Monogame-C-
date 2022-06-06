using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Enemy
    {
        public enum GhostType {Inky, Blinky, Pinky, Clyde};
        public GhostType type;
        public enum EnemyState { Scatter, Chase, Frightened, Eaten };
        public EnemyState state = EnemyState.Scatter;
        public Vector2 ScatterTargetTile;
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool colliding = false;

        protected Vector2 currentTile;
        public Vector2 CurrentTile
        {
            get { return currentTile; }
        }
        protected Vector2 previousTile;
        protected Tile.TileType previousTileType;
        protected Dir direction;

        protected Dir playerLastDir; // if player direction is none, this remembers the last direction so that the ghost can know its target tile

        protected List<Vector2> pathToPacMan;
        protected Vector2 foundpathTile;

        public List<Vector2> PathToPacMan
        {
            get { return pathToPacMan; }
            set { pathToPacMan = value; }
        }

        protected int speed = 140;

        protected Rectangle[] rectsDown = new Rectangle[2];
        protected Rectangle[] rectsUp = new Rectangle[2];
        protected Rectangle[] rectsLeft = new Rectangle[2];
        protected Rectangle[] rectsRight = new Rectangle[2];

        protected Rectangle[] frightenedRects = new Rectangle[2];
        protected Rectangle[] frightenedRectsEnd = new Rectangle[8];

        protected int drawOffSetX = -9;
        protected int drawOffSetY = -9;

        protected SpriteAnimation enemyAnim;

        public SpriteAnimation EnemyAnim
        {
            get { return enemyAnim; }
        }

        public float timerFrightened;
        public float timerFrightenedLength = 8f;

        public Enemy(int tileX, int tileY, Tile[,] tileArray)
        {
            position = tileArray[tileX, tileY].Position;
            currentTile = new Vector2(tileX, tileY);
            previousTile = new Vector2(-1,-1);
            direction = Dir.None;


            frightenedRects[0] = new Rectangle(1755, 195, 42, 42);
            frightenedRects[1] = new Rectangle(1803, 195, 42, 42);

            frightenedRectsEnd[0] = new Rectangle(1755, 195, 42, 42);
            frightenedRectsEnd[1] = new Rectangle(1803, 195, 42, 42);
            frightenedRectsEnd[2] = new Rectangle(1755, 195, 42, 42);
            frightenedRectsEnd[3] = new Rectangle(1803, 195, 42, 42);
            frightenedRectsEnd[4] = new Rectangle(1851, 195, 42, 42);
            frightenedRectsEnd[5] = new Rectangle(1899, 195, 42, 42);
            frightenedRectsEnd[6] = new Rectangle(1851, 195, 42, 42);
            frightenedRectsEnd[7] = new Rectangle(1899, 195, 42, 42);


            enemyAnim = new SpriteAnimation(0.08f, rectsUp);

            position.X += 12;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            enemyAnim.Draw(spriteBatch, spriteSheet, new Vector2(position.X + drawOffSetX, position.Y + drawOffSetY));
        }

        public void Update(GameTime gameTime, Controller gameController, Vector2 playerTilePos, Dir playerDir, Vector2 blinkyPos)
        {
            if (state == EnemyState.Frightened)
            {
                timerFrightened += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerFrightened > timerFrightenedLength)
                {
                    state = EnemyState.Chase;
                    timerFrightened = 0;
                }
            }
            updateTilePosition(gameController.tileArray);
            enemyAnim.Update(gameTime);

            decideDirection(playerTilePos, playerDir, gameController, blinkyPos);
            Move(gameTime, gameController.tileArray);
        }

        // returns target position for diferent ghost states (scatter, chase, etc)
        public virtual Vector2 getScatterTargetPosition()
        {
            return ScatterTargetTile;
        }

        public virtual Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray)
        {
            return playerTilePos;
        }

        public virtual Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray, Vector2 blinkyPos)
        {
            return playerTilePos;
        }

        public virtual Vector2 getFrightenedTargetPosition()
        {
            List<Dir> dirs = new List<Dir>();

            if (Controller.returnOppositeDir(direction) != Dir.Left && Game1.gameController.isNextTileAvailableGhosts(Dir.Left, currentTile))
                dirs.Add(Dir.Left);
            if (Controller.returnOppositeDir(direction) != Dir.Right && Game1.gameController.isNextTileAvailableGhosts(Dir.Right, currentTile))
                dirs.Add(Dir.Right);
            if (Controller.returnOppositeDir(direction) != Dir.Down && Game1.gameController.isNextTileAvailableGhosts(Dir.Down, currentTile))
                dirs.Add(Dir.Down);
            if (Controller.returnOppositeDir(direction) != Dir.Up && Game1.gameController.isNextTileAvailableGhosts(Dir.Up, currentTile))
                dirs.Add(Dir.Up);

            if (dirs.Count >= 0)
            {
                Random rd = new Random();
                int randDirNum = rd.Next(0, dirs.Count);

                Vector2 rPos = currentTile;

                switch (dirs[randDirNum])
                {
                    case Dir.Left:
                        rPos.X--;
                        break;
                    case Dir.Right:
                        rPos.X++;
                        break;
                    case Dir.Down:
                        rPos.Y++;
                        break;
                    case Dir.Up:
                        rPos.Y--;
                        break;
                }

                return rPos;
            }
            return currentTile;
        }

        public void decideDirection(Vector2 playerTilePos, Dir playerDir, Controller gameController, Vector2 blinkyPos)
        {
            if (!foundpathTile.Equals(currentTile))
            {
                if (state == EnemyState.Scatter)
                {
                    pathToPacMan = Pathfinding.findPath(currentTile, getScatterTargetPosition(), gameController.tileArray, direction);
                } else if (state == EnemyState.Chase)
                {
                    pathToPacMan = Pathfinding.findPath(currentTile, getChaseTargetPosition(playerTilePos, playerDir, gameController.tileArray), gameController.tileArray, direction);
                    if (type == GhostType.Inky)
                    {
                        pathToPacMan = Pathfinding.findPath(currentTile, getChaseTargetPosition(playerTilePos, playerDir, gameController.tileArray, blinkyPos), gameController.tileArray, direction);
                    }
                } else if (state == EnemyState.Frightened)
                {
                    pathToPacMan = Pathfinding.findPath(currentTile, getFrightenedTargetPosition(), gameController.tileArray, direction);
                }
                foundpathTile = currentTile;
            }

            if (playerTilePos.Equals(currentTile)) { colliding = true; return; }
            if (pathToPacMan.Count == 0) { return; }

            if (pathToPacMan[0].X > currentTile.X)
            {
                direction = Dir.Right;
                position.Y = gameController.tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y;
            }
            else if (pathToPacMan[0].X < currentTile.X)
            {
                direction = Dir.Left;
                position.Y = gameController.tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y;
            }
            else if (pathToPacMan[0].Y > currentTile.Y)
            {
                direction = Dir.Down;
                position.X = gameController.tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X;
            }
            else if (pathToPacMan[0].Y < currentTile.Y)
            {
                direction = Dir.Up;
                position.X = gameController.tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X;
            }
        }

        public void Move(GameTime gameTime, Tile[,] tileArray)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (direction)
            {
                case Dir.Right:
                    position.X += speed * dt;
                    if (state == EnemyState.Frightened)
                    {
                        if (timerFrightened > 5)
                        {
                            enemyAnim.setSourceRects(frightenedRectsEnd);
                            break;
                        }
                        enemyAnim.setSourceRects(frightenedRects);
                    }
                    else
                        enemyAnim.setSourceRects(rectsRight);
                    break;

                case Dir.Left:
                    position.X -= speed * dt;
                    if (state == EnemyState.Frightened)
                    {
                        if (timerFrightened > 5)
                        {
                            enemyAnim.setSourceRects(frightenedRectsEnd);
                            break;
                        }
                        enemyAnim.setSourceRects(frightenedRects);
                    }
                    else
                        enemyAnim.setSourceRects(rectsLeft);
                    break;

                case Dir.Down:
                    position.Y += speed * dt;
                    if (state == EnemyState.Frightened)
                    {
                        if (timerFrightened > 5)
                        {
                            enemyAnim.setSourceRects(frightenedRectsEnd);
                            break;
                        }
                        enemyAnim.setSourceRects(frightenedRects);
                    }
                    else
                        enemyAnim.setSourceRects(rectsDown);
                    break;

                case Dir.Up:
                    position.Y -= speed * dt;
                    if (state == EnemyState.Frightened)
                    {
                        if (timerFrightened > 5)
                        {
                            enemyAnim.setSourceRects(frightenedRectsEnd);
                            break;
                        }
                        enemyAnim.setSourceRects(frightenedRects);
                    }
                    else
                        enemyAnim.setSourceRects(rectsUp);
                    break;

                case Dir.None:
                    position = tileArray[(int)currentTile.X, (int)currentTile.Y].Position;
                    break;
            }
        }

        public int checkForTeleportPos(Tile[,] tileArray)
        {
            if (new int[2] { (int)currentTile.X, (int)currentTile.Y }.SequenceEqual(new int[2] { 0, 14 }))
            {
                if (position.X < -30)
                {
                    return 1;
                }
            }
            else if (new int[2] { (int)currentTile.X, (int)currentTile.Y }.SequenceEqual(new int[2] { Controller.numberOfTilesX - 1, 14 }))
            {
                if (position.X > tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X + 30)
                {
                    return 2;
                }
            }
            return 0;
        }

        public void teleport(Vector2 pos, Vector2 tilePos)
        {
            position = pos;
            previousTile = currentTile;
            currentTile = tilePos;
        }

        public void updateTilePosition(Tile[,] tileArray)
        {

            if (checkForTeleportPos(tileArray) == 1)
            {
                if (direction == Dir.Left)
                    teleport(new Vector2(Game1.windowWidth + 30, position.Y), new Vector2(Controller.numberOfTilesX - 1, 14));
            }
            else if (checkForTeleportPos(tileArray) == 2)
            {
                if (direction == Dir.Right)
                    teleport(new Vector2(-30, position.Y), new Vector2(0, 14));
            }

            for (int x = 0; x < tileArray.GetLength(0); x++)
            {
                for (int y = 0; y < tileArray.GetLength(1); y++)
                {
                    float tilePosX = tileArray[x, y].Position.X;
                    float tilePosY = tileArray[x, y].Position.Y;

                    float nextTilePosX = tileArray[x, y].Position.X + Controller.tileWidth;
                    float nextTilePosY = tileArray[x, y].Position.Y + Controller.tileHeight;

                    float posOffSetX = 10;
                    float posOffSetY = 10;

                    switch (direction)
                    {
                        case Dir.Right:
                            nextTilePosX = tileArray[x, y].Position.X + Controller.tileWidth;
                            break;
                        case Dir.Left:
                            nextTilePosX = tileArray[x, y].Position.X - Controller.tileWidth;
                            posOffSetX *= -1;
                            break;
                        case Dir.Down:
                            nextTilePosY = tileArray[x, y].Position.Y + Controller.tileHeight;
                            break;
                        case Dir.Up:
                            nextTilePosY = tileArray[x, y].Position.Y - Controller.tileHeight;
                            posOffSetY *= -1;
                            break;
                    }

                    float posX = position.X + posOffSetX;
                    float posY = position.Y + posOffSetY;

                    if (direction == Dir.Right || direction == Dir.Down)
                    {
                        if (posX >= tilePosX && posX < nextTilePosX)
                        {
                            if (posY >= tilePosY && posY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                tileArray[(int)previousTile.X, (int)previousTile.Y].tileType = previousTileType;
                                currentTile = new Vector2(x, y);
                                previousTileType = tileArray[(int)currentTile.X, (int)currentTile.Y].tileType;
                                tileArray[(int)currentTile.X, (int)currentTile.Y].tileType = Tile.TileType.Ghost;
                            }
                        }
                    }
                    else if (direction == Dir.Left)
                    {
                        if (posX <= tilePosX && posX > nextTilePosX)
                        {
                            if (posY >= tilePosY && posY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                tileArray[(int)previousTile.X, (int)previousTile.Y].tileType = previousTileType;
                                currentTile = new Vector2(x, y);
                                previousTileType = tileArray[(int)currentTile.X, (int)currentTile.Y].tileType;
                                tileArray[(int)currentTile.X, (int)currentTile.Y].tileType = Tile.TileType.Ghost;
                            }
                        }
                    }
                    else if (direction == Dir.Up)
                    {
                        if (posX >= tilePosX && posX < nextTilePosX)
                        {
                            if (posY <= tilePosY && posY > nextTilePosY)
                            {
                                previousTile = currentTile;
                                tileArray[(int)previousTile.X, (int)previousTile.Y].tileType = previousTileType;
                                currentTile = new Vector2(x, y);
                                previousTileType = tileArray[(int)currentTile.X, (int)currentTile.Y].tileType;
                                tileArray[(int)currentTile.X, (int)currentTile.Y].tileType = Tile.TileType.Ghost;
                            }
                        }
                    }
                }
            }
        }
    }
}
