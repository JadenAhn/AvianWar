using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace AvianWar
{
    public class HighScoreComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont headerFont;
        private SpriteFont scoreFont;
        public static string[] nameItems;
        public static int[] scoreItems;
        //private Vector2 position;
        private Color regularColor = Color.Black;
        private Color highlightColor = Color.Black;
        
        //new Color(248, 179, 35)

        public HighScoreComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont headerFont,
            SpriteFont scoreFont,
            string[] names,
            int[] scores
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.headerFont = headerFont;
            this.scoreFont = scoreFont;
            //position = new Vector2(Shared.stage.X / 2, 100);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(headerFont, "Rank", new Vector2(260, 180), highlightColor);
            spriteBatch.DrawString(headerFont, "Name", new Vector2(360, 180), highlightColor);
            spriteBatch.DrawString(headerFont, "Score", new Vector2(460, 180), highlightColor);

            for (int i = 0; i < scoreItems.Count(); i++)
            {
                //For the rank
                spriteBatch.DrawString(scoreFont, (i + 1).ToString(), new Vector2(280, 230 + (50 * i)), regularColor);
                spriteBatch.DrawString(scoreFont, nameItems[i], new Vector2(360, 230 + (50 * i)), regularColor);
                spriteBatch.DrawString(scoreFont, scoreItems[i].ToString(), new Vector2(460, 230 + (50 * i)), regularColor);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void UpdateHighScore(string name, int score)
        {
            for (int i = 0; i < scoreItems.Count(); i++)
            {
                if (score > scoreItems[i])
                {
                    for (int j = 4; j > i; j--)
                    {
                        nameItems[j] = nameItems[j - 1];
                        scoreItems[j] = scoreItems[j - 1];
                    }
                    nameItems[i] = name;
                    scoreItems[i] = score;
                    break;
                }
            }
            SaveHighScore();
        }

        public static void LoadHighScore(out string[] nameItems, out int[] scoreItems)
        {
            string fileName = "Highscore.xml";
            nameItems = new string[5];
            scoreItems = new int[5];

            if (File.Exists(fileName))
            {
                string[] highscoreData = new string[5];
                StreamReader reader = new StreamReader(fileName);
                for (int i = 0; i < highscoreData.Length; i++)
                {
                    highscoreData[i] = reader.ReadLine();
                }
                reader.Close();

                for (int i = 0; i < highscoreData.Length; i++)
                {
                    string[] splitData = highscoreData[i].Split(',');
                    nameItems[i] = splitData[0];
                    scoreItems[i] = int.Parse(splitData[1]);
                }
            }
            else
            {
                nameItems = new string[] { "AAA", "BBB", "CCC", "DDD", "EEE" };
                scoreItems = new int[] { 500, 400, 300, 200, 100 };
                SaveHighScore();
            }
            //read each line and split them into eah array
        }

        public static void SaveHighScore()
        {
            string fileName = "Highscore.xml";
            StreamWriter writer = new StreamWriter(fileName);
            for (int i = 0; i < nameItems.Length; i++)
            {
                writer.WriteLine(nameItems[i] + "," + scoreItems[i]);
            }
            writer.Close();
        }
    }
}
