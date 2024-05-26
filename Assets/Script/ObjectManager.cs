using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public Transform rotationCenter; // Le point central de rotation
    public float rotationSpeed = 5f; // Vitesse de rotation autour du centre
    
    // Variable pour contrôler si l'objet doit tourner
    private bool shouldRotateRight = false;
    
    private void Update()
    {
        if (rotationCenter == null)
        {
            Debug.LogError("RotationCenter n'est pas défini dans l'inspecteur.");
            return;
        }

        // Si shouldRotateRight est vrai, tournez l'objet vers la droite
        if (shouldRotateRight)
        {
            RotateRight();
        }
    }

    // Fonction publique pour démarrer la rotation vers la droite
    public void StartRotationRight()
    {
        shouldRotateRight = true;
    }
    
    // Fonction publique pour arrêter la rotation
    public void StopRotation()
    {
        shouldRotateRight = false;
    }

    // Fonction pour effectuer la rotation
    private void RotateRight()
    {
        rotationCenter.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
