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

            rectsDown[0] = new Rectangle(1659, 291, 42, 42);
            rectsDown[1] = new Rectangle(1707, 291, 42, 42);

            rectsUp[0] = new Rectangle(1563, 291, 42, 42);
            rectsUp[1] = new Rectangle(1611, 291, 42, 42);

            rectsLeft[0] = new Rectangle(1467, 291, 42, 42);
            rectsLeft[1] = new Rectangle(1515, 291, 42, 42);

            rectsRight[0] = new Rectangle(1371, 291, 42, 42);
            rectsRight[1] = new Rectangle(1419, 291, 42, 42);
        }
    }
}
