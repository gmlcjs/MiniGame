using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OmokGameManager : MonoBehaviour
{
    [SerializeField] OmokBodardManager omokboardManager; // 매니저

    [SerializeField] GameObject winUI; // 승리 UI
    [SerializeField] TextMeshProUGUI passText;  // 차례
    
    [SerializeField] TextMeshProUGUI winText; // 승리

    bool isBlackTurn = true; // 순서
    bool isEnded = false; // 게임 진행여부
    int victory = 1; // 5개가 되면 승리
    int whiteCount = 0;
    int blackCount = 0;

    void Update()
    {
        if (isEnded) return; // 게임 끝났을때 멈춤

        if (CheckGameOver())
        {
            EndGame();
        }
    }

    bool CheckGameOver() // 클릭시 실행?
    {
        //양쪽 모두 유효한 수가 없는지 확인
        bool blackHasMove = omokboardManager.HasValidMoves(true);
        bool whiteHasMove = omokboardManager.HasValidMoves(false);

        if (!blackHasMove && !whiteHasMove)
        {
            return true;
        }

        return false;
    }

    void EndGame()
    {
        if (isBlackTurn)
        {
            winText.text = "검정 승리";
            winText.GetComponent<TextMeshProUGUI>().color = Color.black;
            Debug.Log("검정승리");
        }
        else
        {
            winText.text = "흰색 승리";
            winText.GetComponent<TextMeshProUGUI>().color = Color.white;
            Debug.Log("흰색승리");
        }
        // else
        // {
        //     winText.text = "Draw";
        // }
        winUI.SetActive(true);
        isEnded = true; // 게임종료
    }
    

    public void OnCellClicked(OmokBoardCell cell)
    {  
        //이미 셀에 돌이 생성된 경우
        if (cell.HasPiece())
        {
            return;
        }

        //유효한 위치에서 생성하는지 확인
        if (cell.IsValidMove(isBlackTurn))
        {
            cell.PlacePiece(isBlackTurn);               //돌 배치
            FlipPieces(cell.x, cell.y, isBlackTurn);    //돌 뒤집어두기
            GetScore(isBlackTurn); // 결산
            SwitchTurn();                               //턴 전환
        }
    }

    public void GetScore(bool isBlackTurn)
    {
        if(victory == 5){ // 5목일때 승리
            EndGame();
        }else{
            victory = 1;
        }
    }
     void FlipPieces(int x, int y, bool isBlack) // 돌 뒤집기 
    {
        int[,] directions =
        {
            {-1, -1}, {-1, 0 }, {-1, 1},
            {0, -1},            {0, 1},
            {1, -1 }, {1, 0}, {1, 1}
        };

        int directionCount = directions.GetLength(0);

        for (int i = 0; i < directionCount; i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];

            if (CanFlipInDirection(x, y, dx, dy, isBlack))
            {
                FlipInDirection(x, y, dx, dy, isBlack);
            }
        }
    }

     // 특정 방향으로 돌을 뒤집을 수 있는지 확인
    bool CanFlipInDirection(int x, int y, int dx, int dy, bool isBlack)
    {
        int nx = x + dx;
        int ny = y + dy;
        int max = 1;
        bool hasOpponent = false;
        while(omokboardManager.GetCellAt(nx, ny) != null)
        {
            OmokBoardCell nextCell = omokboardManager.GetCellAt(nx, ny);

            if (!nextCell.HasPiece())
            {
                return false;
            }

            if (nextCell.IsBlack() == isBlack)
            {
                hasOpponent = true;
                nx += dx;
                ny += dy;    
                max++;
                victory = max;
            }
            else
            {
                return hasOpponent;
            }
        }
        return false;
    }

     public void SwitchTurn() // 턴 전환
    {
        isBlackTurn = !isBlackTurn;
        if (!omokboardManager.HasValidMoves(isBlackTurn) && omokboardManager.HasValidMoves(!isBlackTurn))
        {
            isBlackTurn = !isBlackTurn;
        }
        
        if(isBlackTurn){
            // 검정 차례
            passText.GetComponent<TextMeshProUGUI>().text = "검정 차례";
            passText.GetComponent<TextMeshProUGUI>().color = Color.black;
        }else{
            // 흰색 차례
            passText.GetComponent<TextMeshProUGUI>().text = "흰색 차례";
            passText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }

    }

    void FlipInDirection(int x, int y, int dx, int dy, bool isBlack)
    {
        int nx = x + dx;
        int ny = y + dy;
        // 지정된 방향으로 이동하면서 돌을 뒤집음
        while (omokboardManager.GetCellAt(nx, ny) != null)
        {
            OmokBoardCell nextCell = omokboardManager.GetCellAt(nx, ny);
            // 돌이 없거나 같은 색상의 돌을 만나면 중단
            if (!nextCell.HasPiece() || nextCell.IsBlack() != isBlack) break;
            // 돌을 뒤집음
            // nextCell.PlacePiece(isBlack);
            nx += dx;
            ny += dy;
        }
    }
        
}
