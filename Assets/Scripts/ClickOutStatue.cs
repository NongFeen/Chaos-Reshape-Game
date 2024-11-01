using UnityEngine;

public class ClickOutStatue : MonoBehaviour
{
    public CardImageUpdate outCard;
    private StoreSymbol _cardCurrentSymbol;
    public OutsideRoomManager OutsideRoomManager;
    public StatueShowShape statueShowShape;

    void Start()
    {
        
    }

    public void OnClick()
    {
        PlayerPrefs.SetInt("MoveCount",PlayerPrefs.GetInt("MoveCount")+1);
        _cardCurrentSymbol = outCard.c.curentShape;
        if (_cardCurrentSymbol.IsFull())//if card have 3D it clear itself
        {
            _cardCurrentSymbol.Clear();
            _cardCurrentSymbol.DisplayCurrentSymbol();
        }
        else if (_cardCurrentSymbol.GetFirstValue().HasValue)
        {
            if (IsMatchingSymbol() && !IsSameStatue())
            {
                OutsideRoomManager.SelectOutStatue(_cardCurrentSymbol.GetFirstValue().Value, statueShowShape);
                _cardCurrentSymbol.Clear();
            }
            else
            {
                // Debug.Log(IsSameStatue() ? "Same Selected Statue" : "Found no symbol");
            }
        }

        outCard.UpdateShape();
    }

    private bool IsMatchingSymbol()
    {
        int? cardSymbol = _cardCurrentSymbol.GetFirstValue();
        return cardSymbol == statueShowShape.GetStoreSymbol().GetFirstValue() ||
               cardSymbol == statueShowShape.GetStoreSymbol().GetSecondValue();
    }

    private bool IsSameStatue()
    {
        return OutsideRoomManager.GetSelectedStatue() == statueShowShape;
    }
}
