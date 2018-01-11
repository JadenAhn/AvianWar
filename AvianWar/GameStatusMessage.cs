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
    public class GameStatusMessage : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont titleFont;
        private SpriteFont regularFont;
        private SpriteFont noticeEscFont;
        public string message1;
        public string message2;
        public bool isNoticeEscVisible;
        private Vector2 stringLength1, stringLength2;

        public GameStatusMessage(Game game,
            SpriteBatch spriteBatch,
            SpriteFont titleFont,
            SpriteFont regularFont,
            SpriteFont noticeEscFont) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.titleFont = titleFont;
            this.regularFont = regularFont;
            this.noticeEscFont = noticeEscFont;
            message1 = "";
            message2 = "";
            isNoticeEscVisible = false;
        }

        public override void Update(GameTime gameTime)
        {
            stringLength1 = titleFont.MeasureString(message1);
            stringLength2 = regularFont.MeasureString(message2);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            string noticeEsc = "PRESS ESC TO GO BACK TO MAIN MENU";
            Vector2 dimensionNoticeEsc = noticeEscFont.MeasureString(noticeEsc);

            spriteBatch.Begin();
            Vector2 titlePosition = new Vector2((Shared.stage.X / 2) - (stringLength1.X / 2), (Shared.stage.Y / 2) - (stringLength1.Y / 2));
            Vector2 regularTextPosition = new Vector2((Shared.stage.X / 2) - (stringLength2.X / 2), (Shared.stage.Y / 2) - (stringLength2.Y / 2) + 50);
            spriteBatch.DrawString(titleFont, message1, titlePosition, new Color(255, 204, 51));
            spriteBatch.DrawString(regularFont, message2, regularTextPosition, new Color(255, 204, 51));

            if (isNoticeEscVisible)
            {
                spriteBatch.DrawString(noticeEscFont, noticeEsc, new Vector2((Shared.stage.X / 2) - (dimensionNoticeEsc.X / 2), Shared.stage.Y - dimensionNoticeEsc.Y), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
