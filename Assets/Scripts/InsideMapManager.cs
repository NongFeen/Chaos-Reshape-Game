using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsideMapManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InsideRoomManager LeftRoom;
    public InsideRoomManager MidRoom;
    public InsideRoomManager RightRoom;
    
    public InsideRoomManager[] GetInsideRoom(){
        // Debug.Log(" " +LeftRoom + " " + MidRoom + " " + RightRoom);
        return new [] {LeftRoom, MidRoom, RightRoom};
    }
    public StoreSymbol[] GetAllInsideCardSymbol(){
        StoreSymbol[] cardSymbol = new StoreSymbol[3];
        for(int i=0;i<3;i++){
            cardSymbol[i] = GetInsideRoom()[i].GetInsideCardSymbol();
        }
        return cardSymbol;
    }
}
