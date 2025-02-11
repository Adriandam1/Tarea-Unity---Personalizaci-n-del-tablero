# Tarea-Unity---Personalizaci-n-del-tablero
**Enunciado:**

personaliza el tablero de juego con más obstáculos prueba el recorrido con la bola plantea varios niveles

Explica las personalizaciones y las interacciones en un Readme

Añade varios gif al Readme con diferentes fases del diseño del tablero

Pon el repositorio en la respuesta, con el Readme.


### Readme:

Los scripts están subidos al repositorio:

**CameraControler.cs**: controla la cámara que utilizará el jugador.

**PlayerControler.cs**: control del jugador(bolita azul claro), se mueve el estilo WASD con las flechas del teclado y salta con ESPACIO. Sistema de puntación y texto para visualizarla.

**Rotator.cs**: rota los objetivos que dan puntos(los cuadrados amarillos rotan en ángulos de 45º.)

---------------------------------------------------------------

**A continuación dejo una imagen de como quedaron los tableros:**

![unity1](https://github.com/user-attachments/assets/77cc99bd-b18a-40eb-b29a-56cf575e5a17)



Comienzas en un rectángulo inicial en el que tienes 3 cubitos que rotan sobre si mismos, nos sirven de puntuación.

El jugador (bolita azul claro) puede moverse y en contacto con los cubitos desaparecen y añaden 1 a la puntuación total.

Imagen de la vista del jugador:
![unity2](https://github.com/user-attachments/assets/e5dac2fd-22bb-47c6-ac97-63fc71a1767e)

---- **Aqui meter métodos con explicación**

* **Método que utilizamos para controlar la cámara del jugador:**

## 1) Control del jugador
  Lógicamente necesitamos poder movernos y lo hacemos en el scrip *PlayerControler.cs*

* **Método para controlar el movimiento del jugador:**
```bash
    void OnMove (InputValue movementValue) //este método se llama cuando se detecta un input de movimiento.
    {
       // Convierte el valor del input en Vector2 para el movimiento.
        Vector2 movementVector = movementValue.Get<Vector2>();
        //Guarda los valores de X e Y del componente de movimiento.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
        
    }
```

* **Método que permite al jugador saltar:**
```bash
    void OnFire(){
        // cuando saltamos nos da un mensajito por consola
        Debug.Log("OnFire");
        Debug.Log("Clicada la bolita SALTA");

        // la fuerza vertical aplicada a la bolita
        rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse); 
    }
```

  ## 2) Cámaras

  ### **Scripts de las camaras actualizados**

Los scripts de cámara(uno por cada cámara) funcionan con un scrip **CameraSwicther.cs** que nos permite cambiar de una camara a otra.

Creamos un array cameras, en el que introducimos el gameObject de **CameraManager**, ese array es leido por nuestro script **CameraSwitcher.cs**

![Screenshot_20250122_123004](https://github.com/user-attachments/assets/159a273b-862b-4cd5-842c-74585523c619)  ![Screenshot_20250122_122951](https://github.com/user-attachments/assets/c9946656-475b-4ba6-a474-931d7a6a4352)

Cuando el usuario pulse la tecla **C** sumaremos 1 al integer que usamos de indice, y cambiará a la siguiente cámara.

```bash
public class CameraSwitcher : MonoBehaviour{
    public Camera[] cameras; // Array para todas las cámaras
    private int currentCameraIndex = 0; // Índice de la cámara actual
    public GameObject CamaraTexto; //objeto CamaraTexto

    void Start(){
        ActivateCamera(currentCameraIndex);
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.C)){
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
                currentCameraIndex = 0;
            ActivateCamera(currentCameraIndex);
            TextoCamara(currentCameraIndex);}
    }
    void ActivateCamera(int index){
        for (int i = 0; i < cameras.Length; i++)
        {cameras[i].gameObject.SetActive(i == index);}
    }
```
También tenemos una función TextoCamara, que nos pondrá un texto indicando la cámara que estamos empleando.

```bash
    void TextoCamara(int index){
        if (index == 0){CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara por defecto";}
        if (index == 1){CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara Primera Persona";}
        if (index == 2){CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara Cenital";}
        if (index == 3){CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara Autonoma";}}
```
* #### Cámara por defecto:  
Consiste en la clásica cámara en tercera persona, que sigue al jugador manteniendo la distancia cuando este se mueve en un ángulo fijo.  
Para ello tomamos como referencia al objero *Player* y hacemos un vector *offset* que almacena la diferencia de posición entre la cámara y el jugador, manteniendo una distancia constante. Utilizamos el método *void LateUpdate()* para que se actualize cada vez que se mueve el jugador y mantenga la distancia.
<details><summary>🔍 Scrip CameraControler.cs</summary>  

    public class CameraControler : MonoBehaviour{
        // referencia al objeto jugador
        public GameObject player;
        // distancia entre la camara y el juegador
        private Vector3 offset;
        void Start()   // Método que llamamos cuando se inicia la aplicación.
        {
    
            // Calcula la posicion offset entre la camara y el jugador
            offset = transform.position - player.transform.position; 
        }
    
           void LateUpdate() // Último método que llamamos frame a frame.
        {
            // Para mantener la posición de la camara con respecto al jugador
            transform.position = player.transform.position + offset; 
        }
    }

</details>  

* #### Cámara en primera persona:

<details><summary>🔍 Scrip FirstPersonCameraControler.cs</summary>  
    
    public class FirstPersonCameraControler : MonoBehaviour{
        public float mouseSensitivity = 100f; // Sensibilidad del ratón
        public Transform playerBody; // Referencia al cuerpo del jugador
        public float distanceFromPlayer = 2f; // Distancia de la cámara respecto al jugador    
        private float xRotation = 0f; // Rotación en el eje X (arriba y abajo)
        private float yRotation = 0f; // Rotación en el eje Y (izquierda y derecha)        
        private Vector3 offset; // Offset para que la cámara se mantenga a una distancia fija del jugador
    
        void Start(){
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro
            offset = transform.position - playerBody.position; // Calcula la distancia inicial entre cámara y jugador
        }
    
        void Update(){
            // Captura el movimiento del ratón
            // Time.deltaTime -> tiempo de cada frame
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    
            // Controla la rotación vertical (arriba/abajo)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotación vertical            
    
            // Controla la rotación horizontal (izquierda/derecha) alrededor del eje Y del cuerpo del jugador
            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);
            
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);            
            
            // Actualiza la posición de la cámara para que siga al jugador.
            FollowPlayer();
        }
    
        // Método para que la cámara siga al jugador
        void FollowPlayer(){
            // La cámara sigue al jugador con la misma distancia y offset que calculamos inicialmente
            transform.position = playerBody.position + offset.normalized * distanceFromPlayer;
        }
    }

</details>  

* #### Cámara Cenital (vista desde arriba):  

<details><summary>🔍 Scrip CenitalCameraControler.cs</summary>      

    public class CenitalCameraControler : MonoBehaviour{
        public GameObject player; // Referencia al jugador
        public float height = 20f; // Altura de la cámara
        public float rotationSpeed = 10f; // Velocidad de rotación alrededor del jugador
    
        void Update(){
            // Mantén la cámara encima del jugador
            Vector3 offset = new Vector3(0, height, 0);
            transform.position = player.transform.position + offset;
    
            // Rota alrededor del jugador
            transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
            transform.LookAt(player.transform.position); // Mantén la cámara mirando al jugador
        }
    }

</details>  

* #### Cámara Autónoma(cámara independiente): 
La cámara autónoma es una cámara que esta fija en la plataforma inicial, al margen del jugador.
<details><summary>🔍 Scrip AutonomousCameraControler.cs</summary>  
    
        public class AutonomousCameraControler : MonoBehaviour{
            public GameObject player; // Referencia al jugador
            public Transform[] waypoints; // Puntos por los que se moverá la cámara
            public float speed = 5f; // Velocidad de movimiento
            private int currentWaypointIndex = 0;

            void Update(){
                // Mantén la cámara siguiendo al jugador mientras se mueve entre waypoints
                if (waypoints.Length == 0) return;

                Transform targetWaypoint = waypoints[currentWaypointIndex];
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
                transform.LookAt(player.transform.position); // La cámara siempre apunta al jugador

                // Si llega al waypoint actual, pasa al siguiente
                if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f){
                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                }
            }
        }
    
