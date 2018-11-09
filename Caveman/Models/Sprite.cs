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
    public class Sprite : BaseSprite
    {
        protected Dictionary<string, Animation> AnimationsDict;
        public Animation currentAnimation;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,this.currentAnimation.FrameWidth, this.Texture.Height);
            }
        }

        public virtual void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public virtual void UnloadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(ref GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
