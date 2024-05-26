using UnityEngine;

public class ObjectApproachDynamic : MonoBehaviour
{
    public LineRenderer lineRenderer; // Assignez ici votre LineRenderer
    public Transform objectToMove; // L'objet à déplacer

    public float closeThreshold = 2.0f; // La ligne doit être plus courte que cela pour reculer l'objet
    public float farThreshold = 3.0f; // La ligne doit être plus longue que cela pour avancer l'objet

    public float movementSpeed = 1.0f; // Vitesse de déplacement de l'objet

    private Camera mainCamera; // Caméra principale comme référence
    private bool shouldMove = false; // Contrôle si l'objet doit se déplacer

    void Start()
    {
        // Récupère la caméra principale
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Vérifie si le LineRenderer est actif et a au moins deux points
        if (lineRenderer != null && lineRenderer.positionCount >= 2 && lineRenderer.enabled)
        {
            shouldMove = true;
            float currentLineLength = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            Vector3 directionToCamera = (mainCamera.transform.position - objectToMove.position).normalized;

            if (currentLineLength < closeThreshold)
            {
                // Reculer par rapport à la caméra
                objectToMove.position -= directionToCamera * movementSpeed * Time.deltaTime;
            }
            else if (currentLineLength > farThreshold)
            {
                // Avancer vers la caméra
                objectToMove.position += directionToCamera * movementSpeed * Time.deltaTime;
            }
        }
        else
        {
            // La ligne n'est pas visible ou active, l'objet ne devrait pas bouger
            shouldMove = false;
        }

        // Arrêtez le mouvement de l'objet si shouldMove est false
        if (!shouldMove)
        {
            // Vous pouvez ajouter ici du code pour gérer l'arrêt du mouvement, si nécessaire.
            // Par exemple, arrêter une animation de mouvement ou réinitialiser des paramètres.
        }
    }
}
