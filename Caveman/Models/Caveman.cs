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
    public class Caveman : Sprite
    {
        float _timer;
        public Caveman(Vector2 _position/*, Texture2D _texture, Color _color, Dictionary<string, Animation> _animations*/)
            //: base(_position,_texture, _color, _animations)
        {
            this.Position = _position;
            this.Color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {
            Dictionary<String, Animation> animDict = new Dictionary<string, Animation> {
                {"Standing",
                    new Animation(content.Load<Texture2D>("CavemanStanding_sm"),4,0.5f)
                },
                {"Walking",
                    new Animation(content.Load<Texture2D>("CavemanRunning"),7,0.2f)
                },
                {"Jumping",
                    new Animation(content.Load<Texture2D>("CavemanJumping"),4,0.2f)
                },
                {"Hitting",
                    new Animation(content.Load<Texture2D>("CavemanHitting"),5,0.2f)
                },
            };

            this.AnimationsDict = animDict;
        }

        public override void Update(ref GameTime gameTime)
        {
            currentAnimation = AnimationsDict["Standing"];
            //currentAnimation.CurrentFrame = 0;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > currentAnimation.FrameSpeed)
            {
                _timer = 0f;

                currentAnimation.CurrentFrame++;

                if (currentAnimation.CurrentFrame >= currentAnimation.FrameCount)
                    currentAnimation.CurrentFrame = 0;
            }
        }


        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            //spriteBatch.Draw(this.currentAnimati, this.Position, this.Color);

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
