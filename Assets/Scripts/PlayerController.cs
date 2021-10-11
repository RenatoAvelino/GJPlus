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
    public Vector3 direction;
    public float angle;

    [Header("Inputs")]
    public bool leftMouseButton;
    bool contando = false;
    public Camera mainCamera;

    private float horizontalInput;
    private int indexPedra = 0;

    [Header("Objetos")]
    //public Pedra targetPedra;
    public Medidor medidor;
    public GameObject vetor;

    public GameObject prefabPedra;

    public Interface interfaceManager;

    public LayerMask paredeMask;

    void Start()
    {
        interfaceManager = GameObject.Find("Interface").GetComponent<Interface>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1.5f, paredeMask, 0, 0);
        if (hit.collider)
            medidor.transform.position = new Vector3(transform.position.x + 1, medidor.transform.position.y, medidor.transform.position.z);
        else
            medidor.transform.position = new Vector3(transform.position.x - 1, medidor.transform.position.y, medidor.transform.position.z);

        if (GameManager.Instance.CanPlay && GameManager.Instance.player1Jogando)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(horizontalInput, 0f, 0f) * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.cyan;
            leftMouseButton = Input.GetMouseButton(0);
            PercentageGetter();
            if (Input.GetMouseButtonUp(0))
            {
                Arremesso();
                actualForce = 0f;
                medidor.UpdateMedidor(actualForce);
                vetor.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
            }
        }
    }

    public void Arremesso()
    {
        //Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //direction = (mousePos - (Vector2)vetor.transform.GetChild(0).position).normalized;
        direction = VectorByAngle(angle - 90);
        GameManager.PedraSpecs nextTipo = GameManager.Instance.NextPedra(gameObject);
        if(nextTipo.tipo != Pedra.Tipos.Nada)
        {
            GameObject newPedra = Instantiate(prefabPedra, vetor.transform.GetChild(0).position, Quaternion.identity, GameObject.Find("Pedras").transform);
            newPedra.name = newPedra.name + indexPedra;
            indexPedra++;
            newPedra.GetComponent<Pedra>().tipo = nextTipo.tipo;
            newPedra.GetComponent<Pedra>().cor = nextTipo.cor;
            newPedra.GetComponent<Pedra>().force = direction * (actualForce * maxForce);
        }
        interfaceManager.UpdatePedras();
        GameManager.Instance.CanPlay = false;
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

            UpdateVisualVector();
        }
    }

    private void UpdateVisualVector()
    {
        direction = Input.mousePosition - Camera.main.WorldToScreenPoint(vetor.transform.GetChild(0).position);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

        angle = Mathf.Clamp(angle, -90, 90);

        vetor.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

    //private void OnGUI()
    //{
    //    Vector2 nativeSize = new Vector2(1080, 1920);
    //    GUIStyle style = new GUIStyle(GUI.skin.label);
    //    style.fontSize = (int)(60.0f * ((float)Screen.width / (float)nativeSize.x));
    //    GUI.color = Color.green;
    //    GUI.Label(new Rect(0, 20, Screen.width, Screen.height), angle.ToString(), style);
    //}
}
