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
    public class Caveman : Sprite, IMortable
    {
        float _timer;
        private float VelocityX = 4;
        private float VelocityY =0.0f;
        private float gravity = 0.5f;
        public float score = 0.0f;
        private bool Jumping = false;
        private bool Touched = false;
        private float _timerTouch = 0.0f;
        private Texture2D rockTexture;

        private List<Rock> rocks;
        private List<Rock> removedRocks;
        private Rock auxRock = null;

        public bool Died { get; set; }
        public float Health { get ; set; }

        public Caveman(Vector2 _position)
        {
            this.Position = _position;
            this.Color = Color.White;
            this.rocks = new List<Rock>();
            this.removedRocks = new List<Rock>();
            this.Died = false;
            this.Health = 100;
            this.score = 0;
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

            this.rockTexture = content.Load<Texture2D>("spinning-rock_sm");
        }

        internal void CheckColissions(List<Sprite> enemies)
        {
            foreach(Sprite e in enemies)
            {
                if (e.Rectangle.Intersects(this.Rectangle) && !Touched)
                {
                    (this as IMortable).ReceiveHit(e as IHarmful);
                    Touched = true;
                }
            }

            foreach(Rock r in rocks)
            {
                r.CheckColissions(enemies);
            }
        }

        internal void MarkAsRemovedRock(Rock rock)
        {
            this.removedRocks.Add(rock);
        }

        internal void RemoveRocks()
        {
            if(removedRocks.Count() > 0)
            {
                this.rocks.RemoveAll(c => this.removedRocks.Contains(c));
                this.removedRocks.RemoveAll(c => true);
            }
        }
        public override void Update(ref GameTime gameTime)
        {
            if (this.Health <= 0)
                this.Died = true;

            if (Touched)
            {
                _timerTouch += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timerTouch >= 2)
                {
                    Touched = false;
                    _timerTouch = 0.0f;
                }
            }

            if (!this.Died)
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

                if (this.Position.Y < -10)
                    this.Position = new Vector2(this.Position.X, -10);
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

                UpdateWeapons(ref gameTime);
                RemoveRocks();
            }

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


            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                LoadRock();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.X))
            {
                ThrowRock();
            }
        }

        private void ThrowRock()
        {
            if(auxRock != null)
            {
                this.rocks.Add(auxRock);
                auxRock = null;
            }
        }

        private void LoadRock()
        {
            auxRock = new Rock(this.Position, this.rockTexture, this);
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
            if (/*!Jumping*/true)
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
            this.Texture = currentAnimation.Texture;
            spriteBatch.Draw(this.Texture,
                 Position,
                 new Rectangle(currentAnimation.CurrentFrame * currentAnimation.FrameWidth,
                               0,
                               currentAnimation.FrameWidth,
                               currentAnimation.FrameHeight),
                 Color.White);

            DrawWeapons(ref spriteBatch, ref gameTime);
        }

        private void DrawWeapons(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            foreach(Rock r in this.rocks)
            {
                r.Draw(ref spriteBatch, ref gameTime);
            }
        }

        private void UpdateWeapons(ref GameTime gameTime)
        {
            foreach (Rock r in this.rocks)
            {
                r.Update(ref gameTime);
            }
        }

        public void ReceiveHit(IHarmful harm)
        {
            this.Health -= harm.Damage;
        }

        public void ReceivePoints(float _score)
        {
            this.score += _score;
        }
    }
}
