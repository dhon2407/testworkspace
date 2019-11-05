using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject block = null;

    private Block[,] blocks;
    private GridLayoutGroup layout;

    private int width;
    private int height;

    private List<(int, int)> adjacents;

    private void Awake()
    {
        adjacents = new List<(int, int)>();
        layout = GetComponent<GridLayoutGroup>();
        width = height = layout.constraintCount;
        blocks = new Block[width, height];

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

    public void GetChainingAdjacentSameColor(int startCol, int startRow, Block.Color color)
    {
        adjacents.Clear();
        AddToAdjacent(color, startRow, startCol);
        GetAdjacent(startCol, startRow, color);

        Debug.Log("Total adjacent count : " + adjacents.Count);
    }

    private void GetAdjacent(int startCol, int startRow, Block.Color color)
    {
        int nextRow;
        int nextCol;

        //Check top row +1
        nextRow = startRow + 1;
        nextCol = startCol;

        if (nextRow < height && blocks[nextCol, nextRow].CurrentColor == color)
            AddToAdjacent(color, nextRow, nextCol);

        //Check down row -1
        nextRow = startRow - 1;
        nextCol = startCol;

        if (nextRow >= 0 && blocks[startCol, nextRow].CurrentColor == color)
            AddToAdjacent(color, nextRow, nextCol);

        //Check right col +1
        nextRow = startRow;
        nextCol = startCol + 1;

        if (nextCol < width && blocks[nextCol, nextRow].CurrentColor == color)
            AddToAdjacent(color, nextRow, nextCol);

        //Check left col -1
        nextRow = startRow;
        nextCol = startCol - 1;

        if (nextCol >= 0 && blocks[nextCol, startRow].CurrentColor == color)
            AddToAdjacent(color, nextRow, nextCol);
    }

    private void AddToAdjacent(Block.Color color, int nextRow, int nextCol)
    {
        if (!adjacents.Contains((nextCol, nextRow)))
        {
            adjacents.Add((nextCol, nextRow));
            GetAdjacent(nextCol, nextRow, color);
        }
    }
}
