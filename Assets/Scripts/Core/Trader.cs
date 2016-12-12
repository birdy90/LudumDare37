using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Trader : MonoBehaviour {
    public static Trader Instance;

    public List<Plant> Plants;

    public void Start()
    {
        Instance = this;
    }

    public void SellPlant(Plant soldPlant)
    {
        foreach (var plant in Plants)
        {
            if (plant.gameObject.tag == soldPlant.gameObject.tag)
            {
                if (plant.InitialGrowthValue >= 0)
                {
                    var growthArrayIndex = Math.Min(plant.InitialGrowthValue / 5, 9);
                    gameObject.GetComponent<Money>().Amount += plant.InitialGrowthValue;
                }
                break;
            }
        }
    }
}
