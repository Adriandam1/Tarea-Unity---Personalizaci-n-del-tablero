# Tarea-Unity---Personalizaci-n-del-tablero
personaliza el tablero de juego con más obstáculos
prueba el recorrido con la bola
plantea varios niveles
Explica las personalizaciones y las interacciones en un Readme

Añade varios gif al Readme con diferentes fases del diseño del tablero

Pon el repositorio en la respuesta, con el Readme.


### Readme:

Los scripts están subidos al repositorio:

**CameraControler**: controla la cámara que utilizará el jugador.

**PlayerControler**: control del jugador(bolita azul claro), se mueve con WASD y salta con ESPACIO.

**Rotator**: rota los objetivos que dan puntos(los cuadrados amarillos rotan en ángulos de 45º.

---------------------------------------------------------------

**A continuación dejo una imagen de como quedaron los tableros:**

![unity1](https://github.com/user-attachments/assets/77cc99bd-b18a-40eb-b29a-56cf575e5a17)



Comienzas en un rectángulo inicial en el que tienes 3 cubitos que rotan sobre si mismos, nos sirven de puntuación.

El jugador (bolita azul claro) puede moverse y en contacto con los cubitos desaparecen y añaden 1 a la puntuación total.

Imagen de la vista del jugador:
![unity2](https://github.com/user-attachments/assets/e5dac2fd-22bb-47c6-ac97-63fc71a1767e)


**Método que utilizamos cuando el jugador toca las pelotas:** 
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
   }
```


En la plataforma de la izquierda tenemos varias pelotas negras con cuerpo fisico rigidbody y con una masa muy reducida para que podamos chocar con ellas y patearlas a gusto.

Tambien tenemos un arbolito pelado y con una abertura estrategicamente pequeña, podemos avanzar hacia nuestra rampa gigante, las pelotas negras son demasiado grandes y no caben por lo que no tenemos peligro de lanzarlas al vacio.

![unity3](https://github.com/user-attachments/assets/c9e6d2ba-0445-4f50-83ce-a1cd947ac502)

**Imagen de la rampita(el cubo amarillo gigante es otro punto):**

![unity4](https://github.com/user-attachments/assets/2e4dc32d-64e0-47c7-9fbe-db034d423305)









