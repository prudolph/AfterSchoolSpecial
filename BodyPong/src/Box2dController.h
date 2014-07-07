#pragma once
#include "Box2D/Box2D.h"
#include "cinder/app/AppBasic.h"



class Box2dController{
    
public:
    
    Box2dController( void );
    
    void setup();
    void update();
    void createGround();
    void changeEnvironment(bool next);// if next is false go to previous environment 
    void addEnvironmentLedges();
    void addLedge( int ledgeNum );
    void addLedgeChain( int ledgeNum );
    void destroyLedges();
    
    std::vector<b2Body*> ledgeBodies;
private:
    b2World world;
    b2Vec2 gravity;
};
