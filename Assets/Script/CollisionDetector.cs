using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public GameObject prefabToAppear;
    public GameObject colliderObject1;
    public GameObject colliderObject2;
    public GameObject parentObject; // Objet parent pour l'instanciation
    public float minAppearanceDistance = 0.5f; // Distance minimale pour que l'objet apparaisse
    public float maxAppearanceDistance = 1.5f; // Distance maximale pour l'apparition initiale de l'objet
    public float maxScalingDistance = 1f; // Distance maximale pour la mise à l'échelle de l'objet
    public float disappearanceDistance = 0.3f; // Distance à laquelle l'objet disparaît

    private GameObject instantiatedObject = null;

    private void Start()
    {
        ValidateObjects();
    }

    private void Update()
    {
        if (AreObjectsValid())
        {
            float currentDistance = Vector3.Distance(colliderObject1.transform.position, colliderObject2.transform.position);

            if (instantiatedObject == null && currentDistance >= minAppearanceDistance && currentDistance <= maxAppearanceDistance)
            {
                Vector3 spawnPosition = (colliderObject1.transform.position + colliderObject2.transform.position) / 2;
                instantiatedObject = Instantiate(prefabToAppear, spawnPosition, Quaternion.identity, parentObject.transform); // Ajout du parentObject.transform comme parent
                Debug.Log("Prefab instancié entre les deux objets.");
            }
            else if (instantiatedObject != null && currentDistance < disappearanceDistance)
            {
                Destroy(instantiatedObject);
                instantiatedObject = null;
                Debug.Log("Objet instancié détruit car la distance est inférieure à la limite de disparition.");
            }
            else if (instantiatedObject != null)
            {
                UpdateObjectAppearance(Mathf.Min(currentDistance, maxScalingDistance));
            }
        }
    }

    void UpdateObjectAppearance(float scaleDistance)
    {
        if (instantiatedObject != null)
        {
            Vector3 middlePoint = (colliderObject1.transform.position + colliderObject2.transform.position) / 2;
            instantiatedObject.transform.position = middlePoint;

            float scale = Mathf.Clamp(scaleDistance, 0, maxScalingDistance);
            instantiatedObject.transform.localScale = new Vector3(scale, scale, scale);
            Debug.Log($"Position et taille de l'objet mises à jour. Échelle limitée à {scale}.");
        }
    }

    private void ValidateObjects()
    {
        if (prefabToAppear == null)
            Debug.LogError("CollisionDetector: Aucun prefab assigné à 'prefabToAppear'.");
        if (colliderObject1 == null)
            Debug.LogError("CollisionDetector: Aucun objet assigné à 'colliderObject1'.");
        if (colliderObject2 == null)
            Debug.LogError("CollisionDetector: Aucun objet assigné à 'colliderObject2'.");
        if (parentObject == null)
            Debug.LogError("CollisionDetector: Aucun objet parent assigné.");
    }

    private bool AreObjectsValid()
    {
        return prefabToAppear != null && colliderObject1 != null && colliderObject2 != null && parentObject != null;
    }
}
