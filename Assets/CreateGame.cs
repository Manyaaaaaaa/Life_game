using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGame : MonoBehaviour {

    [Header("Setting")]
    public int sizeX;
    public int sizeY;

    [Header("Prefab")]
    public GameObject prefab;

    private Block[,] Block;

    private int[,] Numbers;
    private bool[,] Life;

    private int[,] Move = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }};


    public void SetSetting() {
        DeletAll();

        Block = new Block[sizeY, sizeX];
        Numbers = new int[sizeY, sizeX];
        Life = new bool[sizeY, sizeX];

        CreateArena();
    }


    public void CreateArena()
    {
        Vector3 vec = Vector3.zero;

        for(int i = 0; i < sizeY; i++) {

            for(int j = 0; j < sizeX; j++) {

                Block[i,j] = Instantiate(prefab, vec, Quaternion.identity).GetComponent<Block>();
                Block[i, j].CG = this;
                Block[i, j].idX = j;
                Block[i, j].idY = i;

                Life[i, j] = false;
                Numbers[i, j] = 0;
                vec.x++;
            }
            vec.y--;
            vec.x = 0;
        }
    }

    public void BlockDark(int idX, int idY) {
        Life[idY, idX] = true;
    }

    public void StartLife()
    {
        CreateAura();
        NewGeneration();
    }
    
    public void DeletAll() {
        if (Block != null && Block.GetLength(0) > 0 ) {

            for (int i = 0; i < Block.GetLength(0); i++) {
                for (int j = 0; j < Block.GetLength(1); j++) {
                    Destroy(Block[i, j].gameObject);
                }
            }
        }
    }

    private void NewGeneration() {

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                switch(Numbers[i, j])
                {
                    case 1:
                        Life[i, j] = false;
                        Block[i, j].ColorId(0);
                        break;
                    case 2:
                        if (Life[i, j]) {
                            Block[i, j].ColorId(3);
                            Life[i, j] = true; 
                        }
                        else Life[i, j] = false;
                        break;
                    case 3:
                        Life[i, j] = true;
                        Block[i, j].ColorId(3);
                        break;
                    case 4:
                        Life[i, j] = false;
                        Block[i, j].ColorId(0);
                        break;
                }       
            }
        }

    }

    private void CreateAura()
    {

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++){ 
                Block[i, j].ColorId(0);
                Numbers[i, j] = 0;
            }
        }

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (Life[i,j])
                {
                    for(int k = 0; k < 8; k++)
                    {
                        int Y = i + Move[k, 0];
                        int X = j + Move[k, 1];

                        if (Y < 0 || X < 0 || Y >= sizeY || X >= sizeX) continue;

                        Numbers[Y, X]++;
                    }
                }
            }
        }

    }

}
