using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject[] color;
    public CreateGame CG;

    public int idX = 0;
    public int idY = 0;


    public void OnMouseDown()
    {
        CG.BlockDark(idX, idY);
        ColorId(color.Length - 1);
    }


    public void ColorId(int id)
    {
        for (int i = 0; i < color.Length; i++) color[i].SetActive(false);
        color[id].SetActive(true);
    }
}
