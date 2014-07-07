/*
 Creates a box2d world, which gets passed to all other elements (particles, outlines of figures, etc. )
 Here, the original gravity and
 */

#include "Box2dController.h"
#include "cinder/app/AppBasic.h"

#include "Box2D/Box2D.h"


using namespace ci;
using namespace ci::app;
using namespace std;

//define gravity vector
b2Vec2 gravity( 0.0f, 0.6f ); //first number makes things go left/right, second is speed/pull

Box2dController::Box2dController(void):world( gravity ){}

void Box2dController::setup(){
    
    createGround();
   
    double environmentTimestamp = getElapsedSeconds();
    addEnvironmentLedges();
    console()<<"ledge creation took"<<getElapsedSeconds()-environmentTimestamp<<endl;
}

void Box2dController::update(){
    
    //step the physical world
    float32 timeStep = .0166f;
    int32 velocityIterations = 6;
    int32 positionIterations = 2;
    
    world.Step(timeStep, velocityIterations, positionIterations );
}



void Box2dController::addEnvironmentLedges(){
    /*   testing the ledges  */
    //loop through the number of ledges on this environment
    int totalLedges = Globals::LEDGES[ Globals::ENVIRONMENT_NUM ].size();
    for( int ledgeNum = 0; ledgeNum < totalLedges; ++ledgeNum ){
        //addLedge( ledgeNum );
        addLedgeChain(ledgeNum);
    }    
}
//Creates ledges as polygon shapes- this method seems to be faster over the chain ledges.(Need to test how particle hit tests are handled )

void Box2dController::addLedge( int ledgeNum ){
    
    //create body
    b2BodyDef ledgeBodyDef;
    ledgeBodyDef.type = b2_staticBody;
    
    //create body, attach definition
    b2Body *ledgeBody;
    ledgeBody = world.CreateBody( &ledgeBodyDef );
    ledgeBodyDef.position.Set( Conversions::toPhysics( 0.0f ), Conversions::toPhysics( 0.0f ));

    
    int32 totalPoints = Globals::LEDGES[ Globals::ENVIRONMENT_NUM ][ ledgeNum ].size();
    //b2Vec2 vertices[ totalPoints ];
    
    b2PolygonShape ledgeShape;
    ledgeShape.m_vertexCount = totalPoints;
    
    for( int32 numPoints = 0; numPoints < totalPoints; ++numPoints ){
        //loop through points that create each ledge
        float xVal = (float)Globals::LEDGES[ Globals::ENVIRONMENT_NUM ][ ledgeNum ][ numPoints ].x;
        float yVal = (float)Globals::LEDGES[ Globals::ENVIRONMENT_NUM ][ ledgeNum ][ numPoints ].y;
        ledgeShape.m_vertices[ numPoints ].Set( Conversions::toPhysics(xVal), Conversions::toPhysics(yVal) );
    }
    
    //create shape
    ledgeShape.Set( ledgeShape.m_vertices, ledgeShape.m_vertexCount );
    
    //create fixture --- a fixture uses the previously made shape
    b2FixtureDef ledgeFixtureDef;
    ledgeFixtureDef.shape = &ledgeShape;
    ledgeFixtureDef.friction = 1;
    ledgeFixtureDef.restitution = 1;
    ledgeFixtureDef.density = 1;
    ledgeBody->CreateFixture( &ledgeFixtureDef );
    ledgeBodies.push_back(ledgeBody);

}
//This will create the ledges as chain shapes- seems to be a bit slower  to create but needs less points to create the ledge.(still need to test how particle touches are handled)
void Box2dController::addLedgeChain( int ledgeNum ){
    
    
    //create body
    b2BodyDef ledgeBodyDef;
    ledgeBodyDef.type = b2_staticBody;
    
    //create body, attach definition
    b2Body *ledgeBody;
    ledgeBody = world.CreateBody( &ledgeBodyDef );
    ledgeBodyDef.position.Set( Conversions::toPhysics( 0.0f ), Conversions::toPhysics( 0.0f ));
    
    
    int32 totalPoints = Globals::LEDGES[ Globals::ENVIRONMENT_NUM ][ ledgeNum ].size();
   //convert vertices to b2Vec2 array

    //delete this
    b2Vec2 *verts = new b2Vec2[totalPoints];
    for(int i=0;i<totalPoints;i++){
        verts[i]= b2Vec2(Conversions::toPhysics( Globals::LEDGES[ Globals::ENVIRONMENT_NUM ][ ledgeNum ][i]));
    }
   
    b2ChainShape ledgeChainShape;
    ledgeChainShape.CreateLoop(verts, totalPoints);
    
    //create fixture --- a fixture uses the previously made shape
    b2FixtureDef ledgeFixtureDef;
    
    ledgeFixtureDef.shape = &ledgeChainShape;    
    ledgeFixtureDef.friction = 1;
    ledgeFixtureDef.restitution = 1.0f;

    
    ledgeBody->CreateFixture( &ledgeFixtureDef );
    ledgeBody->CreateFixture(&ledgeChainShape, 1);

    ledgeBodies.push_back(ledgeBody);
    delete verts;
    
}

void Box2dController::createGround(){
    /*
     If we want to have particles catch on ground, add this ground body definition back.
     Otherwise we don't need to add this setup, can most likely set up the world within the main application class
     */
    
    
    //setup world that will contain all box2d things
    //define body
    b2BodyDef groundBodyDef;
    //crate the boundaries for the box2d objects -- if we want particles to fall off screen
    //then the height has to be larger than screen
    groundBodyDef.position.Set( Conversions::toPhysics( getWindowWidth() ), Conversions::toPhysics( getWindowHeight() ));
    
    //use world to create body
    b2Body *groundBody = world.CreateBody( &groundBodyDef );
    
    //define fixture
    b2PolygonShape groundBox;
    groundBox.SetAsBox( Conversions::toPhysics( getWindowWidth() ), Conversions::toPhysics(0.0f ));
    
    //create fixture on the body
    groundBody->CreateFixture( &groundBox, 0.0f );
    
    
}

void Box2dController::destroyLedges(){
    for(int i=0;i<ledgeBodies.size();i++){
        world.DestroyBody(ledgeBodies[i]);
    }
    ledgeBodies.clear();
}
