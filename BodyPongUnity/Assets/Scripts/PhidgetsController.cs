using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Phidgets; 
using Phidgets.Events;

public class PhidgetsController: MonoBehaviour {
	
	InterfaceKit ifkit;
	public float speed  = 5f;
	public string buttonUp="UP";
	public string buttonDown = "DOWN";
	Vector3 originalPos;
	
	Quaternion originalRot;
	
	float maxSensorValue = 130f;//960f;
	float minSensorValue = 60f;
	
	
	float sensorValue=100;
	float sensorPercentage = 0;
	
	float minPaddlePos = 0f;
	float maxPaddlePos = 18f;
	float offsetPos = -9f;
	
	bool sensorChanged =false;
	
	public int playerIndex = 0;
	
	List<float> paddlePositionList;
	
	// Use this for initialization
	void Awake(){
		paddlePositionList = new List<float>();
	}
	void Start () {
		ifkit = new InterfaceKit();
		ifkit.SensorChange+= new SensorChangeEventHandler(ifKit_SensorChange);
		
		ifkit.open();
		
		try{
			ifkit.waitForAttachment (5000);
		}
		catch (PhidgetException ex)
		{
			Debug.Log(ex.Description);
		}
		Debug.Log ("plyaer index "+playerIndex);
		Debug.Log(ifkit.sensors.Count+" howmany sensors");
		ifkit.sensors [playerIndex].Sensitivity = 0;
		ifkit.outputs [playerIndex] = true;
		
		
		originalPos = transform.position;
		originalRot = transform.rotation;
		
		InvokeRepeating ("UpdateManual", 1f,0.05f);
	}
	
	void UpdateManual(){
		
		if(paddlePositionList.Count == 0) return;
		
		float pos = paddlePositionList.Average();
		LeanTween.cancel(gameObject);
		LeanTween.moveZ(gameObject, pos, .25f);
		paddlePositionList.Clear();
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.y < - 4) {
			transform.position = originalPos;   
			transform.rotation = originalRot;
		}
		
		
		//Sanitize sensor value
		if (sensorValue > maxSensorValue)sensorValue = maxSensorValue;
		if (sensorValue < minSensorValue)sensorValue = minSensorValue;
		
		
		//if (sensorChanged) {

			sensorChanged=false;

			sensorPercentage 		= (sensorValue - minSensorValue) / (maxSensorValue - minSensorValue);
			float paddlePosition 	= ((maxPaddlePos - minPaddlePos) * sensorPercentage) - maxPaddlePos / 2.0f;
			//Debug.Log ("sensorPercentage: " + sensorPercentage + " paddlePosition: " + paddlePosition + "  sensorValue " + sensorValue);

			if(Mathf.Abs(transform.position.z -paddlePosition) > 1.0f ){
				paddlePositionList.Add(Mathf.Floor(paddlePosition));				
			}
		//}
	}
	



	void ifKit_SensorChange(object sender, SensorChangeEventArgs e)
	{
		if (e.Index == playerIndex) {
			sensorValue =  e.Value;
			sensorChanged=true;
		}
	}
}
