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

    
	void										setup();
	void										setupSensor();
	void										shutdownSensor();
	void										update();
    
    
	CPhidgetInterfaceKitHandle ifKit;
	void pollSensor();
	
private:
		

    
    
	int		mCurrentSensor;
	
};