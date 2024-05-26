using UnityEngine;

public class Pinch : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public Camera targetCamera; // La caméra à contrôler
    public float zoomSpeed = 5f; // Vitesse de zoom/dezoom
    public float minZoomDistance = 10f; // Distance minimale de la caméra (zoom maximum)
    public float maxZoomDistance = 50f; // Distance maximale de la caméra (zoom minimum)

    private float currentZoomLevel;
    private Vector3 initialCameraPosition;

    private void Start()
    {
        if (targetCamera == null)
        {
            Debug.LogError("CameraZoomControl: Aucune caméra cible n'est assignée.");
            return;
        }

        // Conserve la position initiale de la caméra pour calculer les ajustements relatifs
        initialCameraPosition = targetCamera.transform.position;
        currentZoomLevel = Vector3.Distance(targetCamera.transform.position, (object1.transform.position + object2.transform.position) / 2);
    }

    private void Update()
    {
        if (object1 != null && object2 != null && targetCamera != null)
        {
            // Calcule la distance actuelle entre les deux objets
            float currentDistance = Vector3.Distance(object1.transform.position, object2.transform.position);

            // Ajuste le niveau de zoom basé sur la distance entre les deux objets
            currentZoomLevel += currentDistance * Time.deltaTime * zoomSpeed;

            // Limite le zoom pour éviter des déplacements trop extrêmes
            currentZoomLevel = Mathf.Clamp(currentZoomLevel, minZoomDistance, maxZoomDistance);

            // Met à jour la position de la caméra basée sur le nouveau niveau de zoom
            // Garde la caméra focalisée entre les deux objets tout en ajustant sa distance
            Vector3 middlePoint = (object1.transform.position + object2.transform.position) / 2;
            targetCamera.transform.position = middlePoint - targetCamera.transform.forward * currentZoomLevel;

            Debug.Log($"Zoom niveau ajusté à {currentZoomLevel}.");
        }
    }
}
