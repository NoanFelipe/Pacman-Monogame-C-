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
        private Vector2 position;
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

        Vector2 previousTile;
        Vector2 currentTile;

        private SpriteAnimation playerAnim;

        public Player(int tileX, int tileY, Tile[,] tileArray)
        {
            position = tileArray[tileX, tileY].Position;
            position.X += 14;
            currentTile = new Vector2(tileX , tileY);
            previousTile = new Vector2(tileX - 1, tileY);

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

            playerAnim = new SpriteAnimation(0.08f, rectsRight, 2);
        }

        public Vector2 CurrentTile
        {
            get { return currentTile; }
        }

        public Vector2 PreviousTile
        {
            get { return previousTile; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void setX(float newX)
        {
            position.X = newX;
        }

        public void setY(float newY)
        {
            position.Y = newY;
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

            if (kState.IsKeyDown(Keys.D))
            {
                nextDirection = Dir.Right;
            }
            if (kState.IsKeyDown(Keys.A))
            {
                nextDirection = Dir.Left;
            }
            if (kState.IsKeyDown(Keys.W))
            {
                nextDirection = Dir.Up;
            }
            if (kState.IsKeyDown(Keys.S))
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
                            position.Y = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Left:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            position.Y = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Down:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            position.X = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X+2;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Up:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            position.X = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X+2;
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
                        position.X += speed * dt;
                        playerAnim.setSourceRects(rectsRight);
                    }
                    break;
                case Dir.Left:
                    if (Game1.gameController.isNextTileAvailable(Dir.Left, currentTile))
                    { 
                        position.X -= speed * dt;
                        playerAnim.setSourceRects(rectsLeft);
                    }
                    break;
                case Dir.Down:
                    if (Game1.gameController.isNextTileAvailable(Dir.Down, currentTile))
                    {
                        position.Y += speed * dt;
                        playerAnim.setSourceRects(rectsDown);
                    }
                    break;
                case Dir.Up:
                    if (Game1.gameController.isNextTileAvailable(Dir.Up, currentTile))
                    {
                        position.Y -= speed * dt;
                        playerAnim.setSourceRects(rectsUp);
                    }
                    break;
                case Dir.None:
                    position = tileArray[(int)currentTile.X, (int)currentTile.Y].Position;
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
            playerAnim.Draw(spriteBatch, spriteSheet, new Vector2(position.X - radiusOffSet / 2, position.Y - radiusOffSet / 2 + 1));
        }

        public int checkForTeleportPos(Tile[,] tileArray)
        {
            if (new int[2] {(int)currentTile.X, (int)currentTile.Y}.SequenceEqual(new int[2] { 0, 14 }))
            {
                if (position.X < -30)
                {
                    return 1;
                }
            }else if (new int[2] { (int)currentTile.X, (int)currentTile.Y }.SequenceEqual(new int[2] { Controller.numberOfTilesX - 1, 14 }))
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

        public void updatePlayerTilePosition(Tile[,] tileArray)
        {
            tileArray[(int)previousTile.X, (int)previousTile.Y].tileType = Tile.TileType.None;
            tileArray[(int)currentTile.X, (int)currentTile.Y].tileType = Tile.TileType.Player;

            if (checkForTeleportPos(tileArray) == 1)
            {
                if (direction == Dir.Left)
                    teleport(new Vector2(Game1.windowWidth + 30, position.Y), new Vector2( Controller.numberOfTilesX - 1, 14 ));
            }
            else if (checkForTeleportPos(tileArray) == 2)
            {
                if (direction == Dir.Right)
                    teleport(new Vector2(-30, position.Y), new Vector2( 0 , 14 ));
            }

            for (int x = 0; x < tileArray.GetLength(0); x++)
            {
                for (int y = 0; y < tileArray.GetLength(1); y++)
                {
                    if (Game1.gameController.checkTileType(new Vector2(x,y), Tile.TileType.Player))
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

                    float pacmanPosX = position.X + pacmanPosOffSetX;
                    float pacmanPosY = position.Y + pacmanPosOffSetY;

                    if (direction == Dir.Right || direction == Dir.Down)
                    {
                        if (pacmanPosX >= tilePosX && pacmanPosX < nextTilePosX)
                        {
                            if (pacmanPosY >= tilePosY && pacmanPosY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new Vector2( x, y );
                            }
                        }
                    }else if (direction == Dir.Left)
                    {
                        if (pacmanPosX <= tilePosX && pacmanPosX > nextTilePosX)
                        {
                            if (pacmanPosY >= tilePosY && pacmanPosY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new Vector2(x, y);
                            }
                        }
                    }else if (direction == Dir.Up)
                    {
                        if (pacmanPosX >= tilePosX && pacmanPosX < nextTilePosX)
                        {
                            if (pacmanPosY <= tilePosY && pacmanPosY > nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new Vector2(x, y);
                            }
                        }
                    }
                }
            }
        }

        public void debugPacmanPosition(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.debuggingDot, position, Color.White);
        }
    }
}
