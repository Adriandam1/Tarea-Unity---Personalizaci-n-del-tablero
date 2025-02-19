# Tarea-Unity---Personalizaci-n-del-tablero

## Índice  
1. [Enunciado e Introducción](#1-enunciado-e-introducción)  
2. [Control del jugador](#2-control-del-jugador)  
3. [Cámaras](#3-cámaras)  
4. [Coleccionables (Pickups)](#4-coleccionables-pickups)  
5. [Enemigo (AI Navigation)](#5-enemigo-ai-navigation)  
6. [Aceleradores y Boosters](#6-aceleradores-y-boosters)  
7. [Estados](#7-estados)
8. [APP móvil](#8-Acelerometro+(móvil))

## 1) Enunciado e Introducción

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

Resumen: el jugador(la bolita amarilla) tiene que ir recolectando los coleccionables(cuadraditos amarillos) hasta alcanzar la puntuacion de 10. Hay enemigos que lo persiguen en algunas zonas, así como obstaculo mortales que pueden hacerle perder la partida. Si el jugador es alcanzado por algun enemigo u obstaculo mortal, se pierde la partida y aparece un mensaje informativo en la pantalla indicando la causa de la derrota.Si el jugador se cae de las plataformas será teletransportado al inicio. Si el jugador logra esquivar los enemigos y trampas y obtener los 10 puntos, la partida acaba y el jugador recibe un mensaje informativo por pantalla.

<details><summary>🔍 SPOILER:</summary>  

**A continuación dejo una imagen de como quedaron los tableros:**

![unity1](https://github.com/user-attachments/assets/77cc99bd-b18a-40eb-b29a-56cf575e5a17)



Comienzas en un rectángulo inicial en el que tienes 3 cubitos que rotan sobre si mismos, nos sirven de puntuación.

El jugador (bolita azul claro) puede moverse y en contacto con los cubitos desaparecen y añaden 1 a la puntuación total.

Imagen de la vista del jugador:
![unity2](https://github.com/user-attachments/assets/e5dac2fd-22bb-47c6-ac97-63fc71a1767e)

</details>

---- **Aqui meter métodos con explicación**
<br><br>

[Volver al inicio](#índice) 

---------------------------------------------------------

## 2) Control del jugador
  Lógicamente necesitamos poder movernos y lo hacemos en el scrip *PlayerControler.cs*. A continuación describo los métodos para controlar el movimiento del jugador:
* El metodo ***OnMove***  se encarga de capturar y almacenar la entrada de movimiento del jugador para que posteriormente se pueda utilizar para mover el personaje o la bolita en el juego.
  - Entrada del jugador: Cuando el jugador mueve el joystick o presiona las teclas de dirección, el sistema de entrada detecta esa acción y llama al método OnMove.
  - Actualización de direcciones: OnMove extrae la dirección del movimiento y actualiza las variables movementX y movementY.
  - Aplicación del movimiento: Más tarde, en FixedUpdate(), se utiliza estas variables para formar un vector de movimiento que se multiplica por la velocidad y se aplica como fuerza al Rigidbody del objeto, logrando así el movimiento físico en el juego.

  <details><summary>🔍 SPOILER:</summary>  
  
      void OnMove (InputValue movementValue){ //este método se llama cuando se detecta un input de movimiento.    
         // Convierte el valor del input en Vector2 para el movimiento.
          Vector2 movementVector = movementValue.Get<Vector2>();
          //Guarda los valores de X e Y del componente de movimiento.
          movementX = movementVector.x; 
          movementY = movementVector.y;         
      }
  </details>
  <br><br>
* El método ***FixedUpdate*** en Unity se utiliza para actualizar la física del juego en intervalos fijos, lo que lo hace ideal para aplicar fuerzas o movimientos a objetos con un componente Rigidbody. En nuestro caso, el FixedUpdate() se engarca de la detección y manejo del salto y el calculo y la aplicación del movimiento:
  - Qué hace?: Verifica si el jugador ha presionado la tecla espacio. Si es así, llama al método *OnFire()*.
  - Se crea un vector de movimiento basado en las variables movementX y movementY, que almacenan la entrada del jugador.
      Se multiplica por la variable speed para ajustar la magnitud de la fuerza aplicada. Nótese que se asigna el componente movementX al eje X y movementY al eje Z (el eje Y se deja en 0) porque en muchos juegos en 3D el movimiento horizontal se realiza en el plano XZ, reservándose el eje Y para movimientos verticales como saltos.
  - Aplicación de la fuerza: Con rb.AddForce(movement); se aplica la fuerza calculada al Rigidbody del objeto, lo que causa el movimiento de la bolita según la física del juego.

  <details><summary>🔍 SPOILER:</summary>  
  
      private void FixedUpdate(){
          // saltar usando la barra espaciadora
          if (Input.GetKeyDown(KeyCode.Space)) {
              OnFire();
          }
          Vector3 movement = new Vector3 (speed*movementX, 0.0f , speed*movementY);        
          // aplica la fuerza al jugador
          rb.AddForce(movement); 
      }    
  </details>
  
<br><br>
* El método ***OnFire*** permite al jugador saltar. Cuando es llamado aplica una fuerza vertical con un Vector3.up a modo de impulso. Para evitar que el jugador pueda saltar en el aire, y solo tenga 1 salo, utilizamos *TocandoSuelo*. Comprobamos con un uso de RayCast la distancia con el suelo, de modo que si el jugador esta en el aire, no puede saltar.  
Ejemplo de RayCast:  
![raycast](https://github.com/user-attachments/assets/0fc09c2e-fa61-45ed-bc32-acda304e198d)


  <details><summary>🔍 SPOILER:</summary>  
  
      void OnFire(){
          if (TocandoSuelo())
          {
              // afuerza vertical aplicada a la bolita
              rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
              animator.SetTrigger("OnFireT");
          }
      }
  ```
      bool TocandoSuelo(){
            // Comprueba si el jugador esta tocando el suelo usando raycast
            return Physics.Raycast(transform.position, Vector3.down, 1.1f);
          }
  ```    
  </details>

<br><br>
* El método **RespawnPlayer**, se encarga de recolocar al jugador en su posición de inicioEn el caso de que el jugador se caiga de la plataforma necesitaremos recolocarlo.**RespawnPlayer** tiene su llamada en el **update** dicho y se ejecuta si se cumple la condición de que el jugador alcanza la posición vertical -1. Este método **RespawnPlayer** tambie cambia el estado del jugador, veremos los estados mas adelante.

  <details><summary>🔍 SPOILER:</summary>  
    
      void Update(){
      // Si la posición en Y es menor a 0, teletransporta al jugador a una posición segura
      if (transform.position.y < -1)
      {
          RespawnPlayer();
      }
    ```
      async void RespawnPlayer(){
          Debug.Log("El jugador se ha caído, transportándolo al inicio...");
          Caiste_mensaje.SetActive(true); // Canva informativo para el jugador
          animator.SetBool("transportado", true); // Condicion estado "transportado".
          
          await Task.Delay(1000);  // Espera de 1 segundos para la animación de teletransporte
  
          Vector3 respawnPosition = new Vector3(0, 3, 0); // vector3 con la posicion a transportar
          rb.MovePosition(respawnPosition); // Corrige la posición con Rigidbody
          rb.velocity = Vector3.zero; // baja velocidades a 0
          rb.angularVelocity = Vector3.zero; // baja velocidad angular a 0
  
          Caiste_mensaje.SetActive(false);
  
          await Task.Delay(750);  // Espera 0,75 segundos antes de volver al estado inicial
          animator.SetBool("transportado", false);
      }
    ```
  </details>

**Movimiento de jugador bolita amarilla**  
![movimiento](https://github.com/user-attachments/assets/da275400-f3ca-4108-9901-713142622e37)

      
<br><br>  

[Volver al inicio](#índice) 

---------------------------------------------
  ## 3) Cámaras

  ### **Scripts de las camaras actualizados**

Los scripts de cámara(uno por cada cámara) funcionan con un scrip **CameraSwicther.cs** que nos permite cambiar de una camara a otra.

Creamos un array cameras, en el que introducimos el gameObject de **CameraManager**, ese array es leido por nuestro script **CameraSwitcher.cs**

![Screenshot_20250122_123004](https://github.com/user-attachments/assets/159a273b-862b-4cd5-842c-74585523c619)  ![Screenshot_20250122_122951](https://github.com/user-attachments/assets/c9946656-475b-4ba6-a474-931d7a6a4352)

Cuando el usuario pulse la tecla **C** sumaremos 1 al integer que usamos de indice, y cambiará a la siguiente cámara.

**GIF CAMBIO DE CAMARA**  
![cambios_camara](https://github.com/user-attachments/assets/83ef0d00-eb79-4d57-838a-67d099bb7041)


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
* Cámara por defecto:  
  Consiste en la clásica cámara en tercera persona, que sigue al jugador manteniendo la distancia cuando este se mueve en un ángulo fijo. Para ello tomamos como referencia al objero *Player* y hacemos un vector *offset* que almacena la diferencia de posición entre la cámara y el jugador, manteniendo una distancia constante. Utilizamos el método *void LateUpdate()* para que se actualize cada vez que se mueve el jugador y mantenga la distancia.
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

<br><br>  
* Cámara en primera persona:
  La cámara que simula ser los ojos de nuestro jugador:

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

  <br><br>  
* Cámara Cenital:
  Cámara aérea que sigue a nuestro jugador desde el aire en un angulo de 90 grados:

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

<br><br>  
* Cámara Autónoma(cámara independiente): 
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

<br><br>

[Volver al inicio](#índice) 

------------------------------------------


## 4) Coleccionables (Pickups)  
Situados en *playercontroler.cs* también usaremos la funcion **OnTriggerEnter (Collider other)**  que será la encargada de los objetos coleccionables que utilizaremos para ganar puntos y la partida.  
Cuando el collider del objeto Player coinciden sus coordenadas con la de otro objeto, comprueba si dicho objeto tiene el tag "*Pickup*", para ello lo asignamos en el unity y nos aseguramos de que tiene un collider:  
![objeto_Pickup](https://github.com/user-attachments/assets/b7acca9a-3c75-472a-9953-85777537f856)  

Cuando la condición se cumple aumentamos nuestra variable score en 1, para aumentar la puntuación.:

<details><summary>🔍 Spoiler</summary>  
  
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
</details>  

En el método anterior llamamos a *SetCountText()* que simplemente nos cambia el canva que utiliza el jugador para comprobar si puntuación. Y nos sirve para añadir una condición para ganar la partida, en este caso que el jugador llegue a 10 de puntuacición:  

       void SetCountText(){
         countText.text =  "Puntación: " + count.ToString();
        if (count >= 10){
          winTextObject.SetActive(true);// Muestra el texto de victoria  
          Destroy(GameObject.FindGameObjectWithTag("Enemy")); // Destruye los objetos con el tag "enemy"
          winTextObject.GetComponent<TextMeshProUGUI>().text = "HAS GANADO MAQUINA!!!"; // Reescribe el texto de victoria winTextObject
          }
       } 
       
<br><br>

En resumen, cuando el objeto del jugador su radio de colider se posicione, en la misma posición(adyacente) que otro objeto que tenga el tag "Pickup", desactivamos dicho objeto Pickup y aumentamos el valor de la puntuacion llamando al metodo de la puntuación. La variable count esta iniciada en "on start" en 0.

**GIF JUGADOR COGIENDO PICKUPS**  
![Grabación de pantalla - Feb 12, 2025-VEED](https://github.com/user-attachments/assets/889d56ec-9450-402b-b4ce-5b5ec4985841)




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

<br><br>

[Volver al inicio](#índice) 

-----------------------------------------

## 5) Enemigo (AI Navigation)  
Lo primero que necesitamos es instalar en Unity el paquete de AI Navigation  
En el menu de unity vamos a Window -> PackageManager -> Packages Unity Registry -> escribimos 'ai' e instalamos “AI Navigation”  
![navigation](https://github.com/user-attachments/assets/42c246af-39eb-49b8-908e-7cb362c34033)
<br><br>

A nuestros enemigos los vamos a controlar con el scrip ***EnemyMovement.cs***  
Lo primero que necesitamos es importar AI del motor de unity ***using UnityEngine.AI;***  
En unity, crearemos nuestro objeto para nuestro enemigo, al que le daremos el **tag *enemy***, un **navMeshAgent**, e indicaremos que debe dirigirse a nuestro objeto Player, para ellos arrastaremos nuestro objeto Player al insprector, así mismo  tenemos que añadir el componente ***NavMeshSurface*** a la zona en la que se encontrará nuestro enemigo para que pueda navegar por ella correctamente, una vez colocados todos los elementos de la zona debemos acordarnos de clicar en "**Bake**".

![Screenshot_20250212_115615](https://github.com/user-attachments/assets/8d1dc0f9-bdd0-44e3-879e-ef8c7dec8d9a)   ![enemy](https://github.com/user-attachments/assets/3e9075e1-357a-4e30-b406-ccede4fc050c)

<details><summary>🔍 EnemyMovement.cs</summary>  

```bash
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AÑADIMOS para la AI
// Hemos arrastrado el GameObject "Player" al playerslot del objeto "Enemy"
public class EnemyMovement : MonoBehaviour
{

    public Transform Player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // Asignamos la variable NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Indicamos al enemigo que debe ir a las coordenadas del player
        // La "destination" del enemigo se actualizara a la posición de Player cada frame
        if (Player != null)
            {    
            navMeshAgent.SetDestination(Player.position);
            //Debug.Log(Player.position); // posicion del jugador en consola
            }
    }
}
```

</details>  

* Interaccion enemigo-jugador
  Con este scrip hemos creado el movimiento del enemigo, que va a localizar la posicion del jugador y moverse hacia el en su zona de navegación. Pero ahora necesitamos que el enemigo haga cosas, así que vamos a hacer que cuando el enemigo nos toque perdamos la partida, para ello nos dirigimos a nuestro *PlayerControler.cs* y creamos un método **OnCollisionEnter**.  
  Nuestro método **OnCollisionEnter** se encargará de comprobar el tag de los objetos que colisionen con nuestro jugador, y cuando sea "*Enemy*" destruirá el objeto Player y mostrará un mensaje por pantalla al jugador indicandole que ha perdido la partida:  
  ```bash
     private void OnCollisionEnter(Collision collision){
       if (collision.gameObject.CompareTag("Enemy")) //tenemos que crear el tag "Enemy" y añadirselo al EnemyBody en unity
       {
           Destroy(gameObject); // Destruye el objeto jugador
           winTextObject.gameObject.SetActive(true); // Activa el canva que usamos para el mensaje de victoria o derrota
           winTextObject.GetComponent<TextMeshProUGUI>().text = "Has perdido! Te ha matado: " + collision.gameObject.name + " !!!"; // Pinta nuestro mensaje de derrota                
       }
  ```
<br><br>
**ENEMY COMIENDO JUGADOR**  
![enemigo](https://github.com/user-attachments/assets/a6765c03-32f7-46ef-8240-462d1002e37b)


<br><br>

[Volver al inicio](#índice) 

-----------------------------------------

## 6) Aceleradores y Boosters  
En nuestro juego tambien tenemos elementos como aceleradores y boosters con los que interactuará nuestro jugador, manejando esto en nuestro *PlayerControler.cs* empleamos los siguientes metodos:

* El método **OnTriggerEnter** tiene varias funcionalidades en nuestro juego, con respecto al movimiento del jugador lo empleamos para nuestros *aceleradores* y *boosters*.  
  - Un **acelerador**, lo entendemos como un objeto que, al hacer contacto, aumenta nuestra velocidad actual. Para ello utilizamos nuestro metodo *OnTriggerEnter(Collider other)* en el que compararemos el **tag** del objeto(que hemos indicado en el inspector de unity) con el que ha colisionado nuestro jugador, si se rorresponde con *acelerador* se crea un vector3 que recoge y multiplica la velocidad del jugador, y la aplica al rigibody del jugador a modo de impulso.
  
  ![acelerador](https://github.com/user-attachments/assets/1c9bc00f-4299-4226-9312-52b8e9ec34bc)

  
    <details><summary>🔍 SPOILER:</summary>  
      
        if (other.gameObject.CompareTag("Acelerador")){
          Debug.Log("Has entrado en un Acelerador, velocidad aumentando x20");
  
          // Se crea un Vector3 llamado boost, que representa la dirección y magnitud del impulso.
          //.normalized: Convierte el vector en un vector unitario, lo que significa que mantiene su dirección, pero su magnitud es 1.
          // Esto garantiza que el impulso se aplique de manera uniforme sin importar si el jugador se mueve en diagonal o en línea recta.
          // Multiplica el vector normalizado por 20f para aumentar la velocidad x20 en la dirección en la que el jugador ya se estaba moviendo.
          Vector3 impulso = new Vector3(movementX, 0, movementY).normalized * 20f; // Ajusta la fuerza
          
          rb.AddForce(impulso, ForceMode.Impulse); // Aplica el impulso inmediato
        }
    
    </details>

  - Un **booster** es básicamente un acelerador, pero que aplica la fuerza en una dirección específica, en el caso de ejemplo, en dirección Z positiva:
      <details><summary>🔍 SPOILER:</summary>  
      
        if (other.gameObject.CompareTag("Booster")){
            Debug.Log("Has entrado en un Booster, empujando x20");
    
            //Se define un vector Vector3 que representa la dirección y magnitud del impulso.
            Vector3 boost = new Vector3(0, 0, 1) * 20f; // Hacia la adelante por que usamos el eje Z
    
            //rb es el Rigidbody del jugador, que permite aplicar fuerzas físicas.
            //AddForce(impulso, ForceMode.Impulse) aplica el vector de impulso al jugador.
            //ForceMode.Impulse significa que la fuerza se aplica de golpe, como si fuera una explosión o un empujón inmediato.
            rb.AddForce(boost, ForceMode.Impulse);
        }
    
      </details>

<br><br>

[Volver al inicio](#índice) 

-----------------------------------------

## 7) Estados


**ESTADOS en funcionamiento**

<details><summary>🔍 SPOILER:</summary>  
  
https://github.com/user-attachments/assets/377abb6f-8649-4e3f-afb8-7e62f1e9b1be

</details>

Existen diferentes formas de manejar nuestros estados, demostradas en el video anterior, lo primero que necesitamos es crear nuestro animator:  
En la ventana del proyecto le damos a click derecho -> create -> Animator Controler  
Una vez dentro de la ventana de animator lo primero que debemos definir son nuestros parametros:  
![animator](https://github.com/user-attachments/assets/42f856be-4e80-461f-babd-812beb018c51)

Como vemos hay de varios tipos, pondremos de ejemplo los parametros Trigger, Bool e Int:
<br><br>
**OnTrigger** esto nos permite cambiar a un estado durante un pulso, para ello crearemos un parametros Trigger, y añadiremos una transición al estado objetivo con la condicion del trigger  
En nuestro codigo elegimos la funcion que queremos que nos haga de trigger y añadimos la linea:
```bash
animator.SetTrigger("OnFireT");
```
<br><br>
**Booleano** crearemos un parametro Boolean, esto nos permitirá hacer una transición de estado cuando nuestro parametro booleano este true o false, segun lo indiquemos  
En nuestro codigo solo necesitamos:
```bash
animator.SetBool("ganaste", true); // cambiamos el parametro booleano a true, esto hace que se cambie al estado Victoria
```
<br><br>
**Integer** crearemos un parametro Int, esto nos permitirña hacer una transición de estado cuando nuestro parametro Integer cumpla la condición que le indiquemos, vease que sea mayor que, igual, menor-igual que...  
En nuestro codigo solo necesitamos:
```bash
animator.SetInteger("puntos", count); //Iguala el parametro de estados 'puntos' al 'count'(que es nuestra variable de puntuación). Cambia al estado Puntuacion <puntos>
```
<br><br>

Con esto ya podemos crear nuestros estados con la interfaz gráfica del animator de unity, solamente tenemos que crear los estados con click derecho, y luego también con click derecho en un estado creamos la transición a otro empleando los parametros que creamos previamente.  
Imagen ejemplo estados de partida:  
![transiciones](https://github.com/user-attachments/assets/18b69f71-ffe5-4032-98e2-b710204f1a59)  
En la imagen superior comprobamos que podemos añadir condiciones a las transiciones creadas.

<br><br>

[Volver al inicio](#índice) 


## 8) Acelerometro (móvil)
El acelerometro del teléfono nos permite dar ordenes de movimiento a nuestro player, en el siguiente spoiler detallo los pasos para preparar nuestro juego para pasarlo a un dispositivo móvil.

<details><summary>🔍 SPOILER Paso a paso creacion APK:</summary>  

Lo primero que vamos a necesitar es instalar el modulo de Android para nuestra version de unity

Vamos en el menu de proyects a la pestaña installs, elegimos nuestra version de unity (en este caso es la Unity (2022.3.55f1) y clicamos en la ruedita -> Add modules.  
![installs](https://github.com/user-attachments/assets/fc76f401-37f8-4995-b073-85a0d262bed2)


Elegimos Android Build Support, ya que usaremos android, si da error hay que darle a reintentar  
![openjdk](https://github.com/user-attachments/assets/dc66af83-ccfe-472c-9df0-9e4957c5e9cc)



Abrimos el proyecto y vamos a File -> build settings -> elegimos android 
primero le damos abajo a le damos abajo a switch platform, esto te comprime las texturas para el telefono  
tras hacerlo ajustamos los player settings en la esquina inferior izquierda, aqui podemos:
 - **Importante**: para que nos funcionen los controles en el telefono debemos ir a **other settings** y cambiar **Active Input Handling** a **Input Manager(old)** para usar los controles del teléfono en lugar de los del pc
 - asignar el nombre de nuestra app
 - Resolution and presentation -> Orientation: podemos configurar si la pantalla de juego rotará cuando rote el telefono o la resolucion y como queremos que tenga el aspecto nuestro juego.
 - Más cosas...

![build](https://github.com/user-attachments/assets/d0f2a65a-1e9e-469e-8a44-88276decd0d7)

Una vez lo tengamos todo le damos a build y esto nos creará el .apk solo tenemos que descargarlo en nuestro dispositivo e instalarlo para poder jugar.

</details>

Lógicamente nuestros scripts de movimiento previos no funcionan para la versión de teléfono movil, ya que no tenemos teclado ni raton. Para hacer uso del acelerometro del móvil con el objetivo de mover nuestro player utilizaremos el siguiente código en nuestro scrip de PlayerControler.cs:

Lo añadimos en nuestra función *update()* o *FixedUpdate()*
```bash
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.x;
        dir.z = Input.acceleration.y;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        
        dir *= Time.deltaTime;
        //transform.Translate(dir * speed);
        transform.Translate(dir * speed, Space.World);
```

<br><br>

[Volver al inicio](#índice) 






