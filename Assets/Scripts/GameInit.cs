using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    // Start is called before the first frame update
    public InsideMapManager insideMap;
    public OutsideRoomManager outsideRoom;
    public BackGroundTimer backGroundTimer;
    public int Timer;

    void Start()
    {   
        if (insideMap == null || outsideRoom == null || backGroundTimer == null)
        {
            Debug.LogError("One or more required components are not assigned in the Inspector.");
            return;
        }
        GamemodeSetup();
        RoundSetup(Timer);
    }
    private void GamemodeSetup(){
        PlayerPrefs.SetInt("MoveCount",0);
        switch(PlayerPrefs.GetInt("GameMode")){
            case 0: 
                Debug.Log("This gamemode is Normal");
                Timer = 180;
                break;
            case 1: 
                Debug.Log("This gamemode is Hard");
                Timer = 30;
                break;
            case 2: 
                Debug.Log("This game is Endless HAVEFUN");
                Timer = 30;
                break;
            default: 
                Debug.Log("WTF");
                break;
        }
    }
    public void RoundSetup(int timer){
        // Initialize symbol lists
        List<int> availableSymbols = new() { 0, 3, 4 };
        List<int> randomOrder = GenerateRandomOrder(availableSymbols, 3);
        List<int> randomShadow = new() { 0, 3, 4 };
        if (backGroundTimer != null){
            backGroundTimer.SetTimer(timer);
        }
        else{
            Debug.LogError("backGroundTimer is not assigned.");
        }
        ClearRound();
        for (int i = 0; i < 3; i++)
        {
            // create base
            int randomSymbol = randomOrder[i];
            insideMap.GetInsideRoom()[i].RoomShadow.Add(randomSymbol);
            insideMap.GetInsideRoom()[i].AddStatueSymbol(randomOrder);

            // random
            int randomShadowIndex = Random.Range(0, randomShadow.Count);
            insideMap.GetInsideRoom()[i].RoomShadow.Add(randomShadow[randomShadowIndex]);
            insideMap.GetInsideRoom()[i].UpdatePrefabs();
            randomShadow.RemoveAt(randomShadowIndex);
        }

        // outside 
            // base
        for (int i = 0; i < 3; i++)
        {
            outsideRoom.GetOutsideStatueSymbol()[i].Append(randomOrder[i]);
        }
            // random
        foreach (var statue in outsideRoom.GetOutsideStatueSymbol())
        {
            if (randomOrder.Count > 0)
            {
                int randomIndex = Random.Range(0, randomOrder.Count);
                statue.Append(randomOrder[randomIndex]);
                randomOrder.RemoveAt(randomIndex);
            }
        }
    }
    private void ClearRound(){
        //inside
        for (int i = 0; i < 3; i++)
        {
            insideMap.GetInsideRoom()[i].ClearInsideRoomRound();
            outsideRoom.GetOutsideStatueSymbol()[i].ClearSymbol();
        }

    }
    List<int> GenerateRandomOrder(List<int> symbols, int count)
    {
        List<int> result = new();
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, symbols.Count);
            result.Add(symbols[randomIndex]);
            symbols.RemoveAt(randomIndex);
        }
        return result;
    }
}
