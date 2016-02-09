using UnityEngine;
using System.Collections;

public class SingletonGame {

    private static SingletonGame instance;

    public int _cameraSwitch = 1;
    public int lifeSnake = 3;
    public int points = 0;
    public float distanceSnake = 0;

    private SingletonGame() { }

    public static SingletonGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SingletonGame();
            }
            return instance;
        }
    }
}
