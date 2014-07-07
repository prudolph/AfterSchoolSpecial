//
//  ActivityController.h
//
//
//  Created by Paul Rudolph on 7/2/13.
//
//
#pragma once

#include <phidget21.h>

class ActivityController{
    
public:
    

    
	ActivityController();
	~ActivityController();

    
	void setup();
	void setupSensors();
	void shutdownSensor();
	void update();
    
    
	CPhidgetInterfaceKitHandle ifKit;

	
    float getLeftSensorValue(){return mLeftSensorValue;};
    
    float getRightSensorValue(){return mRightSensorValue;};
    
private:
		

    float mLeftSensorValue,mRightSensorValue;
    
	int		mCurrentSensor;
	
};