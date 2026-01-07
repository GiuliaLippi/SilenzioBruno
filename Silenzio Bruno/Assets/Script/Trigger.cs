using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoad : MonoBehaviour
{
    public GameObject StreetPrefab; // Il prefab da spawnare
    public Transform ExitPoint;

    private bool spawned = false;

    void OnTriggerEnter(Collider other)
    {
        // Controlla se è il player e se non abbiamo già spawnato
        if (other.CompareTag("Player") && !spawned)
        {
            spawned = true;

            // Spawna il nuovo pezzo esattamente sulla posizione dell'ExitPoint
            // Usiamo exitPoint.position e exitPoint.rotation per sicurezza
            Instantiate(StreetPrefab, ExitPoint.position, ExitPoint.rotation);

            // Opzionale: distrugge questo pezzo dopo 10 secondi per pulizia
            Destroy(transform.parent.gameObject, 10f);
        }
    }
}