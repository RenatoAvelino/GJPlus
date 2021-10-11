using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float Radius = 3f;
    private CircleCollider2D _ring;
    public GameController _gc;
    [SerializeField]
    private int score;

    [FMODUnity.EventRef]
    public string Pontos = "event:/Pontuacao";
    FMOD.Studio.EventInstance PontosEvento;


    void Start()
    {
        PontosEvento = FMODUnity.RuntimeManager.CreateInstance(Pontos);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PontosEvento, transform, new Rigidbody2D ());
        _ring = this.GetComponent<CircleCollider2D>();
        _ring.radius = Radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pedra")
        {
            PontosEvento.setParameterByName("Pontos", score);
            PontosEvento.start();
            GameObject tmp = collision.gameObject;
            int index = tmp.GetComponent<Pedra>()._indexOwner;
            //Debug.Log(index + " Entrou");
            _gc.SetScore(index, score);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pedra")
        {
            PontosEvento.setParameterByName("Pontos", 0);
            PontosEvento.start();
            GameObject tmp = collision.gameObject;
            int index = tmp.GetComponent<Pedra>()._indexOwner;
            //Debug.Log(index + " Saiu");
            _gc.SetScore(index, score * (-1));
        }
    }
}
