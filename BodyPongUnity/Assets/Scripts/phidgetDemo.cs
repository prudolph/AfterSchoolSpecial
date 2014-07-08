/*
 * Codes for IR Wand
Code: E0E0A05F   left flick
Code: 7070       down flick 
Code: E0E020DF   right flick
Code: E0E08877   push forward
Code: E0E0B04F   pull back
Code: E0E006F9   Tap
 */


using UnityEngine;
using System.Collections;
using Phidgets;
using Phidgets.Events;

public class phidgetDemo : MonoBehaviour {
	IR wand;
	bool left, right, push, pull, down,tap;
	public GameObject left_obj,right_obj,down_obj,tap_obj,push_obj,pull_obj, cur;
	private bool connectionOpen = false;
	// Use this for initialization
	void Start () {
		wand = new IR();
		wand.Attach += new AttachEventHandler(ir_Attach);
        wand.Detach += new DetachEventHandler(ir_Detach);
        wand.Error += new ErrorEventHandler(ir_Error);
        wand.Code += new IRCodeEventHandler(ir_Code);
        wand.Learn += new IRLearnEventHandler(ir_Learn);
        wand.RawData += new IRRawDataEventHandler(ir_RawData);
		wand.open();
		connectionOpen = true;
		wand.waitForAttachment();

		left = false;
		right = false;
		down = false;
		tap = false;
		push = false;
		pull = false;	
		cur = null;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(connectionOpen)
		{
			if(left){
				print("left");
				left = false;
				if(cur != null){
					Destroy(cur);
				}
				cur =(GameObject)Instantiate(left_obj);
				}
			else if(right){
				print("right");
				right = false;
				if(cur != null){
					Destroy(cur);
				}
				cur =(GameObject)Instantiate(right_obj);
				}
			else if(down){
				print("down");
				down = false;
				if(cur != null){
					Destroy(cur);
				}
				cur =(GameObject)Instantiate(down_obj);
			}
			else if(tap){
				print("tap");
				tap = false;
				if(cur != null){
					Destroy(cur);
				}
				cur =(GameObject)Instantiate(tap_obj);
			}
			else if(push){
				print("push");
				push = false;
				if(cur != null){
					Destroy(cur);
				}
				cur =(GameObject)Instantiate(push_obj);
			}
			else if(pull){
				print("pull");
				pull = false;
				if(cur != null){
					Destroy(cur);
				}
				cur =(GameObject)Instantiate(pull_obj);
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			
			try
			{
				wand.close();
				wand = null;
	
			}
			catch(PhidgetException ex)
			{
				print("exception : " + ex.Description);
			}
			System.Diagnostics.Process.GetCurrentProcess().Kill();
			Application.Quit();
			//StartCoroutine(waitForDetach());
			
		}
	}
	IEnumerator waitForDetach(){
		wand.close();
		while(connectionOpen){
			yield return new WaitForSeconds(.01f);
		}

	}
	void OnApplicationQuit()
	{
		
	}
	void ir_Attach(object sender, AttachEventArgs e)
	{
	}
	
	void ir_Detach(object sender, DetachEventArgs e)
	{
		connectionOpen = false;
		print("i hit");
	}
	
	void ir_Error(object sender, ErrorEventArgs e)
	{
	}
	
	void ir_Code(object sender, IRCodeEventArgs e)
	{
		if(!e.Repeat){
			switch(e.Code.ToString()){
			case "e0e0a05f":
				left = true;
				break;
			case "e0e020df":
				right = true;
				break;
			case "7070":
				down = true;
				break;
			case "e0e006f9":
				tap = true;
				break;
			case "e0e08877":
				push = true;
				break;
			case "e0e0b04f":
				pull = true;
				break;
			}

		}
	}
	
	void ir_Learn(object sender, IRLearnEventArgs e)
	{
	}
	
	void ir_RawData(object sender, IRRawDataEventArgs e)
	{
	}
	
}
