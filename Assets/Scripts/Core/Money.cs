using UnityEngine;

public class Money : MonoBehaviour {

    public static Money Instance;
    public MoneyIndicator Indicator;

    public int InitialAmount = 200;

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set {
            _amount = value;
            Indicator.UpdateValue();
        }
    }

    void Start () {
        _amount = InitialAmount;
        Instance = this;
        Indicator.UpdateValue();
    }
}
