using System.Collections;
using UnityEngine;

public class AutoBreakObject : MonoBehaviour
{
    public GameObject objectToBreak;
    public float breakDistance = 1.0f; // La distance de séparation entre chaque enfant et sous-enfant
    public float breakDelay = 0.5f; // Délai avant que la séparation ne commence
    public float reassembleThreshold = 0.1f; // La distance de mouvement en X ou Z pour réassembler

    private Vector3 initialPosition;
    private Transform[] allChildren;
    private Vector3[] initialChildLocalPositions;
    private Quaternion[] initialChildLocalRotations;
    private bool isBreaking = false; // Suivi de l'état de la séquence de rupture
    private bool isBroken = false; // Suivi de l'état cassé de l'objet

    void Start()
    {
        StoreInitialPositions(); // Stocker les positions initiales des enfants
        initialPosition = objectToBreak.transform.position; // Stocker la position initiale de l'objet
    }

    void Update()
    {
        // Si l'objet a bougé en X ou Z après la rupture, réassembler les enfants
        if (isBroken && HasMovedSignificantly())
        {
            ReformObject();
        }
    }

    public void InitiateBreakSequence()
    {
        if (!isBreaking && !isBroken) // Vérifier si la séquence de rupture n'est pas déjà en cours ou si l'objet n'est pas déjà cassé
        {
            isBreaking = true; // Indiquer que la séquence de rupture est en cours
            StartCoroutine(BreakObjectAndRepositionChildren());
        }
    }

    private IEnumerator BreakObjectAndRepositionChildren()
    {
        yield return new WaitForSeconds(breakDelay); // Attente avant de commencer la séparation

        // Initiez la séparation avec la distance spécifiée sans augmenter la profondeur initialement
        SeparateChildren(objectToBreak.transform, breakDistance);

        isBreaking = false;
        isBroken = true; // L'objet est maintenant considéré comme cassé
    }


    private void StoreInitialPositions()
    {
        allChildren = objectToBreak.GetComponentsInChildren<Transform>(true);
        initialChildLocalPositions = new Vector3[allChildren.Length];
        initialChildLocalRotations = new Quaternion[allChildren.Length];

        for (int i = 0; i < allChildren.Length; i++)
        {
            initialChildLocalPositions[i] = allChildren[i].localPosition;
            initialChildLocalRotations[i] = allChildren[i].localRotation;
        }
    }

    private void SeparateChildren(Transform parent, float currentBreakDistance)
    {
        int childIndex = 0; // Utilisez un index pour multiplier la distance de séparation

        foreach (Transform child in parent)
        {
            if (child == parent) continue; // Évite de traiter le parent lui-même

            // Direction fixe vers le haut sur l'axe Y
            Vector3 separation = Vector3.up * currentBreakDistance * childIndex;

            // Appliquer la séparation
            child.localPosition += separation; // Utilisez localPosition si vous voulez que cela soit relatif au parent

            // Incrementer l'index pour le prochain enfant
            childIndex++;
        }
    }

    private void ReformObject()
    {
        if (isBroken)
        {
            for (int i = 0; i < allChildren.Length; i++)
            {
                allChildren[i].localPosition = initialChildLocalPositions[i];
                allChildren[i].localRotation = initialChildLocalRotations[i];
            }
            isBroken = false; // Indiquer que l'objet a été réassemblé et n'est plus cassé
        }
    }

    private bool HasMovedSignificantly()
    {
        Vector3 currentPosition = objectToBreak.transform.position;
        float distanceX = Mathf.Abs(currentPosition.x - initialPosition.x);
        float distanceZ = Mathf.Abs(currentPosition.z - initialPosition.z);
        
        return distanceX > reassembleThreshold || distanceZ > reassembleThreshold;
    }
}
