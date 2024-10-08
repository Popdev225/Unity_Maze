using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite defaultSprite;       // Default button sprite
    public Sprite hoverSprite;         // Hovered button sprite
    public Vector2 defaultSize;        // Default size of the button
    public Vector2 hoverSize;          // Size of the button when hovered

    private Image buttonImage;          // Reference to the button's Image component
    private RectTransform rectTransform; // Reference to the button's RectTransform

    void Start()
    {
        buttonImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        // Set the button to its default sprite and size initially
        buttonImage.sprite = defaultSprite;
        rectTransform.sizeDelta = defaultSize;
    }

    // Method called when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite;  // Change to hover sprite
        rectTransform.sizeDelta = hoverSize; // Change to hover size
    }

    // Method called when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite; // Revert to default sprite
        rectTransform.sizeDelta = defaultSize; // Revert to default size
    }
}
