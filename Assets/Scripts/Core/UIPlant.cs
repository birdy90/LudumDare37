using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIPlant : MonoBehaviour {
    public Text UIText;
    public Image UIImage;
    public Plant PlantPrefab;
    // Use this for initialization
    void Start () {
        UIText.text = PlantPrefab.Name+"\nCost: "+PlantPrefab.InitialBuyCost+"\nProfit: "+PlantPrefab.GrowthValueCost[0]+"-"+ PlantPrefab.GrowthValueCost[9];
        UIImage.sprite = PlantPrefab.GrowStages[PlantPrefab.GrowStages.Length - 1];

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
