using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;

    public enum Tipos { Leve, Medio, Pesado, Prender, Nada}
    public Tipos tipo;
    public enum Cores { Amarelo, Azul, Roxo, Verde}
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


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = GameManager.Instance.GetPedraSprite(tipo);

        if(cor == Cores.Amarelo)
        {
            spriteRenderer.color = Color.yellow;
            _rastro = rastroAmarelo;
        }
        else if(cor == Cores.Azul)
        {
            spriteRenderer.color = Color.blue;
            _rastro = rastroAzul;
        }
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude == 0)
        {
            _isPainting = false;
            if(tipo == Tipos.Prender)
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
        tmp.GetComponent<CircleCollider2D>().radius = 0;
        Invoke("dryPaint", _paintDelay - 0.01f);
    }

    private void dryPaint()
    {
        _ultimoPingo.GetComponent<CircleCollider2D>().radius = 0.1f;
    }
}
