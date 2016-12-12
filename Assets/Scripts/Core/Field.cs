using Completed;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Field : MonoBehaviour {

    public static Field Instance;

    public int FieldWidth = 8;
    public int FieldHeight = 6;
    public GameObject EarthSprite;

    
    public AudioClip PlantASeed;
   
   

    public Cell[,] Earth;
    [HideInInspector]
    public List<Plant> Plants;
    [HideInInspector]
    public List<Plant> GrownPlants;
    [HideInInspector]
    public int[,] PlantedCells;
    private int[,] _influences;

    public int Timer = 0;
    private float _lastUpdateTime;
    
    void Start()
    {
        Instance = this;
        Earth = new Cell[FieldWidth, FieldHeight];
        Plants = new List<Plant>();
        GrownPlants = new List<Plant>();
        PlantedCells = new int[FieldWidth, FieldHeight];
        _influences = new int[FieldWidth, FieldHeight];
        InstantiateEarth();
        _lastUpdateTime = Time.time;
    }

    public void InstantiateEarth()
    {
        for (var i = 0; i < FieldWidth; i++)
        {
            for (var j = 0; j < FieldHeight; j++)
            {
                var newTile = Instantiate(EarthSprite);
                newTile.transform.position = new Vector3(i, j, 1);
                var cell = newTile.GetComponent<Cell>();
                cell.SetRenderer(newTile.GetComponent<SpriteRenderer>());
                cell.SetPos(i, j);
                Earth[i, j] = cell;
            }
        }
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
        var grownPlants = new List<Plant>();
        foreach (var plant in Plants)
        {
            if (plant != null)
            {
                CalculateInfluences(plant);
                if (plant.Grow())
                {
                    RemoveInfluences(plant);
                    grownPlants.Add(plant);

                    if (plant.Name == "Flower")
                    {
                        SceneManager.LoadScene("YouWin");
                    }
                }
            }
        }
        foreach (var plant in grownPlants)
            plant.OverGrow();
    }

    public void PutPlant(GameObject plant, int x, int y)
    {
        if (Money.Instance.Amount < plant.GetComponent<Plant>().InitialBuyCost)
            return;

        var newPlant = Instantiate(plant.gameObject);
        var newPlantComponent = newPlant.GetComponent<Plant>();
        newPlantComponent.PlantedTo = new Vector2(x, y);
        SetInfluences(newPlantComponent);
        Plants.Add(newPlantComponent);
        for (var i = -1; i < 2; i++)
            for (var j = -1; j < 2; j++)
                if (newPlantComponent.GetShape(i + 1, j + 1) == 1)
                    PlantedCells[i + x, j + y] += 1;
        newPlant.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
        SoundManager.instance.PlaySingle(PlantASeed);
        Money.Instance.Amount -= newPlantComponent.InitialBuyCost;

    }

    public void RemovePlant(GameObject plant)
    {
        var plantComponent = plant.GetComponent<Plant>();
        var x = (int)plantComponent.PlantedTo.x;
        var y = (int)plantComponent.PlantedTo.y;
        for (var i = -1; i < 2; i++)
            for (var j = -1; j < 2; j++)
                if (plantComponent.GetShape(i + 1, j + 1) == 1)
                    PlantedCells[i + x, j + y] -= 1;
        Plants.Remove(plantComponent);
        GrownPlants.Add(plantComponent);
    }

    public void SetInfluences(Plant plant)
    {
        var x = (int)plant.PlantedTo.x;
        var y = (int)plant.PlantedTo.y;
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (plant.GetShape(i,  j) == 1) // если в точке 1
                {
                    foreach (var plantedPlant in Plants) // проверим другие растения
                    {
                        if (plant == plantedPlant)
                            continue;

                        for (var ni = 0; ni < 3; ni++)
                        {
                            for (var nj = 0; nj < 3; nj++)
                            {
                                if (ni == 1 && nj == 1)
                                    continue;

                                var ppx = (int)plantedPlant.PlantedTo.x;
                                var ppy = (int)plantedPlant.PlantedTo.y;
                                var influenced = false;
                                for (var ppi = 0; ppi < 3 && !influenced; ppi++)
                                {
                                    for (var ppj = 0; ppj < 3 && !influenced; ppj++)
                                    {
                                        if (x + i + ni - 1 == ppx + ppi && y + j + nj - 1 == ppy + ppj) // если координаты сдвинутой точки пересеклись
                                        {
                                            if (plantedPlant.GetShape(ppi, ppj) == 1) // если в этой точке тоже 1
                                            {
                                                if (!plant.Influences.Contains(plantedPlant))
                                                {
                                                    plant.Influences.Add(plantedPlant);
                                                    plantedPlant.Influences.Add(plant);
                                                }
                                                influenced = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void CalculateInfluences(Plant plant)
    {
        foreach (var influencingPlant in plant.Influences)
        {
            var influence = influencingPlant.GrowthInfluence[Plant.PlantNumber[plant.gameObject.tag]];
            plant.InitialGrowthValue += influence;
        }
        foreach (var bonus in Bonus.Bought)
        {
            plant.InitialGrowthValue += bonus.Value;
        }
    }

    public void RemoveInfluences(Plant plant)
    {
        foreach (var influencedPlant in Plants)
        {
            if (influencedPlant != plant)
                if (influencedPlant.Influences.Contains(plant))
                    influencedPlant.Influences.Remove(plant);
        }
        plant.Influences.Clear();
    }

    public bool PointInFieldBounds(int x, int y)
    {
        return !(x < 0 || y < 0 || x >= FieldWidth || y >= FieldHeight);
    }

    public void SetCollisionStateForReadyPlants(bool active)
    {
        foreach (var plant in GrownPlants)
            plant.Collider = active;
    }

    public void ResetCellColors()
    {
        foreach (var cell in Earth)
            cell.SetColor(Color.white);
    }
}
