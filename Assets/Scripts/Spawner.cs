using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("What need to spawn ?")]
    public GameObject ufoPrefab;
    public GameObject rockPrefab;

    [Header("Spawn Options")]
    [SerializeField] float minUfoRespawnTime;
    [SerializeField] float maxUfoRespawnTime;
    [SerializeField] int roksSize = 2;


    [Header("Count asteroids at scene")]
    public static float asteroidCounter = 0;

    private int currentLevel = 0;
    private List<GameObject> roks = new List<GameObject>();

    [Header("UI")]
    [SerializeField] Text lvl;

    private void Start()
    {
        lvl.text = "Level " + currentLevel;

        if (ufoPrefab == null || rockPrefab == null)
            return;

        for (int i = 0; i < roksSize; i++)
        {
            roks.Add(rockPrefab);
        }
        StartCoroutine(UfoSpawn());
    }

    private void Update()
    {
        print(asteroidCounter);
        if (asteroidCounter <= 0)
        {
            Invoke(nameof(RocksSpawn), 3f);
        }
        else if (asteroidCounter > 0)
        {
            CancelInvoke(nameof(RocksSpawn));
        }
    }
    private void RocksSpawn()
    {
        for (int i = 0; i < roks.Count; i++)
        {
            Instantiate(roks[i]);
            asteroidCounter += roksSize;
        }
        currentLevel += 1;
        roks.Add(rockPrefab);
        lvl.text = "Level " + currentLevel;
    }

    IEnumerator UfoSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minUfoRespawnTime, maxUfoRespawnTime));
            Instantiate(ufoPrefab);
        }
    }
}
