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
        private BaseSprite layer0;
        private BaseSprite layer1;
        private BaseSprite layer2;
        public int Rounds { get; set; }

        public float Velocity = 2.0f;

        public Background(Texture2D _texture0, Texture2D _texture1)
        {
            this.layer0 = new BaseSprite { Position = new Vector2(0,0), Texture= _texture0,Color = Color.White };
            this.layer2 = new BaseSprite { Position = new Vector2(0, 260), Texture = _texture1, Color = Color.White };
            position = Game1.Bounds;
            this.Rounds = 0;
        }

        internal void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            spriteBatch.Draw(layer0.Texture, layer0.Position, layer0.Color);
            spriteBatch.Draw(layer2.Texture, layer2.Position, layer2.Color);

            Vector2 rec2 = layer2.Position;
            rec2.X += layer2.Texture.Width;
            spriteBatch.Draw(layer2.Texture, rec2, layer2.Color);

        }

        internal void Update(GameTime gameTime)
        {

        }

        public void MoveForward()
        {
            if (layer2.Position.X <= -layer2.Texture.Width)
            {
                layer2.Position = new Vector2(0, layer2.Position.Y);
                this.Rounds += 1;
            }

            else
                layer2.Position = new Vector2(layer2.Position.X - Velocity, layer2.Position.Y);
        }
    }
}
