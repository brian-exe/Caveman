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
        public static bool NotInterruptible = false;
        public static bool Interruptible = true;

        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool IsInterruptible { get; set; }
        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed, bool isInterruptible)
        {
            this.Texture = texture;
            this.FrameCount = frameCount;
            this.FrameSpeed = frameSpeed;
            this.IsLooping = false;
            this.CurrentFrame = 0;
            this.IsInterruptible = isInterruptible;
        }
    }
}
