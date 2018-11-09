using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    class Rock : Sprite,IHarmful
    {
        private float _timer=0.0f;
        Caveman owner;

        public float Damage { get; set; }

        public Rock(Vector2 _position, Texture2D _texture, Caveman _owner)
        {
            this.Position = _position;
            this.Color = Color.White;
            this.Texture = _texture;
            this.owner = _owner;
            this.Damage = 30;

            this.AnimationsDict = new Dictionary<string, Animation> {
                {"Spinning",
                    new Animation(this.Texture,6,0.1f,Animation.Interruptible)
                }
            };

            currentAnimation = this.AnimationsDict["Spinning"];
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            this.Texture = currentAnimation.Texture;
            spriteBatch.Draw(this.Texture, Position,
                new Rectangle(currentAnimation.CurrentFrame * currentAnimation.FrameWidth, 0, currentAnimation.FrameWidth, currentAnimation.FrameHeight), Color.White);
        }

        internal void CheckColissions(List<Sprite> enemies)
        {
            foreach (Sprite e in enemies)
            {
                if (e.Rectangle.Intersects(this.Rectangle))
                {
                    owner.MarkAsRemovedRock(this);
                    (e as IMortable).ReceiveHit(this);
                }
            }
        }

        public override void Update(ref GameTime gameTime)
        {
            this.Position = new Vector2(Position.X + 5, this.Position.Y);
            if (this.Position.X > Game1.Bounds.Width)
                owner.MarkAsRemovedRock(this);


            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > currentAnimation.FrameSpeed)
            {
                _timer = 0f;

                currentAnimation.CurrentFrame++;

                if (currentAnimation.CurrentFrame >= currentAnimation.FrameCount)
                {
                    currentAnimation.CurrentFrame = 0;
                    currentAnimation.IsLooping = false;
                }
            }
        }

    }
}
