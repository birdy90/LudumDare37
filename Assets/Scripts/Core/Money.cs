using UnityEngine;

public class Money : MonoBehaviour {

    public static Money Instance;
    public MoneyIndicator Indicator;

    private int _amount = 200;
    public int Amount
    {
        get { return _amount; }
        set {
            _amount = value;
            Indicator.UpdateValue();
        }
    }

    void Start () {
        Instance = this;
        Indicator.UpdateValue();
    }
}
