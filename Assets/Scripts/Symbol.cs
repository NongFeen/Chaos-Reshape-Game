using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symbol",menuName ="Symbol")]
public class Symbol : ScriptableObject
{
    public int symbol;
    // Start is called before the first frame update
    public Symbol(int number){
        this.symbol = number;
    }
    public int GetSymbol(){
        return symbol;
    }
    public void SetSymbol(int sym){
       symbol = sym;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
