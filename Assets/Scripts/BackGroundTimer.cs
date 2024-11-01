using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGroundTimer : MonoBehaviour
{
    public int timer; // Timer in seconds
    // public int timer = 180; // Timer in seconds
    
    private int _CapPos = 3250;
    private float velocity;
    private float timeElapsed = 0f;
    private bool IsTimeOut ;
    private Vector3 startPos;
    private RectTransform _RectTransformBG;

// Start is called before the first frame update
    void Start()
    {
        _RectTransformBG = this.GetComponent<RectTransform>();
        // Debug.Log(this.GetComponent<RectTransform>().position);//-1322.748
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        
        if(IsTimeOut){
            return;
        }
        if (timeElapsed <= timer && _RectTransformBG.position.x < _CapPos)
        {
            _RectTransformBG.position += new Vector3(velocity * Time.deltaTime, 0, 0);
        }
        else if(timeElapsed >= timer){
            IsTimeOut = true;
        }
        // Debug.Log("Time Elsaped" + timeElapsed + "IStimeOUT = " + IsTimeOut);
    }
    public void SetTimer(int second){
        timer = second;
        //do not adjust start Pos due to it will never change
        _RectTransformBG = this.GetComponent<RectTransform>();
        startPos = this.GetComponent<RectTransform>().position = new Vector3(-1322.75f,540f,0);//fix postion 
        IsTimeOut = false;
        timeElapsed = 0f;
        float distanceToTravel = _CapPos - startPos.x; 
        velocity = distanceToTravel / timer; 
    }

}
