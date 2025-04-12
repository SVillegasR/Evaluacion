using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colision : MonoBehaviour
{
    // Este script se encargará de detectar colisiones y destruir el proyectil

    // Detectar las colisiones con otros objetos
    void OnCollisionEnter(Collision collision)
    {
        // Puedes hacer una verificación de qué tipo de objeto ha chocado
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruir el proyectil si colisiona con un muro
            Destroy(gameObject);
        }

        // También puedes añadir lógica para otras colisiones si lo necesitas.
    }
}