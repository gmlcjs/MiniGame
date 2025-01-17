using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardManager boardManager;
    [SerializeField] GameObject MenuButton; // �޴� ��ư

    [SerializeField] GameObject winUI;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI blackText;
    [SerializeField] TextMeshProUGUI whiteText;
    [SerializeField] GameObject passUI; // ���� �Ѿ �� ��Ÿ���� UI

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
        //���� ��� ��ȿ�� ���� ������ Ȯ��
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
            winText.text = "���� �¸�";
            passUI.GetComponent<TextMeshProUGUI>().color = Color.black;
        }
        else if (blackCount < whiteCount)
        {
            winText.text = "��� �¸�";
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

    //���
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
        //�̹� ���� ���� ������ ���
        if (cell.HasPiece())
        {
            return;
        }

        //��ȿ�� ��ġ���� �����ϴ��� Ȯ��
        if (cell.IsValidMove(isBlackTurn))
        {
            cell.PlacePiece(isBlackTurn);               //�� ��ġ
            FlipPieces(cell.x, cell.y, isBlackTurn);    //�� ������α�
            GetScore();
            
            SwitchTurn();                               //�� ��ȯ

            blackText.text = blackCount.ToString("00"); //���� ����
            whiteText.text = whiteCount.ToString("00"); // ���� ����
        }
    }

    public void SwitchTurn() // �� ��ȯ
    {
        isBlackTurn = !isBlackTurn;
        if (!boardManager.HasValidMoves(isBlackTurn) && boardManager.HasValidMoves(!isBlackTurn))
        {
            isBlackTurn = !isBlackTurn;
        }
        
        if(isBlackTurn){
            // ���� ����
            passText.GetComponent<TextMeshProUGUI>().text = "���� ����";
            passText.GetComponent<TextMeshProUGUI>().color = Color.black;
        }else{
            // ��� ����
            passText.GetComponent<TextMeshProUGUI>().text = "��� ����";
            passText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        passUI.SetActive(true);
    }

    bool CanPlacePiece(int x, int y, bool isBlack) // ���� ���� �� �ִ��� Ȯ��
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

    bool CanFlipInDirection(int x, int y, int dx, int dy, bool isBlack) // Ư�� �������� ���� ������ �� �ִ��� Ȯ��
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
