using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Bug;
    [SerializeField] private GameObject Hazmat;
    [SerializeField] private GameObject Engineer;
    [SerializeField] private GameObject Janitor;
    [SerializeField] private GameObject Scientist1;
    [SerializeField] private GameObject Scientist2;
    [SerializeField] private GameObject Scientist3;
    [SerializeField] private GameObject SecurityGuard1;
    [SerializeField] private GameObject SecurityGuard2;
    [SerializeField] private GameObject SecurityGuard3;

    private List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        SpawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();

        // For every Spawn Point
        foreach (SpawnPoint point in SpawnPoints)
        {
            List<HostCharactersType> possibleCharacters = new List<HostCharactersType>();

            foreach (HostCharactersType type in Enum.GetValues(typeof(HostCharactersType)))
            {
                if (point.RegisteredCharacters.HasFlag(type))
                {
                    possibleCharacters.Add(type);
                }
            }

            HostCharactersType chosenType = possibleCharacters[UnityEngine.Random.Range(0, possibleCharacters.Count)];
            GameObject hostPrefab = Instantiate(GetHostPrefab(chosenType), point.transform);
            RegisteredEntity entity = hostPrefab.GetComponent<RegisteredEntity>();
            entity.MoveSpeed = GetHostSpeed(chosenType);
            entity.ClearanceLevel = GetHostClearance(chosenType);
            entity.Register();
        }
    }

    private float GetHostSpeed(HostCharactersType type)
    {
        float rtn = 0f;

        switch (type)
        {
            case HostCharactersType.Bug:
                rtn = UnityEngine.Random.Range(1, 3);
                break;
            case HostCharactersType.Hazmat:
                rtn = UnityEngine.Random.Range(3, 7);
                break;
            case HostCharactersType.Engineer:
                rtn = UnityEngine.Random.Range(50, 50);
                break;
            case HostCharactersType.Janitor:
                rtn = UnityEngine.Random.Range(2, 5);
                break;
            case HostCharactersType.Scientist1:
                rtn = UnityEngine.Random.Range(3, 6);
                break;
            case HostCharactersType.Scientist2:
                rtn = UnityEngine.Random.Range(3, 6);
                break;
            case HostCharactersType.Scientist3:
                rtn = UnityEngine.Random.Range(3, 6);
                break;
            case HostCharactersType.SecurityGuard1:
                rtn = UnityEngine.Random.Range(5, 7);
                break;
            case HostCharactersType.SecurityGuard2:
                rtn = UnityEngine.Random.Range(5, 7);
                break;
            case HostCharactersType.SecurityGuard3:
                rtn = UnityEngine.Random.Range(5, 7);
                break;
        }

        return rtn;
    }

    private int GetHostClearance(HostCharactersType type)
    {
        int rtn = 0;

        switch (type)
        {
            case HostCharactersType.Bug:
                rtn = UnityEngine.Random.Range(0, 0);
                break;
            case HostCharactersType.Hazmat:
                rtn = UnityEngine.Random.Range(3, 6);
                break;
            case HostCharactersType.Engineer:
                rtn = UnityEngine.Random.Range(2, 6);
                break;
            case HostCharactersType.Janitor:
                rtn = UnityEngine.Random.Range(1, 4);
                break;
            case HostCharactersType.Scientist1:
                rtn = UnityEngine.Random.Range(2, 4);
                break;
            case HostCharactersType.Scientist2:
                rtn = UnityEngine.Random.Range(3, 4);
                break;
            case HostCharactersType.Scientist3:
                rtn = UnityEngine.Random.Range(4, 5);
                break;
            case HostCharactersType.SecurityGuard1:
                rtn = UnityEngine.Random.Range(3, 6);
                break;
            case HostCharactersType.SecurityGuard2:
                rtn = UnityEngine.Random.Range(4, 6);
                break;
            case HostCharactersType.SecurityGuard3:
                rtn = UnityEngine.Random.Range(5, 6);
                break;
        }

        return rtn;
    }

    private GameObject GetHostPrefab(HostCharactersType type)
    {
        GameObject rtn = null;

        switch (type)
        {
            case HostCharactersType.Bug:
                rtn = Bug;
                break;
            case HostCharactersType.Hazmat:
                rtn = Hazmat;
                break;
            case HostCharactersType.Engineer:
                rtn = Engineer;
                break;
            case HostCharactersType.Janitor:
                rtn = Janitor;
                break;
            case HostCharactersType.Scientist1:
                rtn = Scientist1;
                break;
            case HostCharactersType.Scientist2:
                rtn = Scientist2;
                break;
            case HostCharactersType.Scientist3:
                rtn = Scientist3;
                break;
            case HostCharactersType.SecurityGuard1:
                rtn = SecurityGuard1;
                break;
            case HostCharactersType.SecurityGuard2:
                rtn = SecurityGuard2;
                break;
            case HostCharactersType.SecurityGuard3:
                rtn = SecurityGuard3;
                break;
        }

        return rtn;
    }
}

[Flags]
public enum HostCharactersType
{
    Nothing = -1,
    Janitor = 1,
    Scientist1 = 2,
    Scientist2 = 8,
    Scientist3 = 16,
    SecurityGuard1 = 32,
    SecurityGuard2 = 64,
    SecurityGuard3 = 128,
    Engineer = 256,
    Hazmat = 512,
    Bug = 1024
}
