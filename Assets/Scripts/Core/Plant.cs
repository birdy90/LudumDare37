using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Completed;

public class Plant : MonoBehaviour {
    
    public Sprite[] GrowStages;
    public int OneStageGrowthTime = 1;
    public int GrowthTime { get { return GrowStages.Length; } }
    public Sprite CurrentGrowSprite { get { return GrowStages[_currentGrowstage]; } }

    public Sprite ReadySprite;
    public Sprite SpoiledSprite;

    public AudioClip Growing;

    const int _shapeSideSize = 3;
    public List<int> Shape;

    public int InitialBuyCost;
    public int InitialGrowthValue;
    public int[] GrowthValueCost = new int[10];
    public int[] GrowthInfluence = new int[9];

    [HideInInspector]
    public Vector2 PlantedTo;



    private int _currentGrowstage;
    private SpriteRenderer _renderer;
    private BoxCollider _collider;

    public bool Collider { set { _collider.enabled = value; } }

    public string Name;
    
	void Start () {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<BoxCollider>();
        _renderer.sprite = GrowStages[0];
	}

    public int GetShape(int x, int y)
    {
        return Shape[y * _shapeSideSize + x];
    }

    public bool Grow()
    {
        _currentGrowstage++;
        if (_currentGrowstage >= GrowthTime)
        {
            return true;
        }
        else
        {
            _renderer.sprite = CurrentGrowSprite;
            return false;
        }
    }

    public void OverGrow()
    {
        Field.Instance.RemovePlant(gameObject);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Float");
        SoundManager.instance.PlaySingle(Growing);

        _renderer.sprite = ReadySprite;
        _renderer.gameObject.tag = "GrownPlant";
    }
}
