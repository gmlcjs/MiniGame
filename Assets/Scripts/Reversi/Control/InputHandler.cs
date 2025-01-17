using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject MenuPane; // 메뉴 오브젝트

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //클릭했을 때 작동할 로직을 구현
            HandleClick();
        }
    }

    void HandleClick()
    {
        if(MenuPane.activeSelf != true){ // 메뉴 창이 활성화가 안될때 실행
            //Vector2 inputPosition = Mouse.current.position.ReadValue();
            Vector2 inputPosition;
            if (Mouse.current != null && Mouse.current.leftButton.isPressed)
            {
                inputPosition = Mouse.current.position.ReadValue();
            }
            else if (Touchscreen.current != null
                && Touchscreen.current.primaryTouch.press.isPressed)
            {
                inputPosition = Touchscreen.current.position.ReadValue();
            }
            else
            {
                return;
            }
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
                new Vector3(inputPosition.x, inputPosition.y, 0));
            
            //raycast 사용
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                BoardCell cell = hit.collider.GetComponent<BoardCell>();
                if (cell != null)
                {
                    //gameManager에서 처리
                    gameManager.OnCellClicked(cell);
                }
            }
        }
    }
}
