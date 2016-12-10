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
        _gameMode = GameModes.Selecting;
    }

	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        switch (_gameMode)
        {
            case GameModes.Selecting:
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.collider.CompareTag("Earth"))
                    {
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
                break;
            case GameModes.Actions:
                break;
            case GameModes.Planting:
                break;
        }
	}
    
    public void SetGameMode(GameModes mode)
    {
        _gameMode = mode;
    }
}
