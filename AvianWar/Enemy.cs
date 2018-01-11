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
    public class Enemy : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        public Vector2 initialPosition;
        public Vector2 speed;
        public Vector2 currentSpeed;
        private Vector2 turnPosition;
        private Vector2 turnSpeed;
        private GameStatusMessage statusMessage;
        const int ENEMY_SPEED_Y = 1;
        public int enemyScore;
        private int enemyOrder;
        public int animationStartDelay;
        Random rand;

        //For the animation
        const int TOTAL_FRAME = 30;
        const int ANIMATION_DELAY = 2;
        const int SPEED_DELAY = 1;
        public Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int speedDelayCounter;
        private int animationDelayCounter;
        private int animationStartDelayCounter;
        public int currentLevel = 1;
        public int currentPhase = 1;
        private int enemyLevel;
        private int enemyPhase;
        private bool animationStarted;

        public Enemy(Game game,
            SpriteBatch spriteBatch,
            GameStatusMessage statusMessage,
            Texture2D tex,
            Vector2 dimension,
            Vector2 position,
            Vector2 speed,
            int enemyScore,
            int enemyOrder,
            int enemyLevel,
            int enemyPhase,
            Vector2 turnPosition,
            Vector2 turnSpeed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.statusMessage = statusMessage;
            this.tex = tex;
            this.dimension = dimension;
            this.position = position;
            this.initialPosition = position;
            this.speed = speed;
            this.currentSpeed = speed;
            this.turnPosition = turnPosition;
            this.turnSpeed = turnSpeed;
            this.enemyScore = enemyScore;
            this.enemyOrder = enemyOrder;
            this.enemyLevel = enemyLevel;
            this.enemyPhase = enemyPhase;
            rand = new Random();
            animationStarted = false;
            CreateFrames();
            animationStartDelay = rand.Next(0, 60);
        }
        public override void Update(GameTime gameTime)
        {
            //speed
            speedDelayCounter++;
            if (speedDelayCounter > SPEED_DELAY)
            {
                if (currentLevel == enemyLevel)
                {
                    if (currentPhase == enemyPhase)
                    {
                        this.Enabled = true;
                        //this.Visible = true;
                        position += currentSpeed;
                    }
                }

                //Change speed of enemy if it reaches destined turnPositionX
                if (turnPosition.X != 0)
                {
                    if (speed.X < 0)
                    {
                        if (position.X < turnPosition.X)
                        {
                            currentSpeed = turnSpeed;
                        }
                    }
                    else if (speed.X > 0)
                    {
                        if (position.X > turnPosition.X)
                        {
                            currentSpeed = turnSpeed;
                        }
                    }
                }
                else if (turnPosition.Y != 0)
                {
                    if (speed.Y < 0)
                    {
                        if (position.Y < turnPosition.Y)
                        {
                            currentSpeed = turnSpeed;
                        }
                    }
                    else if (speed.Y > 0)
                    {
                        if (position.Y > turnPosition.Y)
                        {
                            currentSpeed = turnSpeed;
                        }
                    }
                }

                speedDelayCounter = 0;
            }

            //animation
            if (!animationStarted)
            {
                animationStartDelayCounter++;
                if (animationStartDelayCounter > animationStartDelay)
                {
                    animationStarted = true;
                }
            }
            else
            {
                animationDelayCounter++;
                if (animationDelayCounter > ANIMATION_DELAY)
                {
                    frameIndex++;
                    //After showing all the frame once, start from the beginning
                    if (frameIndex >= TOTAL_FRAME)
                    {
                        frameIndex = 0;
                    }
                    animationDelayCounter = 0;
                }
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

        public Rectangle getBound()
        {
            //return the position of the rectangle
            return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y);
        }
    }
}