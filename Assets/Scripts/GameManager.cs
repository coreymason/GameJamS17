using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public int score = 0;

    void Awake()
    {
        current = this;
    }

    public void RaiseScore()
    {
        score++;
    }
}
