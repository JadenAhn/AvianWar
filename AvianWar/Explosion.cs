using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AvianWar
{
    public class Explosion : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 Position { get; set; }
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        //private SoundEffect missSound;

        public Explosion(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.Position = position;
            this.delay = delay;
            dimension = new Vector2(64, 64);
            this.Enabled = false;
            this.Visible = false;
            CreateFrames();
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                //After showing all the frame once, stop the animation
                if (frameIndex > 24)
                {
                    frameIndex = -1;
                    this.Enabled = false;
                    this.Visible = false;
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, Position, frames[frameIndex], Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        public void StartAnimation()
        {
            this.Enabled = true;
            this.Visible = true;
        }
    }
}
