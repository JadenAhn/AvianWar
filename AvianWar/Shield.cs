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
    public class Shield : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Texture2D iconTex;
        public Vector2 position;
        public Vector2 iconPosition;
        public Vector2 initialPosition;
        public Vector2 speed;
        private PlayerCharacter player;
        private Score score;
        private Explosion explosion;
        private SoundEffect hitWallSound;
        private SoundEffect getShieldSound;
        private SoundEffect shieldThrowSound;
        private SoundEffect explodeSound;
        const int SHIELD_SPEED_X = 10;
        const int SHIELD_SPEED_Y = 12;

        //Position allowance to get the shield back
        const int ALLOWANCE_AREA = 120;
        private bool isFlying;
        private bool isReturning;

        public Shield(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Texture2D iconTex,
            PlayerCharacter player,
            Score score,
            Explosion explosion,
            SoundEffect hitWallSound,
            SoundEffect getShieldSound,
            SoundEffect shieldThrowSound,
            SoundEffect explodeSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.iconTex = iconTex;
            isFlying = false;
            isReturning = false;
            this.player = player;
            this.score = score;
            this.explosion = explosion;
            this.hitWallSound = hitWallSound;
            this.getShieldSound = getShieldSound;
            this.shieldThrowSound = shieldThrowSound;
            this.explodeSound = explodeSound;
            initialPosition = new Vector2(player.position.X + (player.dimension.X / 2) - (tex.Width / 2) - 10, player.position.Y + 10);
            position = initialPosition;
            iconPosition = new Vector2(10 , Shared.stage.Y - 55 );
            this.speed = new Vector2(SHIELD_SPEED_X, SHIELD_SPEED_Y);
        }

        public override void Update(GameTime gameTime)
        {
            initialPosition = new Vector2(player.position.X + (player.dimension.X / 2) - (tex.Width / 2) - 10, player.position.Y + 10);
            if (isFlying)
            {
                position += speed;
                float xDiff = (initialPosition.X - 48) - position.X;
                float yDiff = (initialPosition.Y - (speed.Y * 3)) - position.Y;

                if (position.X < 0)
                {
                    explosion.Position = position;
                    explosion.StartAnimation();
                    explodeSound.Play();
                    isReturning = false;
                    isFlying = false;
                    speed.X = Math.Abs(speed.X);
                    position = initialPosition;
                    score.multiplier = 1;
                    score.shieldScore--;
                }

                if (position.X >= 0 & speed.X < 0 & Math.Abs(xDiff) < ALLOWANCE_AREA & Math.Abs(yDiff) < ALLOWANCE_AREA)
                {
                    isReturning = true;
                    speed.X = SHIELD_SPEED_X;
                }

                if (position.X >= Shared.stage.X - tex.Width)
                {
                    speed.X = -SHIELD_SPEED_X;
                    hitWallSound.Play();
                }

                if (position.Y <= 0)
                {
                    score.multiplier++;
                    speed.Y = SHIELD_SPEED_Y;
                    hitWallSound.Play();
                }

                if (position.Y + tex.Height >= Shared.stage.Y)
                {
                    score.multiplier++;
                    speed.Y = -SHIELD_SPEED_Y;
                    hitWallSound.Play();
                }
            }
            else
            {
                position = initialPosition;
                isReturning = false;
                KeyboardState ks = Keyboard.GetState();

                if (ks.IsKeyDown(Keys.Up) && ks.IsKeyDown(Keys.Space))
                {
                    speed.Y = -SHIELD_SPEED_Y;
                    isFlying = true;
                    shieldThrowSound.Play();
                }
                else if (ks.IsKeyDown(Keys.Down) && ks.IsKeyDown(Keys.Space))
                {
                    speed.Y = SHIELD_SPEED_Y;
                    isFlying = true;
                    shieldThrowSound.Play();
                }
                else if (ks.IsKeyDown(Keys.Space))
                {
                    speed.Y = 0;
                    isFlying = true;
                    shieldThrowSound.Play();
                }
            }

            if (isReturning)
            {
                score.multiplier = 1;
                float xDiff = (initialPosition.X - 48) - position.X;
                float yDiff = (initialPosition.Y - (speed.Y * 3)) - position.Y;

                position.X += xDiff * 0.2f;
                position.Y += yDiff * 0.2f;

                //When the shield gets closer, set the position to initial position
                if (Math.Abs(initialPosition.X - position.X) < 9f)
                {
                    isFlying = false;
                    isReturning = false;
                    getShieldSound.Play();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.Draw(iconTex, iconPosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBound()
        {
            //return the position of the rectangle
            return new Rectangle((int)position.X, (int)position.Y, (int)tex.Width, (int)tex.Height);
        }
    }
}
