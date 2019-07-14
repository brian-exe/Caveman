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
    public class Level1:Level
    {
        Caveman caveman;
        List<Sprite> enemies = new List<Sprite>();
        List<Sprite> diedEnemies = new List<Sprite>();
        ContentManager contentManager;
        SpriteFont font;

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
            private set {
                instance = value;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            if (this.contentManager == null)
                this.contentManager = content;

            #region Background
            Texture2D _texture0 = content.Load<Texture2D>("./Backgrounds/posible-bg3");
            Texture2D _texture1 = content.Load<Texture2D>("./Backgrounds/treeses_pasto4");
            this.Theme = content.Load<Song>("./Sounds/Jungle");
            MediaPlayer.Play(this.Theme);
            MediaPlayer.IsRepeating = true;

            this.background = new Background(_texture0,_texture1);

            #endregion

            caveman = new Caveman(new Vector2(0, 350));
            this.font = content.Load<SpriteFont>("./Fonts/Stats");
            caveman.LoadContent(content);
            AddPterodactyl();


        }

        private void AddPterodactyl()
        {
            Pterodactyl ptero = new Pterodactyl();
            ptero.LoadContent(this.contentManager);

            this.enemies.Add(ptero);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);

            if (!caveman.Died)
            {
                caveman.Update(ref gameTime);
                if (caveman.Position.X >= Game1.Bounds.Width / 2)
                {
                    caveman.MoveBackwards();
                    background.MoveForward();
                }
            }
            else
            {
                GameManager.Instance.EndGame();
            }

            UpdateEnemies(ref gameTime);

        }

        private void UpdateEnemies(ref GameTime gameTime)
        {
            foreach(Sprite e in enemies)
            {
                e.Update(ref gameTime);
                if ((e as IMortable).Died)
                {
                    diedEnemies.Add(e);
                    caveman.ReceivePoints(100);
                }
            }

            RemoveDiedEnemies();
        }

        private void RemoveDiedEnemies()
        {
            if (diedEnemies.Count() > 0)
            {
                this.enemies.RemoveAll(c => this.diedEnemies.Contains(c));
                this.diedEnemies.RemoveAll(c => true);
                AddPterodactyl();
            }
        }
        private void DrawEnemies(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            foreach(Sprite e in enemies)
            {
                e.Draw(ref spriteBatch, ref gameTime);
            }
        }

        public override void Draw(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            base.Draw(ref spriteBatch, ref gameTime);
            caveman.Draw(ref spriteBatch, ref gameTime);
            DrawEnemies(ref spriteBatch, ref gameTime);

            caveman.CheckColissions(enemies);
            DrawCavemanHealth(ref spriteBatch, ref gameTime);
        }

        private void DrawCavemanHealth(ref SpriteBatch spriteBatch, ref GameTime gameTime)
        {
            spriteBatch.DrawString(font, "Vida: " + caveman.Health.ToString(), new Vector2(650, 450),Color.Black);
            spriteBatch.DrawString(font, "Score: " + caveman.score.ToString(), new Vector2(0, 450),Color.Black);
        }
    }
}
