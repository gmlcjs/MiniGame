using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public int x, y;
    public GameObject black;
    public GameObject white;
    private GameObject currentPiece;        //현재 칸에 돌이 있는지 확인하는 용도의 변수

    public void PlacePiece(bool isBlack)
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
            {-1, -1}, {-1, 0}, {-1, 1},
            {0, -1 },          {0, 1 },
            {1, -1 }, {1, 0 }, {1, 1 },
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
                // 현재 좌표(nx, ny)에 있는 셀을 가져옵니다.
                BoardCell nextCell = boardManager.GetCellAt(nx, ny);

                // 다음 셀이 비어 있는지 확인합니다.
                if (!nextCell.HasPiece()) break;

                // 다음 셀이 상대방의 돌인지 확인합니다.
                if (nextCell.IsBlack() != isBlackTurn)
                {
                    // 상대방의 돌이 있음을 표시합니다.
                    hasOpponent = true;
                    // 다음 셀로 이동합니다.
                    nx += dx;
                    ny += dy;
                }
                else
                {
                    // 상대방의 돌이 있고, 그 다음 셀이 현재 플레이어의 돌이면 유효한 수입니다.
                    if (hasOpponent)
                    {
                        return true;
                    }
                    // 상대방의 돌이 없으면 반복을 종료합니다.
                    break;
                }    
            }
        }

        return false;
    }
}
