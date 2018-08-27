using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorLevel : MonoBehaviour
{
    private static bool _wallCrashed;
    private static bool _bookReaded;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorFinished") == 1)
        {
            CrashWall();
            _wallCrashed = true;
            _bookReaded = true;
        }
        else
        {
            _wallCrashed = false;
            _bookReaded = false;
        }
                   
    }

    private IEnumerator BreakWall()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Rigidbody block = transform.GetChild(i).GetComponent<Rigidbody>();
            block.constraints = 0;
            Vector3 vec = new Vector3(Random.Range(10,100),Random.Range(10,100),Random.Range(10,100));
            block.AddTorque(vec);
            block.AddForce(vec);
        }

        _wallCrashed = true;
        yield break;
    }

    public static bool WallCrashed
    {
        get { return _wallCrashed; }
    }


    public static bool BookReaded
    {
        get { return _bookReaded; }
        set { _bookReaded = value; }
    }

    public void CrashWall()
    {
        StartCoroutine(BreakWall());
    }
}