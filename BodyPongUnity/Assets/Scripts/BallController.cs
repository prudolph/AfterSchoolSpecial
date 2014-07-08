using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public float cSpeed = 10f;
	public float force = 20f;
	public float maxY = 1.5f;

	Vector3 originalPos;
	Quaternion originalRot;
	float originalSpeed;
	
	// Use this for initialization
	void Start () {

		originalPos = transform.position;
		originalRot = transform.rotation;
		originalSpeed = cSpeed;
		Reset ();
	}

	void Reset(){
		cSpeed = originalSpeed;
		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(force,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cvel = rigidbody.velocity;
		Vector3 tvel = cvel.normalized * cSpeed;
		rigidbody.velocity = Vector3.Lerp(cvel, tvel, Time.deltaTime);
		cSpeed = cSpeed + .1f;
		if (transform.position.y > maxY) {
			transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
		}
		if (transform.position.y < -4) {
			transform.position = originalPos;
			transform.rotation = originalRot;
			Reset();
		}
	}

	void OnCollision(){

	}
}
