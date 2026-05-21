using UnityEngine;

public class MeshTrackSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public MeshCollider trackCollider;
    public int spawnCount = 5;
    public LayerMask trackLayer;

    [Header("Placement")]
    public float extraHeightOffset = 0f; // fine-tune up/down after auto-grounding

    [Header("Edge Avoidance")]
    public float edgeMargin = 2f;
    public int edgeChecks = 8;

    private void Start()
    {
        for (int i = 0; i < spawnCount; i++)
            SpawnOnTrack();
    }

    void SpawnOnTrack()
    {
        if (trackCollider == null || collectiblePrefab == null) return;

        Bounds b = trackCollider.bounds;

        for (int attempts = 0; attempts < 100; attempts++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(b.min.x, b.max.x),
                b.max.y + 10f,
                Random.Range(b.min.z, b.max.z)
            );

            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, 1000f, trackLayer))
            {
                if (IsFarFromEdge(hit.point))
                {
                    float bottomOffset = GetPrefabBottomOffset(collectiblePrefab);
                    Vector3 spawnPos = hit.point + Vector3.up * (bottomOffset + extraHeightOffset);
                    Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);
                    return;
                }
            }
        }
    }

    float GetPrefabBottomOffset(GameObject prefab)
    {
        var rend = prefab.GetComponentInChildren<Renderer>();
        if (rend == null) return 0f;

        // Distance from prefab pivot to its bottom
        return rend.bounds.extents.y;
    }

    bool IsFarFromEdge(Vector3 center)
    {
        for (int i = 0; i < edgeChecks; i++)
        {
            float angle = i * (360f / edgeChecks);
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            Vector3 testPos = center + dir * edgeMargin + Vector3.up * 5f;

            if (!Physics.Raycast(testPos, Vector3.down, out _, 50f, trackLayer))
                return false;
        }
        return true;
    }
}