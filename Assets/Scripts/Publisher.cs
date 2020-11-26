using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Publisher
    {

        //Delegate definition
        public delegate void DeathEventHandler();
        public delegate void DamageEventHandler(int damage);
        public delegate void EnemyDeathEventHandler(GameObject enemy);
        public delegate void StarStrikeEventHandler();
        public delegate void MoonDustBarFullEventHandler();

        //Event Definition
        public event DeathEventHandler PlayerDeath;
        public event DamageEventHandler PlayerHit;
        public event EnemyDeathEventHandler EnemyDeath;
        public event StarStrikeEventHandler StarStrike;
        public event MoonDustBarFullEventHandler MoonDustBarFull;

        public void CallPlayerDeath() => PlayerDeath?.Invoke();
        public void CallPlayerHit(int damage) => PlayerHit?.Invoke(damage);
        public void CallEnemyDeath(GameObject enemy) => EnemyDeath?.Invoke(enemy);
        public void CallStarStrike() => StarStrike?.Invoke();
        public void CallDisplayStarStrikeInstructions() => MoonDustBarFull?.Invoke();

        //Singleton
        private static Publisher instance;
        protected Publisher() { }

        public static Publisher publish
        {
            get
            {
                if (instance == null) instance = new Publisher();
                return instance;
            }
        }
    }
}
