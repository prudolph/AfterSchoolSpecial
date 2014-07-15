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
		ifkit.sensors [0].Sensitivity = 50;
		
		originalPos = transform.position;
		originalRot = transform.rotation;

		InvokeRepeating ("UpdateManual", 1f, .25f);
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
		
		float sensorValueChange = sensorValue/400f;
		Debug.Log(sensorValueChange);

		transform.Translate( new Vector3(0f,0,transform.position.z +sensorValue));
		
	}
}