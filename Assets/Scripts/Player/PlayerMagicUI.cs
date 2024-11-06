using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicUI : MonoBehaviour
{

    [SerializeField]
    private GameObject[] magicIcons;

    public void ChangeIcon(int magicIconIndex)
    {
        for (int i = 0; i < magicIcons.Length; i++)
        {
            magicIcons[i].SetActive(false);
        }

        magicIcons[magicIconIndex].SetActive(true);
    }

}
