using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    public class Clyde : Enemy
    {
        public Clyde(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(2, 29);
            type = GhostType.Clyde;

            rectsDown[0] = new Rectangle(1659, 339, 42, 42);
            rectsDown[1] = new Rectangle(1707, 339, 42, 42);

            rectsUp[0] = new Rectangle(1563, 339, 42, 42);
            rectsUp[1] = new Rectangle(1611, 339, 42, 42);

            rectsLeft[0] = new Rectangle(1467, 339, 42, 42);
            rectsLeft[1] = new Rectangle(1515, 339, 42, 42);

            rectsRight[0] = new Rectangle(1371, 339, 42, 42);
            rectsRight[1] = new Rectangle(1419, 339, 42, 42);
        }

        public override Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray)
        {
            if (Tile.getDistanceBetweenTiles(playerTilePos, currentTile) > 8)
            {
                return playerTilePos;
            }
            return ScatterTargetTile;
        }
    }
}
