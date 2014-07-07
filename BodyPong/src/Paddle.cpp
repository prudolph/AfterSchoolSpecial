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


void Paddle::setup(){

    mPosition=Vec2f(getWindowWidth()/2.0f,getWindowHeight()/2.0f);
    mDirection = Vec2f(randFloat(0,1),randFloat(0,1));
    mSpeed = 1.2f;
}
void Paddle::update(){
    if(mPosition.x<0 ||mPosition.x> getWindowWidth() ){
        mDirection.x*=-1;
    }
    if(mPosition.y<0 ||mPosition.y> getWindowHeight() ){
        mDirection.y*=-1;
    }
    
    mPosition += mDirection*mSpeed;


}
void Paddle::draw(){
    
    cinder::gl::color(1,1,0);
    cinder::gl::drawSolidCircle(mPosition, 50.0);
    
    
}