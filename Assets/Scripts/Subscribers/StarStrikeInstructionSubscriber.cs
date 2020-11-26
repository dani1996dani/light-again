using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Subscribers
{
    public class StarStrikeInstructionSubscriber : MonoBehaviour
    {
        void OnEnable()
        {
            Publisher.publish.MoonDustBarFull += OnMoonDustBarFull;
        }

        private void OnMoonDustBarFull()
        {            
            //TODO: Show instructions            
        }

        void OnDisable()
        {
            Publisher.publish.MoonDustBarFull -= OnMoonDustBarFull;
        }

    }
}
