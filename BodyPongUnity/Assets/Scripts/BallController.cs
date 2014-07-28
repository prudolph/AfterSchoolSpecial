using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public float cSpeed = 10f;
	public float force = 20f;
	public float maxY = 1.5f;

	Vector3 originalPos;
	Quaternion originalRot;
	float originalSpeed;
	
	bool firstTimeVelocitySave = false;
	Vector3 savedVelocity = Vector3.zero;
	float BounceRate = 1.5f;
	
	// Use this for initialization
	void Start () {

		originalPos = transform.position;
		originalRot = transform.rotation;
		originalSpeed = cSpeed;
		Reset ();
	}

	void Reset(){
		CancelInvoke("AddRandomRotation");
		transform.position = originalPos;
		transform.rotation = originalRot;
		cSpeed = originalSpeed;
		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(force,0,0);
		//InvokeRepeating("AddRandomRotation", 0, 1f);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 cvel = rigidbody.velocity;
		Vector3 tvel = cvel.normalized * cSpeed;
		rigidbody.velocity = Vector3.Lerp(cvel, tvel, Time.deltaTime);
		
		cSpeed = cSpeed + .1f;
		if (transform.position.y > maxY) {
			transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
		}

		if (transform.position.y < -4) {

			NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");

			Reset();
		}
	}
	
	void AddRandomRotation(){
		//float randomZ = Random.Range(100f, -100f);
		//float randomY = Random.Range(100f, -100f);
		//float randomX = Random.Range(100f, -100f);
		//LeanTween.rotate(gameObject, new Vector3(randomX, randomY, randomZ), 1f);
	}
	
	void OnCollisionEnter(Collision col) {
		Debug.Log("Collision! " + rigidbody.velocity);
		
		if (! firstTimeVelocitySave) {
			savedVelocity = rigidbody.velocity;
			firstTimeVelocitySave = true;
		}
		rigidbody.velocity = savedVelocity;
		savedVelocity = new Vector3(rigidbody.velocity.x, savedVelocity.y/BounceRate, rigidbody.velocity.z);
		
	}

}
