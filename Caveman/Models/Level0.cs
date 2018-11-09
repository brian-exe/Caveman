using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caveman.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Caveman.Models
{
    public class Level0: Level
    {

        Texture2D title;
        SpriteFont font;

        Song song;
        private static Level0 instance = null;
        public static Level0 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Level0();
                }
                return instance;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            this.title = content.Load<Texture2D>("caveman-word");
            this.song = content.Load<Song>("./Sounds/menu");
            this.font = content.Load<SpriteFont>("./Fonts/Stats");
            MediaPlayer.Play(this.song);
            MediaPlayer.IsRepeating = true;
        }
        public override void Update(ref GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                GameManager.Instance.Start();
            }
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            spriteBatch.Draw(this.title, new Rectangle(100, 100,title.Width,title.Height), Color.White);
            spriteBatch.DrawString(font, "Presione Enter para comenzar!", new Vector2(260, 300), Color.Black);
        }
    }
}
