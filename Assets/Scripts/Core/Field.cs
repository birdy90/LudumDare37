using UnityEngine;

public class Field : MonoBehaviour {

    public int FieldWidth = 8;
    public int FieldHeight = 6;
    public GameObject EarthSprite;

    private GameObject[,] _plants;
    private int[,] _influences;

    public GameObject TestPlant;

    public int Timer = 0;
    private float _lastUpdateTime;
    
    void Start()
    {
        _plants = new GameObject[FieldWidth, FieldHeight];
        _influences = new int[FieldWidth, FieldHeight];
        InstantiateEarth();
        _lastUpdateTime = Time.time;

        Test();
    }

    public void InstantiateEarth()
    {
        for (var i = 0; i < FieldWidth; i++)
        {
            for (var j = 0; j < FieldHeight; j++)
            {
                var newTile = Instantiate(EarthSprite);
                newTile.transform.position = new Vector3(i, j, 1);
            }
        }
    }

    public void Test()
    {
        var plant = Instantiate(TestPlant);
        PutPlant(plant, 1, 1);
    }
	
	void Update () {
        if (Timer > 0 && Time.time - _lastUpdateTime > Timer)
        {
            _lastUpdateTime = Time.time;
            Grow();
        }
	}

    public void Grow()
    {
        foreach (var plant in _plants)
        {
            if (plant != null)
                plant.GetComponent<Plant>().Grow();
        }
    }

    public void PutPlant(GameObject plant, int x, int y)
    {
        _plants[x, y] = plant;
        plant.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
    }
}
