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
    float mSpeed;
    cinder::Vec2f mDirection,
                  mAcceleration;
    
    float           mDecay;
};

