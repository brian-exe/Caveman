using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Caveman.Models.Managers
{
    public class GameManager
    {
        Level currentLevel;

        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public GameManager()
        {
            this.currentLevel = new Level1();
        }

        internal void LoadContent(ContentManager content)
        {
            currentLevel.LoadContent(content);
        }

        internal void UnloadContent(ContentManager content)
        {
            currentLevel.UnloadContent(content);
        }

        internal void Update(ref GameTime gameTime)
        {
            currentLevel.Update(ref gameTime);
        }

        internal void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            currentLevel.Draw(ref spriteBatch,ref gameTime);
        }
    }
}
