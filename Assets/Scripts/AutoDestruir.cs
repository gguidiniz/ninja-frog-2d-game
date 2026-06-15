using UnityEngine;

public class AutoDestruir : MonoBehaviour
{
    void Start()
    {
        // Destrói este objeto automaticamente após 0.5 segundos (tempo da animação)
        Destroy(gameObject, 0.5f);
    }
}