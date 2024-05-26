using UnityEngine;

public class SpawnSpheres : MonoBehaviour
{
    public GameObject handSpherePrefab; // Assignez votre préfabriqué de sphère
    [SerializeField] private Transform sphereParent; // Parent des sphères
    public CollisionDetector collisionDetectorScript; // Référence au script CollisionDetector

    private GameObject handSphere;
    private Vector3 handPosition;

    public void UpdateHandPosition(Vector3 newPosition)
    {
        handPosition = newPosition;
    }

    public void SpawnSphere()
    {
        if (handSphere == null)
        {
            handSphere = Instantiate(handSpherePrefab, handPosition, Quaternion.identity);
            if (sphereParent != null)
            {
                handSphere.transform.SetParent(sphereParent, false);
            }
            
            // Assigner la sphère créée aux objets de collision
            AssignSphereToCollisionDetector(handSphere);
        }
    }

    private void AssignSphereToCollisionDetector(GameObject sphere)
    {
        if (collisionDetectorScript.colliderObject1 == null)
        {
            collisionDetectorScript.colliderObject1 = sphere;
        }
        else if (collisionDetectorScript.colliderObject2 == null)
        {
            collisionDetectorScript.colliderObject2 = sphere;
        }
    }

    public void DespawnSphere()
    {
        if (handSphere != null)
        {
            // Nettoyer la référence dans CollisionDetector si nécessaire
            if (collisionDetectorScript.colliderObject1 == handSphere)
            {
                collisionDetectorScript.colliderObject1 = null;
            }
            else if (collisionDetectorScript.colliderObject2 == handSphere)
            {
                collisionDetectorScript.colliderObject2 = null;
            }

            Destroy(handSphere);
            handSphere = null;
        }
    }
}
