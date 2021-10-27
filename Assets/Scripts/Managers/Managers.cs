using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public GameManager GameManager;
    public SceneManager SceneManager;

    private void Awake()
    {
        GameManager = GetComponentInChildren<GameManager>();
        SceneManager = GetComponentInChildren<SceneManager>();
    }
}
