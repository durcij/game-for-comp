using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {
	public float totalTime;
	string minutes;
	string seconds;
	float startTime;
	float timeElapsed;
	float timeLeft;
	GameObject timerDisplay;
	GameObject risio;
	GameObject tristitia;
	GameObject furia;
	GameObject dormio;
	Text text;

	GameObject finalScore;
	Text finalScoreText;
	string totalScore;
	string risioScore;
	string tristitiaScore;
	string furiaScore;
	string dormioScore;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		timerDisplay = GameObject.Find("GameTimer");
		text = timerDisplay.GetComponent<Text>();
		totalTime = 180.0f;
		risio = GameObject.Find("Risio");
		tristitia = GameObject.Find("Tristitia");
		furia = GameObject.Find("Furia");
		dormio = GameObject.Find("Dormio");
		finalScore = GameObject.Find("FinalScore")
		finalScoreText = finalScore.GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		timeElapsed = Time.time - startTime;
		timeLeft = totalTime - timeElapsed;
		minutes = ((int) timeLeft / 60).ToString();
		seconds = (timeLeft % 60).ToString("f2");
		if ((timeLeft % 60) < 9) {
			text.text = minutes + ":0" + seconds;
		} else {
			text.text = minutes + ":" + seconds;
		}

		if (timeLeft <= 0) {
			text.text = "0:00.00";
			Destroy(risio);
			Destroy(tristitia);
			Destroy(furia);
			Destroy(dormio);

			totalScore

			finalScoreText = "Time's Up!\nTotal Score:  " + totalScore + "\nPlayer 1:  " + risioScore + "\nPlayer 2:  " + tristitiaScore + "\nPlayer 3:  " + furiaScore + "\nPlayer 4:  " + dormioScore;
		}
	}
}
