using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;

    public enum Tipos { Leve, Medio, Pesado, Prender, Nada }
    public Tipos tipo;
    public enum Cores { Amarelo, Azul, Roxo, Verde }
    public Cores cor;

    public int _indexOwner; //Player 1 = 0, Player 2 = 1
    public GameObject _rastro;
    private GameObject _ultimoPingo;
    [SerializeField]
    private float _paintDelay = .8f;
    private float _timer = 0;
    private bool _isPainting = true;

    public GameObject rastroAmarelo;
    public GameObject rastroAzul;

    public SpriteRenderer spriteRenderer;

    [FMODUnity.EventRef]
    public string PedraImpactoPedra = "event:/ImpactoPedraPedra";
    public string PedraImpactoObstaculo = "event:/ImpactoPedraObstaculo";
    public string Escorrega = "event:/Escorrega";

    float fatorVelocidade = 0.016F;
    float impactoVelocidade;

    FMOD.Studio.EventInstance EventoImpactoPedra;
    FMOD.Studio.EventInstance EventoImpactoObstaculo;
    FMOD.Studio.EventInstance EventoEscorrega;

    float Velocidade;

    public Vector3 force;

    // Start is called before the first frame update
    void Start()
    {


        spriteRenderer.sprite = GameManager.Instance.GetPedraSprite(tipo);


        if (tipo == Tipos.Leve)
        {
            rb.mass = 0.1f;
            rb.drag = 0.5f;
        }
        if (tipo == Tipos.Medio)
        {
            rb.mass = 0.2f;
            rb.drag = 1f;
        }
        if (tipo == Tipos.Pesado)
        {
            rb.mass = 0.3f;
            rb.drag = 1.5f;
        }
        if (tipo == Tipos.Prender)
        {
            rb.mass = 0.2f;
            rb.drag = 1f;
        }

        if (cor == Cores.Amarelo)
        {
            spriteRenderer.color = Color.yellow;
            _rastro = rastroAmarelo;
        }
        else if (cor == Cores.Azul)
        {
            spriteRenderer.color = Color.blue;
            _rastro = rastroAzul;
        }
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);

        print(rb.velocity.magnitude);

        Velocidade  = fatorVelocidade*rb.velocity.magnitude;
        EventoImpactoPedra = FMODUnity.RuntimeManager.CreateInstance(PedraImpactoPedra);
        EventoImpactoObstaculo = FMODUnity.RuntimeManager.CreateInstance(PedraImpactoObstaculo);
        EventoEscorrega = FMODUnity.RuntimeManager.CreateInstance(Escorrega);
        EventoEscorrega.start();
        EventoEscorrega.setParameterByName("Velocidade", Velocidade);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(EventoEscorrega, transform, rb);

        fatorVelocidade = Velocidade / rb.velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Velocidade = fatorVelocidade*rb.velocity.magnitude;
        EventoEscorrega.setParameterByName("Velocidade", Velocidade);

        if (rb.velocity.magnitude <= .2f)
        {
            _isPainting = false;
            rb.velocity = Vector2.zero;

            if (tipo == Tipos.Prender)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
            GameManager.Instance.PedraStopped();
            enabled = false;
        }
        if (_isPainting)
        {
            _timer += Time.deltaTime;
            if(_timer >= _paintDelay)
            {
                _timer -= _paintDelay;
                Paint();
            }
        }
    }

    private void Paint()
    {
        GameObject tmp = Instantiate(_rastro);
        _ultimoPingo = tmp;
        tmp.transform.position = this.transform.position;
        if(tmp.name == "Blue(Clone)")
        {
            tmp.GetComponent<Blue>().owner = this.gameObject.name;
        }else if(tmp.name == "Yellow(Clone)")
        {
            tmp.GetComponent<Yellow>().owner = this.gameObject.name;
        }
        tmp.GetComponent<CircleCollider2D>().radius = 0;
        Invoke("dryPaint", _paintDelay - 0.01f);
    }

    private void dryPaint()
    {
        _ultimoPingo.GetComponent<CircleCollider2D>().radius = 0.1f;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pedra")
        {
            impactoVelocidade = Velocidade;
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(EventoImpactoPedra, transform, rb);
            EventoImpactoPedra.setParameterByName("VelocidadeImpacto", impactoVelocidade);
            EventoImpactoPedra.start();
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            impactoVelocidade = Velocidade;
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(EventoImpactoObstaculo, transform, rb);
            EventoImpactoObstaculo.setParameterByName("VelocidadeImpacto", impactoVelocidade);
            EventoImpactoObstaculo.start();
        }
        else if (collision.gameObject.tag == "Respawn")
        {

        }
    }
}
