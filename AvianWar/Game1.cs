using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace AvianWar
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Song songGameTitle;
        SoundEffect enterMenuSound;

        //declare all the scenes here
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private HighScoreScene highScoreScene;
        private AboutScene aboutScene;
        const int SCREEN_WIDTH = 800;
        const int SCREEN_HEIGHT = 600;
        private bool isPlayingTitleMusic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Initialize stage size
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            HighScoreComponent.LoadHighScore(out HighScoreComponent.nameItems, out HighScoreComponent.scoreItems);
            isPlayingTitleMusic = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.Show();
            //Create other scenes here and add to components list

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);
            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);
            highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);

            //Load BGM
            songGameTitle = Content.Load<Song>("Sounds/SongGameTitle");
            enterMenuSound = Content.Load<SoundEffect>("Sounds/EnterMenuSound");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (actionScene.Enabled == false && !isPlayingTitleMusic)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(songGameTitle);
                MediaPlayer.IsRepeating = true;
                isPlayingTitleMusic = true;
            }

            //"START GAME", "HIGH SCORE", "HELP", "ABOUT", "QUIT"
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (ks.IsKeyDown(Keys.Enter))
                {
                    enterMenuSound.Play();
                    if (selectedIndex == 0)
                    {
                        HideAllScenes();
                        actionScene.Show();
                        MediaPlayer.Stop();
                        isPlayingTitleMusic = false;
                        actionScene.phaseControl.isPlayingStage1Song = false;
                    }
                    else if (selectedIndex == 1)
                    {
                        HideAllScenes();
                        highScoreScene.Show();
                    }
                    else if (selectedIndex == 2)
                    {
                        HideAllScenes();
                        helpScene.Show();
                    }
                    else if (selectedIndex == 3)
                    {
                        HideAllScenes();
                        aboutScene.Show();
                    }
                    else if (selectedIndex == 4)
                    {
                        Exit();
                    }
                }
            }

            if (actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if (actionScene.phaseControl.gameStatus == GamePhaseControl.GameStatus.GameOver)
                    {
                        if (!isPlayingTitleMusic)
                        {
                            MediaPlayer.Stop();
                            MediaPlayer.Play(songGameTitle);
                            MediaPlayer.IsRepeating = true;
                            isPlayingTitleMusic = true;
                        }
                        enterMenuSound.Play();
                        HideAllScenes();
                        startScene.Show();
                        startScene.initializeAnimation();
                    }

                }
            }
            if (helpScene.Enabled || highScoreScene.Enabled || aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if (!isPlayingTitleMusic)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(songGameTitle);
                        MediaPlayer.IsRepeating = true;
                        isPlayingTitleMusic = true;
                    }
                    enterMenuSound.Play();
                    HideAllScenes();
                    startScene.Show();
                    startScene.initializeAnimation();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(218, 240, 241));

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void HideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.Hide();
                }
            }
        }
    }
}
