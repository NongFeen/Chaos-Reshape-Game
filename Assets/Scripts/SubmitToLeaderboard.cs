using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitToLeaderboard : MonoBehaviour
{
    public TextMeshProUGUI InputName;
    public LeaderboardManager LeaderboardManager;
    public GameTime gameTime;
    public void SubmitScore(){
        LeaderboardManager.SubmitScore(InputName.text,gameTime.Timer,PlayerPrefs.GetInt("MoveCount"));
        this.transform.parent.gameObject.SetActive(false);
    }
}
