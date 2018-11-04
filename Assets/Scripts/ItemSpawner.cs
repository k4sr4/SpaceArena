using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    public float spawnCountdown = 5;
    public float waitingForNextSpawn = 5;
    public float itemsCount = 0;
    public GameObject itemPrefab;
    public bool reposition = false;
    [Header("X Spawn Range")]
    public float xMin;
    public float xMax;

    // the range of y
    [Header("Y Spawn Range")]
    public float yMin;
    public float yMax;

    // Update is called once per frame
    void Update () {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0 && itemsCount < 5)
        {
            Spawn();
            spawnCountdown = waitingForNextSpawn;
        }
    }

    void Spawn (){

        Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        // Creates the random object at the random 2D position.

        GameObject spawnedItem = Instantiate(itemPrefab,
                                            pos,
                                            transform.rotation) as GameObject;
        itemsCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Player" || collision.tag == "Portal")
        {
            transform.position = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
            Debug.Log("GEre");
        }

    }

}
