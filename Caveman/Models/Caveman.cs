using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    public class Caveman : Sprite
    {
        float _timer;
        private float VelocityX;
        private float VelocityY =0.0f;
        private float gravity = 0.5f;
        private bool Jumping = false;

        public Caveman(Vector2 _position)
        {
            this.Position = _position;
            this.Color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {
            Dictionary<String, Animation> animDict = new Dictionary<string, Animation> {
                {"Standing",
                    new Animation(content.Load<Texture2D>("CavemanStanding_sm"),4,0.4f,Animation.Loopeable)
                },
                {"Running",
                    new Animation(content.Load<Texture2D>("CavemanRunning_sm"),6,0.2f,Animation.Loopeable)
                },
                {"Running_left",
                    new Animation(content.Load<Texture2D>("CavemanRunningLeft_sm"),6,0.2f,Animation.Loopeable)
                },
                {"Jumping",
                    new Animation(content.Load<Texture2D>("CavemanJumping_sm"),7,0.3f,Animation.NotLoopeable)
                },
                {"Hitting",
                    new Animation(content.Load<Texture2D>("CavemanHitting_sm"),5,0.1f,Animation.NotLoopeable)
                },
            };

            this.AnimationsDict = animDict;
        }

        public override void Update(ref GameTime gameTime)
        {
            currentAnimation = AnimationsDict["Standing"];
            currentAnimation.IsLooping = false;

            #region Keys
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                currentAnimation = AnimationsDict["Hitting"];
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (!currentAnimation.IsLooping)
                {
                    this.Position = new Vector2(this.Position.X + 2, this.Position.Y);
                    currentAnimation = AnimationsDict["Running"];
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.Position = new Vector2(this.Position.X - 2, this.Position.Y);
                currentAnimation = AnimationsDict["Running_left"];
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                currentAnimation = AnimationsDict["Jumping"];
                StartJumping();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space)) {
                EndJumping();
            }
            #endregion

            VelocityY += gravity;
            this.Position += new Vector2(0, VelocityY);

            if (this.Position.Y > 350)
            {
                this.Position = new Vector2(this.Position.X, 350);
                VelocityY = 0.0f;
                Jumping = false;
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

        private void StartJumping()
        {
            if (!Jumping)
            {
                VelocityY = -12.0f;
                Jumping = true;
            }
        }

        private void EndJumping()
        {
            if (VelocityY < -6.0f)
            {
                VelocityY = -6.0f;
                
            }
        }

        internal void MoveBackwards()
        {
            this.Position = new Vector2(this.Position.X -2, this.Position.Y);
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {

            spriteBatch.Draw(currentAnimation.Texture,
                 Position,
                 new Rectangle(currentAnimation.CurrentFrame * currentAnimation.FrameWidth,
                               0,
                               currentAnimation.FrameWidth,
                               currentAnimation.FrameHeight),
                 Color.White);
        }
    }
}
