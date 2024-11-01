using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] button;
    public void Awake(){
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel",1);
        for(int i=0;i<button.Length;i++){
            button[i].interactable = false;
        }
        for(int i=0;i< unlockedLevel;i++){
            if(i<button.Length)
                button[i].interactable = true;
        }
    }
    public void OpenGameMode(int mode){
        // Gamemode 
        // Normal  0
        // Hard    1
        // Endless 2
        PlayerPrefs.SetInt("GameMode", mode);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Scenes/PlayScene");
    }
}
