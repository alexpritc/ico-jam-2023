using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LineUpController : MonoBehaviour
{
    public Image posLeft;
    public Image posMiddle;
    public Image posRight;

    /// <summary>
    /// 11 sprites
    /// </summary>
    public Sprite[] sprites;

    int leftSprite = 0;
    int middleSprite = 1;
    int rightSprite = 2;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSprites();
    }

    void UpdateSprites()
    {
        posLeft.sprite = sprites[leftSprite];
        posMiddle.sprite = sprites[middleSprite];
        posRight.sprite = sprites[rightSprite];
    }

    public void MoveRight()
    {
        if (rightSprite == sprites.Length-1)
            return;

        leftSprite++;
        middleSprite++;
        rightSprite++;

        UpdateSprites();
    }

    public void MoveLeft()
    {
        if (leftSprite <= 0)
            return;

        leftSprite--;
        middleSprite--;
        rightSprite--;

        UpdateSprites();
    }
}
