using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Enemy
    {
        private Vector2 position;
        private Dir direction;

        private int speed = 150;

        private Rectangle[] rectsDown = new Rectangle[3];
        private Rectangle[] rectsUp = new Rectangle[3];
        private Rectangle[] rectsLeft = new Rectangle[3];
        private Rectangle[] rectsRight = new Rectangle[3];

        private SpriteAnimation enemyAnim;

        public Vector2 Position
        {
            get{ return position; }
        }
    }
}
