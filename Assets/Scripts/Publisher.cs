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
        public delegate void EventHandler();

        //Event Definition
        public event EventHandler PlayerDeath;


        public void CallPlayerDeath() => PlayerDeath?.Invoke();

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
