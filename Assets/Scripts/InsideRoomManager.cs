using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class InsideRoomManager : MonoBehaviour
{
    // Reference to the prefab
    public GameObject shadowPrefab;
    public Transform parentTransform;  // Parent transform for organizing instances
    public List<int> RoomShadow ;  // List containing symbols for each prefab
    public CardImageUpdate InsideCard;
    public StatueShowShape CorrectStatue;
    public List<GameObject> instantiatedShadows = new List<GameObject>();
    // Adjust this for how far apart you want the prefabs to be
    private float _shadowSpacing;
    private float _shadowOffset;

    void Start()
    {
        RoomShadow = new List<int>();
        _shadowSpacing = 25f;
        UpdatePrefabs();
    }

    // Call this method whenever RoomShadow is updated
    public void UpdatePrefabs()
    {
        // Clear existing instantiated prefabs
        ClearInstantiatedShadows();

        float startX = -(RoomShadow.Count - 1) * _shadowSpacing/2f;

        // create a new prefab for each symbol in RoomShadow
        for (int i = 0; i < RoomShadow.Count; i++)
        {
            int symbol = RoomShadow[i];
            // create the prefab
            GameObject shadowInstance = Instantiate(shadowPrefab, parentTransform);

            // Access the SymbolToCard script on the prefab instance
            SymbolToCard symbolToCardScript = shadowInstance.GetComponent<SymbolToCard>();
            symbolToCardScript.OutCard = InsideCard;
            symbolToCardScript.sym = ScriptableObject.CreateInstance<Symbol>();
            symbolToCardScript.sym.SetSymbol(symbol);

            Button button = shadowInstance.GetComponent<Button>();
            button.onClick.AddListener(symbolToCardScript.OnClick);

            // Set the image sprite
            shadowInstance.GetComponent<Image>().sprite = GetShapeSprite(symbol);

            // Set the position of the prefab (manually calculating the X position)
            RectTransform rectTransform = shadowInstance.GetComponent<RectTransform>();
            rectTransform.localScale *=0.6f;
            rectTransform.anchoredPosition = new Vector2(_shadowOffset + startX + i * _shadowSpacing, 30);  // Space out prefabs horizontally
            instantiatedShadows.Add(shadowInstance);
        }
    }

    private Sprite GetShapeSprite(int? value)
    {
        return value switch
        {
            0 => Resources.Load<Sprite>("Shadows/GreyCircle"),
            3 => Resources.Load<Sprite>("Shadows/GreyTriangle"),
            4 => Resources.Load<Sprite>("Shadows/GreySquare"),
            _ => Resources.Load<Sprite>("General/Blank"),
        };
    }

    // Function to clear instantiated objects
    private void ClearInstantiatedShadows()
    {
        // Destroy all previously instantiated objects
        foreach (GameObject shadow in instantiatedShadows)
        {
            Destroy(shadow);
        }
        instantiatedShadows.Clear();
    }
    public void AddStatueSymbol(List<int> randomOrder){
        int indexPos = 0;
        foreach(var statue in this.GetComponentsInChildren<StatueShowShape>()){
            // Debug.Log(statue + " " + randomOrder[indexPos]);
            statue.GetStoreSymbol().ClearSymbol();
            statue.GetStoreSymbol().Append(randomOrder[indexPos]);
            indexPos++;
        }
    }
    public void ClenseStatue(int symbol){
        foreach(var statue in this.GetComponentsInChildren<StatueShowShape>()){
            if(statue.GetStoreSymbol().GetFirstValue()== symbol)
                statue.Clensed(); 
        }
    }
    public bool IsRoomClensed(){
        foreach(var statue in this.GetComponentsInChildren<StatueShowShape>()){
            //check each staue is clensed?
            if(statue!= CorrectStatue && statue.IsSymbolClesned() == false){
                return false;
            }
        }
        
        return true;
    }

    public CardImageUpdate GetInsideCard(){
        return InsideCard;
    }
    public StoreSymbol GetInsideCardSymbol(){
        return  InsideCard.GetCard().curentShape;;
    }
    public bool IsTwoShadows(){
        if(RoomShadow.Count == 2)
            return true;
        return false;
    }
    public bool IsShadowHaveCorrectedStatueSymbol(){
        bool flag = false;
        foreach (int shadow in RoomShadow)
        {
            if(CorrectStatue.GetStoreSymbol().GetFirstValue() == shadow)
                flag = true;
        }
        return flag;
    }
    public void ClearInsideRoomRound(){
        if (RoomShadow != null)
        {
            RoomShadow.Clear();
        }
        else
        {
            Debug.LogError("RoomShadow is null.");
        }

        if (InsideCard != null && InsideCard.storeSymbol != null)
        {
            InsideCard.storeSymbol.ClearSymbol();
            InsideCard.UpdateShape();
        }
        else
        {
            // Debug.LogError("InsideCard or storeSymbol is null.");
        }

        foreach (var statue in this.GetComponentsInChildren<StatueShowShape>())
        {
            if (statue != null)
            {
                statue.GetComponent<Image>().sprite = Resources.Load<Sprite>("General/Statue");
                statue.gameObject.SetActive(true);
                statue.GetStoreSymbol().ClearSymbol();
                statue.clensed = false;
            }
            else
            {
                Debug.LogError("Statue is null.");
            }
        }
        this.UpdatePrefabs();
    }

}
