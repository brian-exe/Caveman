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

        public float Velocity = 2.0f;

        public Background(Texture2D _texture0, Texture2D _texture1, Texture2D _texture2)
        {
            //this.layer0 = new BaseSprite { Position = new Rectangle(0, 0, Game1.Bounds.Width/*_texture0.Width*/, _texture0.Height), Texture= _texture0,Color = Color.White };
            //this.layer1 = new BaseSprite { Position = new Rectangle(0, -5, Game1.Bounds.Width/*_texture1.Width*/, _texture1.Height), Texture = _texture1, Color = Color.White };
            //this.layer2 = new BaseSprite { Position = new Rectangle(0, 260, _texture2.Width, _texture2.Height), Texture = _texture2, Color = Color.White };

            this.layer0 = new BaseSprite { Position = new Vector2(0,0), Texture= _texture0,Color = Color.White };
            this.layer1 = new BaseSprite { Position = new Vector2(0, -5), Texture = _texture1, Color = Color.White };
            this.layer2 = new BaseSprite { Position = new Vector2(0, 260), Texture = _texture2, Color = Color.White };
            position = Game1.Bounds;
        }

        internal void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            spriteBatch.Draw(layer0.Texture, layer0.Position, layer0.Color);
            spriteBatch.Draw(layer1.Texture, layer1.Position, layer1.Color);
            spriteBatch.Draw(layer2.Texture, layer2.Position, layer2.Color);

            //spriteBatch.Draw(layer0.Texture, new Vector2(layer0.Position.X, layer0.Position.Y), layer0.Color);
            //spriteBatch.Draw(layer1.Texture, new Vector2(layer1.Position.X, layer1.Position.Y), layer1.Color);
            //spriteBatch.Draw(layer2.Texture, new Vector2(layer2.Position.X, layer2.Position.Y), layer2.Color);

            Vector2 rec2 = layer2.Position;
            rec2.X += layer2.Texture.Width;
            spriteBatch.Draw(layer2.Texture, rec2, layer2.Color);

        }

        internal void Update(GameTime gameTime)
        {
            //layer2.Position = new Rectangle(layer2.Position.X - 1, layer2.Position.Y, layer2.Position.Width, layer2.Position.Height);

            if (layer2.Position.X <= -layer2.Texture.Width)
                layer2.Position = new Vector2(0,layer2.Position.Y);
            //new Rectangle(0,
            //                                layer2.Position.Y,
            //                                layer2.Position.Width,
            //                                layer2.Position.Height);

            else
                layer2.Position = new Vector2(layer2.Position.X - Velocity, layer2.Position.Y);
            //layer2.Position = new Rectangle(layer2.Position.X - 1,
            //                            layer2.Position.Y,
            //                            layer2.Position.Width,
            //                            layer2.Position.Height);
        }
    }
}
