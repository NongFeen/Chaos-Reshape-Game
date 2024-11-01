using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI ScoreText;
    public GameObject SubmitField;
    //normal mode
    public void Setup(bool IsWin,int move, float totalTime){
        this.gameObject.SetActive(true);
        if(IsWin){
            EndText.text = "Game Complete!";
            SubmitField.SetActive(true);
        }else{
            EndText.text = "Game OVER!";
            SubmitField.SetActive(false);
        }
        ScoreText.text = "Total Time  :   "+ totalTime.ToString("F3") + "\nTotal Move :   " + move;
    }
    //overload for endless mode
    public void Setup(int move, float totalTime, int round){
        this.gameObject.SetActive(true);
        EndText.text = "Game Complete!";
        ScoreText.text = "Total Time  :   "+ totalTime.ToString("F3") + "\nTotal Move :   " + move + "\nTotal Round :   " + round;
    }
    public void RestartButton(){
        SceneManager.LoadScene("Scenes/PlayScene");
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("Scenes/MainMenu");
        
    }
}
