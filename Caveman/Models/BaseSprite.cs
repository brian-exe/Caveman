using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    public class BaseSprite
    {
        //public Rectangle Position { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; } 

        public BaseSprite(Vector2 _position, Texture2D _texture, Color _color)
        {
            this.Position = _position;
            this.Texture = _texture;
            this.Color = _color;
        }

        public BaseSprite()
        {
        }
    }
}
