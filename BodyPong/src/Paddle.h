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
    
    void setup(cinder::Vec2f pos);
    void update(float yVal);
    void draw();
    

    cinder::Vec2f mPosition,mSize;
    
    
  
};

