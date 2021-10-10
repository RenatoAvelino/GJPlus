using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    public enum Tipos { Leve, Medio, Pesado, Prender}
    public Tipos tipo;
    public int _indexOwner; //Player 1 = 0, Player 2 = 1
    public GameObject _rastro;
    [SerializeField]
    private float _paintDelay = .8f;
    private float _timer = 0;
    private bool _isPainting = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude == 0)
        {
            _isPainting = false;
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
        tmp.transform.position = this.transform.position;
    }
}
