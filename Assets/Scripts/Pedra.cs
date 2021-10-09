using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    private Rigidbody2D rb;

    public enum Tipos { Leve, Medio, Pesado, Prender}
    public Tipos tipo;
    public int _indexOwner; //Player 1 = 0, Player 2 = 1

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude == 0)
        {
            //print("Parou");
        }
    }
}
