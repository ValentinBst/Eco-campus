using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ChangePrefabToAppear : MonoBehaviour
{
    public CollisionDetector collisionDetector;
    public List<GameObject> prefabsToApply = new List<GameObject>();
    public MonoBehaviour scriptToApply;
    public GameObject parentObjectForScript;
    public Material materialToApply; // Le matériau à appliquer
    public float delayBeforeApplyingScript = 0.5f; // Délai en secondes avant d'appliquer le script

    private int nextPrefabIndex = 0;

    private void Awake()
    {
        if (materialToApply == null)
        {
            Debug.LogError("Material to apply is not set.");
        }
    }

    public void ChangePrefab()
    {
        if (collisionDetector == null || prefabsToApply.Count == 0)
        {
            Debug.LogError("CollisionDetector script is not set or there are no prefabs to apply.");
            return;
        }

        collisionDetector.prefabToAppear = prefabsToApply[nextPrefabIndex % prefabsToApply.Count];
        Debug.Log($"PrefabToAppear on CollisionDetector script has been changed successfully. Next prefab: {prefabsToApply[nextPrefabIndex % prefabsToApply.Count].name}");

        if (materialToApply != null && parentObjectForScript != null)
        {
            // Appliquer le matériau et le script aux descendants de manière récursive
            StartCoroutine(ApplyMaterialAndScriptRecursively(parentObjectForScript.transform, scriptToApply.GetType(), materialToApply, delayBeforeApplyingScript));
        }

        nextPrefabIndex = (nextPrefabIndex + 1) % prefabsToApply.Count;
    }

    private IEnumerator ApplyMaterialAndScriptRecursively(Transform transformToApply, System.Type scriptType, Material material, float delay)
    {
        // Appliquer le matériau à tous les renderers trouvés
        Renderer renderer = transformToApply.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
            Debug.Log($"Material {material.name} has been applied to {transformToApply.name}.");
        }

        foreach (Transform child in transformToApply)
        {
            StartCoroutine(ApplyMaterialAndScriptRecursively(child, scriptType, material, delay));
        }

        // Attendre le délai spécifié avant d'ajouter le script
        yield return new WaitForSeconds(delay);

        // Ajouter le script si nécessaire
        if (transformToApply.gameObject.GetComponent(scriptType) == null)
        {
            MonoBehaviour scriptComponent = transformToApply.gameObject.AddComponent(scriptType) as MonoBehaviour;
            if (scriptComponent != null)
            {
                scriptComponent.enabled = true;
                Debug.Log($"Script {scriptType.Name} has been added and enabled on {transformToApply.gameObject.name}.");
            }
        }
    }
}
