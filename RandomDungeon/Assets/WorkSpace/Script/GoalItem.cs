using UnityEngine;

public class GoalItem : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private GameSceneManager gameSceneManager = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerData.tagName))
        {
            gameSceneManager.LoadClearScene();
            Destroy(this.gameObject);
        }
    }
}