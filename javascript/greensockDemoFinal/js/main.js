
/*

css3 vs javascript:
http://css-tricks.com/myth-busting-css-animations-vs-javascript/
GSAP 20x faster than jQuery
css workflow kinda sucks with keyframes

Animated properties	Better w/JavaScript	Better w/CSS

top, left, width, height
JS: Windows Surface RT, iPhone 5s (iOS7), iPad 3 (iOS 6), iPad 3 (iOS7), Samsung Galaxy Tab 2, Chrome, Firefox, Safari, Opera, Kindle Fire HD, IE11
CSS: (none)

transforms (translate/scale)
JS: Windows Surface RT, iPhone 5s (iOS7), iPad 3 (iOS7), Samsung Galaxy Tab 2, Firefox, Opera, IE11
CSS: iPad 3 (iOS6), Safari, Chrome


*/





/* TweenMax properties to get you started

properties:

marginTop
marginBottom
marginLeft
marginRight
left
right
top
bottom
scaleX
scaleY
scale
autoAlpha //manipulates opacity and visibility 
className
bezier: {values:[{x:907, y:555}, {x:244, y:524}]
skewX
skewY

*/


$(function(){

	simple();

})


//Simple tween
//won't get a jump if you set opacity in css

function simple(){
	var a = $('.ani');

	TweenMax.to(a, 0, {css:{autoAlpha:0, top:-300}});
	TweenMax.to(a, 1, {css:{top:0, autoAlpha:1}, delay:1});

}


//EASING

function ease(){
	var a = $('.ani');

	TweenMax.to(a, 0, {css:{autoAlpha:0, top:-300}});
	TweenMax.to(a, 5, {css:{top:0, autoAlpha:1}, delay:.5, ease:Elastic.easeOut});
}

//STacking animations

function stack(){
	var a = $('.ani');

	TweenMax.to(a, 0, {css:{autoAlpha:0, top:-300}});
	TweenMax.to(a, 5, {css:{top:0, autoAlpha:1}, delay:.5, ease:Elastic.easeOut});
	TweenMax.from(a, 2, {css:{scaleY:0, rotation:180}, delay:.6, ease:Back.easeOut});
	TweenMax.to(a, 3, {css:{top:700, rotation:180}, delay:1.35, ease:Expo.easeInOut});

}



//multiple animations

function multiple(){
	var a = $('.ani');

	a.each(function( i ) {
	  var item = $(this);
	  console.log(item);
	  TweenMax.to(item, 0, {css:{autoAlpha:0, top:-300}});
	  TweenMax.to(item, 2, {css:{top:0, autoAlpha:1}, delay:i*.25, ease:Expo.easeOut});
	});
	
}

//timeline
function timeline(){

	var tl = new TimelineMax();
	tl.pause();
	var a = $('.ani');

	a.each(function( i ) {
	  var item = $(this);
	  tl.insert( TweenMax.to(item, 0, {css:{autoAlpha:0, top:-300}}), 0);
	  if(item.c)
	  tl.insert( TweenMax.to(item, 2, {css:{top:0, autoAlpha:1}, ease:Expo.easeOut}), 1+ i*.25);

	});

	tl.play();
	//tl.pause();
	//tl.resume();
	//tl.seek(1.5);
	//tl.totalTime(4); //amount of seconds animation should be
	//tl.reverse();/
	//tl.timeScale(5); //scale it
	//tl.play(1) //jump to frame
	//tl.addCallback(functionName, 1.5) //call this function after 1.5 seconds within the timeline
	

}


function swing(){

	var tl = new TimelineMax();
	tl.pause();
	var a = $('.ani');

	a.each(function( i ) {
	  var item = $(this);
	  tl.insert( TweenMax.to(item, 0, {css:{scaleY:0, skewX:40, top:10}}), 0);

	  tl.insert( TweenMax.to(item, 3, {css:{scaleY:1, skewX:10,top:0, transformOrigin:"center top"}, ease:Elastic.easeOut}), 1+ i*.5);
	});

	tl.play();


}


function classAni(){
	var tl = new TimelineMax({repeat:-1});
	
	var a = $('.ani');

	tl.pause();

	a.each(function( i ) {
	  var item = $(this);

	 tl.insert( TweenMax.to(item, 1, {css:{className:'+=active'}}), i*.25);
	 tl.insert( TweenMax.to(item, 2, {css:{className:'-=active'}}), 1+i*.25);

	});

	tl.play();
}