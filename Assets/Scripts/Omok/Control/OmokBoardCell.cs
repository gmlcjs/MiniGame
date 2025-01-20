using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        // // BoardManager 인스턴스를 찾습니다.
        // OmokBodardManager boardManager = FindFirstObjectByType<OmokBodardManager>();
        
        // 현재 셀에 이미 돌이 있는지 확인합니다.
        if (HasPiece()) return false;

        // // 8방향을 나타내는 배열을 정의합니다.
        // int[,] directions =
        // {
        //     {-1, -1}, {-1, 0}, {-1, 1},
        //     {0, -1 },           {0, 1},
        //     {1, -1},  {1, 0 },  {1, 1},
        // };
        
        // // 방향의 개수를 가져옵니다.
        // int directionCount = directions.GetLength(0);
        // // 각 방향에 대해 반복합니다.
        // for (int i = 0; i < directionCount; i++)
        // {
        //     int dx = directions[i, 0];
        //     int dy = directions[i, 1];
        //     int nx = x + dx;
        //     int ny = y + dy;
        //     bool hasOpponent = false;

        //     bool whileBreak = true;
        // // 보드의 경계를 벗어나지 않는 동안 반복합니다.
        // while (whileBreak)
        //     {
        //         whileBreak = boardManager.GetCellAt(nx, ny) != null;
        //         OmokBoardCell nextCell = boardManager.GetCellAt(nx, ny);

        //         if(nextCell == null) {
        //             whileBreak = false;
        //             Debug.Log(nextCell+ "실행" + whileBreak);
        //             return true;
        //         }

        //         if (nextCell.IsBlack() == isBlackTurn){ // 다음셀이 자신 돌일때,
        //             nx += dx;
        //             ny += dy;
        //             return true;
        //         }else{
        //             bool bolVictory = true;
        //             int victory = 0; // 5일때 승리
        //             int nnx = nx;
        //             int nny = ny; 
        //             while (bolVictory)
        //             {
        //                 nny++; nnx++;
        //                 victory++;
        //                 OmokBoardCell vCell = boardManager.GetCellAt(nnx, nny);

        //                 if(victory == 5){
        //                     whileBreak = false;
        //                 }
        //                 bolVictory = nextCell.IsBlack() == isBlackTurn;
    
        //             }
        //         }
        //     }
        // }

        return true;
    }




}
