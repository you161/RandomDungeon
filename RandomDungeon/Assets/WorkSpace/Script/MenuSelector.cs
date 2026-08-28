using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] menu = null;
    [SerializeField] private GameSceneManager gameSceneManager = null;
    [SerializeField] private ActionType[] actioneType = { ActionType.None };
    private GameObject selectedMenu = null;

    private enum ActionType
    {
        None,
        Title,
        Main,
        GameOver,
        GameClear,
        Exit
    }
    private void Start()
    {
        foreach (GameObject item in menu)
        {
            item.transform.localScale = Vector3.one;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        CheckMouseHover();
        CheckClick();
    }
    private void CheckMouseHover()
    {
        //最新Input Systemからマウス位置を取得
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        foreach (GameObject item in menu)
        {
            RectTransform rect = item.GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(rect,mousePosition))
            {
                item.transform.localScale = Vector3.one * 1.5f;
            }
            else
            {
                item.transform.localScale = Vector3.one;
            }
        }
    }
    private void CheckClick()
    {
        //左クリックされた瞬間
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        foreach (GameObject item in menu)
        {
            RectTransform rect = item.GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(rect,mousePosition))
            {
                SelectMenu(item);
                return;
            }
        }
    }
    private void SelectMenu(GameObject selected)
    {
        selectedMenu = selected;

        for(int i = 0; i < menu.Length; ++i)
        {
            if(menu[i] == selected)
            {
                SceneChange(actioneType[i]);
            }
        }
    }
    private void SceneChange(ActionType nextScneType)
    {
        switch (nextScneType) 
        {
            case ActionType.None:
                Debug.Log("シーンが選択されていない");
                break;
            case ActionType.Title:
                gameSceneManager.LoadTitleScene();
                break;
            case ActionType.Main:
                gameSceneManager.LoadMainScene();
                break;
            case ActionType.GameClear:
                gameSceneManager.LoadClearScene();
                break;
            case ActionType.GameOver:
                gameSceneManager.LoadGameOverScene();
                break;
            case ActionType.Exit:
                Application.Quit();
                break;
        }
    }
}