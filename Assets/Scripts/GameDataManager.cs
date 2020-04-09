using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    private int asteroidCount = 1;
    public int AsteroidCount => asteroidCount;

    public int addRemoveAmount = 1;
    
    public void adjustAsteroidCount(int asteroidAdjustAmount)
    {
        asteroidCount += asteroidAdjustAmount;
        if (asteroidCount < 1)
        {
            asteroidCount = 0;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
