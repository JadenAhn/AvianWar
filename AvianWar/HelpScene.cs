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
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D helpTex;
        private Texture2D background;
        private SpriteFont sceneFont;
        private SpriteFont bodyFont;

        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            sceneFont = g.Content.Load<SpriteFont>("Fonts/SceneFont");
            bodyFont = g.Content.Load<SpriteFont>("Fonts/RegularFont");
            helpTex = g.Content.Load<Texture2D>("Images/HelpImage");
            background = g.Content.Load<Texture2D>("Images/Background");
        }

        public override void Draw(GameTime gameTime)
        {
            string sceneName = "HELP";
            string noticeEsc = "PRESS ESC TO GO BACK TO MAIN MENU";
            Vector2 dimensionSceneName = sceneFont.MeasureString(sceneName);
            Vector2 dimensionNoticeEsc = bodyFont.MeasureString(noticeEsc);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(sceneFont, sceneName, new Vector2((Shared.stage.X / 2) - (dimensionSceneName.X / 2), 20), new Color(42, 26, 0));
            spriteBatch.Draw(helpTex, new Vector2(0, -20), Color.White);
            spriteBatch.DrawString(bodyFont, noticeEsc, new Vector2((Shared.stage.X / 2) - (dimensionNoticeEsc.X / 2), Shared.stage.Y - dimensionNoticeEsc.Y), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
