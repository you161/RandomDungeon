using UnityEngine;

public class HpUi : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject = null;
    private void LateUpdate()
    {
        transform.rotation = cameraObject.transform.rotation;
    }
}