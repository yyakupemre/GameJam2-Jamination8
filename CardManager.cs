using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    public List<CardData> allCards;
    public Transform cardContainer;
    public GameObject cardPrefab;

    public void GenerateNewSet()
    {
        foreach (Transform child in cardContainer)
            Destroy(child.gameObject);

        var tempList = new List<CardData>(allCards);
        for (int i = 0; i < 3; i++)
        {
            if (tempList.Count == 0) break;

            int r = Random.Range(0, tempList.Count);
            var cardData = tempList[r];
            var cardGO = Instantiate(cardPrefab, cardContainer);
            cardGO.GetComponent<CardButton>().cardData = cardData;
            tempList.RemoveAt(r);
        }
    }

    void Start()
    {
        GenerateNewSet();
    }
}
