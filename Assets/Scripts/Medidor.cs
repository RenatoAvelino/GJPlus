using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medidor : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites = new Sprite[10];

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateMedidor(float percentage)
    {
        int force = Mathf.RoundToInt(percentage * 9f);

        if (force > 9)
            force = 9;
        if (force < 0)
            force = 0;

        spriteRenderer.sprite = sprites[force];
    }
}
