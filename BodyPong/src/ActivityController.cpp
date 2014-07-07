//Sonar sensor ::
//Detects objects from 0 inches to 254 inches (6.45 meters) with 1 inch (25.4 mm) resolution.
//Automatic calibration when powered up
//Operates between 2.5V to 5.5V
//Connects to any device with an Analog Input.
//Datasheet ::http://www.phidgets.com/documentation/Phidgets/1128_0_EZ1-Datasheet.pdf

//tutorial :: http://www.phidgets.com/documentation/Tutorials/Getting_Started_Visual_Studio_C-C++.pdf




#include "ActivityController.h"


using namespace std;
using namespace ci;
using namespace ci::app;



ActivityController::ActivityController() :
mCurrentSensor(0)
{
    
}
ActivityController:: ~ActivityController(){
	shutdownSensor();
}


void ActivityController::setup(){
	
    
	setupSensor();
}



void ActivityController::setupSensor(){
    
	
	//Declare an InterfaceKit handle
	ifKit = 0;
    
	//Create the InterfaceKit object
	CPhidgetInterfaceKit_create(&ifKit);
	CPhidget_open((CPhidgetHandle)ifKit, -1);
    
	//get the program to wait for an interface kit device to be attached
	console() << "Waiting for interface kit to be attached....";
	int result;
	const char *err;
	if ((result = CPhidget_waitForAttachment((CPhidgetHandle)ifKit, 100)))
	{
		CPhidget_getErrorDescription(result, &err);
		printf("Problem waiting for attachment: %s\n", err);
	}
    
	
	CPhidgetInterfaceKit_setRatiometric(ifKit, 1);
    
	for (int i = 0; i < 2; i++){
		//Change the sensitivity of each sensor.
		CPhidgetInterfaceKit_setSensorChangeTrigger(ifKit,i , 10);//sensitivity can be 0 to 150
	}
}


void ActivityController::shutdownSensor(){
	CPhidget_close((CPhidgetHandle)ifKit);
	CPhidget_delete((CPhidgetHandle)ifKit);
}


void ActivityController::pollSensor(){
	 
	 
	int currentDistanceReading;
    //Turn the sensor on
    CPhidgetInterfaceKit_setOutputState(ifKit, mCurrentSensor, 1);
    sleep(0.001);//Need to give the senor time to take a reading
    //Get the distance
    CPhidgetInterfaceKit_getSensorValue(ifKit, mCurrentSensor, &currentDistanceReading);
	
    // Turn the sensor off
    CPhidgetInterfaceKit_setOutputState(ifKit, mCurrentSensor, 0);
    
    console()<<"Sensor "<< mCurrentSensor<< "Value :"<< currentDistanceReading<<endl;
    
    
      
    //mCurrentSensor++;
   // if (mCurrentSensor >1)mCurrentSensor = 0;
	
}

void ActivityController::update(){
    
	pollSensor();
	    
}

