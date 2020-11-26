using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class MoonDustProgress : MonoBehaviour
{
    [SerializeField]
    private int moonDustCollectedAmount;

    private void Start()
    {
        moonDustCollectedAmount = 0;
    }

    public void ResetMoonDustAmount()
    {
        moonDustCollectedAmount = 0;
    }

    public void IncrementMoonDustAmount()
    {
        if(moonDustCollectedAmount < Settings.MoonDustMaxAmount)
        {
            ++moonDustCollectedAmount;
        }
    }

    public int GetCurrentAmount()
    {
        return moonDustCollectedAmount;
    }

    public bool IsFull()
    {
        if (moonDustCollectedAmount >= Settings.MoonDustMaxAmount)
        {
            if (Settings.isFirstTimeFullBar)
            {
                Publisher.publish.CallDisplayStarStrikeInstructions();
            }
            Settings.isFirstTimeFullBar = false;            
            return true;
        }
        return false;
    }
}
