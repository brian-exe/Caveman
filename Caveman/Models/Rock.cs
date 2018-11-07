using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    class Rock : Sprite
    {
        private float _timer=0.0f;
        Caveman owner;
        public Rock(Vector2 _position, Texture2D _texture, Caveman _owner)
        {
            this.Position = _position;
            this.Color = Color.White;
            this.Texture = _texture;
            this.owner = _owner;

            this.AnimationsDict = new Dictionary<string, Animation> {
                {"Spinning",
                    new Animation(this.Texture,6,0.1f,Animation.Interruptible)
                }
            };

            currentAnimation = this.AnimationsDict["Spinning"];
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            //spriteBatch.Draw(this.Texture, this.Position, this.Color);

            spriteBatch.Draw(currentAnimation.Texture, Position,
                new Rectangle(currentAnimation.CurrentFrame * currentAnimation.FrameWidth, 0, currentAnimation.FrameWidth, currentAnimation.FrameHeight), Color.White);
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
