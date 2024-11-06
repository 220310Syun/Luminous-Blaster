using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSquareManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] magicSquares;
    private int currentMagicSauare;




    private void Start()
    {
        DeactivateAllMagicSquares();
    }

    void DeactivateAllMagicSquares()
    {
        for (int i = 0; i < magicSquares.Length; i++)
        {
            magicSquares[i].SetActive(false);
        }
    }


    public void ActivateMagicSquare(int newMagicSquare)
    {
        magicSquares[currentMagicSauare].SetActive(false);

        currentMagicSauare = newMagicSquare;

        magicSquares[currentMagicSauare].SetActive(true);

    }
}
