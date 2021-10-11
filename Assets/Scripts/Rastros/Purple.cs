using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            Vector2 randN = new Vector2(Random.Range(0, 4.3f), Random.Range(0, 4.3f));
            tmp.gameObject.GetComponent<Rigidbody2D>().AddForce(randN, ForceMode2D.Impulse);
        }
    }
}
