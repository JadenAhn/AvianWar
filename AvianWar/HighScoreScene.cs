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
    public class HighScoreScene : GameScene
    {
        public HighScoreComponent HighScore { get; set; }
        private SpriteBatch spriteBatch;
        private Texture2D background;
        public static string[] nameItems;
        public static int[] scoreItems;
        private SpriteFont sceneFont;
        private SpriteFont subTitleFont;
        private SpriteFont headerFont;
        private SpriteFont scoreFont;

        public HighScoreScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            subTitleFont = g.Content.Load<SpriteFont>("Fonts/SubTitleFont");
            headerFont = g.Content.Load<SpriteFont>("Fonts/HighlightFont");
            scoreFont = g.Content.Load<SpriteFont>("Fonts/RegularFont");
            sceneFont = g.Content.Load<SpriteFont>("Fonts/SceneFont");
            //LoadHighScore(out nameItems, out scoreItems);
            HighScore = new HighScoreComponent(game, spriteBatch, headerFont, scoreFont, nameItems, scoreItems);

            background = g.Content.Load<Texture2D>("Images/Background");

            this.Components.Add(HighScore);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            string sceneName = "HIGH SCORE";
            string subTitle = "Best 5";
            string noticeEsc = "PRESS ESC TO GO BACK TO MAIN MENU";

            Vector2 dimensionSceneName = sceneFont.MeasureString(sceneName);
            Vector2 dimensionSubTitle = subTitleFont.MeasureString(subTitle);
            Vector2 dimensionNoticeEsc = scoreFont.MeasureString(noticeEsc);

            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(sceneFont, sceneName, new Vector2((Shared.stage.X / 2) - (dimensionSceneName.X / 2), 20), new Color(42, 26, 0));
            spriteBatch.DrawString(subTitleFont, subTitle, new Vector2((Shared.stage.X / 2) - (dimensionSubTitle.X / 2), 110), new Color(211, 111, 104));
            spriteBatch.DrawString(scoreFont, noticeEsc, new Vector2((Shared.stage.X / 2) - (dimensionNoticeEsc.X / 2), Shared.stage.Y - dimensionNoticeEsc.Y), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
