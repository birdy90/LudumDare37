using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIPlant : MonoBehaviour {
    public Text UIText;
    public Image UIImage;
    public Plant PlantPrefab;

    private Image _renderer;

    void Start () {
        if (PlantPrefab.InitialBuyCost>1000000)
        {
            UIText.text = PlantPrefab.Name + "\nCost: " + (double)PlantPrefab.InitialBuyCost/1000000 + "KK\nProfit: " + (double)PlantPrefab.GrowthValueCost[0]/1000000 + "KK-" + (double)PlantPrefab.GrowthValueCost[9]/1000000 + "KK\nTime: " + PlantPrefab.GrowthTime * PlantPrefab.OneStageGrowthTime;
        }
        else if (PlantPrefab.InitialBuyCost > 1000)
        {
            UIText.text = PlantPrefab.Name + "\nCost: " + (double)PlantPrefab.InitialBuyCost / 1000 + "K\nProfit: " + (double)PlantPrefab.GrowthValueCost[0] / 1000 + "K-" + (double)PlantPrefab.GrowthValueCost[9] / 1000 + "K\nTime: " + PlantPrefab.GrowthTime * PlantPrefab.OneStageGrowthTime;
        }
        else 
            UIText.text = PlantPrefab.Name+"\nCost: "+PlantPrefab.InitialBuyCost+"\nProfit: "+PlantPrefab.GrowthValueCost[0]+"-"+ PlantPrefab.GrowthValueCost[9]+"\nTime: "+PlantPrefab.GrowthTime*PlantPrefab.OneStageGrowthTime;
        UIImage.sprite = PlantPrefab.ReadySprite;
        _renderer = GetComponent<Image>();



    }
	
	void Update () {
        if (PlantPrefab.InitialBuyCost <= Money.Instance.Amount)
            _renderer.color = Color.white;
        else
            _renderer.color = Color.red;
    }
}
