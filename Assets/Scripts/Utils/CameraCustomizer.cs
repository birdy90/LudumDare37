using UnityEngine;
using System.Collections;

public class CameraCustomizer : MonoBehaviour {

    public Field FieldController;

	void Start () {
        var camera = gameObject.GetComponent<Camera>();
        camera.transform.position = new Vector3(FieldController.FieldWidth / 2 + 0.75f, FieldController.FieldHeight / 2 + 1.5f, 0);
	}
}
