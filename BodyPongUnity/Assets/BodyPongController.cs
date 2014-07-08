using UnityEngine;
using System.Collections;

public class BodyPongController : MonoBehaviour {

	public GUIText scoreText;
	public GameObject ball;
	public BallController ballController;
	int player1Score = 0;
	int player2Score = 0;


	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdateScore");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateScore(){
		Debug.Log (ball.transform.position.x);
		if (ball.transform.position.x > 0) {
				player1Score++;
		} else
				player2Score++;
		scoreText.text = "player1 " + player1Score + " player2: " + player2Score;

	}
}
