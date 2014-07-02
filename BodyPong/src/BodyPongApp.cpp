#include "cinder/app/AppNative.h"
#include "cinder/gl/gl.h"

#include "PongBall.h"
#include "ActivityController.h"

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
