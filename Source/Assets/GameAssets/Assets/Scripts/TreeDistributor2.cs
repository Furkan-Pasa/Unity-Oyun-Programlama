using UnityEngine;

public class AdvancedTreePlacer2 : MonoBehaviour
{
    [System.Serializable]
    public class TreeType
    {
        public GameObject prefab;
        public int count;
        [Range(0.1f, 2f)] public float minScale = 0.9f;
        [Range(0.1f, 3f)] public float maxScale = 1.1f;
    }

    [Header("Tree Settings")]
    public TreeType[] treeTypes = new TreeType[11]; // 11 farklý prefab için

    [Header("Placement Area")]
    public Vector2 areaSize = new Vector2(100, 100);
    public float minHeight = 10f;
    public float maxHeight = 100f;

    [Header("Raycast Settings")]
    public string requiredTag = "Ground"; // Yeni: Hangi etiketi aradýðýmýz
    public float raycastStartHeight = 100f;
    public float minRaycastDistance = 70f;
    public float maxRaycastDistance = 90f;

    [Header("Placement Settings")]
    public float minDistanceBetweenTrees = 2f;
    public bool randomRotation = true;

    void Start()
    {
        GameObject treeContainer = new GameObject("Generated Trees");

        foreach (TreeType treeType in treeTypes)
        {
            if (treeType.prefab == null) continue;

            for (int i = 0; i < treeType.count; i++)
            {
                TryPlaceTree(treeType, treeContainer);
            }
        }
    }

    void TryPlaceTree(TreeType treeType, GameObject parent)
    {
        Vector2 randomPos2D = new Vector2(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );

        Vector3 rayOrigin = new Vector3(randomPos2D.x, raycastStartHeight, randomPos2D.y);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, maxRaycastDistance))
        {
            // ETÝKET KONTROLÜ: Eðer çarpýþtýðýmýz objenin etiketi istediðimiz etiket deðilse, iptal et
            if (!string.IsNullOrEmpty(requiredTag) && !hit.collider.CompareTag(requiredTag))
                return;

            // Min raycast distance kontrolü
            float hitDistance = raycastStartHeight - hit.point.y;
            if (hitDistance < minRaycastDistance) return;

            // Yükseklik filtresi
            if (hit.point.y < minHeight || hit.point.y > maxHeight) return;

            // Diðer aðaçlarla minimum mesafe kontrolü
            if (IsTooCloseToOtherTrees(hit.point)) return;

            // Aðacý oluþtur
            Quaternion rotation = randomRotation ?
                Quaternion.Euler(0, Random.Range(0, 360), 0) :
                Quaternion.identity;

            GameObject tree = Instantiate(treeType.prefab, hit.point, rotation, parent.transform);

            // Rastgele ölçeklendirme
            float scale = Random.Range(treeType.minScale, treeType.maxScale);
            tree.transform.localScale = new Vector3(scale, scale, scale);

            // Yeni oluþturulan aðaca etiket ekle (mesafe kontrolü için)
            tree.tag = "Tree";
        }
    }

    bool IsTooCloseToOtherTrees(Vector3 position)
    {
        if (minDistanceBetweenTrees <= 0) return false;

        // Sadece "Tree" etiketine sahip objeleri kontrol et
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach (GameObject tree in trees)
        {
            // Sadece aktif ve görünür aðaçlarý kontrol et
            if (tree.activeInHierarchy && tree.CompareTag("Tree"))
            {
                if (Vector3.Distance(position, tree.transform.position) < minDistanceBetweenTrees)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Yerleþim alaný görselleþtirme
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Vector3 center = new Vector3(0, (maxHeight + minHeight) / 2, 0);
        Vector3 size = new Vector3(areaSize.x, maxHeight - minHeight, areaSize.y);
        Gizmos.DrawCube(center, size);

        // Alan sýnýrlarýný göster
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);

        // Raycast mesafeleri görselleþtirme
        Gizmos.color = Color.blue;
        Vector3 rayCenter = new Vector3(0, raycastStartHeight, 0);
        Gizmos.DrawLine(rayCenter + Vector3.left * areaSize.x / 2, rayCenter + Vector3.right * areaSize.x / 2);
        Gizmos.DrawLine(rayCenter + Vector3.back * areaSize.y / 2, rayCenter + Vector3.forward * areaSize.y / 2);
    }
}