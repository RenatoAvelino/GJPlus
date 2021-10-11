using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    public PedraSpecs[] Pedras1 = new PedraSpecs[3];
    [SerializeField]
    public PedraSpecs[] Pedras2 = new PedraSpecs[3];


    public int pedraSelecionadaJogador1;
    public int pedraSelecionadaJogador2;

    public int turno = 0;

    public bool jogandoComIA = true;

    public bool player1Jogando = true;

    public GameObject interfaceManager;

    public Sprite spriteLeve;
    public Sprite spriteMedio;
    public Sprite spritePesado;
    public Sprite spritePrender;

    public bool CanPlay = true;

    public int currentLevel = 0;

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

    public struct PedraSpecs
    {
        public Pedra.Tipos tipo;
        public Pedra.Cores cor;
    }
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
        for(int i = 0; i < Pedras1.Length; i++)
        {
            Pedras1[i].tipo = CreatePedra();
            Pedras2[i].tipo = CreatePedra();

            Pedras1[i].cor = SetColorPedra();
            Pedras2[i].cor = SetColorPedra();
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

    private Pedra.Cores SetColorPedra()
    {
        Pedra.Cores newColor;
        Pedra.Cores[] tipos = { Pedra.Cores.Amarelo, Pedra.Cores.Azul, Pedra.Cores.Roxo, Pedra.Cores.Verde };
        newColor = tipos[(int)Random.Range(0f, 2f)];
        return newColor;
    }

    public PedraSpecs NextPedra(GameObject gameObject)
    {
        //if (gameObject.GetComponent<PlayerController>())
        //{
        //    if (pedrasJogador1[pedraSelecionadaJogador1] != Pedra.Tipos.Nada)
        //    {
        //        Pedra.Tipos pedrinha = pedrasJogador1[pedraSelecionadaJogador1];
        //        pedrasJogador1[pedraSelecionadaJogador1] = Pedra.Tipos.Nada;
        //        return pedrinha;
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < pedrasJogador1.Length; i++)
        //    {
        //        if (pedrasJogador2[i] != Pedra.Tipos.Nada)
        //        {
        //            Pedra.Tipos pedrinha = pedrasJogador2[i];
        //            pedrasJogador2[i] = Pedra.Tipos.Nada;
        //            return pedrinha;
        //        }
        //    }
        //}
        if (gameObject.GetComponent<PlayerController>())
        {
            for (int i = 0; i < Pedras1.Length; i++)
            {
                if (Pedras1[i].tipo != Pedra.Tipos.Nada)
                {
                    PedraSpecs pedrinha = Pedras1[i];
                    Pedras1[i].tipo = Pedra.Tipos.Nada;
                    return pedrinha;
                }
            }
        }
        else
        {
            for (int i = 0; i < Pedras1.Length; i++)
            {
                if (Pedras2[i].tipo != Pedra.Tipos.Nada)
                {
                    PedraSpecs pedrinha = Pedras2[i];
                    Pedras2[i].tipo = Pedra.Tipos.Nada;
                    return pedrinha;
                }
            }
        }
        interfaceManager.GetComponent<Interface>().UpdatePedras();
        return new PedraSpecs();
    }

    public void ProximoTurno()
    {
        turno++;
        player1Jogando = !player1Jogando;
        if(turno == 6)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        currentLevel += 1;
        SceneManager.LoadScene(currentLevel);
        //Reset Score
        //Reset
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

    public void SelectedPedraPlayer1(string pedraIndex)
    {
        pedraSelecionadaJogador1 = System.Convert.ToInt32(pedraIndex);
    }
    public void SelectedPedraPlayer2(string pedraIndex)
    {
        pedraSelecionadaJogador2 = System.Convert.ToInt32(pedraIndex);
    }

    public void PedraStopped()
    {
        CanPlay = true;
        ProximoTurno();
    }
}
