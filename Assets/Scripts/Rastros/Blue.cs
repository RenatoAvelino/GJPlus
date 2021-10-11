using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float Fat = .08f;
    [SerializeField]
    private float framesToSpeed = 2;
    private GameObject pedraT;
    private bool _isTimeToSpeed = false;
    public string owner;
    void Start()
    {
        framesToSpeed = framesToSpeed / (60.0f);
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
            tmp.GetComponent<Rigidbody2D>().drag = 1 - Fat;
            pedraT = tmp;
            _isTimeToSpeed = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pedra" && _isTimeToSpeed)
        {
            StartCoroutine(Countdown(framesToSpeed));
            _isTimeToSpeed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pedra")
        {
            pedraT = null;

        }
    }

    private IEnumerator Countdown(float duration)
    {
        while (true)
        {
            yield return new WaitForSeconds(duration);
            pedraT.GetComponent<Rigidbody2D>().drag -= Fat;
            if (pedraT.GetComponent<Rigidbody2D>().drag >= 0.5f)
                pedraT.GetComponent<Rigidbody2D>().drag = 0.5f;
            _isTimeToSpeed = true;
        }
    }
}
