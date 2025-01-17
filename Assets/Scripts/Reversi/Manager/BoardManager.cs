using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject boardCell; // ���� �� ������
    private BoardCell[,] boardCells = new BoardCell[8, 8]; // 8x8 ���� �� �迭
    [SerializeField] Transform cellSpawn;  // �� ���� ��ġ

    void Start() // 8 x 8 �԰��� ������ ����
    {
        float startX = -3.5f;       //���� ���� x��ǥ
        float startY = -3.5f;       //���� ���� y��ǥ

        for (int x = 0; x <  8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Vector3 position = new Vector3(startX + x, startY + y, 0);
                GameObject cellObj = Instantiate(boardCell, position, Quaternion.identity);
                cellObj.transform.SetParent(cellSpawn, true);
                BoardCell cell = cellObj.GetComponent<BoardCell>();

                cell.x = x;
                cell.y = y;

                boardCells[x, y] = cell;
            }
        }
        
        // �ʱ� �ǽ� ��ġ
        boardCells[3, 3].PlacePiece(true); // ��� �ǽ� ��ġ
        boardCells[4, 4].PlacePiece(true); // ��� �ǽ� ��ġ
        boardCells[3, 4].PlacePiece(false); // ������ �ǽ� ��ġ
        boardCells[4, 3].PlacePiece(false); // ������ �ǽ� ��ġ
    }

    public BoardCell GetCellAt(int x, int y)
    {
        // ��ǥ�� ������ ��ȿ ������ ������� Ȯ��
        if (x < 0 || x >= 8 || y < 0 || y >= 8)
        {
            return null; // ��ȿ���� ���� ��ǥ�� ��� null ��ȯ
        }

        // ��ȿ�� ��ǥ�� ��� �ش� ��ǥ�� ���� �� ��ȯ
        return boardCells[x, y];
    }

    //Ư�� �÷��̾ �� �� �ִ� ��ȿ�� ���� �ִ��� Ȯ��
    public bool HasValidMoves(bool isBlack)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                BoardCell cell = boardCells[x, y];
                if (!cell.HasPiece() && cell.IsValidMove(isBlack))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
