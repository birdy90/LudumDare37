﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIPlant : MonoBehaviour {
    public Text UIText;
    public Image UIImage;
    public Plant PlantPrefab;

    private Image _renderer;

    void Start () {
        UIText.text = PlantPrefab.Name+"\nCost: "+PlantPrefab.InitialBuyCost+"\nProfit: "+PlantPrefab.GrowthValueCost[0]+"-"+ PlantPrefab.GrowthValueCost[9];
        UIImage.sprite = PlantPrefab.GrowStages[PlantPrefab.GrowStages.Length - 1];
        _renderer = GetComponent<Image>();

    }
	
	void Update () {
        if (PlantPrefab.InitialBuyCost <= Money.Instance.Amount)
            _renderer.color = Color.white;
        else
            _renderer.color = Color.red;
    }
}