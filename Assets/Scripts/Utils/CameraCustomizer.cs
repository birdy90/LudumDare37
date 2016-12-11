using UnityEngine;
using System.Collections;

public class CameraCustomizer : MonoBehaviour {

    public Field FieldController;

	void Start () {
        var camera = gameObject.GetComponent<Camera>();
        camera.transform.position = new Vector3(FieldController.FieldWidth / 2, FieldController.FieldHeight / 2 + 1.45f, 0);
	}
}
