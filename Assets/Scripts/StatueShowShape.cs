using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatueShowShape : MonoBehaviour
{
    private StoreSymbol _storeSymbol = new StoreSymbol();
    // public GameObject canvas;
    private GameObject shapeDisplay;
    public bool selectedStatue;
    public bool clensed;
    private int yOffsetPos = 190;
    void Start()
    {
        AddShapeDisplay();
        selectedStatue = false;
    }

    private void AddShapeDisplay()
    {
        shapeDisplay = new GameObject($"Statue_ShapeDisplay_{this.name}");
        RectTransform trans = shapeDisplay.AddComponent<RectTransform>();
        trans.SetParent(this.transform);
        Vector3 scale = this.GetComponent<RectTransform>().localScale;
        scale.x*=0.5f; 
        scale.y*=0.75f;
        scale.z*=0.5f;
        trans.localScale = scale;
        // trans.localScale = this.GetComponent<RectTransform>().localScale - new Vector3(0.5f, 0.3f, 0.5f);
        trans.anchoredPosition = Vector2.zero;
        trans.position = this.transform.position + new Vector3(0, yOffsetPos * scale.y, 0);
        shapeDisplay.AddComponent<Image>();
    }

    private void UpdateShapeDisplay()
    {
        Image shapeImage = shapeDisplay.GetComponent<Image>();
        if(clensed && this != this.GetComponentInParent<InsideRoomManager>().CorrectStatue){
            shapeDisplay.SetActive(false);
            return;
        }
        if (!_storeSymbol.GetSecondValue().HasValue ){//have 1 shpae and shadow is not clensed
            shapeImage.sprite = GetShapeSprite(_storeSymbol.GetFirstValue());
            shapeDisplay.SetActive(true);
        }
        else if(_storeSymbol.GetFirstValue().HasValue &&_storeSymbol.GetSecondValue().HasValue){// have 2 shape
            shapeImage.sprite = Get3DShapeSprite(_storeSymbol.GetFirstValue(), _storeSymbol.GetSecondValue());
            shapeDisplay.SetActive(true);
        }
    }

    private Sprite GetShapeSprite(int? value)
    {
        return value switch
        {
            0 => Resources.Load<Sprite>("CardImages/Circle"),
            3 => Resources.Load<Sprite>("CardImages/Triangle"),
            4 => Resources.Load<Sprite>("CardImages/Square"),
            _ => Resources.Load<Sprite>("CardImages/Blank"),
        };
    }

    private Sprite Get3DShapeSprite(int? firstValue, int? secondValue)
    {
        int combinedValue = firstValue.Value + secondValue.Value;
        return combinedValue switch
        {
            0 => Resources.Load<Sprite>("CardImages/Sphere"),
            3 => Resources.Load<Sprite>("CardImages/Cone"),
            4 => Resources.Load<Sprite>("CardImages/Cylinder"),
            6 => Resources.Load<Sprite>("CardImages/Pyramid"),
            7 => Resources.Load<Sprite>("CardImages/Prism"),
            8 => Resources.Load<Sprite>("CardImages/Cube"),
            _ => Resources.Load<Sprite>("CardImages/Blank"),
        };
    }

    public void UpdateStatueSelection()
    {
        string spritePath = selectedStatue ? "General/SelectedStatue" : "General/Statue";
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);
    }

    void Update()
    {
        UpdateShapeDisplay();
    }

    public StoreSymbol GetStoreSymbol()
    {
        return _storeSymbol;
    }
    public void Clensed(){
        this.clensed=true;
    }
    public bool IsSymbolClesned(){
        return this.clensed;
    }
}
