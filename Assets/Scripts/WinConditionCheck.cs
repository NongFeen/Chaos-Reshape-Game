using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinConditionCheck : MonoBehaviour
{
    public InsideMapManager insideMap;
    public OutsideRoomManager outsideRoom;
    public GameInit gameInit;
    public TextMeshProUGUI RoundCountText;
    public TextMeshProUGUI MoveCount;
    public TextMeshProUGUI TimerText;
    public GameTime GameTimer;
    public GameOver gameOver;
    public GameObject Endscreen;
    private float RoundTimeRemaining;
    private bool IsRoundWin = false;
    private int RoundNumber =1;
    private readonly int RoundWinRequire =3;
    void Awake(){
        IsRoundWin = false;
        GameTimer.StartTimer();
        Debug.Log(PlayerPrefs.GetInt("GameMode")+ " " + GetRoundTimer());
        RoundTimeRemaining = GetRoundTimer();
    }
    void Update()
    {   
        // Test_UpdateTimer();

        RoundTimeRemaining -= Time.deltaTime;
        if(Endscreen.activeSelf){
            return;
        }
        if (RoundTimeRemaining <= 0)
        {
            // GameComplete();
            GameOver(); // Trigger game-over if timer reaches 0.
            return; // Stop further updates after game over.
        }
        if(!IsRoundWin){
            IsRoundWin = CheckWincondition();
            if(IsRoundWin && RoundNumber >= RoundWinRequire && PlayerPrefs.GetInt("GameMode") != 2){
                GameComplete();
                UnlockOtherLevel();
            }
        }
        else{
            InitNewRound(GetRoundTimer());
            RoundNumber++;
        }

        UpdateRoundText();
        UpdateMoveCountText();
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }
    private bool CheckWincondition(){
            // if inside room all clensed?
        // card must have 2 symbol
            // card must have same symbol as shadow
        // each inside room must have 2 symbol 
        // and there own room must not have start symbol(green statue that hold it)
        foreach (InsideRoomManager room in insideMap.GetInsideRoom()){
            if(room.IsRoomClensed() == false){
                // Debug.Log(room + "Inside is not Clensed");
                return false;
            }
        }
        foreach (InsideRoomManager room in insideMap.GetInsideRoom()){
                foreach(var statue in room.GetComponentsInChildren<StatueShowShape>()){
                statue.GetComponent<Image>().sprite = Resources.Load<Sprite>("General/Cracked_Statue");
            }
        }
        
        foreach (InsideRoomManager room in insideMap.GetInsideRoom()){
            if(room.IsRoomClensed() == false){
                // Debug.Log(room + "Inside is not Clensed");
                return false;
            }
            if(!room.GetInsideCard().GetCard().IsFull()){
                // Debug.Log("Card is not Full");
                return false;
            }else{//if full then run loop
                foreach (int symbol in room.RoomShadow)
                {
                    if( !(symbol == room.GetInsideCard().GetCard().GetFirstValue() || 
                        symbol == room.GetInsideCard().GetCard().GetSecondValue())){
                        // Debug.Log("Card Symbol is not Match Shadow");
                        return false;
                    }
                }
            }
            if(!room.IsTwoShadows()){
                // Debug.Log("Shadow is not 2 each");
                return false;
            }
            if(room.IsShadowHaveCorrectedStatueSymbol()){
                // Debug.Log("Room have it own room Shadow");
                return false;
            }
        }
        //check out side match inside
            //left to right is 0-2
        StoreSymbol[] outSideSymbol = outsideRoom.GetOutsideStatueSymbol();
        StoreSymbol[] inSideCardSymbol = insideMap.GetAllInsideCardSymbol();
        for(int i=0;i<3;i++){
            if(!outSideSymbol[i].IsMatch(inSideCardSymbol[i])){
                // Debug.Log("Inside is not matching outside");
                return false;
            }
        }
        Debug.Log("Round win");
        return true;
    }
    private void InitNewRound(int time){
        gameInit.RoundSetup(time);
        RoundTimeRemaining = time;
        IsRoundWin = false;
    }
    private void UnlockOtherLevel(){
        PlayerPrefs.SetInt("UnlockedLevel",3);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("Unlock All level"));
    }
    private void UpdateRoundText(){
        RoundCountText.text = "Round : " + RoundNumber;
    }
    private void UpdateMoveCountText(){
        MoveCount.text = "Move : " + PlayerPrefs.GetInt("MoveCount");
    }
    private int GetRoundTimer(){
        int GameModeType = PlayerPrefs.GetInt("GameMode");
        int BaseTimer = 180;
        //normal mode
        if(GameModeType == 0){ //180 140 100 sec
            switch (RoundNumber-1)
            {
                case 0 : //this would never 
                    Debug.Log("Timer : " + BaseTimer);
                    return BaseTimer;
                case 1 :
                    Debug.Log("Timer : " + (BaseTimer-40));
                    return BaseTimer-40;
                case 2 : 
                    Debug.Log("Timer : " + (BaseTimer-80));
                    return BaseTimer-80;
            }
        }
        //hard mode
        else if(GameModeType ==1){ //30 25 20
            switch (RoundNumber-1)
            {
                case 0 : //this would never
                    // Debug.Log("Timer : " + BaseTimer/2);
                    return BaseTimer/6;
                case 1 :
                    // Debug.Log("Timer : " + (BaseTimer/2));
                    return BaseTimer/7;
                case 2 : 
                    // Debug.Log("Timer : " + ((BaseTimer/2) -30));
                    return BaseTimer/9;
            }
        }
        //endless mode
        else if(GameModeType ==2){
            // return 2;
            return GetRampDownTime(RoundNumber);
        }
        Debug.Log("WHAT MODE DO YOU PICK HUH????");
        return 10;
    }
    public int GetRampDownTime(int round)
    {
        float startTime = 90f;
        float endTime = 30f;   
        float rampSpeed = 0.05f;
        // Apply the exponential decay formula: y = 30 + (90 - 30) * e^(-0.1 * round)
        // yes gpt do this work gj
        float time = endTime + (startTime - endTime) * Mathf.Exp(-rampSpeed * round);
        return (int)Mathf.Max(time, endTime);
    }
    private void GameComplete(){
        // Show time
        // Create field to enter name
        // return to menu and retry button
        GameTimer.StopTimer();
        gameOver.Setup(true,PlayerPrefs.GetInt("MoveCount"),GameTimer.Timer);
        // Debug.Log(GameTimer.Timer);
    }
    private void Test_UpdateTimer(){
        TimerText.text = "" + GameTimer.Timer + "\nRound Timer: " + Mathf.Ceil(RoundTimeRemaining);
    }
    private void GameOver()
    {
        GameTimer.StopTimer();
        if(PlayerPrefs.GetInt("GameMode") ==2){//endless mode
            gameOver.Setup(PlayerPrefs.GetInt("MoveCount"),GameTimer.Timer,RoundNumber);
        }else{//normal mode
            gameOver.Setup(false,PlayerPrefs.GetInt("MoveCount"),GameTimer.Timer);
        }
        // Debug.Log("Game Over");
    }


}