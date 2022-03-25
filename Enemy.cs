using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Enemy
    {
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        private Vector2 currentTile;
        private Vector2 previousTile;
        private Tile.TileType previousTileType;
        private Dir direction;
        private List<Vector2> pathToPacMan;

        public List<Vector2> PathToPacMan
        {
            get { return pathToPacMan; }
        }

        private int speed = 150;

        private Rectangle[] rectsDown = new Rectangle[2];
        private Rectangle[] rectsUp = new Rectangle[2];
        private Rectangle[] rectsLeft = new Rectangle[2];
        private Rectangle[] rectsRight = new Rectangle[2];

        private int drawOffSetX = -9;
        private int drawOffSetY = -9;

        private SpriteAnimation enemyAnim;

        public Enemy(int tileX, int tileY, Tile[,] tileArray)
        {
            position = tileArray[tileX, tileY].Position;
            currentTile = new Vector2(tileX, tileY);
            previousTile = new Vector2(-1,-1);
            direction = Dir.None;

            rectsDown[0] = new Rectangle(1659, 291, 42, 42);
            rectsDown[1] = new Rectangle(1707, 291, 42, 42);

            rectsUp[0] = new Rectangle(1563, 291, 42, 42);
            rectsUp[1] = new Rectangle(1611, 291, 42, 42);

            rectsLeft[0] = new Rectangle(1467, 291, 42, 42);
            rectsLeft[1] = new Rectangle(1515, 291, 42, 42);

            rectsRight[0] = new Rectangle(1371, 291, 42, 42);
            rectsRight[1] = new Rectangle(1419, 291, 42, 42);

            enemyAnim = new SpriteAnimation(0.08f, rectsRight);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            enemyAnim.Draw(spriteBatch, spriteSheet, new Vector2(position.X + drawOffSetX, position.Y + drawOffSetY));
        }

        public void Update(GameTime gameTime, Tile[,] tileArray, Vector2 playerTilePos)
        {
            updateTilePosition(tileArray);
            enemyAnim.Update(gameTime);

            decideDirection(playerTilePos, tileArray);
            Move(gameTime, tileArray);
        }

        public void decideDirection(Vector2 playerTilePos, Tile[,] tileArray)
        {
            pathToPacMan = Pathfinding.findPath(currentTile, playerTilePos, tileArray);
            if (pathToPacMan.Count == 0) return;

            if (pathToPacMan[0].X > currentTile.X)
            {
                direction = Dir.Right;
                position.Y = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y;
            }
            else if (pathToPacMan[0].X < currentTile.X)
            {
                direction = Dir.Left;
                position.Y = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y;
            }
            else if (pathToPacMan[0].Y > currentTile.Y)
            {
                direction = Dir.Down;
                position.X = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X;
            }
            else if (pathToPacMan[0].Y < currentTile.Y)
            {
                direction = Dir.Up;
                position.X = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X;
            }
        }

        public void Move(GameTime gameTime, Tile[,] tileArray)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (direction)
            {
                case Dir.Right:
                    position.X += speed * dt;
                    enemyAnim.setSourceRects(rectsRight);
                    break;

                case Dir.Left:
                    position.X -= speed * dt;
                    enemyAnim.setSourceRects(rectsLeft);
                    break;

                case Dir.Down:
                    position.Y += speed * dt;
                    enemyAnim.setSourceRects(rectsDown);
                    break;

                case Dir.Up:
                    position.Y -= speed * dt;
                    enemyAnim.setSourceRects(rectsUp);
                    break;

                case Dir.None:
                    position = tileArray[(int)currentTile.X, (int)currentTile.Y].Position;
                    break;
            }
        }

        public void updateTilePosition(Tile[,] tileArray)
        {
            if (!previousTile.Equals(new Vector2(-1,-1)))
                tileArray[(int)previousTile.X, (int)previousTile.Y].tileType = previousTileType;
            tileArray[(int)currentTile.X, (int)currentTile.Y].tileType = Tile.TileType.Player;

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
                                currentTile = new Vector2(x, y);
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
                                previousTileType = tileArray[(int)previousTile.X, (int)previousTile.Y].tileType;
                                currentTile = new Vector2(x, y);
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
                                currentTile = new Vector2(x, y);
                            }
                        }
                    }
                }
            }
        }
    }
}
