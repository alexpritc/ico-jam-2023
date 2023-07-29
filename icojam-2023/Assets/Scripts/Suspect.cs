using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Suspect : MonoBehaviour, IPointerEnterHandler
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Default"))
        {
            animator.Play("Select");
        }
    }
}
