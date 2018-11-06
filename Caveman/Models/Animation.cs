using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveman.Models
{
    public class Animation
    {
        public static bool NotLoopeable = false;
        public static bool Loopeable = true;

        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool IsLooping { get; set; }
        public bool IsLoopeable { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed, bool isLoopeable)
        {
            this.Texture = texture;
            this.FrameCount = frameCount;
            this.IsLooping = true;
            this.FrameSpeed = frameSpeed;
            this.CurrentFrame = 0;
            this.IsLoopeable = isLoopeable;
        }
    }
}
