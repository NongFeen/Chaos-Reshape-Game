using UnityEngine;
using UnityEngine.UI;

public class CardImageUpdate : MonoBehaviour
{
    public Card c;
    public StoreSymbol storeSymbol;

    void Start(){
        c = ScriptableObject.CreateInstance<Card>();
        storeSymbol = c.curentShape;
    }

    public void AddSymbol(int s)
    {
        c.curentShape.Append(s);
        UpdateShape();
    }
    public void UpdateShape()
    {
        UnityEngine.UI.Image cardImage = GetComponent<UnityEngine.UI.Image>();
        if (c.GetFirstValue().HasValue && !c.GetSecondValue().HasValue)
        {
            cardImage.sprite = GetShapeSprite(c.GetFirstValue());
        }
        else if (c.IsFull())
        {
            cardImage.sprite = Get3DShapeSprite(c.GetFirstValue(), c.GetSecondValue());
        }
        else
        {
            cardImage.sprite = Resources.Load<Sprite>("CardImages/BlankCard");
        }
    }

    private Sprite GetShapeSprite(int? value)
    {
        return value switch
        {
            0 => Resources.Load<Sprite>("CardImages/CircleCard"),
            3 => Resources.Load<Sprite>("CardImages/TriangleCard"),
            4 => Resources.Load<Sprite>("CardImages/SquareCard"),
            _ => null,
        };
    }

    private Sprite Get3DShapeSprite(int? firstValue, int? secondValue)
    {
        int combinedValue = firstValue.Value + secondValue.Value;
        return combinedValue switch
        {
            0 => Resources.Load<Sprite>("CardImages/SphereCard"),
            3 => Resources.Load<Sprite>("CardImages/ConeCard"),
            4 => Resources.Load<Sprite>("CardImages/CylinderCard"),
            6 => Resources.Load<Sprite>("CardImages/PyramidCard"),
            7 => Resources.Load<Sprite>("CardImages/PrismCard"),
            8 => Resources.Load<Sprite>("CardImages/CubeCard"),
            _ => null,
        };
    }
    public Card GetCard(){
        return c;
    }
    public void SetCard(Card card){
        c = card;
    }
}
