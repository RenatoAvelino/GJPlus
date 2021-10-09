using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Carregamento")]
    public float timeToFill = 2f; //Em Segundos
    public float timeToDeplete = 2f; //Em Segundos
    public float maxChargeWait = 0.2f; //Em Segundos

    [Header("Características do Arremesso")]
    private float actualForce = 0f;
    public float maxForce = 10f;
    public Vector3 dir;

    [Header("Inputs")]
    public bool leftMouseButton;
    bool contando = false;
    public Camera mainCamera;

    [Header("Objetos")]
    public Pedra targetPedra;
    public Medidor medidor;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftMouseButton = Input.GetMouseButton(0);
        PercentageGetter();
        if (Input.GetMouseButtonUp(0))
        {
            Arremesso(targetPedra);
        }
    }

    public void Arremesso(Pedra pedra)
    {
        Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dir = (mousePos - (Vector2)transform.position).normalized;
        pedra.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * (actualForce * maxForce), ForceMode2D.Impulse);
    }

    private void PercentageGetter()
    {
        if (leftMouseButton)
        {
            if(actualForce == 1 && contando == false)
                StartCoroutine(Timer(timeToDeplete, false));
            else if(contando == false)
                StartCoroutine(Timer(timeToFill, true));
            contando = true;
            medidor.UpdateMedidor(actualForce);
        }
    }

    private IEnumerator Timer(float duration, bool subindo)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f && leftMouseButton)
        {
            normalizedTime += Time.deltaTime / duration;

            if (subindo)
            {
                actualForce = normalizedTime;
                if (actualForce > 1f)
                    actualForce = 1f;
            }
            else
            {
                actualForce = 1f - normalizedTime;
                if (actualForce < 0f)
                    actualForce = 0f;
            }

            yield return null;
        }
        if (subindo)
            yield return new WaitForSeconds(maxChargeWait);
        contando = false;
    }

    private void OnGUI()
    {
        GUI.color = Color.green;
        GUI.Label(new Rect(0, 20, Screen.width, Screen.height), actualForce.ToString(), GUI.skin.label);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + dir.normalized * 5);
    }
}
