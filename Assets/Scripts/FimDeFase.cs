using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FimDeFase : MonoBehaviour
{
    public Animator animatorTrofeu;
    public GameObject telaVitoria;
    public AudioSource audioSource;
    public AudioClip somVitoria;
    private bool jaAtivou = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !jaAtivou)
        {
            jaAtivou = true;
            if (audioSource != null && somVitoria != null) audioSource.PlayOneShot(somVitoria);

            Rigidbody2D rbPlayer = collision.GetComponent<Rigidbody2D>();
            if (rbPlayer != null)
            {
                rbPlayer.linearVelocity = Vector2.zero;
                rbPlayer.simulated = false; // Desliga a física dele
            }

            if (animatorTrofeu != null) animatorTrofeu.SetTrigger("Ativou");
            if (telaVitoria != null) telaVitoria.SetActive(true);

            StartCoroutine(RotinaTransicao());
        }
    }

    IEnumerator RotinaTransicao()
    {
        yield return new WaitForSeconds(2.0f); // Espera 2 segundos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}