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
    void Start()
    {
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
            GameObject tmp = collision.gameObject;
            int index = tmp.GetComponent<Pedra>()._indexOwner;
            //Debug.Log(index + " Saiu");
            _gc.SetScore(index, score * (-1));
        }
    }
}
