using UnityEngine;

public class ObjectApproach : MonoBehaviour
{
    public LineRenderer lineRenderer; // Assignez ici votre LineRenderer
    public Transform objectToMove; // Assignez ici l'objet que vous souhaitez déplacer

    public float approachSensitivity = 0.1f; // Sensibilité de l'approche

    private float initialLineLength; // Longueur initiale de la ligne
    private bool initialLengthSet = false; // Indicateur si la longueur initiale a été enregistrée

    void Update()
    {
        // Vérifie si le LineRenderer a deux points définis
        if (lineRenderer != null && lineRenderer.positionCount >= 2)
        {
            Vector3 startPoint = lineRenderer.GetPosition(0);
            Vector3 endPoint = lineRenderer.GetPosition(1);

            float currentLineLength = Vector3.Distance(startPoint, endPoint);

            // Enregistrez la longueur initiale de la ligne la première fois que la ligne apparaît
            if (!initialLengthSet && currentLineLength > 0)
            {
                initialLineLength = currentLineLength;
                initialLengthSet = true;
            }

            // Si la ligne est présente et que la longueur de la ligne a changé, ajustez la position de l'objet
            if (initialLengthSet && currentLineLength > 0 && currentLineLength != initialLineLength)
            {
                float lengthDifference = currentLineLength - initialLineLength;

                // Appliquez la différence à la position actuelle de l'objet pour obtenir la nouvelle position
                Vector3 newPosition = objectToMove.position + (objectToMove.position - lineRenderer.transform.position).normalized * lengthDifference * approachSensitivity;

                // Mettez à jour la position de l'objet
                objectToMove.position = newPosition;

                // Mise à jour de la longueur initiale de la ligne pour la prochaine frame
                initialLineLength = currentLineLength;
            }
        }
        // Si la ligne n'est pas présente, ne faites rien pour laisser l'objet à sa position actuelle
    }
}
