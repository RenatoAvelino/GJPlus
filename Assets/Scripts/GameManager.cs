using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pedra[] pedrasJogador1 = new Pedra[3];
    public Pedra[] pedrasJogador2 = new Pedra[3];

    public int turno = 0;

    public bool player1Jogando = true;

    public GameObject interfaceManager;

    #region SINGLETOM
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    void Start()
    {
        interfaceManager = GameObject.Find("Interface");
        Debug.Log("Startei");
        SetInitialPedras();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetInitialPedras()
    {
        for(int i = 0; i < pedrasJogador1.Length; i++)
        {
            pedrasJogador1[i] = CreatePedra();
            pedrasJogador2[i] = CreatePedra();
        }
        interfaceManager.GetComponent<Interface>().UpdatePedras();
    }

    private Pedra CreatePedra()
    {
        Pedra newPedra = new Pedra();
        Pedra.Tipos[] tipos = { Pedra.Tipos.Leve, Pedra.Tipos.Medio, Pedra.Tipos.Pesado, Pedra.Tipos.Prender };
        newPedra.tipo = tipos[(int) Random.Range(0f, 3f)];
        return newPedra;
    }

    public void ProximoTurno()
    {
        turno++;
        player1Jogando = !player1Jogando;
    }
}
