using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardManager boardManager;
    [SerializeField] GameObject MenuButton; // 메뉴 버튼

    [SerializeField] GameObject winUI;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI blackText;
    [SerializeField] TextMeshProUGUI whiteText;
    [SerializeField] GameObject passUI; // 턴이 넘어갈 때 나타나는 UI

    [SerializeField] TextMeshProUGUI passText;
  

    bool isBlackTurn = true;
    bool isEnded = false;
    int whiteCount = 0;
    int blackCount = 0;

    private void Update()
    {
        if (isEnded) return;

        if (CheckGameOver())
        {
            EndGame();
        }
    }

    bool CheckGameOver()
    {
        //양쪽 모두 유효한 수가 없는지 확인
        bool blackHasMove = boardManager.HasValidMoves(true);
        bool whiteHasMove = boardManager.HasValidMoves(false);

        if (!blackHasMove && !whiteHasMove)
        {
            return true;
        }

        return false;
    }

    void EndGame()
    {
        isEnded = true;
        GetScore();
        passUI.SetActive(false);

        if (blackCount > whiteCount)
        {
            winText.text = "검정 승리";
            passUI.GetComponent<TextMeshProUGUI>().color = Color.black;
        }
        else if (blackCount < whiteCount)
        {
            winText.text = "흰색 승리";
            passUI.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            winText.text = "Draw";
        }
        blackText.text = blackCount.ToString("00");
        whiteText.text = whiteCount.ToString("00");
        winUI.SetActive(true);
    }

    //결산
    public void GetScore()
    {
        blackCount = 0;
        whiteCount = 0;

        var allPiece = FindObjectsOfType<BoardCell>();
        foreach (var piece in allPiece)
        {
            if (piece.IsBlack())
            {
                blackCount++;
            }
            else if (piece.IsWhite())
            {
                whiteCount++;
            }
        }
    }

    public void OnCellClicked(BoardCell cell)
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
            GetScore();
            
            SwitchTurn();                               //턴 전환

            blackText.text = blackCount.ToString("00"); //점수 갱신
            whiteText.text = whiteCount.ToString("00"); // 점수 갱신
        }
    }

    public void SwitchTurn() // 턴 전환
    {
        isBlackTurn = !isBlackTurn;
        if (!boardManager.HasValidMoves(isBlackTurn) && boardManager.HasValidMoves(!isBlackTurn))
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

        passUI.SetActive(true);
    }

    bool CanPlacePiece(int x, int y, bool isBlack) // 돌을 놓을 수 있는지 확인
    {
        int[,] directions =
        {
            {-1, -1}, {-1, 0 }, {-1, 1},
            {0, -1},            {0, 1},
            {1, -1 }, {1, 0}, {1, 1}
        };
        
        int directionCount = directions.GetLength(0);

        for(int i = 0; i < directionCount; i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];

            if (CanFlipInDirection(x, y, dx, dy, isBlack))
            {
                return true;
            }
        }

        return false;
    }

    bool CanFlipInDirection(int x, int y, int dx, int dy, bool isBlack) // 특정 방향으로 돌을 뒤집을 수 있는지 확인
    {
        int nx = x + dx;
        int ny = y + dy;

        bool hasOpponent = false;

        while(boardManager.GetCellAt(nx, ny) != null)
        {
            BoardCell nextCell = boardManager.GetCellAt(nx, ny);

            if (!nextCell.HasPiece())
            {
                return false;
            }

            if (nextCell.IsBlack() != isBlack)
            {
                hasOpponent = true;
                nx += dx;
                ny += dy;
            }
            else
            {
                return hasOpponent;
            }
        }

        return false;
    }

    void FlipPieces(int x, int y, bool isBlack)
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

    void FlipInDirection(int x, int y, int dx, int dy, bool isBlack)
    {
        int nx = x + dx;
        int ny = y + dy;
        
        while (boardManager.GetCellAt(nx, ny) != null)
        {
            BoardCell nextCell = boardManager.GetCellAt(nx, ny);

            if (!nextCell.HasPiece() || nextCell.IsBlack() == isBlack) break;

            nextCell.PlacePiece(isBlack);
            nx += dx;
            ny += dy;
        }
    }

   





}
