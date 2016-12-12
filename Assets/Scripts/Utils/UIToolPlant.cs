using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIToolPlant : MonoBehaviour {
    public Text UIText1;
    public Text UIText2;
    public Image UIImage;
    public Plant PlantPrefab;

    private Image _renderer;
    // Use this for initialization
    void Start () {
        UIText1.text = PlantPrefab.GoodWith;
        UIText2.text = PlantPrefab.BadWith;
        UIImage.sprite = PlantPrefab.FormLook;
        _renderer = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
