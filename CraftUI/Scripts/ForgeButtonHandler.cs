using UnityEngine;

public class ForgeButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject craftSwordPrefab; // Reference to the CraftSwordPrefab (with the script)
    [SerializeField] private GameObject craftedSwordPrefab; // Reference to the new prefab (without the script)
    [SerializeField] private Transform spawnPoint; // The point where the prefab will be spawned
    [SerializeField] private LayerMask floorLayer; // Layer mask to detect the floor
    [SerializeField] private float spawnRadius = 1.0f;

    public void OnForgeButtonClick()
    {
        if (craftSwordPrefab == null || craftedSwordPrefab == null)
        {
            Debug.LogError("CraftSwordPrefab or CraftedSwordPrefab is not assigned!");
            return;
        }

        // Instantiate the new prefab
        GameObject spawnedSword = Instantiate(craftedSwordPrefab);

        // Copy meshes and materials from CraftSwordPrefab to the spawned prefab
        CopyMeshesAndMaterials(craftSwordPrefab, spawnedSword);

        // Find the floor position with random variation
        Vector3 spawnPosition = GetRandomizedFloorPosition(spawnPoint.position);
        spawnedSword.transform.position = spawnPosition;
        spawnedSword.SetActive(true);

        Debug.Log("Sword spawned at: " + spawnPosition);
    }

    private Vector3 GetFloorPosition(Vector3 startPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPosition, Vector3.down, out hit, Mathf.Infinity, floorLayer))
        {
            return hit.point; // Return the point where the ray hits the floor
        }

        Debug.LogWarning("Floor not found! Using default spawn position.");
        return startPosition; // Default to the spawn point position if no floor is found
    }

    private void CopyMeshesAndMaterials(GameObject source, GameObject target)
    {
        // Get all MeshRenderers and MeshFilters in the source and target
        MeshRenderer[] sourceRenderers = source.GetComponentsInChildren<MeshRenderer>();
        MeshRenderer[] targetRenderers = target.GetComponentsInChildren<MeshRenderer>();

        MeshFilter[] sourceFilters = source.GetComponentsInChildren<MeshFilter>();
        MeshFilter[] targetFilters = target.GetComponentsInChildren<MeshFilter>();

        // Ensure the source and target have the same structure
        if (sourceRenderers.Length != targetRenderers.Length || sourceFilters.Length != targetFilters.Length)
        {
            Debug.LogError("Source and target prefabs do not have matching structures!");
            return;
        }

        // Copy materials
        for (int i = 0; i < sourceRenderers.Length; i++)
        {
            targetRenderers[i].material = new Material(sourceRenderers[i].material); // Create unique material instances
        }

        // Copy meshes
        for (int i = 0; i < sourceFilters.Length; i++)
        {
            targetFilters[i].mesh = Instantiate(sourceFilters[i].mesh); // Create unique mesh instances
        }
    }

    private Vector3 GetRandomizedFloorPosition(Vector3 startPosition)
    {
        // Add random variation to the x and z coordinates
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius), // Random x offset
            0,                                      // No change to y
            Random.Range(-spawnRadius, spawnRadius) // Random z offset
        );

        Vector3 randomizedPosition = startPosition + randomOffset;

        // Perform a raycast to ensure the randomized position is on the floor
        RaycastHit hit;
        if (Physics.Raycast(randomizedPosition, Vector3.down, out hit, Mathf.Infinity, floorLayer))
        {
            return hit.point; // Return the point where the ray hits the floor
        }

        Debug.LogWarning("Floor not found! Using default spawn position.");
        return randomizedPosition; // Default to the randomized position if no floor is found
    }
}

