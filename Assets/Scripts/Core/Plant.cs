using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour {
    
    public Sprite[] GrowStages;
    public int GrowthTime { get { return GrowStages.Length; } }
    public Sprite CurrentGrowSprite { get { return GrowStages[_currentGrowstage]; } }

    const int _shapeSideSize = 3;
    public List<int> Shape;
    private int[,] _shape = new int[_shapeSideSize, _shapeSideSize];

    public int InitialBuyCost;
    public int InitialSellCost;
    public int[] GrowthValueCost = new int[10];
    public int[] GrowthInfluence = new int[9];

    private int _currentGrowstage;
    private SpriteRenderer _renderer;

    public string Name;

	// Use this for initialization
	void Start () {
        for (var i = 0; i < _shapeSideSize; i++)
            for (var j = 0; j < _shapeSideSize; j++)
                _shape[i, j] = Shape[i * 3 + j];
        _renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Grow()
    {
        _currentGrowstage++;
        if (_currentGrowstage >= GrowthTime)
            _currentGrowstage = 0;
        _renderer.sprite = CurrentGrowSprite;
    }
}
