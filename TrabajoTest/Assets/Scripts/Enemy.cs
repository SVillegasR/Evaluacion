using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;  // Velocidad de movimiento
    private Transform player;
    private Rigidbody eRb;

    public float avoidanceDistance = 3f; // Distancia para evitar la pared
    public float sideMoveDistance = 2f;  // Distancia lateral para moverse cuando detecta una pared
    public float frontMoveDistance = 3f; // Distancia para moverse hacia adelante si est� bloqueado

    private Vector3 direction;

    void Start()
    {
        // Obtener el Rigidbody y asegurarse de que no se vea afectado por la gravedad
        eRb = GetComponent<Rigidbody>();
        if (eRb != null)
        {
            eRb.useGravity = false;  // Desactivar la gravedad
            eRb.freezeRotation = true;  // Evitar que el Rigidbody se voltee (o gire)
        }

        player = GameObject.FindWithTag("jugador").transform;  // Encontrar al jugador
        direction = Vector3.zero;
    }

    void Update()
    {
        // Calcular la direcci�n hacia el jugador
        direction = (player.position - transform.position).normalized;

        // Verificar si hay una pared entre el enemigo y el jugador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, avoidanceDistance))
        {
            // Si hay una pared, intentar moverse lateralmente
            Vector3 leftDirection = Vector3.Cross(direction, Vector3.up);  // Mover a la izquierda
            Vector3 rightDirection = -leftDirection;  // Mover a la derecha

            // Verificar si hay espacio a la izquierda
            RaycastHit leftHit;
            if (!Physics.Raycast(transform.position, leftDirection, out leftHit, sideMoveDistance))
            {
                // Si no hay pared a la izquierda, mover a la izquierda
                direction = leftDirection;
            }
            // Verificar si hay espacio a la derecha
            else if (!Physics.Raycast(transform.position, rightDirection, out leftHit, sideMoveDistance))
            {
                // Si no hay pared a la derecha, mover a la derecha
                direction = rightDirection;
            }
            else
            {
                // Si est� completamente bloqueado en la parte frontal y los laterales, moverse atr�s
                direction = -direction;  // Retroceder
            }
        }

        // Si no est� bloqueado, mover al enemigo hacia el jugador
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Usamos Rigidbody para mover el enemigo de manera f�sica
        Vector3 moveDirection = direction * speed * Time.deltaTime;
        eRb.MovePosition(transform.position + moveDirection);  // Usar MovePosition para respetar las colisiones
    }

    // Detectar colisiones con otros objetos
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pared"))  // Si choca con una pared
        {
            // Evitar atravesar paredes: El enemigo se detendr� moment�neamente
            eRb.velocity = Vector3.zero;  // Detener el movimiento
        }
    }

    // Otra forma de manejar las colisiones de forma continua
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("pared"))
        {
            // Evitar atravesar paredes: El enemigo se detendr� mientras siga tocando la pared
            eRb.velocity = Vector3.zero;
        }
    }
}

