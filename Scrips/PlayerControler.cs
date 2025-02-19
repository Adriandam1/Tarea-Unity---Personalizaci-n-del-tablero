using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks; // Necesario para async-await
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
    public float speed = 12.0f;

    //Variable para texto de puntuacion
    public TextMeshProUGUI countText;
    // objeto mensaje te caiste
    public GameObject Caiste_mensaje;

     // UI object to display winning text.
    public GameObject winTextObject;

    private Animator animator; // animator del unity
    
    /**
    * Start se ejecuta en el inicio
    */ 
    void Start()
    {
        Caiste_mensaje.SetActive(false);
        winTextObject.SetActive(false);
        rb = GetComponent <Rigidbody>();

        // Obtén la referencia al componente Animator
        animator = GetComponent<Animator>();

        // Debug nos sirve para poner mensajes que apareceran por consola
        Debug.Log("Hello, I'am a message in Start");
        // iniciamos la variable para la puntuacion
        count = 0; 
        SetCountText();
    }
    /**
    * Update se llama cada framee
    */
    void Update()
    {
        // Aqui podriamos poner un mensaje de actualizacion en los frames, mejor tenerlo comentado para no saturar la consola
        // Debug.Log("Hello, I'am a message in Update");


    // Si la posición en Y es menor a 0, teletransporta al jugador a una posición segura
    if (transform.position.y < -1)
    {
        RespawnPlayer();
    }

    }   

    async void RespawnPlayer()
    {
        Debug.Log("El jugador se ha caído, transportándolo al inicio...");
        Caiste_mensaje.SetActive(true); // Canva informativo para el jugador
        animator.SetBool("transportado", true); // Condicion estado "transportado".
        
        //Pausa la ejecución del método
        await Task.Delay(1000);  // Espera 1 segundos

        Vector3 respawnPosition = new Vector3(0, 3, 0); // vector3 con la posicion a transportar
        rb.MovePosition(respawnPosition); // Corrige la posición con Rigidbody
        rb.velocity = Vector3.zero; // baja velocidades a 0
        rb.angularVelocity = Vector3.zero;

        Caiste_mensaje.SetActive(false);

        await Task.Delay(750);  // Espera 1,6 segundos
        animator.SetBool("transportado", false);

    }

    /**
    * OnMove se llama cuando el jugador mueve la bolita.
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
        // Condicion de victoria del jugador que la variable count sea igual o mayor a 10
            if (count >= 1 && count < 10)
            {
                Debug.Log("Puntuación: "+count);
                animator.SetInteger("puntos", count); //Cambia la variable de estados puntos al count. Cambia al estado Puntuacion <puntos>
            }
            if (count >= 10)
                {
                // Display the win text.
                winTextObject.SetActive(true);
                // activo los bolean ganaste para llegar al final
                animator.SetInteger("puntos", 0); //hay que resetear el parametro puntos del animator a 0 para prevenir fallos con el anyState
                animator.SetBool("ganaste", true);

                // Destroy the enemy GameObject.
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                winTextObject.GetComponent<TextMeshProUGUI>().text = "HAS GANADO MAQUINA!!!";
        }
    } 

    /*
       void SetCountText() 
   {
       countText.text =  "Puntación: " + count.ToString();
        // Condicion de victoria del jugador que la variable count sea igual o mayor a 10
            if (count>= 5 && count<10)
            {
                animator.SetBool("puntuacion5", true);
            }
            if (count >= 10)
                {
                // Display the win text.
                winTextObject.SetActive(true);
                // activo los bolean de puntuacion5 y ganaste para llegar al final
                animator.SetBool("puntuacion5", true);
                animator.SetBool("ganaste", true);

                // Destroy the enemy GameObject.
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                winTextObject.GetComponent<TextMeshProUGUI>().text = "HAS GANADO MAQUINA!!!";
        }
    }   */

    /**
     * Salto con la barra espaciadora
     */
    void OnFire(){
        // cuando saltamos nos da un mensajito
        //Debug.Log("OnFire");
        //Debug.Log("Clicada la bolita SALTA");

        // Comprueba si el booleanno tocando
        if (TocandoSuelo())
        {
            // afuerza vertical aplicada a la bolita
            rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
            animator.SetTrigger("OnFireT");

            //otra manera de hacer el animator seria:
            //animator.SetBool("salta", true);
            //await Task.Delay(1500);
            //animator.SetBool("salta", false);
        }
    }
    // // Comprueba si el booleanno tocando suelo
    bool TocandoSuelo()
        {
            // Comprueba si el jugador esta tocando el suelo usando raycast
            return Physics.Raycast(transform.position, Vector3.down, 1.1f);
        }
    
    
    /**
    * FixedUpdate is called once per frame
    * it's called every frame
    * Difference between Update and FixedUpdate
    * https://learn.unity.com/tutorial/update-and-fixedupdate
    */
    private void FixedUpdate() 
    {
        // -------------------------------------
        //acelerometro damian
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        
        dir *= Time.deltaTime;
        transform.Translate(dir * speed, Space.World);
        //-------------------------------------

            // Detecta si se presiona la tecla G
    if (Input.GetKeyDown(KeyCode.G)) 
    {

        // Aumenta la escala del jugador
        transform.localScale *= 2.0f; // Duplica el tamaño del jugador
        Debug.Log("El jugador se ha hecho mas grande");
    }
        if (Input.GetKeyDown(KeyCode.H)) 
    {

        // Disminuye la escala del jugador
        transform.localScale /= 2.0f; // Duplica el tamaño del jugador
        Debug.Log("El jugador se ha hecho mas pequeño");
    }
        // jump with space bar
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnFire();
        }

        Vector3 movement = new Vector3 (speed*movementX, 0.0f , speed*movementY);

        
        // aplica la fuerza al jugador
        rb.AddForce(movement); 
    }

    // cuando el collider esta en las mismas coordenadar que el collider de otro objeto
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

    // Si toca un objeto con tag Booster,, aplica una fuerza en la dirección que indiquemos (dere/izq, arriba/abajo, adelante/atras)
    if (other.gameObject.CompareTag("Booster"))
    {
        Debug.Log("Has entrado en un Booster, empujando x20");

        //Se define un vector Vector3 que representa la dirección y magnitud del impulso.
        Vector3 boost = new Vector3(0, 0, 1) * 20f; // Hacia la adelante por que usamos el eje Z

        //rb es el Rigidbody del jugador, que permite aplicar fuerzas físicas.
        //AddForce(impulso, ForceMode.Impulse) aplica el vector de impulso al jugador.
        //ForceMode.Impulse significa que la fuerza se aplica de golpe, como si fuera una explosión o un empujón inmediato.
        rb.AddForce(boost, ForceMode.Impulse);

    }
    // Si toca un objeto con tag Acelerador multiplica la velocidad que tenia el objeto x20 en la direccion del vector que aplicase el jugador
    if (other.gameObject.CompareTag("Acelerador"))
    {
        Debug.Log("Has entrado en un Acelerador, velocidad aumentando x20");

        // Se crea un Vector3 llamado boost, que representa la dirección y magnitud del impulso.
        //.normalized: Convierte el vector en un vector unitario, lo que significa que mantiene su dirección, pero su magnitud es 1.
        // Esto garantiza que el impulso se aplique de manera uniforme sin importar si el jugador se mueve en diagonal o en línea recta.
        // Multiplica el vector normalizado por 20f para aumentar la velocidad x20 en la dirección en la que el jugador ya se estaba moviendo.
        Vector3 impulso = new Vector3(movementX, 0, movementY).normalized * 20f; // Ajusta la fuerza
        
        rb.AddForce(impulso, ForceMode.Impulse); // Aplica el impulso inmediato
    }
   }

    // Con esto hacemos la condicion de derrota, que es que nos toque el enemigo.
   private void OnCollisionEnter(Collision collision)
{
   if (collision.gameObject.CompareTag("Enemy")) //tenemos que crear el tag "Enemy" y añadirselo al EnemyBody en unity
   {
       // Destroy the current object
       //Destroy(gameObject); 
       // Desactiva el objeto en lugar de romperlo para que los estados no se vuelvan locos
        gameObject.SetActive(false);
       // Update the winText to display "You Lose!"
       animator.SetInteger("puntos", 0); //hay que resetear el parametro puntos del animator a 0 para prevenir fallos con el anyState
      
       animator.SetBool("perdiste", true);
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "Has perdido! Te ha matado: " + collision.gameObject.name + " !!!";
       
   }
}


}