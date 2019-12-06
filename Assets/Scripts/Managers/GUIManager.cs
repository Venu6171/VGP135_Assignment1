using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GUIManager : MonoBehaviour {
    public static GUIManager instance;

    public GameObject gameOverPanel;
    public Text yourScoreTxt;
    public Text highScoreTxt;

    public Text scoreTxt;
    public Text moveCounterTxt;

    private int score;
    private int moveCounter;
    private int matchTime;
    void Awake()
    {
        matchTime = 60;
        moveCounter = matchTime;
        //moveCounterTxt = moveCounter.ToString();
        instance = GetComponent<GUIManager>();
    }

        public int Score
        {
        get{return score;}

        set
        {
            score = value;
            scoreTxt.text = score.ToString();
        }
    }

    //private void Update()
    //{
    //    moveCounter -= (int)Time.deltaTime;
    //    if(moveCounter < 0)
    //    {
    //        GameOver();
    //    }
    //}

    public int MoveCounter
    {
        get { return moveCounter; }

        set
        {
            moveCounter = value;
            moveCounterTxt.text = moveCounter.ToString();
        }
    }

	// Show the game over panel
	public void GameOver() {
		GameManager.instance.gameOver = true;

		gameOverPanel.SetActive(true);

		if (score > PlayerPrefs.GetInt("HighScore")) {
			PlayerPrefs.SetInt("HighScore", score);
			highScoreTxt.text = "New Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		} else {
			highScoreTxt.text = "Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

		yourScoreTxt.text = score.ToString();
	}

    private IEnumerator WaitForShifting()
    {
        yield return new WaitUntil(() => !BoardManager.instance.IsShifting);
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(WaitForShifting());
    }

    private IEnumerator StartCoroutine(int moveCounter)
    {
        moveCounter -= (int)Time.deltaTime;
        if (moveCounter < 0)
        {
            GameOver();
        }
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(moveCounter);
    }
}
