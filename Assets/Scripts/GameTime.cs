using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameTime : MonoBehaviour
{
    public float Timer;
    private bool IsTimerStart;
    void Update(){
        if(IsTimerStart){
            Timer += Time.deltaTime;
        }
    }
    public void StartTimer(){
        Timer=0;
        IsTimerStart = true;
    }
    public void StopTimer(){
        IsTimerStart = false;
    }
    public void ResetTimer(){
        Timer = 0;
    }
}
