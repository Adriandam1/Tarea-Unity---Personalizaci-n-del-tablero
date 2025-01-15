using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody del jugador
    private Rigidbody rb;
    // Integer para contar la puntuacion
    private int count;
    // Valores de movimiento
    private float movementX;
    private float movementY;
    // Esta es la velocidad a la que mueves la bolita
    // la puedes cambiar en el editor de unity
    public float speed = 10.0f;

    //Variable para texto de puntuacion
    public TextMeshProUGUI countText;
    /**
    * Start se ejecuta en el inicio
    */ 
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        // Debug nos sirve para poner mensajes que apareceran por consola
        Debug.Log("Hello, I'am a message in Start");
        // iniciamos la variable para la puntuacion
        count = 0; 
        SetCountText();
    }
    /**
    * Update se llama cada frame
    */
    void Update()
    {
        // Aqui podriamos poner un mensaje de actualizacion en los frames, mejor tenerlo comentado para no saturar la consola
        // Debug.Log("Hello, I'am a message in Update");
    }   

    /**
    * OnMove se llama cuando el jugador mueve la bolita
    */
    void OnMove (InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
        
    }

    // Método para mostrar la puntuación
       void SetCountText() 
   {
       countText.text =  "Puntación: " + count.ToString();
   }

    /**
     * Salto con la barra espaciadora
     */
    void OnFire(){
        // cuando saltamos nos da un mensajito
        Debug.Log("OnFire");
        Debug.Log("Clicada la bolita SALTA");

        // afuerza vertical aplicada a la bolita
        rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse); 
    }
    
    /**
    * FixedUpdate is called once per frame
    * it's called every frame
    * Difference between Update and FixedUpdate
    * https://learn.unity.com/tutorial/update-and-fixedupdate
    */
    private void FixedUpdate() 
    {
        // jump with space bar
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnFire();
        }

        Vector3 movement = new Vector3 (speed*movementX, 0.0f , speed*movementY);

        // debug force values
        // Warning: this message is called every frame
        // Debug.Log("X: " + movementX + " Y: " + movementY + " Z: 0");
        
        // apply the force to the player
        rb.AddForce(movement); 
    }


   void OnTriggerEnter (Collider other) 
   {
        // cuando la bolita toque el objetivo pickup lo hacemos desaparecer
       if (other.gameObject.CompareTag("Pickup")) 
       {
           other.gameObject.SetActive(false);
            // aumentamos el score en 1
           count = count + 1;
           SetCountText();
       }
   }


}