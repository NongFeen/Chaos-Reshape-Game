using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSymbol 
{
    public int?[] values; 
    private int currentIndex; 
    private readonly List<int> ShapeList = new(){0,3,4};
    public StoreSymbol()
    {
        values = new int?[2]; 
        currentIndex = 0; 
    }
    public void Append(int value)
    {
        if(!ShapeList.Contains(value)){
            // Debug.Log("Input Invalid Symbol");
            return;
        }
        if (currentIndex < 2)
        {
            values[currentIndex] = value; 
            currentIndex++; 
        }
    }
    public void Clear()
    {
        values[0] = null; 
        values[1] = null; 
        currentIndex = 0;
    }
    public void SwapSymbol(int statueSymbol, int newSymbol){
        if(values[0]==statueSymbol)
            values[0] = newSymbol;
        else if(values[1]==statueSymbol)
            values[1] = newSymbol;
        // Debug.Log("Statue has been swap symbol");
    }
    public void DisplayCurrentSymbol()
    {
        // Debug.Log($"Shape have symbol: {values[0]}, {values[1]}");
    }
    public int? GetFirstValue()
    {
        return values[0];
    }
    public int? GetSecondValue()
    {
        return values[1];
    }
    public bool IsFull(){
        if(values[1]==null)
            return false;
        return true;
    }
    public bool IsMatch(StoreSymbol ss){
        if (this.IsFull() && ss.IsFull())
        {
            return (this.values[0] == ss.values[0] && this.values[1] == ss.values[1]) ||
                (this.values[0] == ss.values[1] && this.values[1] == ss.values[0]);
        }
        return false;
    }
    public void ClearSymbol(){
        values = new int?[2];
        currentIndex = 0; 
    }
}
