using Caveman.Models.Managers;
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
    public class Level1:Level
    {
        Caveman caveman;
        private static Level1 instance = null;
        public static Level1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Level1();
                }
                return instance;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            #region Background
            Texture2D _texture0 = content.Load<Texture2D>("./Backgrounds/posible-bg3");
            Texture2D _texture1 = content.Load<Texture2D>("./Backgrounds/treeses_pasto4");

            this.background = new Background(_texture0,_texture1);

            #endregion

            caveman = new Caveman(new Vector2(0, 350));
            caveman.LoadContent(content);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            caveman.Update(ref gameTime);
            if (caveman.Position.X >= Game1.Bounds.Width / 2)
            {
                caveman.MoveBackwards();
                background.MoveForward();
            }
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            base.Draw(ref spriteBatch, ref gameTime);
            caveman.Draw(ref spriteBatch, ref gameTime);
        }
    }
}
