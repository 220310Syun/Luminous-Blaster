using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPool : MonoBehaviour
{
    public static MagicPool Instance;



    [SerializeField]
    private GameObject[] magics;

    private List<Magic> fireMagic = new List<Magic>();
    private List<Magic> iceMagic = new List<Magic>();
    private List<Magic> thunderMagic = new List<Magic>();


    private bool magicSpawned;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }




    public void Fire(int magicIndex,Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirecion)
    {
        magicSpawned = false;

        //ä÷êîÇåƒÇ‘
        TakeMagicPool(magicIndex, spawnPos,
        magicRotation, magicDirecion);
    }


    void TakeMagicPool(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirection)
    {
        if (magicIndex == 0)//fire
        {
            for (int i = 0; i < fireMagic.Count; i++)
            {
                if (!fireMagic[i].gameObject.activeInHierarchy)
                {
                    fireMagic[i].gameObject.SetActive(true);
                    fireMagic[i].gameObject.transform.position = spawnPos;
                    fireMagic[i].gameObject.transform.rotation = magicRotation;
                    fireMagic[i].MoveDirection(magicDirection);



                    magicSpawned = true;
                    break;

                }
            }
        }

        if (magicIndex == 1)//ice
        {
            for (int i = 0; i < iceMagic.Count; i++)
            {
                if (!iceMagic[i].gameObject.activeInHierarchy)
                {
                    iceMagic[i].gameObject.SetActive(true);
                    iceMagic[i].gameObject.transform.position = spawnPos;
                    iceMagic[i].gameObject.transform.rotation = magicRotation;
                    iceMagic[i].MoveDirection(magicDirection);



                    magicSpawned = true;
                    break;

                }
            }
        }

        if (magicIndex == 2)//thunder
        {
            for (int i = 0; i < thunderMagic.Count; i++)
            {
                if (!thunderMagic[i].gameObject.activeInHierarchy)
                {
                    thunderMagic[i].gameObject.SetActive(true);
                    thunderMagic[i].gameObject.transform.position = spawnPos;
                    thunderMagic[i].gameObject.transform.rotation = magicRotation;
                    thunderMagic[i].MoveDirection(magicDirection);



                    magicSpawned = true;
                    break;

                }
            }
        }



        if (!magicSpawned)
        {
            //ñÇñ@ä÷êîÇåƒÇ‘
            CreateNewMagic(magicIndex,spawnPos,magicRotation,magicDirection);
        }

    }

    void CreateNewMagic(int magicIndex, Vector3 spawnPos,
        Quaternion magicRotation, Vector2 magicDirection) 
    {
        GameObject newMagic = Instantiate(magics[magicIndex],
            spawnPos, magicRotation);

        newMagic.transform.SetParent(transform);
        newMagic.GetComponent<Magic>().MoveDirection(magicDirection);


        if (magicIndex == 0) 
        {
            fireMagic.Add(newMagic.GetComponent<Magic>());
        }
        if (magicIndex == 1)
        {
            iceMagic.Add(newMagic.GetComponent<Magic>());
        }
        if (magicIndex == 2) 
        {
            thunderMagic.Add(newMagic.GetComponent<Magic>());
        }
    }

}
