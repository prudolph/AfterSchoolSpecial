//
//  Paddle.h
//  BodyPong
//
//  Created by Administrator on 6/30/14.
//
//


#include "cinder/app/AppNative.h"


class Paddle{
public:
    Paddle();
    
    void setup();
    void update();
    void draw();
    

    cinder::Vec2f mPosition;

    
  
};

