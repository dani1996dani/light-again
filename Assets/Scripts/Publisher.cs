using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Publisher
    {

        //Delegate definition
        public delegate void DeathEventHandler();
        public delegate void DamageEventHandler(int damage);

        //Event Definition
        public event DeathEventHandler PlayerDeath;
        public event DamageEventHandler PlayerHit;

        public void CallPlayerDeath() => PlayerDeath?.Invoke();
        public void CallPlayerHit(int damage) => PlayerHit?.Invoke(damage);

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
