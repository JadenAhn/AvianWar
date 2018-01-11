using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvianWar
{
    class CollisionDetection : GameComponent
    {
        private Shield shield;
        private List<Enemy> enemyList;
        private SoundEffect hitSound;
        private SoundEffect explodeSound;
        private Explosion explosion;
        private Score score;

        public CollisionDetection(Game game,
            Shield shield,
            List<Enemy> enemyList,
            Explosion explosion,
            Score score,
            SoundEffect hitSound,
            SoundEffect explodeSound) : base(game)
        {
            this.shield = shield;
            this.enemyList = enemyList;
            this.score = score;
            this.explosion = explosion;
            this.hitSound = hitSound;
            this.explodeSound = explodeSound;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var enemy in enemyList)
            {
                if (enemy.Visible)
                {
                    //shield is going right and touching left side of the enemy
                    if (shield.speed.X > 0 &&
                        shield.getBound().Right + shield.speed.X > enemy.getBound().Left &&
                        shield.getBound().Left < enemy.getBound().Left &&
                        shield.getBound().Bottom > enemy.getBound().Top &&
                        shield.getBound().Top < enemy.getBound().Bottom)
                    {
                        shield.speed.X = -shield.speed.X;
                        score.score += (score.multiplier * enemy.enemyScore);
                        score.multiplier++;
                        enemy.Visible = false;
                        explosion.Position = enemy.position;
                        explosion.StartAnimation();
                        hitSound.Play();
                        explodeSound.Play();
                    }

                    //shield is going left and touching right side of the enemy
                    if (shield.speed.X < 0 &&
                        shield.getBound().Left + shield.speed.X < enemy.getBound().Right &&
                        shield.getBound().Right > enemy.getBound().Right &&
                        shield.getBound().Bottom > enemy.getBound().Top &&
                        shield.getBound().Top < enemy.getBound().Bottom)
                    {
                        shield.speed.X = -shield.speed.X;
                        score.score += (score.multiplier * enemy.enemyScore);
                        score.multiplier++;
                        enemy.Visible = false;
                        explosion.Position = enemy.position;
                        explosion.StartAnimation();
                        hitSound.Play();
                        explodeSound.Play();
                    }

                    //shield is going down and touching top side of the enemy
                    if (shield.speed.Y > 0 &&
                        shield.getBound().Bottom + shield.speed.Y > enemy.getBound().Top &&
                        shield.getBound().Top < enemy.getBound().Top &&
                        shield.getBound().Right > enemy.getBound().Left &&
                        shield.getBound().Left < enemy.getBound().Right)
                    {
                        shield.speed.Y = -shield.speed.Y;
                        score.score += (score.multiplier * enemy.enemyScore);
                        score.multiplier++;
                        enemy.Visible = false;
                        explosion.Position = enemy.position;
                        explosion.StartAnimation();
                        hitSound.Play();
                        explodeSound.Play();
                    }

                    //shield is going up and touching bottom side of the enemy
                    if (shield.speed.Y < 0 &&
                        shield.getBound().Top + shield.speed.Y < enemy.getBound().Bottom &&
                        shield.getBound().Bottom > enemy.getBound().Bottom &&
                        shield.getBound().Right > enemy.getBound().Left &&
                        shield.getBound().Left < enemy.getBound().Right)
                    {
                        shield.speed.Y = -shield.speed.Y;
                        score.score += (score.multiplier * enemy.enemyScore);
                        score.multiplier++;
                        enemy.Visible = false;
                        explosion.Position = enemy.position;
                        explosion.StartAnimation();
                        hitSound.Play();
                        explodeSound.Play();
                    }
                }
            }


            base.Update(gameTime);
        }
    }
}
