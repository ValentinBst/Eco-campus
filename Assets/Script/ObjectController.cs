using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // Références aux objets à activer/désactiver.
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    // Références aux boutons comme objets 3D
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    // Méthode pour activer object1 et désactiver les autres, et ajuster les boutons.
    public void ActivateObject1()
    {
        object1.SetActive(true);
        object2.SetActive(false);
        object3.SetActive(false);

        // Active/Désactive les boutons
        button1.SetActive(true); // Assurez-vous que le bouton 1 est toujours activé si nécessaire
        button2.SetActive(true); // Active ou réactive le bouton 2 s'il était désactivé
        button3.SetActive(true); // Active ou réactive le bouton 3 s'il était désactivé
    }

    // Méthodes similaires pour activer object2 et object3
    public void ActivateObject2()
    {
        object1.SetActive(false);
        object2.SetActive(true);
        object3.SetActive(false);

        // Active/Désactive les boutons
        button1.SetActive(true); // Active ou réactive le bouton 1 s'il était désactivé
        button2.SetActive(true); // Assurez-vous que le bouton 2 est toujours activé si nécessaire
        button3.SetActive(true); // Active ou réactive le bouton 3 s'il était désactivé
    }

    public void ActivateObject3()
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(true);

        // Active/Désactive les boutons
        button1.SetActive(true); // Active ou réactive le bouton 1 s'il était désactivé
        button2.SetActive(true); // Active ou réactive le bouton 2 s'il était désactivé
        button3.SetActive(true); // Assurez-vous que le bouton 3 est toujours activé si nécessaire
    }
}
