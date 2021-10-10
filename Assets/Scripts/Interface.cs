using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Transform pedrasJogador1;
    public Transform pedrasJogador2;

    public Sprite spriteLeve;
    public Sprite spriteMedio;
    public Sprite spritePesado;
    public Sprite spritePrender;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePedras()
    {
        for (int i = 0; i < GameManager.Instance.pedrasJogador1.Length; i++)
        {
            if (GameManager.Instance.pedrasJogador1[i] != null)
            {
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().sprite = GetSprite(GameManager.Instance.pedrasJogador1[i].tipo);
            }
            else
            {
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            }

            if(GameManager.Instance.pedrasJogador2[i] != null)
            {
                pedrasJogador2.GetChild(i).gameObject.GetComponent<Image>().sprite = GetSprite(GameManager.Instance.pedrasJogador2[i].tipo);
            }
            else
            {
                pedrasJogador2.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            }

        }
    }

    private Sprite GetSprite(Pedra.Tipos tipo)
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
