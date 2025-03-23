using System.Collections;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segments;  
    public Transform player;     
    private Vector3 nextSpawnPosition;
    void Start()
    {
        nextSpawnPosition = Vector3.zero;
        StartCoroutine(SpawnSegments());
    }

    IEnumerator SpawnSegments()
    {
        while (true) 
        {
            GameObject newSegment = Instantiate(segments[Random.Range(0, segments.Length)], nextSpawnPosition, Quaternion.identity);
            
            nextSpawnPosition.z += 50;

            yield return new WaitForSeconds(6f);
        }
    }
}
