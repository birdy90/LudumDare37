using UnityEngine;
using System.Collections;

public class GameTypeValidator : MonoBehaviour {

    public GameObject NextStepButton;

	void Start () {
        var type = PlayerPrefs.GetInt("GameType", (int)MainMenu.GameTypes.Normal);
        switch (type)
        {
            case (int)MainMenu.GameTypes.Normal:
                NextStepButton.SetActive(false);
                break;
            case (int)MainMenu.GameTypes.Zen:
                GetComponent<Field>().Timer = 0;
                break;
        }
	}
}
