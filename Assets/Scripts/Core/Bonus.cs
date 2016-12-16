using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Completed;

[RequireComponent(typeof(Button))]
public class Bonus : MonoBehaviour {

    public static List<Bonus> Bought = new List<Bonus>();

    public int Cost;
    public int Value;
   
    private Image _renderer;
    private Button _button;

    void Start() {
        _renderer = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    void Update()
    {
        if (Bought.Contains(this))
            return;
        if (Cost <= Money.Instance.Amount)
            _renderer.color = Color.white;
        else
            _renderer.color = Color.red;
    }

    public void BuyBonus()
    {
      
        if (Bought.Contains(this)|| Money.Instance.Amount < Cost)
            return;
        _button.interactable = false;
        Money.Instance.Amount -= Cost;
        SoundManager.instance.PlaySingle(Controller.Instance.Clicking);
        Bought.Add(this);
    }
}
        