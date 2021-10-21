using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBrick : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeForRefresh=3f;
    public float timeForDestroy = 0f;
    public GameObject brick;
    //public float refreshCooldown = 1f;
    void Start()
    {
        timeForDestroy= Time.time;
        timeForRefresh = Time.time+1f;
    }

    public void LateDestroy()
    {
    }
    //Update is called once per frame
    void Update()
    {
        if (Time.time > timeForRefresh)
        {
            Destroy(brick);
        }
    }
}
