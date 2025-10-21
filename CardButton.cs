using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButton : MonoBehaviour
{
    [Header("Card Data")]
    public CardData cardData;

    [Header("UI Elements")]
    public TMP_Text nameText;
    public Image cardImage;

    [Header("Panels (Horizontal Layout)")]
    public GameObject firePanel;
    public GameObject waterPanel;
    public GameObject airPanel;
    public GameObject earthPanel;

    [Header("Icons & Texts")]
    public Image fireIcon, waterIcon, airIcon, earthIcon;
    public TMP_Text fireText, waterText, airText, earthText;

    [Header("Element Sprites")]
    public Sprite fireIconSprite;
    public Sprite waterIconSprite;
    public Sprite airIconSprite;
    public Sprite earthIconSprite;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnSelect);
    }

    void Start()
    {
        SetupCardUI();
    }

    void SetupCardUI()
    {
        if (cardData == null) return;

        nameText.text = cardData.cardName;

        if (cardImage != null && cardData.icon != null)
            cardImage.sprite = cardData.icon;

        UpdateCardValues(GameManager.Instance.turn);
    }

    public void UpdateCardValues(int turn)
    {
        SetPanel(firePanel, fireText, fireIcon, fireIconSprite, cardData.fireEffect, turn);
        SetPanel(waterPanel, waterText, waterIcon, waterIconSprite, cardData.waterEffect, turn);
        SetPanel(airPanel, airText, airIcon, airIconSprite, cardData.airEffect, turn);
        SetPanel(earthPanel, earthText, earthIcon, earthIconSprite, cardData.earthEffect, turn);
    }

    void SetPanel(GameObject panel, TMP_Text text, Image icon, Sprite sprite, float baseValue, int turn)
    {
        if (baseValue != 0)
        {
            panel.SetActive(true);
            float visualValue = baseValue * turn;
            text.text = FormatEffect(visualValue);
            icon.sprite = sprite;
        }
        else
        {
            panel.SetActive(false);
        }
    }

    string FormatEffect(float value)
    {
        if (value > 0) return "+" + value;
        else if (value < 0) return value.ToString();
        else return "0";
    }

    public void OnSelect()
    {
        GameManager.Instance.ApplyCardEffect(
            cardData.fireEffect,
            cardData.waterEffect,
            cardData.airEffect,
            cardData.earthEffect
        );

        FindObjectOfType<CardManager>().GenerateNewSet();
    }
}
