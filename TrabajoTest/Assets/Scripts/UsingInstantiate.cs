using UnityEngine;
using System.Collections;

public class UsingInstantiate : MonoBehaviour
{
    public GameObject proyectilPrefab;   // Prefab del proyectil
    public Transform puntoDisparo;       // Punto desde donde disparar el proyectil
    public float velocidad = 20f;        // Velocidad del proyectil
    public float tiempoVida = 2f;        // Tiempo después del cual el proyectil se destruye

    void Update()
    {
        // Detecta cuando se presiona el espacio (disparo)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DispararProyectil();
        }
    }

    void DispararProyectil()
    {
        // Asegúrate de que el prefab no sea nulo
        if (proyectilPrefab != null)
        {
            // Crear el proyectil
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

            // Obtener el Rigidbody del proyectil (si existe)
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aplicar una fuerza al proyectil
                rb.AddForce(puntoDisparo.forward * velocidad, ForceMode.VelocityChange);
            }

            // Destruir el proyectil después de un tiempo
            Destroy(proyectil, tiempoVida);
        }
        else
        {
            Debug.LogError("Prefab del proyectil no asignado");
        }

    }
    
    /*void DesactivarYDestruir(GameObject proyectil)
    {
        proyectil.SetActive(false);  // Desactiva el objeto
        Destroy(proyectil, tiempoVida);  // Destruye después de un tiempo
    }*/
}