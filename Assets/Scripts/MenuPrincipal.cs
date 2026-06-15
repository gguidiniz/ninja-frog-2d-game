using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Importa o Novo Input System

public class MenuPrincipal : MonoBehaviour
{
    void Update()
    {
        // Verifica se existe um teclado conectado e se a tecla de Espaço foi pressionada neste exato frame
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Carrega a Fase 1 (índice 0 no Build Profiles)
            SceneManager.LoadScene(0);
        }
    }
}