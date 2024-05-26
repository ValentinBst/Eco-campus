using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public Transform parent1; // Assignez le premier parent dans l'inspecteur Unity
    public Transform parent2; // Assignez le second parent dans l'inspecteur Unity
    public GameObject prefabToCreate; // Assignez le préfab à créer dans l'inspecteur Unity

    private GameObject createdPrefabInstance;

    // Cette méthode vérifie si les deux parents ont un enfant préfab et crée un nouveau préfab si nécessaire
    public void CheckAndCreatePrefab()
    {
        // Vérifie si chaque parent a au moins un enfant et si cet enfant est le clone du préfab
        if (parent1.childCount > 0 && parent2.childCount > 0 
            && parent1.GetChild(0).gameObject.CompareTag("YourPrefabTag") 
            && parent2.GetChild(0).gameObject.CompareTag("YourPrefabTag"))
        {
            // Si le préfab à créer n'a pas encore été instancié, créez-le
            if (createdPrefabInstance == null)
            {
                createdPrefabInstance = Instantiate(prefabToCreate, Vector3.zero, Quaternion.identity);
            }
        }
        else
        {
            // Si l'une des conditions n'est pas remplie et que le préfab a été créé, détruisez-le
            if (createdPrefabInstance != null)
            {
                Destroy(createdPrefabInstance);
                createdPrefabInstance = null;
            }
        }
    }

    void Update()
    {
        // Appeler la vérification à chaque frame peut ne pas être nécessaire, 
        // considérez l'appeler seulement quand un changement est détecté
        CheckAndCreatePrefab();
    }
}
