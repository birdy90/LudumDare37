using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Field))]
public class Controller : MonoBehaviour {

    private GameModes _gameMode = GameModes.None;
    private Field _field;
    private Plant _plantedPlant;

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
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                _field.ResetCellColors();
                if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.CompareTag("Earth"))
                {
                    var cell = hit.collider.gameObject.GetComponent<Cell>();
                    var pos = cell.GetPos();
                    var canPlant = CanPlant((int)pos.x, (int)pos.y);
                    var color = canPlant ? Color.green : Color.red;

                    var minI = (int)pos.x - 1;
                    var maxI = (int)pos.x + 2;
                    var minJ = (int)pos.y - 1;
                    var maxJ = (int)pos.y + 2;
                    var ii = 0;
                    for (var i = minI; i < maxI; i++, ii++)
                    {
                        var jj = 0;
                        for (var j = minJ; j < maxJ; j++, jj++)
                            if (_plantedPlant.GetShape(ii, jj) == 1)
                            {
                                if (i < 0 || j < 0 || i >= _field.FieldWidth || j >= _field.FieldHeight)
                                    continue;
                                _field.Earth[i, j].SetColor(color);
                            }
                    }

                    if (color == Color.green && Input.GetMouseButton(0))
                    {
                        ResetGameMode();
                        _field.PutPlant(_plantedPlant.gameObject, (int)pos.x, (int)pos.y);
                    }
                }
                else
                {
                    if (Input.GetMouseButton(0))
                        ResetGameMode();
                }
                break;
        }
	}
    
    public void SetGameMode(GameModes mode)
    {
        _gameMode = mode;
    }

    public void ResetGameMode()
    {
        _gameMode = GameModes.None;
        _field.ResetCellColors();
    }

    public bool CanPlant(int x, int y)
    {
        var x1 = x - 1;
        var x2 = x + 2;
        var y1 = y - 1;
        var y2 = y + 2;
        var ii = 0;
        for (var i = x1; i < x2; i++, ii++)
        {
            var jj = 0;
            for (var j = y1; j < y2; j++, jj++)
            {
                var point = _plantedPlant.GetShape(ii, jj);
                if (point == 1 && (i < 0 || j < 0 || i >= _field.FieldWidth || j >= _field.FieldHeight))
                    return false;
            }
        }
        return true;
    }

    public void StartPlant(GameObject plant)
    {
        _plantedPlant = plant.GetComponent<Plant>();
        SetGameMode(GameModes.Planting);
    }
}
