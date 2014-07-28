using UnityEngine;
using System.Collections;

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
	
	// Use this for initialization
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
		ifkit.sensors [0].Sensitivity = 3;
		ifkit.outputs [0] = true;
		
		
		originalPos = transform.position;
		originalRot = transform.rotation;
		
		InvokeRepeating ("UpdateManual", 1f,0.0005f);
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
		

		
		
		
		//Sanitize sensor value
		if (sensorValue > maxSensorValue)sensorValue = maxSensorValue;
		if (sensorValue < minSensorValue)sensorValue = minSensorValue;
		
		
		if (sensorChanged) {

			sensorChanged=false;

			sensorPercentage 		= (sensorValue - minSensorValue) / (maxSensorValue - minSensorValue);
			float paddlePosition 	= ((maxPaddlePos - minPaddlePos) * sensorPercentage) - maxPaddlePos / 2.0f;



		

			Debug.Log ("sensorPercentage: " + sensorPercentage + " paddlePosition: " + paddlePosition + "  sensorValue " + sensorValue);

			if(Mathf.Abs(transform.position.z -paddlePosition)>1.0f ){
				transform.position = (new Vector3 (transform.position.x, transform.position.y, Mathf.Floor(paddlePosition)));
			}
				}
	}
	



	void ifKit_SensorChange(object sender, SensorChangeEventArgs e)
	{
		if (e.Index == 0) {
			sensorValue =  e.Value;
			sensorChanged=true;
		}
	}
}
