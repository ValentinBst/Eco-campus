using UnityEngine;

public class HandInteraction : MonoBehaviour
{
    public GameObject handSpherePrefab; // Assignez votre préfabriqué de sphère dans l'inspecteur Unity
    public GameObject objectBetweenHandsPrefab; // Assignez votre préfabriqué d'objet entre les mains dans l'inspecteur Unity

    private GameObject leftHandSphere, rightHandSphere, objectBetweenHands;

    // Appelez cette méthode via l'événement "On Pose Detected" de l'interface graphique Hand Pose Detector
    public void CreateHandsAndObject()
    {
        // Remplacez ces positions par les positions réelles des mains obtenues par votre système de suivi
        Vector3 leftHandPosition = new Vector3(-0.5f, 0, 0);
        Vector3 rightHandPosition = new Vector3(0.5f, 0, 0);

        // Créez ou récupérez les sphères pour les mains si elles n'existent pas déjà
        if (leftHandSphere == null)
        {
            leftHandSphere = Instantiate(handSpherePrefab, leftHandPosition, Quaternion.identity);
        }
        if (rightHandSphere == null)
        {
            rightHandSphere = Instantiate(handSpherePrefab, rightHandPosition, Quaternion.identity);
        }

        // Créez l'objet entre les mains
        if (objectBetweenHands == null)
        {
            Vector3 positionBetweenHands = (leftHandPosition + rightHandPosition) / 2;
            objectBetweenHands = Instantiate(objectBetweenHandsPrefab, positionBetweenHands, Quaternion.identity);
        }
    }

    // Mettez à jour cette méthode si nécessaire pour être appelée pendant que la pose est maintenue
    public void Update()
    {
        if (leftHandSphere != null && rightHandSphere != null && objectBetweenHands != null)
        {
            // Mettre à jour la position de l'objet entre les mains
            objectBetweenHands.transform.position = (leftHandSphere.transform.position + rightHandSphere.transform.position) / 2;
            
            // Mettre à jour la taille de l'objet en fonction de l'écartement des mains
            float scale = Vector3.Distance(leftHandSphere.transform.position, rightHandSphere.transform.position);
            objectBetweenHands.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    // Appelez cette méthode via l'événement "On Pose Lost" pour nettoyer
    public void ClearObjects()
    {
        if (leftHandSphere != null)
        {
            Destroy(leftHandSphere);
            leftHandSphere = null;
        }
        if (rightHandSphere != null)
        {
            Destroy(rightHandSphere);
            rightHandSphere = null;
        }
        if (objectBetweenHands != null)
        {
            Destroy(objectBetweenHands);
            objectBetweenHands = null;
        }
    }
}