</details>  

### **Método que utilizamos cuando el JUGADOR toca los objetivos(puntos):** 
```bash  
       void SetCountText() 
   {
       countText.text =  "Puntación: " + count.ToString();
   }

   void OnTriggerEnter (Collider other) // trigger cuando comparta posicion con otro objeto
   {
        // cuando la bolita toque el objetivo pickup lo hacemos desaparecer
       if (other.gameObject.CompareTag("Pickup")) // condicion el otro objeto tenga el tag "Pickup"
       {
           other.gameObject.SetActive(false);   // desactiva el otro objeto
            // aumentamos el score en 1
           count = count + 1;
           SetCountText();   // llamamos a SetCountText
       }
   }
```
En resumen, cuando el objeto del jugador su radio de colider se posicione, en la misma posición(adyacente) que otro objeto que tenga el tag "Pickup", desactivamos dicho objeto Pickup y aumentamos el valor de la puntuacion llamando al metodo de la puntuación. La variable count esta iniciada en "on start" en 0.

* **Método utilizado para que los puntos(cubitos amarillos) roten sobre si mismos**
```bash
public class Rotator : MonoBehaviour
{

    void Update()
    {
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime); 
    }
}
```




**Imagen de la rampita(el cubo amarillo gigante es otro punto):**

![unity4](https://github.com/user-attachments/assets/2e4dc32d-64e0-47c7-9fbe-db034d423305)




## 3) Coleccionables (Pickups)  
En el playercontroler.cs crearemos una funcion **OnTriggerEnter (Collider other)**  que será la encargada de los objetos coleccionables que utilizaremos para ganar puntos y la partida.  
Cuando el collider del objeto Player coinciden sus coordenadas con la de otro objeto, comprueba si dicho objeto tiene el tag "*Pickup*", para ello lo asignamos en el unity y nos aseguramos de que tiene un collider:  
![objeto_Pickup](https://github.com/user-attachments/assets/b7acca9a-3c75-472a-9953-85777537f856)  

Cuando la condición se cumple aumentamos nuestra variable score en 1, para aumentar la puntuación.

```bash
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
```
## 4) Enemigo (AI Navigation)

## 5) Aceleradores y Boosters

## 6) Estados


