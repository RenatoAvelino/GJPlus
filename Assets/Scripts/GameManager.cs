using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Pedra.Tipos[] pedrasJogador1 = new Pedra.Tipos[3];
    [SerializeField]
    public Pedra.Tipos[] pedrasJogador2 = new Pedra.Tipos[3];

    public int turno = 0;

    public bool jogandoComIA = true;

    public bool player1Jogando = true;

    public GameObject interfaceManager;

    public Sprite spriteLeve;
    public Sprite spriteMedio;
    public Sprite spritePesado;
    public Sprite spritePrender;

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

    private Pedra.Tipos CreatePedra()
    {
        Pedra.Tipos newPedra;
        Pedra.Tipos[] tipos = { Pedra.Tipos.Leve, Pedra.Tipos.Medio, Pedra.Tipos.Pesado, Pedra.Tipos.Prender };
        newPedra = tipos[(int) Random.Range(0f, 4f)];
        return newPedra;
    }

    public Pedra.Tipos NextPedra(GameObject gameObject)
    {
        if (gameObject.GetComponent<PlayerController>())
        {
            for(int i = 0; i < pedrasJogador1.Length; i++)
            {
                if (pedrasJogador1[i] != Pedra.Tipos.Nada)
                {
                    Pedra.Tipos pedrinha = pedrasJogador1[i];
                    pedrasJogador1[i] = Pedra.Tipos.Nada;
                    return pedrinha;
                }
            }
        }
        else
        {
            for (int i = 0; i < pedrasJogador1.Length; i++)
            {
                if (pedrasJogador2[i] != Pedra.Tipos.Nada)
                {
                    Pedra.Tipos pedrinha = pedrasJogador2[i];
                    pedrasJogador2[i] = Pedra.Tipos.Nada;
                    return pedrinha;
                }
            }
        }
        interfaceManager.GetComponent<Interface>().UpdatePedras();
        return Pedra.Tipos.Nada;
    }

    public void ProximoTurno()
    {

        turno++;
        player1Jogando = !player1Jogando;
    }

    public Sprite GetPedraSprite(Pedra.Tipos tipo)
    {
        Sprite spriteCorreto = null;
        if (tipo == Pedra.Tipos.Leve)
        {
            spriteCorreto = spriteLeve;
        }
        else if (tipo == Pedra.Tipos.Medio)
        {
            spriteCorreto = spriteMedio;
        }
        else if (tipo == Pedra.Tipos.Pesado)
        {
            spriteCorreto = spritePesado;
        }
        else if (tipo == Pedra.Tipos.Prender)
        {
            spriteCorreto = spritePrender;
        }
        return spriteCorreto;
    }
}
