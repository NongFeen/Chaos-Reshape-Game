using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPref : MonoBehaviour
{
    public void OnClick(){
        PlayerPrefs.SetInt("UnlockedLevel",PlayerPrefs.GetInt("UnlockedLevel",1)+1);
        Debug.Log(PlayerPrefs.GetInt("UnlockedLevel"));
        PlayerPrefs.Save();
    }
    public void ClearPref(){
        PlayerPrefs.SetInt("UnlockedLevel",1);
        PlayerPrefs.SetInt("MoveCount",0);
        Debug.Log(PlayerPrefs.GetInt("UnlockedLevel"));
        PlayerPrefs.Save();
    }
}
 