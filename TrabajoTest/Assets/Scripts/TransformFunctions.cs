using UnityEngine;


public class TransformFunctions : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    
    private Vector2 direccion;

    public Transform cameraTransform;  // Referencia a la cámara
    private float currentRotationY = 0f;

    void Start()
    {
       
    }
    void Update()
    {
        direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        
        // Mover el jugador hacia adelante y hacia atrás con las teclas de dirección
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        // Girar el jugador a la izquierda y derecha
        if (Input.GetKey(KeyCode.LeftArrow))
            currentRotationY -= turnSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            currentRotationY += turnSpeed * Time.deltaTime;

        // Aplicar la rotación al cuerpo del jugador
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);

        // Hacer que la cámara siga la rotación del jugador (solo en el eje Y)
        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x, currentRotationY, cameraTransform.rotation.eulerAngles.z);
    }
    

}