using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmokBoardCell : MonoBehaviour
{
     public int x, y;
    public GameObject black;
    public GameObject white;
    private GameObject currentPiece;        //현재 칸에 돌이 있는지 확인하는 용도의 변수

    public void PlacePiece(bool isBlack) // 돌 배치
    {
        if (currentPiece != null) Destroy(currentPiece);
        currentPiece = Instantiate(isBlack ? black : white, transform.position, Quaternion.identity, transform);
    }

    public bool HasPiece()
    {
        return currentPiece != null;
    }

    public bool IsBlack()
    {
        if (currentPiece == null) return false;
        return currentPiece.GetComponent<SpriteRenderer>().color == Color.black;
    }

    public bool IsWhite()
    {
        if (currentPiece == null) return false;
        return currentPiece.GetComponent<SpriteRenderer>().color == Color.white;
    }

    //유효한 수인지 확인
    public bool IsValidMove(bool isBlackTurn)
    {
        BoardManager boardManager = FindFirstObjectByType<BoardManager>();
        if (HasPiece()) return false;

        int[,] directions =
        {
            {-1, -1 }, {-1, 0}, {-1, 1},
            {0, -1 },           {0, 1 },
            {1, -1}, {1, 0 }, {1, 1},
        };

        int directionCount = directions.GetLength(0);

        for (int i = 0; i < directionCount; i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];

            int nx = x + dx;
            int ny = y + dy;

            bool hasOpponent = false;

            while (boardManager.GetCellAt(nx, ny) != null)
            {
                BoardCell nextCell = boardManager.GetCellAt(nx, ny);

                if (!nextCell.HasPiece()) break;

                if (nextCell.IsBlack() != isBlackTurn)
                {
                    hasOpponent = true;
                    nx += dx;
                    ny += dy;
                }
                else
                {
                    if (hasOpponent)
                    {
                        return true;
                    }
                    break;
                }    
            }
        }

        return false;
    }
}
