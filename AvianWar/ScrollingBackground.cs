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
    public class ScrollingBackground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position1, position2, speed;
        private int backgroundLevel;
        public int currentbackgroundLevel = 1;

        public ScrollingBackground(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            int backgroundLevel) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position1 = position;
            //Put the second image on the right side of the first image
            this.position2 = new Vector2(position.X + tex.Width, position.Y);
            this.speed = speed;
            this.backgroundLevel = backgroundLevel;
        }

        public override void Update(GameTime gameTime)
        {
            if (currentbackgroundLevel == backgroundLevel)
            {
                //this.Enabled = true;
                this.Visible = true;

                position1.X -= speed.X;
                position2.X -= speed.X;

                if (position1.X < -tex.Width)
                {
                    position1.X = position2.X + tex.Width;
                }
                if (position2.X < -tex.Width)
                {
                    position2.X = position1.X + tex.Width;
                }
            }
            else
            {
                //this.Enabled = false;
                this.Visible = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position1, Color.White);
            spriteBatch.Draw(tex, position2, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
