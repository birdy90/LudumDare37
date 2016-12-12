using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyIndicator : MonoBehaviour {

    public static MoneyIndicator Instance;
    public Text Label;

	void Start () {
        Instance = this;
    }

    public void SetValue(int value)
    {
        Label.text = value.ToString();
    }

    public void UpdateValue()
    {
        Label.text = Money.Instance.Amount.ToString();
    }
}
