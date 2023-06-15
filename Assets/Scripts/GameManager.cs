using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
        var spawnPoint1 = spawnPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
