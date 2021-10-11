using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [Header("Características do Arremesso")]
    public float angle = 0f;
    public float actualForce = 0f;
    public float maxForce = 10f;
    public Vector3 dir;
    private int indexPedra = 0;

    [Range(-180f, -90f)]
    public float minAngle;
    [Range(-90f, 0f)]
    public float maxAngle;


    [Header("Objetos")]
    public Pedra targetPedra;
    public GameObject player;

    public GameObject prefabPedra;
    public GameObject vetor;

    public Interface interfaceManager;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CanPlay && !GameManager.Instance.player1Jogando)
        {
            player.GetComponent<SpriteRenderer>().color = Color.green;
            Arremesso();
        }
    }

    public void Arremesso()
    {

        angle = Random.Range(minAngle, maxAngle);
        dir = VectorByAngle(angle);
        actualForce = Random.Range(0f, 1f);
        GameManager.PedraSpecs nextTipo = GameManager.Instance.NextPedra(gameObject);
        if (nextTipo.tipo != Pedra.Tipos.Nada)
        {
            GameObject newPedra = Instantiate(prefabPedra, vetor.transform.GetChild(0).position, Quaternion.identity, GameObject.Find("Pedras").transform);
            newPedra.GetComponent<Pedra>().tipo = nextTipo.tipo;
            newPedra.GetComponent<Pedra>()._indexOwner = 1;
            newPedra.name = newPedra.name + indexPedra;
            indexPedra++;
            newPedra.GetComponent<Pedra>().cor = nextTipo.cor;
            newPedra.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * (actualForce * maxForce), ForceMode2D.Impulse);
        }
        interfaceManager.UpdatePedras();
        GameManager.Instance.CanPlay = false;
    }

    private Vector3 VectorByAngle(float angulo)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angulo), Mathf.Sin(Mathf.Deg2Rad * angulo), 0).normalized;
    }

    //private void OnGUI()
    //{
    //    GUI.color = Color.green;
    //    GUI.Label(new Rect(0, 0, Screen.width, Screen.height), dir.ToString(), GUI.skin.label);
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, transform.position + dir.normalized * 5);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, transform.position + VectorByAngle(minAngle) * 5);
    //    Gizmos.DrawLine(transform.position, transform.position + VectorByAngle(maxAngle) * 5);
    //}
}
