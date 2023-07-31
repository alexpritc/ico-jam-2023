using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Sprite defaultSprite;
    public Sprite hoverSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultSprite;
    }
}
