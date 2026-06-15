using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 8f;
    public float forcaDoPulo = 12f;

    [Header("Configurações de Escada")]
    public float velocidadeEscada = 5f;

    [Header("Configurações de Chão")]
    public Transform pontoDoChao;
    public float raioDoChao = 0.2f;
    public LayerMask layerDoChao;

    [Header("Efeitos e Áudio")]
    public GameObject efeitoColetaPrefab;
    public AudioSource audioSource;
    public AudioClip somPulo;
    public AudioClip somColeta;
    public AudioClip somDano;

    [Header("UI")]
    public TMP_Text textoPontuacao;
    public Image[] imagensVidas;
    public GameObject telaGameOver;
    
    private static int pontuacao = 0;
    private static int vidas = 3;

    private Rigidbody2D rb;
    private Animator animator;
    
    private float movimentoHorizontal;
    private float movimentoVertical;
    
    private bool estaNoChao;
    private bool pertoDaEscada;
    private bool escalando;
    private float gravidadeOriginal;
    private bool isDead = false;
    private bool aguardandoReiniciar = false;

    private InputAction movimentoAcao;
    private InputAction puloAcao;
    private InputAction subirAcao;

    void Awake()
    {
        movimentoAcao = new InputAction("Movimento");
        movimentoAcao.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d")
            .With("Negative", "<Keyboard>/leftArrow")
            .With("Positive", "<Keyboard>/rightArrow");

        subirAcao = new InputAction("Subir");
        subirAcao.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/s")
            .With("Positive", "<Keyboard>/w")
            .With("Negative", "<Keyboard>/downArrow")
            .With("Positive", "<Keyboard>/upArrow");

        puloAcao = new InputAction("Pulo", binding: "<Keyboard>/space");
    }

    void OnEnable()
    {
        movimentoAcao.Enable();
        subirAcao.Enable();
        puloAcao.Enable();
    }

    void OnDisable()
    {
        movimentoAcao.Disable();
        subirAcao.Disable();
        puloAcao.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravidadeOriginal = rb.gravityScale;
        
        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Frutas: " + pontuacao;
        }
        AtualizarHUDVidas();
        
        // Garante que a tela de Game Over comece desativada
        if (telaGameOver != null)
        {
            telaGameOver.SetActive(false);
        }
    }

    void Update()
    {
        if (aguardandoReiniciar)
        {
            if (puloAcao.WasPressedThisFrame())
            {
                vidas = 3;
                pontuacao = 0;
                aguardandoReiniciar = false;
                
                if (telaGameOver != null) telaGameOver.SetActive(false);
                
                SceneManager.LoadScene(0);
            }
            return;
        }

        if (isDead) return;

        if (pontoDoChao != null)
        {
            estaNoChao = Physics2D.OverlapCircle(pontoDoChao.position, raioDoChao, layerDoChao);
        }

        movimentoHorizontal = movimentoAcao.ReadValue<float>();
        movimentoVertical = subirAcao.ReadValue<float>();

        if (movimentoHorizontal > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (movimentoHorizontal < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (animator != null)
        {
            animator.SetBool("isRunning", Mathf.Abs(movimentoHorizontal) > 0f);
            
            animator.SetBool("isJumping", !estaNoChao && !escalando); 
            
            animator.SetBool("isClimbing", escalando);
        }

        if (pertoDaEscada && Mathf.Abs(movimentoVertical) > 0f)
        {
            escalando = true;
        }

        if (puloAcao.WasPressedThisFrame() && estaNoChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaDoPulo);
            if (audioSource != null && somPulo != null) audioSource.PlayOneShot(somPulo);
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (escalando)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidade, movimentoVertical * velocidadeEscada);
        }
        else
        {
            rb.gravityScale = gravidadeOriginal;
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidade, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            pertoDaEscada = true;
        }
        else if (collision.CompareTag("Coletavel"))
        {
            if (efeitoColetaPrefab != null)
            {
                Instantiate(efeitoColetaPrefab, collision.transform.position, Quaternion.identity);
            }
            if (audioSource != null && somColeta != null) audioSource.PlayOneShot(somColeta);
            Destroy(collision.gameObject);
            
            pontuacao++;
            if (textoPontuacao != null)
            {
                textoPontuacao.text = "Frutas: " + pontuacao;
            }
        }
        else if (collision.CompareTag("Espinho"))
        {
            if (isDead) return;

            isDead = true;
            vidas--;
            AtualizarHUDVidas();
            if (audioSource != null && somDano != null) audioSource.PlayOneShot(somDano);

            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;

            if (animator != null)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isClimbing", false);
                animator.SetTrigger("Morreu");
            }

            StartCoroutine(RotinaMorte());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            pertoDaEscada = false;
            escalando = false;
        }
    }

    private void AtualizarHUDVidas()
    {
        if (imagensVidas == null) return;

        for (int i = 0; i < imagensVidas.Length; i++)
        {
            if (imagensVidas[i] != null)
            {
                imagensVidas[i].enabled = i < vidas;
            }
        }
    }

    private IEnumerator RotinaMorte()
    {
        yield return new WaitForSeconds(0.5f);

        if (vidas > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Ativa o Game Over e espera o input do jogador no Update
            aguardandoReiniciar = true;
            if (telaGameOver != null) telaGameOver.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pontoDoChao != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pontoDoChao.position, raioDoChao);
        }
    }
}
