using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AvianWar
{
    class WarningMessage : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private float opacity;
        private float blinkSpeed;
        public bool isIncreasing = true;
        private GamePhaseControl phaseControl;
        private int currentlevelDelayCounter = 0;
        private bool isBlinking = false;

        private SoundEffect warningSound;
        private bool isPlayingSound = false;

        //Show the message for MESSAGE_DELAY/60 secs
        private int messageDelayCounter;
        const int MESSAGE_DELAY = 180;

        public WarningMessage(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            GamePhaseControl phaseControl,
            SoundEffect warningSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.phaseControl = phaseControl;
            this.warningSound = warningSound;
            position = new Vector2((Shared.stage.X / 2) - (tex.Width / 2), (Shared.stage.Y / 2) - (tex.Height / 2));
            opacity = 0;
            blinkSpeed = 0.02f;
            currentlevelDelayCounter = phaseControl.levelDelayCounter;
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("currentlevelDelayCounter" + currentlevelDelayCounter);
            Console.WriteLine("phaseControl.levelDelayCounter" + phaseControl.levelDelayCounter);
            if (phaseControl.gameStatus == GamePhaseControl.GameStatus.Stage1 || phaseControl.gameStatus == GamePhaseControl.GameStatus.Stage2)
            {
                //For the first part, do not show warning
                if (phaseControl.levelDelayCounter == 0)
                {
                    currentlevelDelayCounter = 0;
                }

                if (currentlevelDelayCounter != phaseControl.levelDelayCounter)
                {
                    this.Visible = true;
                    isBlinking = true;
                    if (!isPlayingSound)
                    {
                        warningSound.Play();
                        isPlayingSound = true;
                    }
                    messageDelayCounter++;
                    if (messageDelayCounter > MESSAGE_DELAY)
                    {
                        isBlinking = false;
                        isPlayingSound = false;
                        messageDelayCounter = 0;
                        currentlevelDelayCounter = phaseControl.levelDelayCounter;
                        this.Visible = false;
                    }
                }
                else
                {
                    this.Visible = false;
                }
            }
            else
            {
                this.Visible = false;
            }



            if (isBlinking)
            {
                if (opacity <= 0)
                {
                    opacity += blinkSpeed;
                    isIncreasing = true;
                }
                else if (opacity < 1)
                {
                    if (isIncreasing)
                    {
                        opacity += blinkSpeed;
                    }
                    else
                    {
                        opacity -= blinkSpeed;
                    }
                }
                else
                {
                    opacity -= blinkSpeed;
                    isIncreasing = false;
                }
            }
            else
            {
                opacity = 0;
            }

            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White * opacity);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
