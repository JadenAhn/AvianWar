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
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D aboutTex;
        private Texture2D background;
        private SpriteFont sceneFont;
        private SpriteFont subTitleFont;
        private SpriteFont headerFont;
        private SpriteFont nameFont;
        private SpriteFont bodyFont;
        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            sceneFont = g.Content.Load<SpriteFont>("Fonts/SceneFont");
            subTitleFont = g.Content.Load<SpriteFont>("Fonts/SubTitleFont");
            aboutTex = g.Content.Load<Texture2D>("Images/AboutImage");
            headerFont = g.Content.Load<SpriteFont>("Fonts/HighlightFont");
            bodyFont = g.Content.Load<SpriteFont>("Fonts/RegularFont");
            nameFont = g.Content.Load<SpriteFont>("Fonts/StatusRegularFont");
            background = g.Content.Load<Texture2D>("Images/Background");
        }

        public override void Draw(GameTime gameTime)
        {
            string sceneName = "ABOUT";
            string subTitle = "CREATED BY";
            string name = "JADEN (JI HONG) AHN";
            string message1 = "As a Final Project for PROG2370:";
            string message2 = "Object Oriented Game Programming";
            string message3 = "All copyrights are the property";
            string message4 = "of their respective owners";
            string noticeEsc = "PRESS ESC TO GO BACK TO MAIN MENU";
            Vector2 dimensionSceneName = sceneFont.MeasureString(sceneName);
            Vector2 dimensionSubTitle = subTitleFont.MeasureString(subTitle);
            Vector2 dimensionName = nameFont.MeasureString(name);
            Vector2 dimensionMessage1 = bodyFont.MeasureString(message1);
            Vector2 dimensionMessage2 = bodyFont.MeasureString(message2);
            Vector2 dimensionMessage3 = bodyFont.MeasureString(message3);
            Vector2 dimensionMessage4 = bodyFont.MeasureString(message4);
            Vector2 dimensionNoticeEsc = bodyFont.MeasureString(noticeEsc);

            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(sceneFont, sceneName, new Vector2((Shared.stage.X / 2) - (dimensionSceneName.X / 2), 20), new Color(42, 26, 0));
            spriteBatch.DrawString(subTitleFont, subTitle, new Vector2((Shared.stage.X / 2) - (dimensionSubTitle.X / 2), 110), new Color(211, 111, 104));
            spriteBatch.DrawString(nameFont, name, new Vector2((Shared.stage.X / 2) - (dimensionName.X / 2), 180), Color.Black);
            spriteBatch.DrawString(bodyFont, message1, new Vector2((Shared.stage.X / 2) - (dimensionMessage1.X / 2), 260), Color.Black);
            spriteBatch.DrawString(bodyFont, message2, new Vector2((Shared.stage.X / 2) - (dimensionMessage2.X / 2), 300), Color.Black);
            spriteBatch.DrawString(bodyFont, message3, new Vector2((Shared.stage.X / 2) - (dimensionMessage3.X / 2), 380), Color.Black);
            spriteBatch.DrawString(bodyFont, message4, new Vector2((Shared.stage.X / 2) - (dimensionMessage4.X / 2), 420), Color.Black);
            spriteBatch.DrawString(bodyFont, noticeEsc, new Vector2((Shared.stage.X / 2) - (dimensionNoticeEsc.X / 2), Shared.stage.Y - dimensionNoticeEsc.Y), Color.White);
            //spriteBatch.Draw(aboutTex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
