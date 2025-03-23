using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segments;  // Array of segment prefabs
    public Transform player;       // Reference to player transform
    private Vector3 nextSpawnPosition;
    private float segmentLength = 50f; // Each segment is 50 units long
    private int maxSegments = 5; // Number of segments to keep in the scene
    private Queue<GameObject> activeSegments = new Queue<GameObject>(); // Store spawned segments

    void Start()
    {
        nextSpawnPosition = Vector3.zero;
        nextSpawnPosition.z += 50;
        // Spawn initial segments
        for (int i = 0; i < maxSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // Check if we need to spawn a new segment (player getting close to the last one)
        if (player.position.z > nextSpawnPosition.z - (maxSegments * segmentLength))
        {
            SpawnSegment();
        }
    }

    void SpawnSegment()
    {
        GameObject newSegment = Instantiate(segments[Random.Range(0, segments.Length)], nextSpawnPosition, Quaternion.identity);
        
        // Disable all colliders initially to prevent unwanted collisions
        Collider[] colliders = newSegment.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        // Enable colliders after a short delay
        StartCoroutine(EnableCollidersAfterDelay(colliders, 0.5f));

        // Move segment slightly below and then smoothly raise it
        newSegment.transform.position = nextSpawnPosition - new Vector3(0, 5, 0);
        StartCoroutine(MoveSegmentUp(newSegment));

        activeSegments.Enqueue(newSegment);
        nextSpawnPosition.z += segmentLength;

        // Remove old segments
        if (activeSegments.Count > maxSegments+2)
        {
            Destroy(activeSegments.Dequeue());
        }
    }

    IEnumerator EnableCollidersAfterDelay(Collider[] colliders, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }

    IEnumerator MoveSegmentUp(GameObject segment)
    {
        Vector3 targetPosition = segment.transform.position + new Vector3(0, 5, 0);
        float elapsedTime = 0f;
        float duration = 0.5f; 
        while (elapsedTime < duration)
        {
            segment.transform.position = Vector3.Lerp(segment.transform.position, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        segment.transform.position = targetPosition;
    }
}
