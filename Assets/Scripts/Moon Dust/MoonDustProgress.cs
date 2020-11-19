using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonDustProgress : MonoBehaviour
{
    public int moonDustCollectedAmount;

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
        ++moonDustCollectedAmount;
    }
}
