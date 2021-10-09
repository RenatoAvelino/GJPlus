using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Carregamento")]
    public float timeToFill = 2f; //Em Segundos
    public float timeToDeplete = 2f; //Em Segundos
    public float maxChargeWait = 0.2f; //Em Segundos

    [Header("Caracter�sticas do Arremesso")]
    private float actualForce = 0f;
    public float maxForce = 10f;
    public Vector3 direction;
    public float angle;

    [Header("Inputs")]
    public bool leftMouseButton;
    bool contando = false;
    public Camera mainCamera;

    [Header("Objetos")]
    public Pedra targetPedra;
    public Medidor medidor;
    public GameObject vetor;

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
        //Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //direction = (mousePos - (Vector2)vetor.transform.GetChild(0).position).normalized;
        direction = VectorByAngle(angle - 90);
        pedra.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * (actualForce * maxForce), ForceMode2D.Impulse);
    }

    private void PercentageGetter()
    {
        if (leftMouseButton)
        {
            if (actualForce == 1 && contando == false)
                StartCoroutine(Timer(timeToDeplete, false));
            else if (contando == false)
                StartCoroutine(Timer(timeToFill, true));
            contando = true;
            medidor.UpdateMedidor(actualForce);

            direction = Input.mousePosition - Camera.main.WorldToScreenPoint(vetor.transform.GetChild(0).position);
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

            angle = Mathf.Clamp(angle, -90, 90);

            vetor.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

    private Vector3 VectorByAngle(float angulo)
    {
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angulo), Mathf.Sin(Mathf.Deg2Rad * angulo), 0).normalized;
    }

    private void OnGUI()
    {
        Vector2 nativeSize = new Vector2(1080, 1920);
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = (int)(60.0f * ((float)Screen.width / (float)nativeSize.x));
        GUI.color = Color.green;
        GUI.Label(new Rect(0, 20, Screen.width, Screen.height), angle.ToString(), style);
    }
}