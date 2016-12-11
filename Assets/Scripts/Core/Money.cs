using UnityEngine;

public class Money : MonoBehaviour {

    public static Money Instance;

    public int Amount = 200;
    
	void Start () {
        Instance = this;
	}
}
