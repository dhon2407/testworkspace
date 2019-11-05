using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject block = null;

    private Block[,] blocks;
    private GridLayoutGroup layout;

    private void Awake()
    {
        layout = GetComponent<GridLayoutGroup>();
        blocks = new Block[layout.constraintCount, layout.constraintCount];

        for (int row = 0; row < layout.constraintCount; row++)
        {
            for (int col = 0; col < layout.constraintCount; col++)
            {
                blocks[col, row] = Instantiate(block, transform).GetComponent<Block>();
                blocks[col, row].SetPosition(col, row);
                blocks[col, row].onTap.AddListener(GetChainingAdjacentSameColor);
            }
        }
    }

    public void GetChainingAdjacentSameColor(int startCol, int startRow)
    {
        Debug.Log("Tap at " + startCol + " : " + startRow);
    }
}
