using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private float timer = 0f;
    [SerializeField]
    private float maxTime = 300f; // 300 seconds
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        timer += Time.deltaTime;

        if (timer >= maxTime)
        {
            // Exit the game
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        

    }


    public float GetTimer()
    {
        return timer;
    }
}