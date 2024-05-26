using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class MaterialChangerOnHover : MonoBehaviour
{
    public Material hoverMaterial; // Le matériau à appliquer lors du survol.
    private Material originalMaterial; // Pour stocker le matériau original.
    private Renderer objectRenderer; // Renderer de l'objet.

    private InteractionBehaviour interactionBehaviour;

    void Start()
    {
        interactionBehaviour = GetComponent<InteractionBehaviour>();
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;

        // Abonnez-vous aux événements de hover.
        interactionBehaviour.OnHoverBegin += OnHoverBegin;
        interactionBehaviour.OnHoverEnd += OnHoverEnd;
    }

    private void OnHoverBegin()
    {
        // Change le matériau de l'objet au matériau de survol.
        objectRenderer.material = hoverMaterial;
    }

    private void OnHoverEnd()
    {
        // Rétablit le matériau original quand le survol se termine.
        objectRenderer.material = originalMaterial;
    }

    void OnDestroy()
    {
        // Nettoyage des abonnements aux événements.
        if (interactionBehaviour != null)
        {
            interactionBehaviour.OnHoverBegin -= OnHoverBegin;
            interactionBehaviour.OnHoverEnd -= OnHoverEnd;
        }
    }
}
