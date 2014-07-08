using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 5f;
	public string buttonUp = "UP";
	public string buttonDown = "DOWN";

	Vector3 originalPos;
	Quaternion originalRot;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		originalRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton(buttonUp)){
			transform.Translate(new Vector3(0f, 0 , speed) * Time.deltaTime);
		}
		if(Input.GetButton(buttonDown)){
			transform.Translate(new Vector3(0f, 0 , -speed) * Time.deltaTime);
		}

		if (transform.position.y < -4) {
			transform.position = originalPos;
			transform.rotation = originalRot;
		}
	}
}
