using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class KillScore : MonoBehaviour
{
    public int killScore;
    public TMP_Text scoreKillText;

    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreKillText.text = "Kills: " + killScore;
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
        finalScoreText.text = "LastScore: " + killScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int Kills)
    {
        killScore += Kills;
        scoreKillText.text = "Kills: " + killScore;
    }
    public void HighScoreUpdate()
    {
        //ia there already a top score? 
        if(PlayerPrefs.HasKey("SavedHighScore"))
        {
            //is the new score higher then the saved one? 
            if(killScore > PlayerPrefs.GetInt("SavedHighScore"))
            {
                //Set a new highScore
                PlayerPrefs.SetInt("SavedHighScore", killScore);
            }
        }
        else
        {
            //if there is no highscore.. set it
            PlayerPrefs.SetInt("SavedHighScore", killScore);
        }
        //update our TMP 
        finalScoreText.text = "LastScore: " + killScore.ToString();
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
    }
    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
       
    }
}
