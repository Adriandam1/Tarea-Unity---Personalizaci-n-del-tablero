using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array para todas las cámaras
    private int currentCameraIndex = 0; // Índice de la cámara actual

    public GameObject CamaraTexto; //objeto CamaraTexto

    void Start()
    {
        // Asegúrate de que solo una cámara esté activa al inicio
        ActivateCamera(currentCameraIndex);
    }

    void Update()
    {
        // Cambiar de cámara al presionar el botón (por defecto 'C')
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
                currentCameraIndex = 0;

            ActivateCamera(currentCameraIndex);
            TextoCamara(currentCameraIndex);
        }
    }

    void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }

    // metodo para cambiar el texto de la camara
    // coge el componente de TextoCamara
    void TextoCamara(int index){
        if (index == 0){
            CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara por defecto";
        }
        if (index == 1){
            CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara Primera Persona";
        }
        if (index == 2){
            CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara Cenital";
        }
        if (index == 3){
            CamaraTexto.GetComponent<TextMeshProUGUI>().text = "Cámara Autonoma";
        }

    }

}

