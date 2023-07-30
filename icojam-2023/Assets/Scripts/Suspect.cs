using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Suspect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int lineNumber;
    public int currentPosition;

    private Animator animator;

    public Sprite defaultSprite;
    public Sprite selectedSprite;

    private Image img;

    public bool isSeven = false;

    private void Awake()
    {
        if (!isSeven)
        {
            animator = GetComponent<Animator>();
        }
        img = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.SELECTION)
        {
            if (!isSeven)
            {
                animator.SetBool("IsSelected", true);
                img.sprite = selectedSprite;
                AudioManager.instance.Play("Click");
            }
            else
            {
                if (GameManager.Instance.finalThree)
                {
                    img.sprite = selectedSprite;
                    AudioManager.instance.Play("Click");
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = defaultSprite;
        if (!isSeven)
        {
            animator.SetBool("IsSelected", false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.SELECTION)
        {
            // show pop up
            GameManager.Instance.ShowPopUp(lineNumber);
            AudioManager.instance.Play("Click");
        }

    }
}
