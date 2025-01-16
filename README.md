# Tarea-Unity---Personalizaci-n-del-tablero
personaliza el tablero de juego con más obstáculos
prueba el recorrido con la bola
plantea varios niveles
Explica las personalizaciones y las interacciones en un Readme

Añade varios gif al Readme con diferentes fases del diseño del tablero

Pon el repositorio en la respuesta, con el Readme.


### Readme:

Los scripts están subidos al repositorio:

**CameraControler.cs**: controla la cámara que utilizará el jugador.

**PlayerControler.cs**: control del jugador(bolita azul claro), se mueve con WASD y salta con ESPACIO.

**Rotator.cs**: rota los objetivos que dan puntos(los cuadrados amarillos rotan en ángulos de 45º.

---------------------------------------------------------------

**A continuación dejo una imagen de como quedaron los tableros:**

![unity1](https://github.com/user-attachments/assets/77cc99bd-b18a-40eb-b29a-56cf575e5a17)



Comienzas en un rectángulo inicial en el que tienes 3 cubitos que rotan sobre si mismos, nos sirven de puntuación.

El jugador (bolita azul claro) puede moverse y en contacto con los cubitos desaparecen y añaden 1 a la puntuación total.

Imagen de la vista del jugador:
![unity2](https://github.com/user-attachments/assets/e5dac2fd-22bb-47c6-ac97-63fc71a1767e)

---- **Aqui meter métodos con explicación**

* **Método que utilizamos para controlar la cámara del jugador:**

```bash
public class CameraControler : MonoBehaviour
{
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
```

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

* **Método que utilizamos cuando el JUGADOR toca las pelotas:** 
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
En resumen, cuando el objeto del jugador su radio de colider se posicione, en la misma posición(adyacente) que otro objeto que tenga el tag "Pickup", desactivamos dicho objeto Pickup y aumentamos el valor de la puntuacion llamando al metodo de la puntuación.

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


En la plataforma de la izquierda tenemos varias pelotas negras con cuerpo fisico rigidbody y con una masa muy reducida para que podamos chocar con ellas y patearlas a gusto.

Tambien tenemos un arbolito pelado y con una abertura estrategicamente pequeña, podemos avanzar hacia nuestra rampa gigante, las pelotas negras son demasiado grandes y no caben por lo que no tenemos peligro de lanzarlas al vacio.

![unity3](https://github.com/user-attachments/assets/c9e6d2ba-0445-4f50-83ce-a1cd947ac502)

**Imagen de la rampita(el cubo amarillo gigante es otro punto):**

![unity4](https://github.com/user-attachments/assets/2e4dc32d-64e0-47c7-9fbe-db034d423305)









