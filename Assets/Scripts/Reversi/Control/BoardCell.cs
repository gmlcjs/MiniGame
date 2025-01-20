using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public int x, y;
    public GameObject black;
    public GameObject white;
    private GameObject currentPiece;        //���� ĭ�� ���� �ִ��� Ȯ���ϴ� �뵵�� ����

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

    //��ȿ�� ������ Ȯ��
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
                // ���� ��ǥ(nx, ny)�� �ִ� ���� �����ɴϴ�.
                BoardCell nextCell = boardManager.GetCellAt(nx, ny);

                // ���� ���� ��� �ִ��� Ȯ���մϴ�.
                if (!nextCell.HasPiece()) break;

                // ���� ���� ������ ������ Ȯ���մϴ�.
                if (nextCell.IsBlack() != isBlackTurn)
                {
                    // ������ ���� ������ ǥ���մϴ�.
                    hasOpponent = true;
                    // ���� ���� �̵��մϴ�.
                    nx += dx;
                    ny += dy;
                }
                else
                {
                    // ������ ���� �ְ�, �� ���� ���� ���� �÷��̾��� ���̸� ��ȿ�� ���Դϴ�.
                    if (hasOpponent)
                    {
                        return true;
                    }
                    // ������ ���� ������ �ݺ��� �����մϴ�.
                    break;
                }    
            }
        }

        return false;
    }
}
