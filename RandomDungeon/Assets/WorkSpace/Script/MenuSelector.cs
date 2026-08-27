using UnityEngine;
using UnityEngine.InputSystem;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] menu = null;
    [SerializeField] private GameSceneManager gameSceneManager = null;
    private GameObject selectedMenu = null;

    private void Start()
    {
        foreach (GameObject item in menu)
        {
            item.transform.localScale = Vector3.one;
        }
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

        if (selectedMenu == menu[0])
        {
            gameSceneManager.LoadMainScene();
        }
        else if (selectedMenu == menu[1])
        {
            Application.Quit();
        }
    }
}