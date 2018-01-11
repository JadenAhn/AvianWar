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
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D titleShield;
        private Texture2D titleSub;
        private Texture2D titleMain;
        private float titleShieldInitialScale = 0.01f;
        private Vector2 titleShieldOrigin;
        private Vector2 titleSubOrigin;
        private Vector2 titleMainOrigin;

        private const float TITLE_SHIELD_SCALE_CHANGE = 0.05f;
        private float titleSubInitialScale = 0.01f;
        private const float TITLE_SUB_SCALE_CHANGE = 0.05f;
        private float titleMainInitialScale = 0.85f;
        private const float TITLE_MAIN_SCALE_CHANGE = 0.001f;
        private float titleMainInitialTransparency = 0f;
        private const float TITLE_MAIN_TRANSPARENCY_CHANGE = 0.01f;
        private int animationDelayCounter = 0;
        private int animationDelay = 35;
        private bool animationDelayFinished;
        private bool shieldAnimationFinished;
        private bool subAnimationFinished;

        SoundEffect selectMenuSound;

        string[] menuItems = { "START GAME", "HIGH SCORE", "HELP", "ABOUT", "QUIT"};

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("Fonts/RegularFont");
            SpriteFont highlightFont = g.Content.Load<SpriteFont>("Fonts/HighlightFont");
            //SFX
            selectMenuSound = g.Content.Load<SoundEffect>("Sounds/SelectMenuSound");

            Menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menuItems, selectMenuSound);

            background = g.Content.Load<Texture2D>("Images/Background");
            titleShield = g.Content.Load<Texture2D>("Images/TitleShield");
            titleSub = g.Content.Load<Texture2D>("Images/TitleSub");
            titleMain = g.Content.Load<Texture2D>("Images/TitleMain");

            titleShieldOrigin = new Vector2(titleShield.Width / 2, titleShield.Height / 2);
            titleSubOrigin = new Vector2(titleSub.Width / 2, titleSub.Height / 2);
            titleMainOrigin = new Vector2(titleMain.Width / 2, titleMain.Height / 2);
            animationDelayFinished = false;
            shieldAnimationFinished = false;
            subAnimationFinished = false;
            this.Components.Add(Menu);
        }

        public override void Update(GameTime gameTime)
        {
            if (!animationDelayFinished)
            {
                animationDelayCounter++;
                if (animationDelayCounter > animationDelay)
                {
                    animationDelayCounter = 0;
                    animationDelayFinished = true;
                }
            }
            else
            {
                if (titleShieldInitialScale <= 1.0f)
                {
                    titleShieldInitialScale += TITLE_SHIELD_SCALE_CHANGE;
                }
                else
                {
                    shieldAnimationFinished = true;
                }
            }

            if (shieldAnimationFinished)
            {
                if (titleSubInitialScale <= 1.0f)
                {
                    titleSubInitialScale += TITLE_SUB_SCALE_CHANGE;
                }
                else
                {
                    subAnimationFinished = true;
                }

            }

            if (subAnimationFinished)
            {
                if (titleMainInitialScale <= 1.0f)
                {
                    titleMainInitialScale += TITLE_MAIN_SCALE_CHANGE;
                }
                if (titleMainInitialTransparency <= 1.0f)
                {
                    titleMainInitialTransparency += TITLE_MAIN_TRANSPARENCY_CHANGE;
                }
            }

            //KeyboardState ks = Keyboard.GetState();
            //if (ks.IsKeyDown(Keys.Q))
            //{
            //    initializeAnimation();
            //}

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            //spriteBatch.Draw(titleShield, Vector2.Zero, Color.White);
            if (animationDelayFinished)
            {
                spriteBatch.Draw(titleShield, new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2), null, Color.White, 0f, titleShieldOrigin, titleShieldInitialScale, SpriteEffects.None, 0f);
            }
            if (shieldAnimationFinished)
            {
                spriteBatch.Draw(titleSub, new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2), null, Color.White, 0f, titleSubOrigin, titleSubInitialScale, SpriteEffects.None, 0f);
            }
            if (subAnimationFinished)
            {
                spriteBatch.Draw(titleMain, new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2), null, Color.White * titleMainInitialTransparency, 0f, titleMainOrigin, titleMainInitialScale, SpriteEffects.None, 0f);
            }

            //spriteBatch.Draw(titleSub, Vector2.Zero, Color.White);
            //spriteBatch.Draw(titleMain, Vector2.Zero, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void initializeAnimation()
        {
            animationDelay = 0;
            animationDelayFinished = false;
            shieldAnimationFinished = false;
            subAnimationFinished = false;
            titleShieldInitialScale = 0.01f;
            titleSubInitialScale = 0.01f;
            titleMainInitialScale = 0.85f;
            titleMainInitialTransparency = 0f;
        }
    }
}
