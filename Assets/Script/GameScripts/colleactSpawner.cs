using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class colleactSpawner : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    public GameObject[] prefabs;
    private Dictionary<GameObject, int> prefabCounts = new Dictionary<GameObject, int>();
   


    void Start()
    {
        prefabCounts.Add(prefabs[0], 80);
        prefabCounts.Add(prefabs[1], 200);
        prefabCounts.Add(prefabs[2], 80);
        prefabCounts.Add(prefabs[3], 150);
        spawnPrefab();
    }

    
    void Update()
    {
        
    }

    private void RandomPose()
    {
        Bounds bounds = this.spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(x, y, 0);
    }
    void spawnPrefab()
    {
        foreach (var kvp in prefabCounts)
        {
            GameObject prefab = kvp.Key;
            int count = kvp.Value;

            for (int i = 0; i < count; i++)
            {
                RandomPose();
                Instantiate(prefab, transform.position, Quaternion.identity);

            }
        }
    }
  
}

