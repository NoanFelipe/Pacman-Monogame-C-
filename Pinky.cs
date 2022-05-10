using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    public class Pinky : Enemy
    {
        public Pinky(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(1, 2);
            type = GhostType.Pinky;

            rectsDown[0] = new Rectangle(1659, 243, 42, 42);
            rectsDown[1] = new Rectangle(1707, 243, 42, 42);

            rectsUp[0] = new Rectangle(1563, 243, 42, 42);
            rectsUp[1] = new Rectangle(1611, 243, 42, 42);

            rectsLeft[0] = new Rectangle(1467, 243, 42, 42);
            rectsLeft[1] = new Rectangle(1515, 243, 42, 42);

            rectsRight[0] = new Rectangle(1371, 243, 42, 42);
            rectsRight[1] = new Rectangle(1419, 243, 42, 42);

            enemyAnim = new SpriteAnimation(0.08f, rectsDown);
        }

        public override Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray)
        {
            Vector2 pos = playerTilePos;
            Dir PlayerDir = playerDir;

            if (PlayerDir == Dir.None)
            {
                PlayerDir = playerLastDir;
            }

            switch (PlayerDir)
            {
                case Dir.Right:
                    pos = new Vector2(playerTilePos.X + 4, playerTilePos.Y);
                    playerLastDir = Dir.Right;
                    break;
                case Dir.Left:
                    pos = new Vector2(playerTilePos.X - 4, playerTilePos.Y);
                    playerLastDir = Dir.Left;
                    break;
                case Dir.Down:
                    pos = new Vector2(playerTilePos.X, playerTilePos.Y + 4);
                    playerLastDir = Dir.Down;
                    break;
                case Dir.Up:
                    pos = new Vector2(playerTilePos.X, playerTilePos.Y - 4);
                    playerLastDir = Dir.Up;
                    break;
            }
            if (pos.X < 0 || pos.Y < 0 || pos.X > Controller.numberOfTilesX - 1 || pos.Y > Controller.numberOfTilesY - 1)
            {
                return playerTilePos;
            }
            if (tileArray[(int)pos.X, (int)pos.Y].tileType == Tile.TileType.Wall)
            {
                return playerTilePos;
            }
            return pos;
        }
    }
}
