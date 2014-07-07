#include "cinder/app/AppNative.h"
#include "cinder/gl/gl.h"

#include "PongBall.h"
#include "Paddle.h"
//For Sensors
#include "ActivityController.h"

//For Physics
//#include "Box2D/Box2D.h"
//#include "Box2dController.h"




using namespace ci;
using namespace ci::app;
using namespace std;

class BodyPongApp : public AppNative {
  public:
	void setup();
	void mouseDown( MouseEvent event );	
	void update();
	void draw();
    ActivityController activityController;
    Pongball ball;
    Paddle LeftPaddle,rightPaddle;
    Vec2f position;
};

void BodyPongApp::setup()
{

    activityController.setup();
    
    position=Vec2f(0,0);
    ball.setup();
}

void BodyPongApp::mouseDown( MouseEvent event )
{
}

void BodyPongApp::update()
{
     activityController.update();
    ball.update();
    position = position+Vec2f(1,1);
    
}

void BodyPongApp::draw()
{
	// clear out the window with black
	gl::clear( Color( 0, 0, 0 ) );
    ball.draw();
}

CINDER_APP_NATIVE( BodyPongApp, RendererGl )
