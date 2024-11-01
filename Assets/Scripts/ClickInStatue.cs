using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickInStatue : MonoBehaviour
{
    // Start is called before the first frame update

    public StatueShowShape LeftStatue;
    public StatueShowShape MidStatue;
    public StatueShowShape RightStatue;
    public InsideRoomManager LeftRoomManager;
    public InsideRoomManager MidRoomManager;
    public InsideRoomManager RightRoomManager;
    private StatueShowShape _CorrectStatue;
    public CardImageUpdate InsideCard;
    public StoreSymbol _cardCurrentSymbol;
    public int cardFirst;

    public void Start(){
        _CorrectStatue = this.GetComponentInParent<InsideRoomManager>().CorrectStatue;
    }
    // Update is called once per frame
    public void OnClick()
    {
        PlayerPrefs.SetInt("MoveCount",PlayerPrefs.GetInt("MoveCount")+1);
        _cardCurrentSymbol = InsideCard.c.curentShape;//some how can not init on Start
        try{
            cardFirst = InsideCard.c.curentShape.GetFirstValue().Value;
        }
        catch (System.Exception){
            // Debug.Log("Card have no symbol");
            return;
        }
        if (IsCorrectStatue())
        {
            // Debug.Log("Cannot Send Symbol");
            return;
        }
        if (IsStatue(LeftStatue))
        {
            SendSymbolToRoom(LeftRoomManager);
        }
        else if (IsStatue(MidStatue))
        {
            SendSymbolToRoom(MidRoomManager);
        }
        else if (IsStatue(RightStatue))
        {
            SendSymbolToRoom(RightRoomManager);
        }
        InsideCard.UpdateShape();
    }

    private bool IsCorrectStatue()
    {
        return this.GetComponentInParent<StatueShowShape>() == _CorrectStatue;
    }

    private bool IsStatue(StatueShowShape statue)
    {
        return this.GetComponentInParent<StatueShowShape>() == statue;
    }

    private void SendSymbolToRoom(InsideRoomManager roomManager)
    {
        // Debug.Log($"Send symbol to {this}: " + _cardCurrentSymbol);
        if (_cardCurrentSymbol.IsFull())
        {
            _cardCurrentSymbol.Clear();
        }
        else if (_cardCurrentSymbol.GetFirstValue().HasValue)
        {
            roomManager.RoomShadow.Add(_cardCurrentSymbol.GetFirstValue().Value);
            this.GetComponentInParent<InsideRoomManager>().RoomShadow.Remove(_cardCurrentSymbol.GetFirstValue().Value);
            roomManager.ClenseStatue(_cardCurrentSymbol.GetFirstValue().Value);
            UpdateAllRoom();
            _cardCurrentSymbol.Clear();
        }
    }

    private void UpdateAllRoom(){
        MidRoomManager.UpdatePrefabs();
        LeftRoomManager.UpdatePrefabs();
        RightRoomManager.UpdatePrefabs();
    }

    
}