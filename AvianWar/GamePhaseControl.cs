using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AvianWar
{
    public class GamePhaseControl : GameComponent
    {
        public GameStatus gameStatus;
        private PlayerCharacter player;
        private Shield shield;
        private List<ScrollingBackground> bgList;
        private List<Enemy> enemyList;
        private Score score;
        private GameStatusMessage statusMessage;
        private HighScoreInputBox highScoreInputBox;
        //ScrollingBackground sun, cloud, sb1, sb2, sb3, sb4, sb5, sb6;
        private int phaseDelayCounter = 0;
        public int levelDelayCounter = 0;

        //This is the length of game
        const int NUMBER_OF_PHASE = 3;
        const int PHASE_DELAY = 60 * 35;
        //const int PHASE_DELAY = 60 * 3;
        private int statusMessageDelayCounter;
        const int STATUS_MESSAGE_DELAY = 120;
        private Song songStage1;
        private Song songStage2;
        private Song songGameOver;
        private SoundEffect highScoreSound;
        public bool isPlayingStage1Song;
        public bool isPlayingStage2Song;
        public bool isPlayingGameOVerSong;

        public enum GameStatus
        {
            //GameReady,
            Stage1,
            Stage1Over,
            Stage2,
            GameOver
            //Continue
        }
        public GamePhaseControl(Game game,
            PlayerCharacter player,
            Shield shield,
            List<Enemy> enemyList,
            Score score,
            GameStatusMessage statusMessage,
            HighScoreInputBox highScoreInputBox,
            List<ScrollingBackground> bgList,
            Song songStage1,
            Song songStage2,
            Song songGameOver,
            SoundEffect highScoreSound) : base(game)
        {
            this.player = player;
            this.shield = shield;
            this.enemyList = enemyList;
            this.score = score;
            this.statusMessage = statusMessage;
            this.highScoreInputBox = highScoreInputBox;
            gameStatus = GameStatus.Stage1;
            this.bgList = bgList;
            this.songStage1 = songStage1;
            this.songStage2 = songStage2;
            this.songGameOver = songGameOver;
            this.highScoreSound = highScoreSound;
            isPlayingStage1Song = false;
            isPlayingStage2Song = false;
            isPlayingGameOVerSong = false;
            statusMessage.message1 = "STAGE 1";
        }

        public override void Update(GameTime gameTime)
        {
            if (gameStatus == GameStatus.Stage1 || gameStatus == GameStatus.Stage1Over)
            {
                if (!isPlayingStage1Song)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songStage1);
                    MediaPlayer.IsRepeating = true;
                    isPlayingStage1Song = true;
                }
            }
            else if (gameStatus == GameStatus.Stage2)
            {
                if (!isPlayingStage2Song)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songStage2);
                    MediaPlayer.IsRepeating = true;
                    isPlayingStage2Song = true;
                }
            }
            else
            {
                if (!isPlayingGameOVerSong)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songGameOver);
                    MediaPlayer.IsRepeating = true;
                    isPlayingGameOVerSong = true;
                }
            }

            if (score.shieldScore < 1)
            {
                gameStatus = GameStatus.GameOver;
                isPlayingStage1Song = true;
                isPlayingStage2Song = true;
                statusMessage.Enabled = true;
                statusMessage.Visible = true;
            }

            if (gameStatus == GameStatus.GameOver)
            {
                ProcessGameOver();
            }
            else if (gameStatus == GameStatus.Stage1Over)
            {
                ProcessNextStage();
            }
            else
            {
                phaseDelayCounter++;
                if (phaseDelayCounter > PHASE_DELAY)
                {
                    foreach (var enemy in enemyList)
                    {
                        enemy.currentPhase++;
                    }
                    levelDelayCounter++;
                    phaseDelayCounter = 0;
                    //statusMessage.message1 = "WARNING";
                    //statusMessage.message2 = "NEW ENEMIES APPROACHING";
                    //statusMessage.Visible = true;
                }

                if (levelDelayCounter > NUMBER_OF_PHASE - 1)
                {
                    statusMessage.Visible = true;

                    if (gameStatus == GameStatus.Stage1)
                    {
                        gameStatus = GameStatus.Stage1Over;
                    }
                    else if (gameStatus == GameStatus.Stage2)
                    {
                        gameStatus = GameStatus.GameOver;
                    }
                    levelDelayCounter = 0;
                }
            }

            //STAGE 1, STAGE 2 message is visible only for 2 seconds
            if (statusMessage.Visible)
            {
                if (gameStatus == GameStatus.Stage1 || gameStatus == GameStatus.Stage2)
                {
                    statusMessageDelayCounter++;
                    if (statusMessageDelayCounter > STATUS_MESSAGE_DELAY)
                    {
                        statusMessageDelayCounter = 0;
                        statusMessage.Visible = false;
                    }
                }
            }

            base.Update(gameTime);
        }

        public void ProcessNextStage()
        {
            statusMessage.message1 = "STAGE COMPLETE!";
            statusMessage.message2 = "PLAY NEXT STAGE? Y/N";
            player.Enabled = false;
            shield.position = shield.initialPosition;
            shield.Enabled = false;
            foreach (var enemy in enemyList)
            {
                enemy.Enabled = false;
            }
            score.multiplier = 1;
            statusMessage.Visible = true;
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Y))
            {
                foreach (var enemy in enemyList)
                {
                    enemy.currentLevel = 2;
                    enemy.currentPhase = 1;
                }

                foreach (var item in bgList)
                {
                    item.currentbackgroundLevel = 2;
                }
                statusMessageDelayCounter = 0;
                statusMessage.message1 = "STAGE 2";
                statusMessage.message2 = "";
                player.Enabled = true;
                shield.position = shield.initialPosition;
                shield.Enabled = true;
                foreach (var enemy in enemyList)
                {
                    enemy.Enabled = true;

                }

                gameStatus = GameStatus.Stage2;
                isPlayingStage1Song = true;
            }
            else if (ks.IsKeyDown(Keys.N))
            {
                isPlayingStage1Song = true;
                isPlayingStage2Song = true;
                gameStatus = GameStatus.GameOver;
            }
        }

        public void ProcessGameOver()
        {
            statusMessage.message1 = "GAME OVER";
            statusMessage.message2 = "PRESS R TO RESTART";
            player.Enabled = false;
            shield.position = shield.initialPosition;
            shield.Enabled = false;
            foreach (var enemy in enemyList)
            {
                enemy.Enabled = false;

            }

            score.multiplier = 1;
            levelDelayCounter = 0;
            statusMessage.isNoticeEscVisible = true;

            //highScoreInputBox is turned off
            if (highScoreInputBox.Enabled == false)
            {
                //current score is available for high score
                if (score.score > HighScoreComponent.scoreItems[4])
                {
                    highScoreSound.Play();
                    highScoreInputBox.Enabled = true;
                    highScoreInputBox.Visible = true;
                    statusMessage.Visible = false;
                }
                //if current score is not available for high score then show continue message
                else
                {
                    if (gameStatus == GameStatus.GameOver)
                    {
                        statusMessage.Visible = true;
                        KeyboardState ks = Keyboard.GetState();
                        if (ks.IsKeyDown(Keys.R))
                        {
                            initializeStage();
                        }
                    }
                }
            }
            //highScoreInputBox is turned on
            else
            {
                highScoreInputBox.Visible = true;
                statusMessage.Visible = false;
            }
        }
        public void initializeStage()
        {

            isPlayingStage1Song = false;
            isPlayingStage2Song = false;
            isPlayingGameOVerSong = false;
            gameStatus = GameStatus.Stage1;
            phaseDelayCounter = 0;
            levelDelayCounter = 0;
            statusMessageDelayCounter = 0;
            player.Enabled = true;
            player.position = player.initialPosition;

            shield.Enabled = true;
            shield.position = shield.initialPosition;

            foreach (var enemy in enemyList)
            {
                enemy.Enabled = true;
                enemy.Visible = true;
                enemy.position = enemy.initialPosition;
                enemy.currentLevel = 1;
                enemy.currentPhase = 1;
                enemy.currentSpeed = enemy.speed;

            }


            statusMessage.message1 = "STAGE 1";
            statusMessage.message2 = "";
            statusMessage.isNoticeEscVisible = false;
            statusMessage.Visible = true;

            score.score = 0;
            score.shieldScore = 3;

            foreach (var item in bgList)
            {
                item.Enabled = true;
                item.Visible = true;
                item.currentbackgroundLevel = 1;
            }
        }
    }
}
