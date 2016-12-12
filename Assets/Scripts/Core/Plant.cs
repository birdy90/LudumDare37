using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Completed;

public class Plant : MonoBehaviour {

    public static Dictionary<string, int> PlantNumber = new Dictionary<string, int>
    {
        { "Cucumber", 0 },
        {"Tomato", 1},
        {"Carrot", 2},
        {"Onion", 3},
        {"Radish", 4},
        {"Pumpkin", 5},
        {"Eggplant", 6},
        {"Potato", 7},
        {"Pepper", 8},
        {"Flower", 9}
    };

    public Sprite[] GrowStages;
    public int OneStageGrowthTime = 1;
    private int _stageGrowthTimer;
    public int GrowthTime { get { return GrowStages.Length; } }
    public Sprite CurrentGrowSprite { get { return GrowStages[_currentGrowstage]; } }

    public Sprite ReadySprite;
    public Sprite SpoiledSprite;
    public Sprite FormLook;
    public string GoodWith;
    public string BadWith;

    public AudioClip Growing;

    const int _shapeSideSize = 3;
    public List<int> Shape;

    public int InitialBuyCost;
    public int InitialGrowthValue;
    public int[] GrowthValueCost = new int[10];
    public int[] GrowthInfluence = new int[9];

    [HideInInspector]
    public Vector2 PlantedTo;

    [HideInInspector]
    public List<Plant> Influences = new List<Plant>();

    private int _currentGrowstage;
    private SpriteRenderer _renderer;
    private BoxCollider _collider;

    public bool Collider { set { _collider.enabled = value; } }

    public string Name;
    
	void Start () {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<BoxCollider>();
        _renderer.sprite = GrowStages[0];

        _stageGrowthTimer = OneStageGrowthTime;
    }

    public int GetShape(int x, int y)
    {
        return Shape[y * _shapeSideSize + x];
    }

    public bool Grow()
    {
        _stageGrowthTimer--;
        if (_stageGrowthTimer == 0)
        {
            _stageGrowthTimer = OneStageGrowthTime;
            _currentGrowstage++;
        }
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
        Controller.Instance.UpdateColiisionsForReadyPlants();
    }
}
