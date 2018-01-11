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
    public class PlayerCharacter : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        public Vector2 initialPosition;
        public Vector2 speed;
        const int PLAYER_SPEED_Y = 7;
        //Random random;

        //For the animation
        const int TOTAL_FRAME = 30;
        const int ANIMATION_DELAY = 1;
        public Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        public PlayerCharacter(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 dimension,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.dimension = dimension;
            this.position = position;
            speed = new Vector2(0, PLAYER_SPEED_Y);
            this.initialPosition = position;
            //this.Enabled = false;
            //this.Visible = false;
            delay = ANIMATION_DELAY;
            CreateFrames();
            StartAnimation();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
            {
                position.Y -= speed.Y;
                if (position.Y  <= 10)
                {
                    position.Y = 10;
                }
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                position.Y += speed.Y;
                if (position.Y >= Shared.stage.Y - dimension.Y - 12)
                {
                    position.Y = Shared.stage.Y - dimension.Y - 12;
                }
            }
            
            //animation
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                //After showing all the frame once, stop the animation
                if (frameIndex >= TOTAL_FRAME)
                {
                    frameIndex = 0;
                    //this.Enabled = false;
                    //this.Visible = false;
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
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 6; i++)
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

        public Rectangle getBound()
        {
            //return the position of the rectangle
            return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y);
        }
        //private void Restart()
        //{
        //    //&& ball.Enabled == false
        //    if (Keyboard.GetState().IsKeyDown(Keys.Enter) && this.Enabled == false/* && gamePhase == GamePhase.Playing*/)
        //    {
        //        this.Enabled = true;
        //        Vector2 velocity;
        //        var direction = random.Next(0, 4);
        //        switch (direction)
        //        {
        //            case 0:
        //                velocity = new Vector2(1, 1);
        //                break;
        //            case 1:
        //                velocity = new Vector2(1, -1);
        //                break;
        //            case 2:
        //                velocity = new Vector2(-1, 1);
        //                break;
        //            default:
        //                velocity = new Vector2(-1, -1);
        //                break;
        //        }

        //        Vector2 ballSpeed = new Vector2(5, 5) * velocity;

        //        position = initialPosition;
        //        speed = ballSpeed;
        //    }
        //}
    }
}