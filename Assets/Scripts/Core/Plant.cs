using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {
    
    public Sprite[] GrowStages;
    public int GrowthTime { get { return GrowStages.Length; } }
    public Sprite CurrentGrowSprite { get { return GrowStages[_currentGrowstage]; } }
    
    public int[,] Shape;

    public int InitialBuyCost;
    public int InitialSellCost;

    private int _currentGrowstage;
    private SpriteRenderer _renderer;

	// Use this for initialization
	void Start () {
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
