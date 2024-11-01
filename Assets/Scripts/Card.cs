using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card",menuName ="Card")]
public class Card : ScriptableObject
{
    public StoreSymbol curentShape = new StoreSymbol();
    
    public int? GetFirstValue()
    {
        return curentShape.values[0];
    }
    public int? GetSecondValue()
    {
        return curentShape.values[1];
    }
    public bool IsFull(){
        return curentShape.IsFull();
    }
}
