using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Field))]
public class Controller : MonoBehaviour {

    private GameModes _gameMode = GameModes.None;
    private Field _field;

    public enum GameModes
    {
        None, Selecting, Planting, Actions
    }
	
    void Start()
    {
        _field = GetComponent<Field>();
        _gameMode = GameModes.Planting;
    }

	void Update () {
        if (Input.GetMouseButton(1))
        {
            _gameMode = GameModes.None;
            _field.ResetCellColors();
        }

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
                    // hit.collider.gameObject.GetComponent<Cell>().SetColor(new Color(225f, 130f, 130f, 1f));
                    hit.collider.gameObject.GetComponent<Cell>().SetColor(Color.green);
                }
                break;
        }
	}
    
    public void SetGameMode(GameModes mode)
    {
        _gameMode = mode;
    }
}
