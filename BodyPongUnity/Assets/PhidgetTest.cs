using UnityEngine; 
using System.Collections; 
using Phidgets; 
public class PhidgetTest : MonoBehaviour { 
	InterfaceKit motionFloor; 
	// Use this for initialization 
	void Start () { 
		motionFloor = new InterfaceKit(); 
		motionFloor.open(); 
		motionFloor.waitForAttachment(1000); 
	} 
	// Update is called once per frame 
	void Update () { 
		if(Input.GetKeyDown(KeyCode.LeftArrow)){ 
			//by outputting true to the phidget's first output position, we close a circuit on the phidget board. 
			motionFloor.outputs[0] = true; 
		} 
	} 
}
