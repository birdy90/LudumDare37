using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Bonus : MonoBehaviour {

    public static List<Bonus> Bought = new List<Bonus>();

    public int Cost;
    public int Value;

    public void BuyBonus()
    {
        GetComponent<Button>().interactable = false;
        if (Bought.Contains(this))
            return;
        Bought.Add(this);
    }
}
