using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    public class Background
    {
        private Rectangle position;
        private Texture2D texture;

        public Background(Texture2D _texture)
        {
            this.texture = _texture;
            position = Game1.Bounds;
        }

        internal void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            spriteBatch.Draw(texture,Game1.Bounds, Color.White);
        }

        internal void Update(GameTime gameTime)
        {
            
        }
    }
}
