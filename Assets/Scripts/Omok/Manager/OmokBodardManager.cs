using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmokBodardManager : MonoBehaviour
{

    public GameObject boardCell; // 보드 셀 프리팹
    private OmokBoardCell[,] boardCells = new OmokBoardCell[15, 15]; // 15x15 보드 셀 배열
    [SerializeField] GameObject cellSpawn;  // 셀 스폰 위치
    private int bodarSide = 15;
    void Start()
    {

        float startX = -7f;       //보드 시작 x좌표
        float startY = -7f;       //보드 시작 y좌표

         for (int x = 0; x <  bodarSide; x++)
        {
            for (int y = 0; y < bodarSide; y++)
            {
                Vector3 position = new Vector3(startX + x, startY + y, 0);
                GameObject cellObj = Instantiate(boardCell, position, Quaternion.identity);
                cellObj.transform.SetParent(cellSpawn.transform, true);
                OmokBoardCell cell = cellObj.GetComponent<OmokBoardCell>();

                cell.x = x;
                cell.y = y;

                boardCells[x, y] = cell;
            }
        }
        
    }
    
     public OmokBoardCell GetCellAt(int x, int y)
    {
        // 좌표가 보드의 유효 범위를 벗어나는지 확인
        if (x < 0 || x >= bodarSide || y < 0 || y >= bodarSide)
        {
            return null; // 유효하지 않은 좌표일 경우 null 반환
        }

        // 유효한 좌표일 경우 해당 좌표의 보드 셀 반환
        return boardCells[x, y];
    }


    //특정 플레이어가 둘 수 있는 유효한 수가 있는지 확인
    public bool HasValidMoves(bool isBlack)
    {
        for (int x = 0; x < bodarSide; x++)
        {
            for (int y = 0; y < bodarSide; y++)
            {
                OmokBoardCell cell = boardCells[x, y];
                if (!cell.HasPiece() && cell.IsValidMove(isBlack))
                {
                    return true;
                }
            }
        }
        return false;
    }

}
