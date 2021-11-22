using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HostSpawner : MonoBehaviour
{

    public GameObject Engineer;
    public GameObject Janitor;
    public GameObject Scientist1;
    public GameObject Scientis2;
    public GameObject Scientist3;
    public GameObject SecurityGuard1;
    public GameObject SecurityGuard2;
    public GameObject SecurityGuard3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Flags]
public enum HostCharactersType
{
    Nothing = 0,
    Janitor = 1,
    Scientist1 = 2,
    Scientist2 = 8,
    Scientist3 = 16,
    SecurityGuard1 = 32,
    SecurityGuard2 = 64,
    SecurityGuard = 128,
    Engineer = 256
}
