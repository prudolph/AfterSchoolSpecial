using UnityEngine;
using System.Collections;

public class BodyPongController : MonoBehaviour {

	public GUIText scoreText;
	int player1Score;
	int player2Score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateScore(){
		Debug.Log ("you should update the score");
	}
}
