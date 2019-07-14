using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    public class Egg : Sprite, IHarmful
    {
        public float Damage { get; set; }

        private float _timer = 0.0f;
        Pterodactyl owner;

        public Egg(Vector2 _position, Texture2D _texture, Pterodactyl _owner)
        {
            this.Position = _position;
            this.Texture = _texture;
            this.Color = Color.White;
            this.owner = _owner;
            this.Damage = 30;

            //this.AnimationsDict = new Dictionary<string, Animation> {
            //    {"Spinning",
            //        new Animation(this.Texture,6,0.1f,Animation.Interruptible)
            //    }
            //};

            currentAnimation = this.AnimationsDict["Spinning"];
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            this.Texture = currentAnimation.Texture;
            spriteBatch.Draw(this.Texture, Position, Color.White);
        }

        internal void CheckColissions(Caveman caveman)
        {
            if (caveman.Rectangle.Intersects(this.Rectangle))
            {
                owner.MarkAsRemovedEgg(this);
                (caveman as IMortable).ReceiveHit(this);
            }
        }

        public override void Update(ref GameTime gameTime)
        {
            this.Position = new Vector2(Position.X, this.Position.Y+5);
            if (this.Position.Y > Game1.Bounds.Height)
                owner.MarkAsRemovedEgg(this);
        }

    }
}
}
