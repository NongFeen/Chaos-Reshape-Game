using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class SymbolToCard : MonoBehaviour
{
    // Start is called before the first frame update
    public CardImageUpdate OutCard;
    public Symbol sym;
    public void OnClick(){
        // PlayerPrefs.SetInt("MoveCount",PlayerPrefs.GetInt("MoveCount")+1);
        OutCard.AddSymbol(sym.GetSymbol());
    }
}
