using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheBlockade : MonoBehaviour
{
    Vector3 firstPosition;
    public int howManyTimesMoved = 0;
    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(howManyTimesMoved>24)
        {
            transform.position = firstPosition;
            howManyTimesMoved = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "BlueGuy":
                if (collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter >0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z + 0.15f);
                    howManyTimesMoved++;
                }
                break;
            case "RedGuy":
                if (collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter > 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z + 0.15f);
                    howManyTimesMoved++;
                }
                break;
            case "YellowGuy":
                if (collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter > 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z + 0.15f);
                    howManyTimesMoved++;
                }
                break;
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "PassageBlockade")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 2.4f, transform.position.z + 0.3f);
        }
    }
}
