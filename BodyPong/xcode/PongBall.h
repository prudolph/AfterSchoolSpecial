//
//  PongBall.h
//  BodyPong
//
//  Created by Administrator on 6/30/14.
//
//


#include "cinder/app/AppNative.h"


class Pongball{
public:
    Pongball();
    
    void setup();
    void update();
    void draw();
    

    cinder::Vec2f mPosition;

    cinder::Vec2f mVelocity,
                  mAcceleration;
    
    float           mDecay;
};

