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
    public class HighScoreInputBox : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont inputTitleFont;
        private SpriteFont inputBodyFont;
        private SpriteFont inputBodySmallFont;
        private Score score;
        private string titleMessage1, titleMessage2, titleMessage3;
        private string[] userNameArray;
        private string userName;
        private int cursorPosition;
        private string[] letters;
        private int letterIndex1, letterIndex2, letterIndex3;
        private KeyboardState oldState;
        private Color regularColor = new Color(255, 204, 51);
        private Color highlightColor = new Color(211, 111, 104);
        private Vector2 headerPosition1;
        private Vector2 headerPosition2;
        private Vector2 headerPosition3;
        private Vector2 position1;
        private Vector2 position2;
        private Vector2 position3;
        private Color color1, color2, color3;
        private float scale1, scale2, scale3;
        private const float DEFAULT_SCALE = 1.0f;
        private const float MAX_SCALE = 1.2f;
        //public bool isFinished = false;
        private SoundEffect moveCursorSound;
        private SoundEffect selectLetterSound;


        public HighScoreInputBox(Game game,
            SpriteBatch spriteBatch,
            SpriteFont inputTitleFont,
            SpriteFont inputBodyFont,
            SpriteFont inputBodySmallFont,
            Score score,
            SoundEffect moveCursorSound,
            SoundEffect selectLetterSound) : base(game)
        {
            this.Enabled = false;
            this.Visible = false;
            this.spriteBatch = spriteBatch;
            this.inputTitleFont = inputTitleFont;
            this.inputBodyFont = inputBodyFont;
            this.inputBodySmallFont = inputBodySmallFont;
            this.score = score;
            this.moveCursorSound = moveCursorSound;
            this.selectLetterSound = selectLetterSound;
            titleMessage1 = "NEW HIGH SCORE!";
            titleMessage2 = "ENTER YOUR NAME";
            titleMessage3 = "PRESS ENTER WHEN DONE";
            userName = "";
            cursorPosition = 0;
            letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "!", "$", "&", "." };
            letterIndex1 = 0;
            letterIndex2 = 0;
            letterIndex3 = 0;
            userNameArray = new string[3];
            userNameArray[0] = letters[0];
            userNameArray[1] = letters[0];
            userNameArray[2] = letters[0];
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                moveCursorSound.Play();
                cursorPosition--;
                if (cursorPosition == -1)
                {
                    cursorPosition = 2;
                }
            }
            if (ks.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                moveCursorSound.Play();
                cursorPosition++;
                if (cursorPosition == 3)
                {
                    cursorPosition = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectLetterSound.Play();
                if (cursorPosition == 0)
                {
                    letterIndex1--;
                }
                else if (cursorPosition == 1)
                {
                    letterIndex2--;
                }
                else if (cursorPosition == 2)
                {
                    letterIndex3--;
                }
                
                if (letterIndex1 == -1)
                {
                    letterIndex1 = letters.Length - 1;
                }
                if (letterIndex2 == -1)
                {
                    letterIndex2 = letters.Length - 1;
                }
                if (letterIndex3 == -1)
                {
                    letterIndex3 = letters.Length - 1;
                }

                userNameArray[0] = letters[letterIndex1];
                userNameArray[1] = letters[letterIndex2];
                userNameArray[2] = letters[letterIndex3];
            }
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectLetterSound.Play();
                if (cursorPosition == 0)
                {
                    letterIndex1++;
                }
                else if (cursorPosition == 1)
                {
                    letterIndex2++;
                }
                else if (cursorPosition == 2)
                {
                    letterIndex3++;
                }

                if (letterIndex1 == letters.Length)
                {
                    letterIndex1 = 0;
                }
                if (letterIndex2 == letters.Length)
                {
                    letterIndex2 = 0;
                }
                if (letterIndex3 == letters.Length)
                {
                    letterIndex3 = 0;
                }
                userNameArray[0] = letters[letterIndex1];
                userNameArray[1] = letters[letterIndex2];
                userNameArray[2] = letters[letterIndex3];
            }

            userName = userNameArray[0] + userNameArray[1] + userNameArray[2];

            if (this.Enabled == true)
            {
                if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    HighScoreComponent.UpdateHighScore(userName, score.score);
                    //reset score
                    score.score = 0;
                    initializeHighscoreComponent();
                    this.Enabled = false;
                    this.Visible = false;
                }
                oldState = ks;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (cursorPosition == 0)
            {
                color1 = highlightColor;
                color2 = regularColor;
                color3 = regularColor;
                scale1 = MAX_SCALE;
                scale2 = DEFAULT_SCALE;
                scale3 = DEFAULT_SCALE;

            }
            else if (cursorPosition == 1)
            {
                color1 = regularColor;
                color2 = highlightColor;
                color3 = regularColor;
                scale1 = DEFAULT_SCALE;
                scale2 = MAX_SCALE;
                scale3 = DEFAULT_SCALE;
            }
            else
            {
                color1 = regularColor;
                color2 = regularColor;
                color3 = highlightColor;
                scale1 = DEFAULT_SCALE;
                scale2 = DEFAULT_SCALE;
                scale3 = MAX_SCALE;
            }
            Vector2 titleStringSize = inputTitleFont.MeasureString(titleMessage1);
            Vector2 bodyStringSize = inputBodyFont.MeasureString(titleMessage2);
            Vector2 bodySmallStringSize = inputBodySmallFont.MeasureString(titleMessage3);

            headerPosition1 = new Vector2((Shared.stage.X / 2) - (titleStringSize.X / 2), 90);
            headerPosition2 = new Vector2((Shared.stage.X / 2) - (bodyStringSize.X / 2), 150);
            headerPosition3 = new Vector2((Shared.stage.X / 2) - (bodySmallStringSize.X / 2), 200);
            position1 = new Vector2((Shared.stage.X / 2) - 70, 240);
            position2 = new Vector2((Shared.stage.X / 2) - 20, 240);
            position3 = new Vector2((Shared.stage.X / 2) + 30, 240);

        spriteBatch.DrawString(inputTitleFont, titleMessage1, headerPosition1, regularColor);
            spriteBatch.DrawString(inputBodyFont, titleMessage2, headerPosition2, regularColor);
            spriteBatch.DrawString(inputBodySmallFont, titleMessage3, headerPosition3, regularColor);
            spriteBatch.DrawString(inputTitleFont, userNameArray[0], position1, color1, 0f, Vector2.Zero, scale1, SpriteEffects.None, 0f);
            spriteBatch.DrawString(inputTitleFont, userNameArray[1], position2, color2, 0f, Vector2.Zero, scale2, SpriteEffects.None, 0f);
            spriteBatch.DrawString(inputTitleFont, userNameArray[2], position3, color3, 0f, Vector2.Zero, scale3, SpriteEffects.None, 0f);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void initializeHighscoreComponent()
        {
            //statusMessage.Enabled = false;
            //statusMessage.Visible = false;
            letterIndex1 = 0;
            letterIndex2 = 0;
            letterIndex3 = 0;
            userNameArray[0] = letters[0];
            userNameArray[1] = letters[0];
            userNameArray[2] = letters[0];
            cursorPosition = 0;
            //isFinished = false;
            this.Visible = false;
            this.Enabled = false;
        }
    }
}
