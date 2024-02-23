using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
using System.Xml;
using System;

[System.Serializable]
public class KillScore : MonoBehaviour
{
    public static int killScore;
    public TMP_Text scoreKillText;
    public TMP_InputField confirmButton;
    public TMP_InputField nicknameInputField;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;
    public GameOverMenu gameOverMenu;
    private Dictionary<string, int> leaderboard = new Dictionary<string, int>();
    public TMP_Text topScoresText;
    private bool confirmationCompleted = false;
    public int noTies = 0;
    public TMP_Text progressText;
    // New variable to store play time
    private float playTime = 0f;
    private bool isGPressed = false;
    private bool isVPressed = false;
    private bool isDPressed = false;
    private bool isBPressed = false;
    // Function to add or update a player's score

    public void AddScore(string playerName, int kills, float playTime)
    {
        // Check if the player name is already in the leaderboard
        if (leaderboard.ContainsKey(playerName))
        {
            // If the player is already in the leaderboard, check if the new kills are higher
            if (kills > leaderboard[playerName])
            {
                leaderboard[playerName] = kills;
                Debug.Log($"Kills updated for {playerName}.");
            }
            else
            {
                Debug.Log("Existing kills are higher. No changes made.");
            }
        }
        else
        {
            // If the player is not in the leaderboard, add them
            leaderboard[playerName] = kills;
            Debug.Log($"Kills added for {playerName}.");
        }

    
    }
    // Function to print the leaderboard
    public void PrintLeaderboard()
    {
        // Sort the leaderboard based on kills and print the top 10
        var sortedLeaderboard = leaderboard.OrderByDescending(x => x.Value).Take(15);
        Debug.Log("Leaderboard:");

        int rank = 1;
        foreach (var entry in sortedLeaderboard)
        {
            Debug.Log($"{rank}. {entry.Key}: {entry.Value} kills");
            rank++;
        }
    }

    // Example usage:

    // Start is called before the first frame update
    public void Start()
    {
        nicknameInputField.characterLimit = 12;  // Set the character limit to 12
        LoadHighScores();
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();

        killScore = 0;
        scoreKillText.text = "Kills: " + killScore;
        confirmButton.onValueChanged.AddListener(ConfirmButtonClicked);
        string playerName = nicknameInputField.text;

        HighScoreUpdate();

        PrintLeaderboard();

        finalScoreText.text = "LastScore: " + killScore.ToString();
    }

    public void ConfirmButtonClicked(string value)
    {
        // Check if confirmation has already been completed
        if (confirmationCompleted)
        {
            // Confirmation already completed, return or handle accordingly
            return;
        }

        // Set the flag to true to indicate that confirmation has been completed
        confirmationCompleted = true;

        // Get the player's nickname from the input field
        nicknameInputField.onEndEdit.AddListener(OnEndEdit);
        //calling the leaderboard from here so after you have confirm your named you'll directly get to the leaderboard.
        
    }

    private void OnEndEdit(string value)
    {
        // This method will be called when the user finishes editing the input field
        string playerName = nicknameInputField.text;
        HighScoreUpdate();

        // Log the playTime before calling UpdateTop10List
        Debug.Log("playTime before UpdateTop10List: " + playTime);

        // Call UpdateTop10List with the correct playTime value
        Debug.Log("playTime before UpdateTop10List: " + playTime);
        UpdateTop10List(playerName, killScore, playTime);
        Debug.Log("playTime after UpdateTop10List: " + playTime);

        // Log the playTime after calling UpdateTop10List
        Debug.Log("playTime after UpdateTop10List: " + playTime);

        // Call SaveScoreToLeaderboard with the correct playTime value
        SaveScoreToLeaderboard(playTime);

        // Log a message to indicate that SaveScoreToLeaderboard was called
        Debug.Log("SaveScoreToLeaderboard called with playTime: " + playTime);

    }

