using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float Fat = 2;
    public string owner;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pedra" && collision.gameObject.name != owner)
        {
            GameObject tmp = collision.gameObject;
            tmp.GetComponent<Rigidbody2D>().drag = 1 + Fat;
        }
    }
}
