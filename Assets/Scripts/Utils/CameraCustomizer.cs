using UnityEngine;
using System.Collections;

public class CameraCustomizer : MonoBehaviour {

    public Field FieldController;

	void Start () {
        var camera = gameObject.GetComponent<Camera>();
        camera.orthographicSize = FieldController.FieldHeight;
        camera.transform.position = new Vector3(FieldController.FieldWidth / 2, FieldController.FieldHeight / 2, 0);
	}
}
