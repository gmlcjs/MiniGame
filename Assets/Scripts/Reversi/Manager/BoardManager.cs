using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject boardCell; // 보드 셀 프리팹
    private BoardCell[,] boardCells = new BoardCell[8, 8]; // 8x8 보드 셀 배열
    [SerializeField] Transform cellSpawn;  // 셀 스폰 위치

    void Start() // 8 x 8 규격의 보드판 생성
    {
        float startX = -3.5f;       //보드 시작 x좌표
        float startY = -3.5f;       //보드 시작 y좌표

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
        
        // 초기 피스 배치
        boardCells[3, 3].PlacePiece(true); // 흰색 피스 배치
        boardCells[4, 4].PlacePiece(true); // 흰색 피스 배치
        boardCells[3, 4].PlacePiece(false); // 검은색 피스 배치
        boardCells[4, 3].PlacePiece(false); // 검은색 피스 배치
    }

    public BoardCell GetCellAt(int x, int y)
    {
        // 좌표가 보드의 유효 범위를 벗어나는지 확인
        if (x < 0 || x >= 8 || y < 0 || y >= 8)
        {
            return null; // 유효하지 않은 좌표일 경우 null 반환
        }

        // 유효한 좌표일 경우 해당 좌표의 보드 셀 반환
        return boardCells[x, y];
    }

    //특정 플레이어가 둘 수 있는 유효한 수가 있는지 확인
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
