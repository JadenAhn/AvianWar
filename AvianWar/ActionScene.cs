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
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Explosion explosion;
        private Explosion shieldExplosion;
        private PlayerCharacter player;
        private Enemy enemy;
        private CollisionDetection collision;
        private Score score;
        private HighScoreInputBox highScoreInputBox;
        private GameStatusMessage statusMessage;
        public GamePhaseControl phaseControl;
        private WarningMessage warningMessage;
        private Song songStage1;
        private Song songStage2;
        private Song songGameOver;
        private SoundEffect hitsound;
        private SoundEffect hitWallSound;
        private SoundEffect getShieldSound;
        private SoundEffect explodeSound;
        private SoundEffect shieldThrowSound;
        private SoundEffect moveCursorSound;
        private SoundEffect selectLetterSound;
        private SoundEffect highScoreSound;
        private SoundEffect warningSound;

        private List<ScrollingBackground> bglist;
        private List<Enemy> enemyList;
        public ActionScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            spriteBatch = g.spriteBatch;

            //For Stage 1
            Texture2D scrollTex1 = g.Content.Load<Texture2D>("Images/Scroll1");
            Texture2D scrollTex2 = g.Content.Load<Texture2D>("Images/Scroll2");
            Texture2D scrollTex3 = g.Content.Load<Texture2D>("Images/Scroll3");

            //For Stage 2
            Texture2D scrollTex4 = g.Content.Load<Texture2D>("Images/Scroll4");
            Texture2D scrollTex5 = g.Content.Load<Texture2D>("Images/Scroll5");
            Texture2D scrollTex6 = g.Content.Load<Texture2D>("Images/Stage2BackGround");
            Texture2D sunTex = g.Content.Load<Texture2D>("Images/Sun");
            Texture2D cloudTex = g.Content.Load<Texture2D>("Images/Cloud");


            bglist = new List<ScrollingBackground>();
            Vector2 posSun = new Vector2(0, 0);
            Vector2 sunSspeed = new Vector2(0, 0);
            Vector2 cloudSspeed = new Vector2(1, 0);
            ScrollingBackground sun = new ScrollingBackground(game, spriteBatch, sunTex, posSun, sunSspeed, 1);
            ScrollingBackground cloud = new ScrollingBackground(game, spriteBatch, cloudTex, posSun, cloudSspeed, 1);

            Vector2 pos1 = new Vector2(0, Shared.stage.Y - scrollTex1.Height);
            Vector2 speed1 = new Vector2(7, 0);
            ScrollingBackground sb1 = new ScrollingBackground(game, spriteBatch, scrollTex1, pos1, speed1, 1);

            Vector2 pos2 = new Vector2(0, Shared.stage.Y - scrollTex2.Height);
            Vector2 speed2 = new Vector2(3, 0);
            ScrollingBackground sb2 = new ScrollingBackground(game, spriteBatch, scrollTex2, pos2, speed2, 1);

            Vector2 pos3 = new Vector2(0, Shared.stage.Y - scrollTex3.Height);
            Vector2 speed3 = new Vector2(1, 0);
            ScrollingBackground sb3 = new ScrollingBackground(game, spriteBatch, scrollTex3, pos3, speed3, 1);

            Vector2 pos4 = new Vector2(0, 0);
            Vector2 speed4 = new Vector2(5, 0);
            ScrollingBackground sb4 = new ScrollingBackground(game, spriteBatch, scrollTex4, pos4, speed4, 2);

            Vector2 pos5 = new Vector2(0, 0);
            Vector2 speed5 = new Vector2(1, 0);
            ScrollingBackground sb5 = new ScrollingBackground(game, spriteBatch, scrollTex5, pos5, speed5, 2);

            //This is for dark background
            Vector2 pos6 = new Vector2(0, 0);
            Vector2 speed6 = new Vector2(0, 0);
            ScrollingBackground sb6 = new ScrollingBackground(game, spriteBatch, scrollTex6, pos6, speed6, 2);
            bglist.Add(sb3);
            bglist.Add(sb2);
            bglist.Add(sb1);
            bglist.Add(sun);
            bglist.Add(cloud);
            bglist.Add(sb6);
            bglist.Add(sb5);
            bglist.Add(sb4);


            foreach (var item in bglist)
            {
                this.Components.Add(item);
            }

            //Sounds
            songStage1 = g.Content.Load<Song>("Sounds/SongStage1");
            songStage2 = g.Content.Load<Song>("Sounds/SongStage2");
            songGameOver = g.Content.Load<Song>("Sounds/SongGameOver");
            hitsound = g.Content.Load<SoundEffect>("Sounds/HitSound");
            hitWallSound = g.Content.Load<SoundEffect>("Sounds/HitWallSound");
            getShieldSound = g.Content.Load<SoundEffect>("Sounds/GetShieldSound");
            shieldThrowSound = g.Content.Load<SoundEffect>("Sounds/ShieldThrowSound");
            explodeSound = g.Content.Load<SoundEffect>("Sounds/ExplodeSound");
            moveCursorSound = g.Content.Load<SoundEffect>("Sounds/SelectMenuSound");
            selectLetterSound = g.Content.Load<SoundEffect>("Sounds/SelectLetterSound");
            highScoreSound = g.Content.Load<SoundEffect>("Sounds/HighScoreSound");
            warningSound = g.Content.Load<SoundEffect>("Sounds/WarningSound");

            //Player
            Texture2D playerTex = g.Content.Load<Texture2D>("Images/Player");
            Vector2 dimension = new Vector2(60, 39);
            Vector2 playerPosition = new Vector2(30, (Shared.stage.Y - dimension.Y) / 2);
            player = new PlayerCharacter(game, g.spriteBatch, playerTex, dimension, playerPosition);
            this.Components.Add(player);

            //Status message
            SpriteFont titleMessageFont = g.Content.Load<SpriteFont>("Fonts/StatusTitleFont");
            SpriteFont regularMessageFont = g.Content.Load<SpriteFont>("Fonts/StatusRegularFont");
            SpriteFont noticeEscFont = g.Content.Load<SpriteFont>("Fonts/RegularFont");

            statusMessage = new GameStatusMessage(game, spriteBatch, titleMessageFont, regularMessageFont, noticeEscFont);
            

            //score
            SpriteFont scoreFont = g.Content.Load<SpriteFont>("Fonts/ScoreFont");
            SpriteFont comboFont = g.Content.Load<SpriteFont>("Fonts/ComboFont");
            SpriteFont shieldScoreFont = g.Content.Load<SpriteFont>("Fonts/ShieldScoreFont");
            score = new Score(game, spriteBatch, scoreFont, comboFont, shieldScoreFont);
            

            //High Score
            SpriteFont inputTitleFont = g.Content.Load<SpriteFont>("Fonts/InputTitleFont");
            SpriteFont inputBodyFont = g.Content.Load<SpriteFont>("Fonts/InputBodyFont");
            SpriteFont inputBodySmallFont = g.Content.Load<SpriteFont>("Fonts/InputBodySmallFont");
            highScoreInputBox = new HighScoreInputBox(game, spriteBatch, inputTitleFont, inputBodyFont, inputBodySmallFont, score, moveCursorSound, selectLetterSound);


            //Explosion effect
            Texture2D exposionTex = g.Content.Load<Texture2D>("Images/Explosion");
            shieldExplosion = new Explosion(game, spriteBatch, exposionTex, Vector2.Zero, 1);
            this.Components.Add(shieldExplosion);
            //delay 5/60 sec
            
            //Shield
            Texture2D shieldTex = g.Content.Load<Texture2D>("Images/Shield");
            Texture2D iconTex = g.Content.Load<Texture2D>("Images/ShieldIcon");
            Shield shield = new Shield(game, g.spriteBatch, shieldTex, iconTex, player, score, shieldExplosion, hitWallSound, getShieldSound, shieldThrowSound, explodeSound);
            this.Components.Add(shield);

            
            Texture2D enemyTex = g.Content.Load<Texture2D>("Images/Enemy");
            Texture2D enemyTexVar1 = g.Content.Load<Texture2D>("Images/EnemyVar1");
            Texture2D enemyTexVar2 = g.Content.Load<Texture2D>("Images/EnemyVar2");
            Vector2 enemyDimension = new Vector2(60, 39);
            Vector2 turnPosition;
            Vector2 turnSpeed;
            int enemyPoint;


            enemyList = new List<Enemy>();
            
            //Decide all the patterns here!!!
            //Stage 1
            //Pattern 1: Diagonal
            for (int i = 1; i <= 30; i++)
            {
                explosion = new Explosion(game, spriteBatch, exposionTex, Vector2.Zero, 1);
                this.Components.Add(explosion);

                turnPosition = Vector2.Zero;
                turnSpeed = Vector2.Zero;
                enemyPoint = 100;
                Vector2 enemyPosition;
                Vector2 enemySpeed;
                Texture2D currentTex = enemyTex;
                int enemyLevel = 1;
                int enemyPhase = 1;
                //1-5
                if (i <= 5)
                {
                    enemyPosition = new Vector2((Shared.stage.X) + (80 * i), -20 - (80 * i));
                    enemySpeed = new Vector2(-1, 1);
                    turnPosition = new Vector2(400, 0);
                    turnSpeed = new Vector2(1, 1);
                }
                //6-10
                else if (i <= 10)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) + 80 + (80 * (i - 5)), -20 - (80 * (i - 5)));
                    enemySpeed = new Vector2(-1, 1);
                }
                //11-15
                else if (i <= 15)
                {
                    enemyPosition = new Vector2((Shared.stage.X) + 160 + (80 * (i - 10)), -20 - (80 * (i - 10)));
                    enemySpeed = new Vector2(-1, 1);
                    turnPosition = new Vector2(400, 0);
                    turnSpeed = new Vector2(1, 1);
                }

                //16-20
                else if (i <= 20)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) + (80 * (i - 15)), Shared.stage.Y + (80 * (i - 15)));
                    enemySpeed = new Vector2(-1, -1);
                    turnPosition = new Vector2(400, 0);
                    turnSpeed = new Vector2(1, -1);
                }
                //21-25
                else if (i <= 25)
                {
                    currentTex = enemyTex;
                    enemyPosition = new Vector2((Shared.stage.X) + 80 + (80 * (i - 20)), Shared.stage.Y + (80 * (i - 20)));
                    enemySpeed = new Vector2(-1, -1);
                }
                //26-30
                else
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) + 160 + (80 * (i - 25)), Shared.stage.Y + (80 * (i - 25)));
                    enemySpeed = new Vector2(-1, -1);
                    turnPosition = new Vector2(400, 0);
                    turnSpeed = new Vector2(1, -1);
                }
                enemy = new Enemy(game, g.spriteBatch, statusMessage, currentTex, enemyDimension, enemyPosition, enemySpeed, enemyPoint, i, enemyLevel, enemyPhase, turnPosition, turnSpeed);
                enemyList.Add(enemy);
            }

            //Pattern 2: Diagonal-Reverse
            for (int i = 1; i <= 30; i++)
            {
                explosion = new Explosion(game, spriteBatch, exposionTex, Vector2.Zero, 1);
                this.Components.Add(explosion);
                turnPosition = Vector2.Zero;
                turnSpeed = Vector2.Zero;
                enemyPoint = 100;
                Vector2 enemyPosition;
                Vector2 enemySpeed;
                Texture2D currentTex = enemyTex;
                int enemyLevel = 1;
                int enemyPhase = 2;

                //1-5
                if (i <= 5)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2(200 + (- 80 * i), Shared.stage.Y + (80 * i));
                    enemySpeed = new Vector2(1, -1);
                }
                //6-10
                else if (i <= 10)
                {
                    currentTex = enemyTex;
                    enemyPosition = new Vector2(280 + (-80 * (i - 5)), Shared.stage.Y + (80 * (i - 5)));
                    enemySpeed = new Vector2(1, -1);
                }
                //11-15
                else if (i <= 15)
                {
                    enemyPosition = new Vector2(360 + (-80 * (i - 10)), Shared.stage.Y + (80 * (i - 10)));
                    enemySpeed = new Vector2(1, -1);
                }

                //16-20
                else if (i <= 20)
                {
                    enemyPosition = new Vector2(200 + 80 * -(i - 15), -20 - (80 * (i - 15)));
                    enemySpeed = new Vector2(1, 1);
                }
                //21-25
                else if (i <= 25)
                {
                    enemyPosition = new Vector2(280 + (80 * -(i - 20)), - 20 - (80 * (i - 20)));
                    enemySpeed = new Vector2(1, 1);
                }
                //26-30
                else
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2(360 + (80 * -(i - 25)), - 20 - (80 * (i - 25)));
                    enemySpeed = new Vector2(1, 1);
                }

                enemy = new Enemy(game, g.spriteBatch, statusMessage, currentTex, enemyDimension, enemyPosition, enemySpeed, enemyPoint, i, enemyLevel, enemyPhase, turnPosition, turnSpeed);
                enemyList.Add(enemy);
            }

            //Pattern 3: Up and Down
            for (int i = 1; i <= 20; i++)
            {
                explosion = new Explosion(game, spriteBatch, exposionTex, Vector2.Zero, 1);
                this.Components.Add(explosion);
                turnPosition = Vector2.Zero;
                turnSpeed = Vector2.Zero;
                enemyPoint = 100;
                Vector2 enemyPosition;
                Vector2 enemySpeed;
                Texture2D currentTex = enemyTex;
                int enemyLevel = 1;
                int enemyPhase = 3;
                //1-5
                if (i <= 5)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) - 400, 10 - (80 * i));
                    enemySpeed = new Vector2(0, 1);
                    turnPosition = new Vector2(0, 200);
                    turnSpeed = new Vector2(1, 0);
                }
                //6-10
                else if (i <= 10)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) - 100, 10 - (80 * (i - 5)));
                    enemySpeed = new Vector2(0, 1);
                }

                //11-15
                else if (i <= 15)
                {
                    currentTex = enemyTex;
                    enemyPosition = new Vector2((Shared.stage.X) - 250, Shared.stage.Y - 70 + (80 * (i - 10)));
                    enemySpeed = new Vector2(0, -1);
                    turnPosition = new Vector2(0, 400);
                    turnSpeed = new Vector2(1, 0);
                }

                //16-20
                else
                {
                    currentTex = enemyTex;
                    enemyPosition = new Vector2((Shared.stage.X) - 550, Shared.stage.Y - 70 + (80 * (i - 15)));
                    enemySpeed = new Vector2(0, -1);
                }

                enemy = new Enemy(game, g.spriteBatch, statusMessage, currentTex, enemyDimension, enemyPosition, enemySpeed, enemyPoint, i, enemyLevel, enemyPhase, turnPosition, turnSpeed);
                enemyList.Add(enemy);
            }

            //For Stage 2
            //Pattern 1: Diagonal
            for (int i = 1; i <= 30; i++)
            {
                explosion = new Explosion(game, spriteBatch, exposionTex, Vector2.Zero, 1);
                this.Components.Add(explosion);
                turnPosition = Vector2.Zero;
                turnSpeed = Vector2.Zero;
                enemyPoint = 100;
                Vector2 enemyPosition;
                Vector2 enemySpeed;
                Texture2D currentTex = enemyTex;
                int enemyLevel = 2;
                int enemyPhase = 1;
                //1-5
                if (i <= 5)
                {
                    enemyPosition = new Vector2((Shared.stage.X) + (80 * i), -20 - (80 * i));
                    enemySpeed = new Vector2(-1, 1);
                    turnPosition = new Vector2(300, 0);
                    turnSpeed = new Vector2(0, 1);
                }
                //6-10
                else if (i <= 10)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) + 80 + (80 * (i - 5)), -20 - (80 * (i - 5)));
                    enemySpeed = new Vector2(-1, 1);
                    turnPosition = new Vector2(380, 0);
                    turnSpeed = new Vector2(0, 1);
                }
                //11-15
                else if (i <= 15)
                {
                    currentTex = enemyTexVar2;
                    enemyPoint = 300;
                    enemyPosition = new Vector2((Shared.stage.X) + 160 + (80 * (i - 10)), -20 - (80 * (i - 10)));
                    enemySpeed = new Vector2(-1, 1);
                    turnPosition = new Vector2(460, 0);
                    turnSpeed = new Vector2(0, 1);
                }

                //16-20
                else if (i <= 20)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) + (80 * (i - 15)), Shared.stage.Y + (80 * (i - 15)));
                    enemySpeed = new Vector2(-1, -1);
                    turnPosition = new Vector2(340, 0);
                    turnSpeed = new Vector2(0, -1);
                }
                //21-25
                else if (i <= 25)
                {
                    currentTex = enemyTex;
                    enemyPosition = new Vector2((Shared.stage.X) + 80 + (80 * (i - 20)), Shared.stage.Y + (80 * (i - 20)));
                    enemySpeed = new Vector2(-1, -1);
                    turnPosition = new Vector2(420, 0);
                    turnSpeed = new Vector2(0, -1);
                }
                //26-30
                else
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) + 160 + (80 * (i - 25)), Shared.stage.Y + (80 * (i - 25)));
                    enemySpeed = new Vector2(-1, -1);
                    turnPosition = new Vector2(500, 0);
                    turnSpeed = new Vector2(0, -1);
                }
                enemy = new Enemy(game, g.spriteBatch, statusMessage, currentTex, enemyDimension, enemyPosition, enemySpeed, enemyPoint, i, enemyLevel, enemyPhase, turnPosition, turnSpeed);
                enemyList.Add(enemy);
            }

            //Pattern 2: Up and Down
            for (int i = 1; i <= 20; i++)
            {
                explosion = new Explosion(game, spriteBatch, exposionTex, Vector2.Zero, 1);
                this.Components.Add(explosion);
                turnPosition = Vector2.Zero;
                turnSpeed = Vector2.Zero;
                enemyPoint = 100;
                Vector2 enemyPosition;
                Vector2 enemySpeed;
                Texture2D currentTex = enemyTex;
                int enemyLevel = 2;
                int enemyPhase = 2;
                //1-5
                if (i <= 5)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) - 400, 10 - (80 * i));
                    enemySpeed = new Vector2(0, 1);
                    turnPosition = new Vector2(0, 280);
                    turnSpeed = new Vector2(1, -1);
                }
                //6-10
                else if (i <= 10)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2((Shared.stage.X) - 100, 10 - (80 * (i - 5)));
                    enemySpeed = new Vector2(0, 1);
                }

                //11-15
                else if (i <= 15)
                {
                    currentTex = enemyTexVar2;
                    enemyPoint = 300;
                    enemyPosition = new Vector2((Shared.stage.X) - 250, Shared.stage.Y - 70 + (80 * (i - 10)));
                    enemySpeed = new Vector2(0, -1);
                    turnPosition = new Vector2(0, 320);
                    turnSpeed = new Vector2(1, 1);
                }
                //16-20
                else
                {
                    currentTex = enemyTex;
                    enemyPosition = new Vector2((Shared.stage.X) - 550, Shared.stage.Y - 70 + (80 * (i - 15)));
                    enemySpeed = new Vector2(0, -1);
                }

                enemy = new Enemy(game, g.spriteBatch, statusMessage, currentTex, enemyDimension, enemyPosition, enemySpeed, enemyPoint, i, enemyLevel, enemyPhase, turnPosition, turnSpeed);
                enemyList.Add(enemy);
            }

            //Pattern 3: Diagonal-Reverse
            for (int i = 1; i <= 30; i++)
            {
                explosion = new Explosion(game, spriteBatch, exposionTex, Vector2.Zero, 1);
                this.Components.Add(explosion);
                turnPosition = Vector2.Zero;
                turnSpeed = Vector2.Zero;
                enemyPoint = 100;
                Vector2 enemyPosition;
                Vector2 enemySpeed;
                Texture2D currentTex = enemyTex;
                int enemyLevel = 2;
                int enemyPhase = 3;

                //1-5
                if (i <= 5)
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2(200 + (-80 * i), Shared.stage.Y + (80 * i));
                    enemySpeed = new Vector2(1, -1);
                    turnPosition = new Vector2(400, 0);
                    turnSpeed = new Vector2(0, -1);
                }
                //6-10
                else if (i <= 10)
                {
                    currentTex = enemyTex;
                    enemyPosition = new Vector2(280 + (-80 * (i - 5)), Shared.stage.Y + (80 * (i - 5)));
                    enemySpeed = new Vector2(1, -1);
                }
                //11-15
                else if (i <= 15)
                {
                    currentTex = enemyTexVar2;
                    enemyPoint = 300;
                    enemyPosition = new Vector2(360 + (-80 * (i - 10)), Shared.stage.Y + (80 * (i - 10)));
                    enemySpeed = new Vector2(1, -1);
                    turnPosition = new Vector2(560, 0);
                    turnSpeed = new Vector2(0, -1);
                }

                //16-20
                else if (i <= 20)
                {
                    enemyPosition = new Vector2(200 + 80 * -(i - 15), -20 - (80 * (i - 15)));
                    enemySpeed = new Vector2(1, 1);
                    turnPosition = new Vector2(400, 0);
                    turnSpeed = new Vector2(1, -1);

                }
                //21-25
                else if (i <= 25)
                {
                    enemyPosition = new Vector2(280 + (80 * -(i - 20)), -20 - (80 * (i - 20)));
                    enemySpeed = new Vector2(1, 1);
                }
                //26-30
                else
                {
                    currentTex = enemyTexVar1;
                    enemyPoint = 200;
                    enemyPosition = new Vector2(360 + (80 * -(i - 25)), -20 - (80 * (i - 25)));
                    enemySpeed = new Vector2(1, 1);
                    turnPosition = new Vector2(560, 0);
                    turnSpeed = new Vector2(1, 0);
                }

                enemy = new Enemy(game, g.spriteBatch, statusMessage, currentTex, enemyDimension, enemyPosition, enemySpeed, enemyPoint, i, enemyLevel, enemyPhase, turnPosition, turnSpeed);
                enemyList.Add(enemy);
            }

            foreach (var item in enemyList)
            {
                this.Components.Add(item);
            }

            collision = new CollisionDetection(game, shield, enemyList, explosion, score, hitsound, explodeSound);
            this.Components.Add(collision);

            phaseControl = new GamePhaseControl(game, player, shield, enemyList, score, statusMessage, highScoreInputBox, bglist, songStage1, songStage2, songGameOver, highScoreSound);
            this.Components.Add(phaseControl);


            this.Components.Add(score);
            this.Components.Add(highScoreInputBox);
            this.Components.Add(statusMessage);

            Texture2D warningTex = g.Content.Load<Texture2D>("Images/WarningMessage");
            warningMessage = new WarningMessage(game, spriteBatch, warningTex, phaseControl, warningSound);
            this.Components.Add(warningMessage);
        }

        //Don't override update and draw because it is inheriting GameScene and GameScene is updating and drawing already
    }
}
