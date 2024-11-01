using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URSolution : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.canva.com/design/DAGUUssp4-M/8tRi2GmidURhst5Q1K2vUA/view?utm_content=DAGUUssp4-M&utm_campaign=designshare&utm_medium=link&utm_source=editor");
    }
    
    public void ExitGame(){
        Application.Quit();
    }
}
