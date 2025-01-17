using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmokBodardManager : MonoBehaviour
{

    public GameObject boardCell; // 보드 셀 프리팹
    private OmokBoardCell[,] boardCells = new OmokBoardCell[15, 15]; // 15x15 보드 셀 배열
    [SerializeField] GameObject cellSpawn;  // 셀 스폰 위치
    public int bodarSide = 15;
    void Start()
    {
        float startX = -7f;       //보드 시작 x좌표
        float startY = -7f;       //보드 시작 y좌표

         for (int x = 0; x <  bodarSide; x++)
        {
            for (int y = 0; y < bodarSide; y++)
            {
                // 셀의 위치를 설정합니다.
                Vector3 position = new Vector3(startX + x, startY + y, 0);
                // 셀 오브젝트를 인스턴스화합니다.
                GameObject cellObj = Instantiate(boardCell, position, Quaternion.identity);
                // 셀 오브젝트를 cellSpawn의 자식으로 설정합니다.
                cellObj.transform.SetParent(cellSpawn.transform, true);
                // 셀 오브젝트에서 OmokBoardCell 컴포넌트를 가져옵니다.
                OmokBoardCell cell = cellObj.GetComponent<OmokBoardCell>();
                // 셀의 x, y 좌표를 설정합니다.
                cell.x = x;
                cell.y = y;

                // boardCells 배열에 셀을 저장합니다.
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
