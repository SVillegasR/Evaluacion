using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public Transform[] spawnTransforms;
    
    private void Start()
    {
        // Asegúrate de que el objeto tenga un Collider
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("¡Falta un Collider en el enemigo!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el proyectil colisiona
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Destruir el enemigo al recibir el impacto
            Destroy(gameObject);
            Debug.Log("¡Enemigo destruido por el proyectil!");
        }
    }
   
}
