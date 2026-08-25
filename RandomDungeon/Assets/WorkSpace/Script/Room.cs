using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject wallFront;
    [SerializeField] private GameObject wallRear;
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;
    [SerializeField] private Transform[] enemySpawnPoints;

    public void SetWalls(bool hasFront,bool hasRear,bool hasLeft,bool hasRight)
    {
        wallFront.SetActive(!hasFront);
        wallRear.SetActive(!hasRear);
        wallLeft.SetActive(!hasLeft);
        wallRight.SetActive(!hasRight);
    }
    public Transform GetRandomEnemySpawnPoint()
    {
        return enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
    }
}