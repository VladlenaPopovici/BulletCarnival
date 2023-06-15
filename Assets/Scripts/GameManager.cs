using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Canvas winCanvas;
    
    private List<GameObject> _enemies = new();


    void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            _enemies.Add(enemy);
        }
    }

    void Update()
    {
        if (AllEnemiesDead())
        {
            WinGameUI();
        }
    }

    private bool AllEnemiesDead()
    {
        return _enemies.All(x => x == null);
    }

    private void WinGameUI()
    {
        Time.timeScale = 0;
        winCanvas.gameObject.SetActive(true);
    }
}
