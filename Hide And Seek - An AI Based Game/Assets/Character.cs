using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject[] skins;
    public string[] skinNames;

    public void UpdateSkin(int index)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }

        skins[index].SetActive(true);
    }

}
