using Caveman.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    public class Level
    {
        protected GameManager gameManager;
        public Background background { get; set; }
        public Song Theme { get; set; }

        public virtual void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public virtual void UnloadContent(ContentManager content)
        {
        }

        public virtual void Update(ref GameTime gameTime)
        {
            this.background.Update(gameTime);
        }

        public virtual void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            this.background.Draw(ref spriteBatch, ref gameTime);
        }
    }
}
