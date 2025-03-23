using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;  // Reference to the coin prefab
    public Transform player;       // Reference to the player
    public float spawnDistance = 50f; // How far ahead to spawn coins
    public float minX = -3f, maxX = 3f; // Random X position range
    public float minY = 1f, maxY = 2.5f; // Random Y position range

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        while (true) // Infinite loop for endless spawning
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX),  // Random X position
                Random.Range(minY, maxY),  // Slightly above the ground
                player.position.z + spawnDistance // Always ahead of the player
            );

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity); 

            yield return new WaitForSeconds(2f); // Adjust timing as needed
        }
    }
}
