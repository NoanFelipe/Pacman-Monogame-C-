using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    public class Inky : Enemy
    {
        public Inky(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(25, 29);
            type = GhostType.Inky;

            rectsDown[0] = new Rectangle(1659, 291, 42, 42);
            rectsDown[1] = new Rectangle(1707, 291, 42, 42);

            rectsUp[0] = new Rectangle(1563, 291, 42, 42);
            rectsUp[1] = new Rectangle(1611, 291, 42, 42);

            rectsLeft[0] = new Rectangle(1467, 291, 42, 42);
            rectsLeft[1] = new Rectangle(1515, 291, 42, 42);

            rectsRight[0] = new Rectangle(1371, 291, 42, 42);
            rectsRight[1] = new Rectangle(1419, 291, 42, 42);
        }

        public override Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray, Vector2 blinkyPos)
        {
            Dir PlayerDir = playerDir;
            Vector2 PacmanPos = playerTilePos;
            Vector2 BlinkyPos = blinkyPos;

            if (PlayerDir == Dir.None)
            {
                PlayerDir = playerLastDir;
            }

            Vector2 finalTarget = new Vector2(0,0);

            switch (PlayerDir)
            {
                case Dir.Down:
                    finalTarget.Y += 2;
                    playerLastDir = Dir.Down;
                    break;
                case Dir.Up:
                    finalTarget.Y -= 2;
                    playerLastDir = Dir.Up;
                    break;
                case Dir.Left:
                    finalTarget.X -= 2;
                    playerLastDir = Dir.Left;
                    break;
                case Dir.Right:
                    finalTarget.X += 2;
                    playerLastDir = Dir.Right;
                    break;
            }


            if (PacmanPos.X < BlinkyPos.X)
            {
                finalTarget.X = BlinkyPos.X - PacmanPos.X;
            }
            else
            {
                finalTarget.X = PacmanPos.X - BlinkyPos.X;
            }

            if (PacmanPos.Y < BlinkyPos.Y)
            {
                finalTarget.Y = BlinkyPos.Y - PacmanPos.Y;
            }
            else
            {
                finalTarget.Y = PacmanPos.Y - BlinkyPos.Y;
            }

            finalTarget *= 2;

            finalTarget.X += currentTile.X;
            finalTarget.Y += currentTile.Y;

            if (finalTarget.X < 0 || finalTarget.Y < 0 || finalTarget.X > Controller.numberOfTilesX - 1 || finalTarget.Y > Controller.numberOfTilesY - 1)
            {
                return playerTilePos;
            }
            if (tileArray[(int)finalTarget.X, (int)finalTarget.Y].tileType == Tile.TileType.Wall)
            {
                return playerTilePos;
            }

            return finalTarget;
        }
    }
}
