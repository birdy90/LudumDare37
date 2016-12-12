using UnityEngine;
using System.Collections;
using System;
using Completed;

[RequireComponent(typeof(Field))]
public class Controller : MonoBehaviour {

    private GameModes _gameMode = GameModes.None;
    private Field _field;
    private Plant _plantedPlant;

    public AudioClip Clicking;
    public AudioClip PickUp;
    public AudioClip Failure;

    public enum GameModes
    {
        None, Selecting, Planting, Actions
    }
	
    void Start()
    {
        _field = GetComponent<Field>();
    }

	void Update () {
        if (Input.GetMouseButton(1))
            ResetGameMode();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        switch (_gameMode)
        {
            case GameModes.Selecting:
                break;
            case GameModes.Actions:
                break;
            case GameModes.Planting:
                _field.ResetCellColors();
                if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.CompareTag("Earth"))
                {
                    var cell = hit.collider.gameObject.GetComponent<Cell>();
                    var pos = cell.GetPos();
                    var canPlant = CanPlant((int)pos.x, (int)pos.y);
                    var color = canPlant ? Color.green : Color.red;
                    HighLightPlantPosition(color, (int)pos.x, (int)pos.y);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (canPlant)  
                            _field.PutPlant(_plantedPlant.gameObject, (int)pos.x, (int)pos.y);
                        else
                            SoundManager.instance.PlaySingle(Failure);

                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                        ResetGameMode();
                }
                break;
            case GameModes.None:
                if (Input.GetMouseButtonDown(0))
                {
                    _field.ResetCellColors();
                    if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.CompareTag("GrownPlant"))
                    {
                        var plant = hit.collider.gameObject.transform.parent.gameObject;
                        var plantComponent = plant.GetComponent<Plant>();
                        Trader.Instance.SellPlant(plantComponent);
                        SoundManager.instance.PlaySingle(PickUp);
                        Field.Instance.GrownPlants.Remove(plantComponent);
                        Destroy(plant);
                    }
                }
                break;
        }
	}
    
    public void SetGameMode(GameModes mode)
    {
        if (mode == GameModes.None)
            Field.Instance.SetCollisionStateForReadyPlants(true);
        else
            Field.Instance.SetCollisionStateForReadyPlants(false);

        _gameMode = mode;
    }

    public void ResetGameMode()
    {
        SetGameMode(GameModes.None);
        _field.ResetCellColors();
    }

    public bool CanPlant(int x, int y)
    {
        var x1 = x - 1;
        var x2 = x + 2;
        var y1 = y - 1;
        var y2 = y + 2;
        var ii = 0;
        if (Money.Instance.Amount < _plantedPlant.InitialBuyCost)
            return false;

        for (var i = x1; i < x2; i++, ii++)
        {
            var jj = 0;
            for (var j = y1; j < y2; j++, jj++)
            {
                var plantPoint = _plantedPlant.GetShape(ii, jj);
                if (!_field.PointInFieldBounds(i, j))
                {
                    if (plantPoint == 1)
                        return false;
                } else
                {
                    if (plantPoint == 1 && _field.PlantedCells[i, j] > 0)
                        return false;
                }
            }
        }
        return true;
    }

    public void HighLightPlantPosition(Color color, int x, int y)
    {
        var minI = x - 1;
        var maxI = x + 2;
        var minJ = y - 1;
        var maxJ = y + 2;
        var ii = 0;
        for (var i = minI; i < maxI; i++, ii++)
        {
            var jj = 0;
            for (var j = minJ; j < maxJ; j++, jj++)
                if (_plantedPlant.GetShape(ii, jj) == 1)
                {
                    if (!_field.PointInFieldBounds(i, j))
                        continue;
                    _field.Earth[i, j].SetColor(color);
                }
        }
    }

    public void StartPlant(GameObject plant)
    {
        SoundManager.instance.PlaySingle(Clicking);
        _plantedPlant = plant.GetComponent<Plant>();
        SetGameMode(GameModes.Planting);
    }
}
