using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    public class Blinky : Enemy
    {
        public Blinky(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(26, 2);
            type = GhostType.Blinky;

            rectsDown[0] = new Rectangle(1659, 195, 42, 42);
            rectsDown[1] = new Rectangle(1707, 195, 42, 42);

            rectsUp[0] = new Rectangle(1563, 195, 42, 42);
            rectsUp[1] = new Rectangle(1611, 195, 42, 42);

            rectsLeft[0] = new Rectangle(1467, 195, 42, 42);
            rectsLeft[1] = new Rectangle(1515, 195, 42, 42);

            rectsRight[0] = new Rectangle(1371, 195, 42, 42);
            rectsRight[1] = new Rectangle(1419, 195, 42, 42);

            enemyAnim = new SpriteAnimation(0.08f, rectsLeft);
        }
    }
}
