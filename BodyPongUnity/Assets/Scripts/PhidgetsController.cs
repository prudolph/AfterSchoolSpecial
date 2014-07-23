﻿using UnityEngine;
using System.Collections;

using Phidgets; 
using Phidgets.Events;

public class PhidgetsController: MonoBehaviour {

	InterfaceKit ifkit;
	int sensorValue=0;
	public float speed  = 5f;
	public string buttonUp="UP";
	public string buttonDown = "DOWN";
	Vector3 originalPos;
	
	Quaternion originalRot;

	float maxSensorValue = 960f;
	float minSesorValue = 50f;

	float minPaddlePos = 0f;
	float maxPaddlePos = 18f;
	float offsetPos = -9f;
	
	
	// Use this for initialization
	void Start () {
		ifkit = new InterfaceKit();
		ifkit.open();
		
		try{
			ifkit.waitForAttachment (5000);
		}
		catch (PhidgetException ex)
		{
			Debug.Log(ex.Description);
		}
		ifkit.sensors [0].Sensitivity = 10;
		
		originalPos = transform.position;
		originalRot = transform.rotation;

		InvokeRepeating ("UpdateManual", 1f, .05f);
	}
	
	// Update is called once per frame
	void UpdateManual () {
		
		/*
		if (Input.GetButton (buttonUp)) {
			transform.Translate( new Vector3(0f,0,speed) * Time.deltaTime);		
		}
		if (Input.GetButton (buttonDown)) {
			transform.Translate( new Vector3(0f,0,-speed) * Time.deltaTime);		
		}
*/
		if (transform.position.y < - 4) {
			transform.position = originalPos;	
			transform.rotation = originalRot;
		}

		int oldsensorValue = sensorValue;
		try{ 
			ifkit.outputs [0] = true;
			sensorValue = ifkit.sensors [0].Value;
			
			ifkit.outputs [0] = false;
		}catch (PhidgetException ex)
		{
			//Debug.Log(ex.Description);
		}
		
		//float sensorValueChange = sensorValue/800f;


		float x;

		float sensorPerc = (sensorValue - minSesorValue) / (maxSensorValue - minSesorValue);
		float paddlePosition = (sensorPerc * (maxPaddlePos - minPaddlePos)) + offsetPos;



		float nV = paddlePosition;

		Debug.Log("sendorValue: "+sensorValue+ " newPaddlePos: " + nV+ " sensorPerc: "+sensorPerc);

		transform.position = ( new Vector3(transform.position.x,transform.position.y,nV));
		
	}
}
