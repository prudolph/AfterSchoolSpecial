//
//  Paddle
//  BodyPong
//
//  Created by Administrator on 6/30/14.
//
//

#include "Paddle.h"
#include "cinder/gl/gl.h"
#include "cinder/Rand.h"

using namespace ci;
using namespace ci::app;
using namespace std;

Paddle::Paddle()
{

 

}


void Paddle::setup(cinder::Vec2f pos){
    mPosition = pos;
    mSize=Vec2f(50.0f,100.0f);
    
}
void Paddle::update(float yVal){
    
    mPosition.y = yVal;
  

}
void Paddle::draw(){
    cinder::gl::color(1,0,0);
    
    gl::drawSolidRect(Rectf(mPosition,mPosition+mSize));
    
    
}