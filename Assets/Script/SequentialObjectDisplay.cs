using UnityEngine;
using System.Collections.Generic;

public class SequentialObjectDisplay : MonoBehaviour
{
    // Liste des prefabs à afficher.
    public List<GameObject> prefabsToDisplay = new List<GameObject>();

    // Le parent sous lequel les objets instanciés seront placés (optionnel).
    public Transform parentForInstantiatedObjects;

    // Référence à l'objet actuellement affiché.
    private GameObject currentDisplayedObject;

    // Index du prefab actuellement affiché.
    private int currentIndex = -1;

    private void Start()
    {
        DisplayNextObject(); // Affiche le premier objet au démarrage.
    }

    public void DisplayNextObject()
    {
        // Détruit l'objet actuellement affiché s'il existe.
        if (currentDisplayedObject != null)
        {
            Destroy(currentDisplayedObject);
        }

        // Incrémente l'index et boucle si nécessaire.
        currentIndex = (currentIndex + 1) % prefabsToDisplay.Count;

        // Instancie le prochain objet.
        currentDisplayedObject = Instantiate(prefabsToDisplay[currentIndex], parentForInstantiatedObjects);

        // Vous pouvez ajuster la position, la rotation, etc., ici si nécessaire.
        // Par exemple, pour positionner l'objet instancié à une position spécifique :
        // currentDisplayedObject.transform.position = new Vector3(<x, y, z);
    }
}