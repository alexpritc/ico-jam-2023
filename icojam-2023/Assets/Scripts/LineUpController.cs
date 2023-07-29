using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LineUpController : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;

    int leftIndex = 0;
    int middleIndex = 1;
    int rightIndex = 2;

    public List<GameObject> lefties;
    public List<GameObject> middleies;
    public List<GameObject> righties;

    int suspects;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAllColumns();
        suspects = righties.Count;
    }

    public void MoveRight()
    {
        if (rightIndex >= suspects - 1)
        {
            rightButton.interactable = false;
            return;
        }

        rightButton.interactable = true;
        leftButton.interactable = true;

        leftIndex++;
        middleIndex++;
        rightIndex++;

        UpdateAllColumns();

        if (rightIndex >= suspects - 1)
        {
            rightButton.interactable = false;
        }
    }

    public void MoveLeft()
    {
        if (leftIndex <= 0)
        {
            leftButton.interactable = false;
            return;
        }

        leftButton.interactable = true;
        rightButton.interactable = true;

        leftIndex--;
        middleIndex--;
        rightIndex--;

        UpdateAllColumns();

        if (leftIndex <= 0)
        {
            leftButton.interactable = false;
        }
    }

    void UpdateAllColumns()
    {
        SetColumn(lefties, leftIndex);
        SetColumn(middleies, middleIndex);
        SetColumn(righties, rightIndex);
    }
    private void SetColumn(List<GameObject> column, int index)
    {
        // hide all sprites
        foreach (var go in column)
        {
            go.SetActive(false);
        }

        if (index < column.Count)
            column[index].SetActive(true);
    }

    void RemoveFromAllColumns(int i)
    {
        suspects--;

        Debug.Log(lefties[i].name);

        lefties[i].SetActive(false);
        middleies[i].SetActive(false);
        righties[i].SetActive(false);  

        lefties.RemoveAt(i);
        middleies.RemoveAt(i);
        righties.RemoveAt(i);

        UpdateAllColumns();

        if (suspects <= 3)
        {
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
    }

    public void RemoveRandom(int index)
    {
        int i = (int)Random.Range(0f, suspects);
        Debug.Log(i);
        RemoveFromAllColumns(i);
    }
}
