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

    [Range(-180f, -90f)]
    public float minAngle;
    [Range(-90f, 0f)]
    public float maxAngle;

    [Header("Objetos")]
    public Pedra targetPedra;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Arremesso(Pedra pedra)
    {
        angle = Random.Range(minAngle, maxAngle);
        dir = VectorByAngle(angle);
        actualForce = Random.Range(0f, 1f);
        pedra.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * (actualForce * maxForce), ForceMode2D.Impulse);
    }

    private Vector3 VectorByAngle(float angulo)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angulo), Mathf.Sin(Mathf.Deg2Rad * angulo), 0).normalized;
    }

    private void OnGUI()
    {
        GUI.color = Color.green;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), dir.ToString(), GUI.skin.label);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + dir.normalized * 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + VectorByAngle(minAngle) * 5);
        Gizmos.DrawLine(transform.position, transform.position + VectorByAngle(maxAngle) * 5);
    }
}
