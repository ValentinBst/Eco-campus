using UnityEngine;

public class ScoreAnneau : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) // Assurez-vous que votre boule a le tag "Ball"
        {
            // Incrémentez le score ici
            Debug.Log("Boule passée à travers l'anneau !");
        }
    }
}
