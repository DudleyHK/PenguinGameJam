using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetAnimationEvents : MonoBehaviour 
{
    public SpriteRenderer sprRenderer;
    public Sprite close;
    public Sprite midClose;
    public Sprite midOpen;
    public Sprite open;

    public void ChangeSprite(int state)
    {
        switch(state)
        {
            case 0:
                sprRenderer.sprite = close;
                break;
            case 1:
                sprRenderer.sprite = midClose;
                break;
            case 2:
                sprRenderer.sprite = midOpen;
                break;
            case 3:
                sprRenderer.sprite = open;
                break;
            default:
                break;
        }
    }
}
