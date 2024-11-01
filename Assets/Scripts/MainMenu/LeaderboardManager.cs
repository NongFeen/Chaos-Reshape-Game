using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Leaderboards;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using System;
using UnityEngine.TextCore.Text;
using Unity.Services.Leaderboards.Exceptions;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms.Impl;
using Unity.Services.Leaderboards.Models;
using TMPro;
using Unity.VisualScripting;

public class LeaderboardManager : MonoBehaviour
{
    public Transform LeaderboardContent;
    public Transform LeaderboardItemPrefab;
    public GameObject MainLeaderboard;

    private string NormalModeLeaderboardID = "Normal_Mode";
    
    // Start is called before the first frame update
    async void Start()
    {
        // Debug.Log(AuthenticationService.Instance.SessionTokenExists);
        // try{
        //     if (AuthenticationService.Instance.SessionTokenExists)
        //     {
        //         // Sign out the current user
        //         AuthenticationService.Instance.ClearSessionToken();
        //         Debug.Log("Signed out current user.");
        //     }
        // }
        // catch (Exception e){
        //     Debug.LogError($"{e}");
        // }
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 
            AuthenticationService.Instance.SignOut(true);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 
        }
        catch (Exception e)
        {
            if (AuthenticationService.Instance.SessionTokenExists) 
            {
                // if not, then do nothing
                return;
            }
            Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateLeaderboard();
    }
    public class NormalModeMetadata
    {
        public string DisplayName;
        public string MoveCount;
    }
    public async void UpdateLeaderboard(){
        if(MainLeaderboard.activeSelf){
            LeaderboardScoresPage leaderboardScoresPage = await LeaderboardsService.Instance.GetScoresAsync(NormalModeLeaderboardID,new GetScoresOptions
            {
                IncludeMetadata = true // Set to true to include metadata in the response
            });

            foreach(Transform t in LeaderboardContent){
                Destroy(t.gameObject);
            }

            foreach(Unity.Services.Leaderboards.Models.LeaderboardEntry entry in leaderboardScoresPage.Results){
                NormalModeMetadata metadata = JsonConvert.DeserializeObject<NormalModeMetadata>(entry.Metadata.ToString());
                double score = entry.Score; // Assuming score is in seconds
                // Calculate minutes, seconds, and milliseconds
                int minutes = (int)(score / 60);
                int seconds = (int)(score % 60);
                int milliseconds = (int)((score - (int)score) * 1000); // Get milliseconds

                // Format the time as mm:ss.ms
                string formattedTime = string.Format("{0:D2}:{1:D2}.{2:D3}", minutes, seconds, milliseconds);

                Transform leaderboardItem = Instantiate(LeaderboardItemPrefab,LeaderboardContent);
                leaderboardItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = "#" + (entry.Rank +1);
                leaderboardItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = metadata.DisplayName;
                leaderboardItem.GetChild(2).GetComponent<TextMeshProUGUI>().text = formattedTime;
                leaderboardItem.GetChild(3).GetComponent<TextMeshProUGUI>().text = metadata.MoveCount;
            }
            Debug.Log("Leaderboard Updated");
        }
        await Task.Delay(1000);
    }
    public class LeaderboardEntry
    {
        public string PlayerName;
        public float Time;
        public int MoveCount;
    }
    public async void SubmitScore(string playerName, float time, int moveCount)
    {
        var metadata = new Dictionary<string, string>
        {
            { "DisplayName", playerName },
            { "MoveCount", moveCount.ToString() },
        };

        var addPlayerScoreOptions = new AddPlayerScoreOptions
        {
            Metadata = metadata
        };
        try
        {
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(
                NormalModeLeaderboardID, 
                time,          
                addPlayerScoreOptions
            );

            Debug.Log("Score submitted successfully!");
            Debug.Log(JsonConvert.SerializeObject(playerEntry));
        }
        catch (LeaderboardsException ex)
        {
            Debug.LogError($"Error submitting score: {ex.Message}. Please check if the leaderboard ID is correct.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"An unexpected error occurred: {ex.Message}");
        }
    }
    public async void ChangeAnonymousUserId()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 
            AuthenticationService.Instance.SignOut(true);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 
        }
        catch (Exception e)
        {
            if (AuthenticationService.Instance.SessionTokenExists) 
            {
                // if not, then do nothing
                return;
            }
            Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
        }
    }
}
