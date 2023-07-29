using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Suspect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int lineNumber;
    public int currentPosition;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("IsSelected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("IsSelected", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.SELECTION)
        {
            // do something
        }

    }
}
