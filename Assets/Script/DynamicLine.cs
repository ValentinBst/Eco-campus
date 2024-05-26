using UnityEngine;

public class DynamicLine : MonoBehaviour
{
    // Variables publiques pour référencer les parents 'PinchPoint'
    public GameObject pinchPointParent1;
    public GameObject pinchPointParent2;

    private LineRenderer lineRenderer;

    void Start()
    {
        // Récupère le composant LineRenderer attaché à cet objet
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Vérifie si les deux parents 'pinchPoint' ont des enfants avant de continuer
        if (pinchPointParent1 != null && pinchPointParent1.transform.childCount > 0 &&
            pinchPointParent2 != null && pinchPointParent2.transform.childCount > 0)
        {
            // Récupère le premier enfant de chaque parent 'PinchPoint'
            Transform childOfParent1 = pinchPointParent1.transform.GetChild(0);
            Transform childOfParent2 = pinchPointParent2.transform.GetChild(0);

            // Met à jour les positions de début et de fin de la ligne
            lineRenderer.SetPosition(0, childOfParent1.position);
            lineRenderer.SetPosition(1, childOfParent2.position);

            // Assurez-vous que la ligne est visible
            lineRenderer.enabled = true;
        }
        else
        {
            // Cache la ligne si l'un des parents n'a pas d'enfant
            lineRenderer.enabled = false;
        }
    }
}
