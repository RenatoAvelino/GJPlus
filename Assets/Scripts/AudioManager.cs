using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    void Awake()
    {
    DontDestroyOnLoad(gameObject);

    }
}