    // Update is called once per frame
    void Update()
    {
        CheckSecretCode();


     playTime += Time.deltaTime;

        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            int topScore = PlayerPrefs.GetInt("SavedHighScore");
            float progressPercentage = (float)killScore / topScore * 100f;
            progressText.text = $"Progress: {progressPercentage:F1}%";
        }
    }

    public void SaveScoreToLeaderboard(float playTime)
    {
        // Your existing code for saving the score

        // Save the time along with the score

        float time = playTime;

        Debug.Log("playTime updated: SaveScoreToLeaderboard " + time);
        // Other relevant code...




    }

    public void UpdateScore(int Kills)
    {
        killScore += Kills;
        scoreKillText.text = "Kills: " + killScore;
    }

    public void HighScoreUpdate()
    {
        // Check if there already a top score
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            // Is the new score higher than the saved one?
            if (killScore > PlayerPrefs.GetInt("SavedHighScore"))
            {
               
                // Set a new highScore
                PlayerPrefs.SetInt("SavedHighScore", killScore);
                SaveHighScore();
            }
        }
        else
        {
            // If there is no high score, set it
            PlayerPrefs.SetInt("SavedHighScore", killScore);
        }

        // Update our TMP
        finalScoreText.text = "Your Score: " + killScore.ToString();
        highScoreText.text =  "HighScore: " + PlayerPrefs.GetInt("SavedHighScore").ToString();

    }

    void SaveHighScore()
    {
        // Save the high score to PlayerPrefs or any other storage mechanism
        PlayerPrefs.SetInt("SavedHighScore", killScore );
        PlayerPrefs.Save();
    }

    void LoadHighScores()
    {
        // Load the top 10 list from PlayerPrefs
        if (PlayerPrefs.HasKey("SavedTop10List"))
        {
            string top10Json = PlayerPrefs.GetString("SavedTop10List");
            List<HighScoreEntry> top10List = JsonUtility.FromJson<HighScoreList>(top10Json).entries;
            DisplayTop10List(top10List);
        }
        // Load the saved high score
    }

    void UpdateTop10List(string playerName, int score, float playTime)
    {
        Debug.Log("Updating top 10 list. playerName: " + playerName + ", score: " + score + ", playTime: " + playTime);

        // Load the existing top 10 list
        List<HighScoreEntry> top10List = new List<HighScoreEntry>();
        if (PlayerPrefs.HasKey("SavedTop10List"))
        {
            string top10Json = PlayerPrefs.GetString("SavedTop10List");
            top10List = JsonUtility.FromJson<HighScoreList>(top10Json).entries;
        }

        // Check if the player is already in the top 10
        var existingEntry = top10List.FirstOrDefault(entry => entry.playerName == playerName);

        if (existingEntry != null)
        {
            // If the player is already in the top 10, update their score and time
            if (score > existingEntry.score)
            {
                existingEntry.score = score + noTies;
                existingEntry.playTime = playTime;
                Debug.Log("playTime: UpdateTop10List " + playTime);
            }
        }
        else
        {
            // If the player is not in the top 10, add a new entry
            // Only add a new entry if there is no entry with the same name in the list
            if (!top10List.Any(entry => entry.playerName == playerName))
            {
                top10List.Add(new HighScoreEntry(playerName, score, playTime));
                Debug.Log("playTime: UpdateTop10List player isn't in top 10 list " + playTime);
            }
        }

        // Sort the top 10 list based on scores
        top10List = top10List.OrderByDescending(entry => entry.score).Take(15).ToList();

        // Save the updated top 10 list to PlayerPrefs
        PlayerPrefs.SetString("SavedTop10List", JsonUtility.ToJson(new HighScoreList(top10List)));
        PlayerPrefs.Save();

        // Display the updated top 10 list
        DisplayTop10List(top10List);
    }

    void DisplayTop10List(List<HighScoreEntry> top10List)
    {
        // Clear the existing text
        topScoresText.text = "";
        // Display the top 10 list in your UI
        // You can use this information to update your UI elements
        string topScoresString = "Top 15 Scores:\n";


        for (int i = 0; i < top10List.Count; i++) //I tæller plasen som har på leaderboardet 
        {
            var entry = top10List[i];
            string rankColor = GetRankColor(i + 1); // Adjust the index to start from 1

            // Apply different colors to rank, name, and kills
            topScoresString += $"{rankColor}{i + 1}. </color>";

            // Apply special colors to names in the top 3 positions
            if (i < 3)
            {
                string nameColor = GetNameColor(i);
                topScoresString += $"<color={nameColor}>{entry.playerName}:</color> ";
            }
            else
            {
                topScoresString += $"<color=white>{entry.playerName}:</color> ";
            }

            topScoresString += $"<color=red>{entry.score} kills</color> ";
            topScoresString += $"in {FormatTime(entry.playTime)}\n";
        }

        topScoresText.text = topScoresString;
    }
    string GetRankColor(int rank)
    {
        switch (rank)
        {
            case 1:
                return "<color=yellow>"; // gold
            case 2:
                return "<color=#C0C0C0>"; // silver 
            case 3:
                return "<color=#CD7F32>"; // Bronze color
            default:
                return "<color=white>";
        }
    }
    string GetNameColor(int index)
    {
        // Switch the name color based on the index
        switch (index % 3)
        {
            case 0:
                return "yellow";
            case 1:
                return "#C0C0C0";
            case 2:
                return "#CD7F32";
            default:
                return "white";
        }
    }
    string FormatTime(float timeInSeconds)
    {
        // Format the time into minutes and seconds
        float minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    [System.Serializable]
    public class HighScoreEntry
    {
        public string playerName;
        public int score;
        public float playTime;

        public HighScoreEntry(string playerName, int score, float playTime)
        {
            this.playerName = playerName;
            this.score = score;
            this.playTime = playTime;
        }
    }

    [System.Serializable]
    public class HighScoreList
    {
        public List<HighScoreEntry> entries;

        public HighScoreList(List<HighScoreEntry> entries)
        {
            this.entries = entries;
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }
    private void CheckSecretCode()
    {
        // Check for G, V, D, B keys pressed simultaneously
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isVPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isDPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBPressed = true;
        }

        // Check if all keys are pressed simultaneously
        if (isGPressed && isVPressed && isDPressed && isBPressed)
        {
            // Reset the score when the secret code is entered
            ResetScore();
        }

        // Reset the flags if any of the keys are released
        if (Input.GetKeyUp(KeyCode.G))
        {
            isGPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            isVPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isDPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            isBPressed = false;
        }
    }

}