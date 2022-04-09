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
    }
}
