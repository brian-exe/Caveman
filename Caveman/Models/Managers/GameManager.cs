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
        ContentManager contentManager;


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
            this.currentLevel = Level0.Instance;
            contentManager = null;
        }

        public void EndGame()
        {
            this.UnloadContent();
            this.currentLevel = Level0.Instance;
            LoadContent(contentManager);
        }

        internal void LoadContent(ContentManager content)
        {
            if(this.contentManager ==null)
                this.contentManager = content;
            currentLevel.LoadContent(content);
        }

        internal void UnloadContent()
        {
            contentManager.Unload();
            //currentLevel.UnloadContent(contentManager);
        }

        internal void Update(ref GameTime gameTime)
        {
            currentLevel.Update(ref gameTime);
        }

        internal void Start()
        {
            UnloadContent();
            currentLevel = Level1.Instance;
            LoadContent(contentManager);
        }

        internal void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            currentLevel.Draw(ref spriteBatch,ref gameTime);
        }
    }
}
