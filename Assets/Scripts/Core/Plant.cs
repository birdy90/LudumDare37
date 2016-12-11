using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour {
    
    public Sprite[] GrowStages;
    public int OneStageGrowthTime = 1;
    public int GrowthTime { get { return GrowStages.Length; } }
    public Sprite CurrentGrowSprite { get { return GrowStages[_currentGrowstage]; } }

    const int _shapeSideSize = 3;
    public List<int> Shape;

    public int InitialBuyCost;
    public int InitialGrowthValue;
    public int[] GrowthValueCost = new int[10];
    public int[] GrowthInfluence = new int[9];

    private int _currentGrowstage;
    private SpriteRenderer _renderer;

    public string Name;
    
	void Start () {
        _renderer = GetComponent<SpriteRenderer>();
	}

    public int GetShape(int x, int y)
    {
        return Shape[y * _shapeSideSize + x];
    }

    public void Grow()
    {
        _currentGrowstage++;
        if (_currentGrowstage >= GrowthTime)
            OverGrow();
        else
            _renderer.sprite = CurrentGrowSprite;
    }

    public void OverGrow()
    {

    }
}
