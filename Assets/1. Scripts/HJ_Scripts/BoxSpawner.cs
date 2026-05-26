using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    public List<Transform> spawnPoints;

    void Start()
    {
        SpawnNewBox();
    }

    private void SpawnNewBox()
    {
        if (boxPrefab == null || spawnPoints == null || spawnPoints.Count <= 0) return;

        Transform selectedPoint = PickSpawnPoint();
        GameObject newBox = Instantiate(boxPrefab, selectedPoint.position, selectedPoint.rotation);
        ItemBox itemBox = newBox.GetComponent<ItemBox>();

        if (itemBox != null)
        {
            itemBox.OnBoxDepleted += SpawnNewBox;
        }
    }

    private Transform PickSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}