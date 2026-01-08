using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10f;
    void Start()
    {
        Debug.Log (name);
    }
    void OnDestroy()
    {
        Debug.Log("Destroyed"+name);
    }
    void Update()
    {
        // Muove la strada verso il player (asse Z positivo)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Opzionale: Se la strada è troppo lontana dietro il player, distruggila
        if (transform.position.x > 50f) 
        {
            Destroy(gameObject);
        }
    }
}
