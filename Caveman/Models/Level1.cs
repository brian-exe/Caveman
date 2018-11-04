using Caveman.Models.Managers;
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
            Texture2D _texture1 = content.Load<Texture2D>("./Backgrounds/layer_01");
            Texture2D _texture2 = content.Load<Texture2D>("./Backgrounds/layer_02");
            Texture2D _texture3 = content.Load<Texture2D>("./Backgrounds/treeses_pasto4");

            this.background = new Background(_texture1,_texture2,_texture3);
        }
    }
}
