using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutsideRoomManager : MonoBehaviour
{
    public GameObject playCanvas;
    public List<StatueShowShape> OutStatue = new();
    public StatueShowShape SelectedStatue;
    public int SelectedSymbol = -1;

    private StoreSymbol LeftOutStatue;
    private StoreSymbol MidOutStatue;
    private StoreSymbol RightOutStatue;

    void Start()
    {
        SimpleStartGame();
    }

    private void SimpleStartGame()
    {
        playCanvas.SetActive(true);
        // SetOutside();
    }

    private void SetOutside()
    {
        LeftOutStatue = OutStatue[0].GetStoreSymbol();
        MidOutStatue = OutStatue[1].GetStoreSymbol();
        RightOutStatue = OutStatue[2].GetStoreSymbol();
        List<int> availableSymbols = new() { 0, 0, 3, 3, 4, 4 };
        //random symbol to statue
        foreach (var statue in new[] { LeftOutStatue, MidOutStatue, RightOutStatue })
        {
            while (!statue.IsFull())
            {
                int randomIndex = Random.Range(0, availableSymbols.Count);
                statue.Append(availableSymbols[randomIndex]);
                // Debug.Log(availableSymbols[randomIndex]);
                availableSymbols.RemoveAt(randomIndex);
            }
        }
    }
    public StoreSymbol[] GetOutsideStatueSymbol(){
        LeftOutStatue = OutStatue[0].GetStoreSymbol();
        MidOutStatue = OutStatue[1].GetStoreSymbol();
        RightOutStatue = OutStatue[2].GetStoreSymbol();
        return new[] { LeftOutStatue, MidOutStatue, RightOutStatue };
    }

    public void SelectOutStatue(int symbol, StatueShowShape statue)
    {
        if (SelectedStatue != null)//have selected statue(green one)
        {
            // Debug.Log("Swapped symbols");
            statue.GetStoreSymbol().SwapSymbol(symbol, SelectedSymbol);
            SelectedStatue.GetStoreSymbol().SwapSymbol(SelectedSymbol, symbol);
            ResetSelectedStatue();
        }
        else{
            SelectedStatue = statue;
            SelectedSymbol = symbol;
            statue.selectedStatue = true;
            statue.UpdateStatueSelection();
        }
    }

    private void ResetSelectedStatue()
    {
        SelectedStatue.selectedStatue = false;
        SelectedStatue.UpdateStatueSelection();
        SelectedStatue = null;
        SelectedSymbol = -1;
    }

    public StatueShowShape GetSelectedStatue() => SelectedStatue;
}
