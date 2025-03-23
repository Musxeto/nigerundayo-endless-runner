using System.Collections;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segments;  // Array to hold segment prefabs
    public Transform player;       // Reference to the player to control spawning
    private Vector3 nextSpawnPosition;  // Stores the next segment's position

    void Start()
    {
        // Initialize starting position
        nextSpawnPosition = Vector3.zero;
        StartCoroutine(SpawnSegments());
    }

    IEnumerator SpawnSegments()
    {
        while (true) // Infinite loop for endless generation
        {
            // Choose a random segment from the array
            GameObject newSegment = Instantiate(segments[Random.Range(0, segments.Length)], nextSpawnPosition, Quaternion.identity);
            
            // Update the next spawn position (moving forward in Z direction)
            nextSpawnPosition.z += 50;

            yield return new WaitForSeconds(6f); // Adjust the delay as needed
        }
    }
}
