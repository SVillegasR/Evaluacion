using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colision : MonoBehaviour
{
    // Este script se encargar� de detectar colisiones y destruir el proyectil

    // Detectar las colisiones con otros objetos
    void OnCollisionEnter(Collision collision)
    {
        // Puedes hacer una verificaci�n de qu� tipo de objeto ha chocado
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruir el proyectil si colisiona con un muro
            Destroy(gameObject);
        }

        // Tambi�n puedes a�adir l�gica para otras colisiones si lo necesitas.
    }
}