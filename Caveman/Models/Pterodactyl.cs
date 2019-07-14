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
    class Pterodactyl : Sprite,IMortable,IHarmful
    {
        float velocity = 0.0f;
        float _timer = 0.0f;

        public bool Died { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }

        public Pterodactyl()
        {
            this.Position = new Vector2(1000, 0);
            this.Color = Color.White;
            velocity = -7.0f;
            this.Died = false;
            this.Health = 100;
            this.Damage = 20;
        }

        public override void LoadContent(ContentManager content)
        {
            this.AnimationsDict = new Dictionary<string, Animation> {
                {"FlyingLeft",
                    new Animation(content.Load<Texture2D>("pterodactilo_sm"),7,0.2f,Animation.Interruptible)
                },
                { "FlyingRight",
                    new Animation(content.Load<Texture2D>("pterodactilo-right_sm"),7,0.2f,Animation.Interruptible)
                }
            };

            currentAnimation = this.AnimationsDict["FlyingLeft"];
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            this.Texture = currentAnimation.Texture;
            spriteBatch.Draw(this.Texture, Position,new Rectangle(currentAnimation.CurrentFrame * currentAnimation.FrameWidth, 0, currentAnimation.FrameWidth, currentAnimation.FrameHeight), Color.White);
        }

        public override void Update(ref GameTime gameTime)
        {
            if (this.Health <= 0)
                this.Died = true;

            if (!this.Died)
            {
                this.Position = new Vector2(Position.X + velocity, this.Position.Y);
                if (this.Position.X < -200 || this.Position.X > 1000)
                {
                    velocity = velocity * -1;
                    if (velocity < 0)
                    {
                        currentAnimation = this.AnimationsDict["FlyingLeft"];
                    }
                    else
                    {
                        currentAnimation = this.AnimationsDict["FlyingRight"];
                    }

                }

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

        public void ReceiveHit(IHarmful harm)
        {
            this.Health -= harm.Damage;
        }
    }
}
