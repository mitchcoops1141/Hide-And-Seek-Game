using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    public int buttonNumber;
    public Color locked;
    Image img;
    private void Start()
    {
        img = GetComponentInChildren<Image>();
        //if unlocked
        if (GameManager.instance.charUnlocks[buttonNumber])
        {
            img.color = new Color(1,1,1,1);
        }
        //if not unlocked
        else
        {
            img.color = locked;
        }

    }
    public void HoverEnter()
    {
        transform.localScale += new Vector3(0.1f, 0.1f);
    }

    public void HoverExit()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f);
    }
}
