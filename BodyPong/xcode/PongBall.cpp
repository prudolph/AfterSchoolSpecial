//
//  PongBall.cpp
//  BodyPong
//
//  Created by Administrator on 6/30/14.
//
//

#include "PongBall.h"
#include "cinder/gl/gl.h"
#include "cinder/Rand.h"

using namespace ci;
using namespace ci::app;
using namespace std;

Pongball::Pongball()
{

 

}


void Pongball::setup(){

    mPosition=Vec2f(getWindowWidth()/2.0f,getWindowHeight()/2.0f);
    mAcceleration = Vec2f(1,1);
    mDecay = 0.5f;
}
void Pongball::update(){
  
    mVelocity += mAcceleration;
    mPosition += mVelocity;
    mVelocity *= mDecay;

}
void Pongball::draw(){
    
    cinder::gl::color(1,1,0);
    cinder::gl::drawSolidCircle(mPosition, 50.0);
    
    
}