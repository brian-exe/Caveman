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
        private float VelocityX = 2;
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
            this.AnimationsDict = new Dictionary<string, Animation> {
                {"Standing",
                    new Animation(content.Load<Texture2D>("CavemanStanding_sm"),4,0.4f,Animation.Interruptible)
                },
                {"Running_Right",
                    new Animation(content.Load<Texture2D>("CavemanRunning_sm"),6,0.2f,Animation.Interruptible)
                },
                {"Running_left",
                    new Animation(content.Load<Texture2D>("CavemanRunningLeft_sm"),6,0.2f,Animation.Interruptible)
                },
                {"Jumping",
                    new Animation(content.Load<Texture2D>("CavemanJumping_sm"),7,0.2f,Animation.NotInterruptible)
                },
                {"Hitting",
                    new Animation(content.Load<Texture2D>("CavemanHitting_sm"),5,0.1f,Animation.NotInterruptible)
                },
                {"Throwing",
                    new Animation(content.Load<Texture2D>("CavemanThrowing"),3,0.2f,Animation.NotInterruptible)
                },
            };

            this.currentAnimation = AnimationsDict["Standing"];
        }

        public override void Update(ref GameTime gameTime)
        {
            ExecuteMoves();
            if (currentAnimation.IsInterruptible || !currentAnimation.IsLooping)
            {
                currentAnimation = AnimationsDict["Standing"];
                currentAnimation.IsLooping = true;
                SetAnimation();
            }

            #region Gravity and Y Coordinates
            VelocityY += gravity;
            this.Position += new Vector2(0, VelocityY);

            if (this.Position.Y > 350)
            {
                this.Position = new Vector2(this.Position.X, 350);
                VelocityY = 0.0f;
                Jumping = false;
            }
            #endregion

            #region Manage Animation Frames
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
            #endregion
        }
        private void ExecuteMoves()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                MoveForward();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                MoveBackwards();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                StartJumping();
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                EndJumping();
            }
        }
        private void SetAnimation()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                currentAnimation = AnimationsDict["Hitting"];
                currentAnimation.IsLooping = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.X)) {
                currentAnimation = AnimationsDict["Throwing"];
                currentAnimation.IsLooping = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                currentAnimation = AnimationsDict["Running_Right"];
                currentAnimation.IsLooping = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                currentAnimation = AnimationsDict["Running_left"];
                currentAnimation.IsLooping = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                currentAnimation = AnimationsDict["Jumping"];
                currentAnimation.IsLooping = true;
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
            this.Position = new Vector2(this.Position.X - VelocityX, this.Position.Y);
            if(this.Position.X <0)
                this.Position = new Vector2(0, this.Position.Y);
        }

        internal void MoveForward()
        {
            this.Position = new Vector2(this.Position.X + VelocityX, this.Position.Y);
            if (this.Position.X > Game1.Bounds.Width)
                this.Position = new Vector2(Game1.Bounds.Width, this.Position.Y);
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
