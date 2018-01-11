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
    public class Score : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont scoreFont;
        private SpriteFont comboFont;
        private SpriteFont shieldScoreFont;
        public float Scale { get; set; } = 1.0f;
        private float currentScale;
        private float SCALE_CHANGE = 0.05f;
        private const float MAX_SCALE = 1.5f;
        public int score = 0;
        public int shieldScore = 3;
        public int multiplier = 1;
        private string combo;
        private bool isVisible = false;
        private int comboDelayCounter;
        const int COMBO_DELAY = 180;

        public Score(Game game,
            SpriteBatch spriteBatch,
            SpriteFont scoreFont,
            SpriteFont comboFont,
            SpriteFont shieldScoreFont) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.scoreFont = scoreFont;
            this.comboFont = comboFont;
            this.shieldScoreFont = shieldScoreFont;
            multiplier = 1;
        }

        public override void Update(GameTime gameTime)
        {
            if (multiplier != 1)
            {
                isVisible = true;
            }
            else
            {
                Scale = 1.0f;
            }

            if (isVisible)
            {
                if (multiplier > 1)
                {
                    if (Scale < multiplier * 0.4f && Scale < MAX_SCALE)
                    {
                        Scale += multiplier * SCALE_CHANGE;
                    }
                    currentScale = Scale;
                    combo = multiplier.ToString();
                }

                comboDelayCounter++;
                if (comboDelayCounter > COMBO_DELAY)
                {
                    isVisible = false;
                    comboDelayCounter = 0;
                    Scale = 1.0f;
                }
            }

            base.Update(gameTime);  
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            Vector2 shieldScorePosition = new Vector2(62, Shared.stage.Y - 55);

            Vector2 scoreLength = scoreFont.MeasureString(score.ToString());
            Vector2 scorePosition = new Vector2(Shared.stage.X - scoreLength.X - 15, Shared.stage.Y - scoreLength.Y - 5);
            Vector2 multiplierPosition = new Vector2(20, 10);

            spriteBatch.DrawString(shieldScoreFont, "X " + shieldScore.ToString(), shieldScorePosition, new Color(255, 204, 51));
            spriteBatch.DrawString(scoreFont, score.ToString(), scorePosition, new Color(211, 111, 104));

            if (isVisible)
            {
                //spriteBatch.DrawString(spriteFont, "X " + combo, multiplierPosition, new Color(255, 204, 51));
                spriteBatch.DrawString(comboFont, " X " + combo + " bounce", multiplierPosition, new Color(211, 111, 104), 0f, Vector2.Zero, currentScale, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
