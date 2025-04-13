using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;  // Velocidad de movimiento
    private Transform player;
    private Rigidbody eRb;     // Usamos Rigidbody normal para 3D
    public float distanciaDeDeteccion = 5f;  // Distancia de detecci�n para el proyectil
    public LayerMask capaProyectiles;         // Capa de los proyectiles
    public float avoidanceDistance = 3f;      // Distancia para evitar la pared
    public float sideMoveDistance = 2f;       // Distancia lateral para moverse cuando detecta una pared
    private Transform proyectilDetectado;     // El proyectil que se detecta
    private Vector3 direccionDeEsquive;      // Direcci�n de esquive
    private Vector3 direction;               // Direcci�n del jugador

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
    }

    void Update()
    {
        // Calcular la direcci�n hacia el jugador
        direction = (player.position - transform.position).normalized;

        // Detectar si hay proyectiles cerca
        DetectarProyectil();

        // Si el proyectil es detectado, esquivarlo
        if (proyectilDetectado != null)
        {
            EsquivarProyectil();
        }
        else
        {
            // Si no hay proyectil, moverse hacia el jugador
            MoveEnemy();
        }

        // Verificar si hay una pared entre el enemigo y el jugador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, avoidanceDistance))
        {
            if (hit.collider != null && hit.collider.CompareTag("pared"))
            {
                // Si hay una pared, intentar moverse lateralmente
                MoverLateralmente();
            }
        }
    }

    // Detectar proyectiles cercanos
    void DetectarProyectil()
    {
        // Raycast para detectar los proyectiles (usando Collider)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, distanciaDeDeteccion, capaProyectiles))
        {
            proyectilDetectado = hit.transform;  // Asignar el proyectil detectado
            Debug.Log("Proyectil detectado");   // Depuraci�n para verificar que se detect� un proyectil
        }
        else
        {
            proyectilDetectado = null;  // No se detect� ning�n proyectil
        }
    }

    // Esquivar el proyectil
    void EsquivarProyectil()
    {
        if (proyectilDetectado == null) return; // Si no hay proyectil detectado, no hacer nada

        // Obtener la direcci�n del proyectil (su vector de movimiento)
        Vector3 direccionProyectil = proyectilDetectado.position - transform.position;

        // Obtener la velocidad del proyectil si tiene un Rigidbody
        Rigidbody proyectilRb = proyectilDetectado.GetComponent<Rigidbody>();
        if (proyectilRb != null)
        {
            direccionProyectil = proyectilRb.velocity.normalized;  // Direcci�n del proyectil basada en su velocidad
        }

        // La direcci�n de esquive ser� en el lado opuesto al proyectil (evitar el impacto)
        direccionDeEsquive = -direccionProyectil.normalized;

        // Mover al enemigo en la direcci�n opuesta al proyectil
        eRb.velocity = direccionDeEsquive * speed;  // Movimiento de evasi�n
        Debug.Log("Esquivando proyectil");          // Depuraci�n para verificar que est� esquivando
    }

    // Mover al enemigo hacia el jugador
    void MoveEnemy()
    {
        eRb.velocity = direction * speed;  // Movimiento hacia el jugador
    }

    // Intentar moverse lateralmente si hay una pared
    void MoverLateralmente()
    {
        Vector3 leftDirection = Vector3.Cross(direction, Vector3.up).normalized;  // Mover a la izquierda
        Vector3 rightDirection = -leftDirection;  // Mover a la derecha

        // Verificar si hay espacio a la izquierda
        RaycastHit leftHit;
        if (!Physics.Raycast(transform.position, leftDirection, out leftHit, sideMoveDistance))
        {
            // Si no hay pared a la izquierda, mover a la izquierda
            eRb.velocity = leftDirection * speed;
            Debug.Log("Moviendo hacia la izquierda"); // Depuraci�n para verificar que se mueve a la izquierda
        }
        // Verificar si hay espacio a la derecha
        else
        {
            RaycastHit rightHit;
            if (!Physics.Raycast(transform.position, rightDirection, out rightHit, sideMoveDistance))
            {
                // Si no hay pared a la derecha, mover a la derecha
                eRb.velocity = rightDirection * speed;
                Debug.Log("Moviendo hacia la derecha"); // Depuraci�n para verificar que se mueve a la derecha
            }
            else
            {
                // Si est� completamente bloqueado, retroceder
                eRb.velocity = -direction * speed;
                Debug.Log("Retrocediendo"); // Depuraci�n para verificar que retrocede
            }
        }
    }

    // Verificar y dibujar el raycast para depuraci�n (opcional)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction * distanciaDeDeteccion); // Dibujar raycast en el editor
    }
}
