using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medidor : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites = new Sprite[10];

    private Image imagem;
    void Start()
    {
        imagem = GetComponent<Image>();
    }

    public void UpdateMedidor(float percentage)
    {
        int force = Mathf.RoundToInt(percentage * 9f);

        if (force > 9)
            force = 9;
        if (force < 0)
            force = 0;

        imagem.sprite = sprites[force];
    }
}
