using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class U10PS_DissolveOverTime : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    // Vitesse à laquelle l'effet de dissolution progresse
    public float speed = .5f;

    // Variable pour suivre la progression de la dissolution
    private float dissolveProgress = 0.0f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Si dissolveProgress est déjà à 1, ne rien faire
        if (dissolveProgress >= 1.0f) return;

        // Augmente dissolveProgress basé sur le temps et la vitesse
        dissolveProgress += Time.deltaTime * speed;

        // Assure que dissolveProgress ne dépasse pas 1
        dissolveProgress = Mathf.Clamp01(dissolveProgress);

        // Met à jour le matériel avec la valeur actuelle de dissolveProgress
        Material[] mats = meshRenderer.materials;
        mats[0].SetFloat("_Cutoff", dissolveProgress);
        meshRenderer.materials = mats;

        // Vérifie si la dissolution est complète
        if (dissolveProgress >= 1.0f)
        {
            // Détruit l'objet
            Destroy(gameObject);
        }
    }
}
