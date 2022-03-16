using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Player
    {
        private Vector2 gridPosition;
        private Dir direction = Dir.Right;
        private Dir nextDirection = Dir.None;
        private int speed = 150;
        private int radiusOffSet = 19;
        private static Rectangle lastRect = new Rectangle(1467, 3, 39, 39);
        private static Rectangle[] rectsDown = new Rectangle[3];
        private static Rectangle[] rectsUp = new Rectangle[3];
        private static Rectangle[] rectsLeft = new Rectangle[3];
        private static Rectangle[] rectsRight = new Rectangle[3];

        bool canMove = true;
        float canMoveTimer = 0;
        float TimerThreshold = 0.2f;

        int[] previousTile;
        int[] currentTile;

        private SpriteAnimation playerAnim;

        public Player(int tileX, int tileY, Tile[,] tileArray)
        {
            gridPosition = tileArray[tileX, tileY].Position;
            gridPosition.X += 14;
            currentTile = new int[2] { tileX , tileY };
            previousTile = new int[2] { tileX - 1, tileY };

            rectsDown[0] = new Rectangle(1371, 147, 39, 39);
            rectsDown[1] = new Rectangle(1419, 147, 39, 39);
            rectsDown[2] = lastRect;

            rectsUp[0] = new Rectangle(1371, 99, 39, 39);
            rectsUp[1] = new Rectangle(1419, 99, 39, 39);
            rectsUp[2] = lastRect;

            rectsLeft[0] = new Rectangle(1371, 51, 39, 39);
            rectsLeft[1] = new Rectangle(1419, 51, 39, 39);
            rectsLeft[2] = lastRect;

            rectsRight[0] = new Rectangle(1371, 3, 39, 39);
            rectsRight[1] = new Rectangle(1419, 3, 39, 39);
            rectsRight[2] = lastRect;

            playerAnim = new SpriteAnimation(0.08f, rectsRight);
        }

        public Vector2 GridPosition
        {
            get { return gridPosition; }
        }

        public void setX(float newX)
        {
            gridPosition.X = newX;
        }

        public void setY(float newY)
        {
            gridPosition.Y = newY;
        }

        public void Update(GameTime gameTime, Tile[,] tileArray)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!canMove)
            {
                canMoveTimer += dt;
                if (canMoveTimer >= TimerThreshold)
                {
                    canMove = true;
                    canMoveTimer = 0;
                }
            }
            playerAnim.Update(gameTime);

            if (kState.IsKeyDown(Keys.D) && direction != Dir.Left && playerAnim.SourceRectangles != rectsLeft)
            {
                nextDirection = Dir.Right;
            }
            if (kState.IsKeyDown(Keys.A) && direction != Dir.Right && playerAnim.SourceRectangles != rectsRight)
            {
                nextDirection = Dir.Left;
            }
            if (kState.IsKeyDown(Keys.W) && direction != Dir.Down && playerAnim.SourceRectangles != rectsDown)
            {
                nextDirection = Dir.Up;
            }
            if (kState.IsKeyDown(Keys.S) && direction != Dir.Up && playerAnim.SourceRectangles != rectsUp)
            {
                nextDirection = Dir.Down;
            }

            if (canMove)
            {
                switch (nextDirection)
                {
                    case Dir.Right:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            gridPosition.Y = tileArray[currentTile[0], currentTile[1]].Position.Y;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Left:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            gridPosition.Y = tileArray[currentTile[0], currentTile[1]].Position.Y;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Down:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            gridPosition.X = tileArray[currentTile[0], currentTile[1]].Position.X+2;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Up:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            gridPosition.X = tileArray[currentTile[0], currentTile[1]].Position.X+2;
                            nextDirection = Dir.None;
                        }
                        break;
                }
            }

            if (!Game1.gameController.isNextTileAvailable(direction, currentTile))
                direction = Dir.None;

            switch (direction)
            {
                case Dir.Right:
                    if (Game1.gameController.isNextTileAvailable(Dir.Right, currentTile))
                    {
                        gridPosition.X += speed * dt;
                        playerAnim.setSourceRects(rectsRight);
                    }
                    break;
                case Dir.Left:
                    if (Game1.gameController.isNextTileAvailable(Dir.Left, currentTile))
                    { 
                        gridPosition.X -= speed * dt;
                        playerAnim.setSourceRects(rectsLeft);
                    }
                    break;
                case Dir.Down:
                    if (Game1.gameController.isNextTileAvailable(Dir.Down, currentTile))
                    {
                        gridPosition.Y += speed * dt;
                        playerAnim.setSourceRects(rectsDown);
                    }
                    break;
                case Dir.Up:
                    if (Game1.gameController.isNextTileAvailable(Dir.Up, currentTile))
                    {
                        gridPosition.Y -= speed * dt;
                        playerAnim.setSourceRects(rectsUp);
                    }
                    break;
                case Dir.None:
                    gridPosition = tileArray[currentTile[0], currentTile[1]].Position;
                    break;
            }
        }

        public void eatSnack(int listPosition)
        {
            Game1.gameController.snackList.RemoveAt(listPosition);
            Game1.score += 10;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            playerAnim.Draw(spriteBatch, spriteSheet, new Vector2(gridPosition.X - radiusOffSet / 2, gridPosition.Y - radiusOffSet / 2 + 1));
        }

        public int checkForTeleportPos(Tile[,] tileArray)
        {
            if (currentTile.SequenceEqual(new int[2] { 0, 14 }))
            {
                if (gridPosition.X < -30)
                {
                    return 1;
                }
            }else if (currentTile.SequenceEqual(new int[2] { Controller.numberOfTilesX - 1, 14 }))
            {
                if (gridPosition.X > tileArray[currentTile[0], currentTile[1]].Position.X + 30)
                {
                    return 2;
                }
            }
            return 0;
        }

        public void teleport(Vector2 pos, int[] tilePos)
        {
            gridPosition = pos;
            previousTile = currentTile;
            currentTile = tilePos;
        }

        public void updatePlayerTilePosition(Tile[,] tileArray)
        {
            tileArray[previousTile[0], previousTile[1]].tileType = Tile.TileType.None;
            tileArray[currentTile[0], currentTile[1]].tileType = Tile.TileType.Player;

            if (checkForTeleportPos(tileArray) == 1)
            {
                if (direction == Dir.Left)
                    teleport(new Vector2(Game1.windowWidth + 30, gridPosition.Y), new int[2] { Controller.numberOfTilesX - 1, 14 });
            }
            else if (checkForTeleportPos(tileArray) == 2)
            {
                if (direction == Dir.Right)
                    teleport(new Vector2(-30, gridPosition.Y), new int[2] { 0 , 14 });
            }

            for (int x = 0; x < tileArray.GetLength(0); x++)
            {
                for (int y = 0; y < tileArray.GetLength(1); y++)
                {
                    if (Game1.gameController.checkTileType(new int[] { x, y }, Tile.TileType.Player))
                    {
                        int snackListPos = Game1.gameController.findSnackListPosition(tileArray[x,y].Position);
                        if (snackListPos != -1)
                        {
                            eatSnack(snackListPos);
                        }
                    }

                    float tilePosX = tileArray[x, y].Position.X;
                    float tilePosY = tileArray[x, y].Position.Y;

                    float nextTilePosX = tileArray[x, y].Position.X + Controller.tileWidth;
                    float nextTilePosY = tileArray[x, y].Position.Y + Controller.tileHeight;

                    float pacmanPosOffSetX = radiusOffSet / 2;
                    float pacmanPosOffSetY = radiusOffSet / 2;

                    switch (direction)
                    {
                        case Dir.Right:
                            nextTilePosX = tileArray[x, y].Position.X + Controller.tileWidth;
                            break;
                        case Dir.Left:
                            nextTilePosX = tileArray[x, y].Position.X - Controller.tileWidth;
                            pacmanPosOffSetX *= -1;
                            break;
                        case Dir.Down:
                            nextTilePosY = tileArray[x, y].Position.Y + Controller.tileHeight;
                            break;
                        case Dir.Up:
                            nextTilePosY = tileArray[x, y].Position.Y - Controller.tileHeight;
                            pacmanPosOffSetY *= -1;
                            break;
                    }

                    float pacmanPosX = gridPosition.X + pacmanPosOffSetX;
                    float pacmanPosY = gridPosition.Y + pacmanPosOffSetY;

                    if (direction == Dir.Right || direction == Dir.Down)
                    {
                        if (pacmanPosX >= tilePosX && pacmanPosX < nextTilePosX)
                        {
                            if (pacmanPosY >= tilePosY && pacmanPosY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new int[2] { x, y };
                            }
                        }
                    }else if (direction == Dir.Left)
                    {
                        if (pacmanPosX <= tilePosX && pacmanPosX > nextTilePosX)
                        {
                            if (pacmanPosY >= tilePosY && pacmanPosY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new int[2] { x, y };
                            }
                        }
                    }else if (direction == Dir.Up)
                    {
                        if (pacmanPosX >= tilePosX && pacmanPosX < nextTilePosX)
                        {
                            if (pacmanPosY <= tilePosY && pacmanPosY > nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new int[2] { x, y };
                            }
                        }
                    }
                }
            }
        }

        public void debugPacmanPosition(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.debuggingDot, gridPosition, Color.White);
        }
    }
}
