using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject Street;

    void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(Street);
        }
    }
}
