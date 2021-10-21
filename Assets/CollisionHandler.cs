using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject player;
    public float force = 5;
    public ForceMode forceMode = ForceMode.Impulse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "RedGuy":
                if (player.GetComponent<BrickHolder>().blueBrickCounter > collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter)
                {
                    collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter = 0;
                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - collision.gameObject.transform.position;

                    // We then get the opposite (-Vector3) and normalize it
                    dir = -dir.normalized;

                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                }
                else if(player.GetComponent<BrickHolder>().blueBrickCounter == collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter)
                {
                    player.GetComponent<BrickHolder>().blueBrickCounter = 0;
                    collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter = 0;

                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - playerPosition;

                    // We then get the opposite (-Vector3) and normalize it
                    //dir = -dir.normalized;

                    //collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force,dir,1f);

                }
                else
                {
                    player.GetComponentInChildren<BuildBricksTower>().throwGreyBricks = true;

                    player.GetComponent<BrickHolder>().blueBrickCounter = 0;
                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - playerPosition;

                    // We then get the opposite (-Vector3) and normalize it
                    dir = -dir.normalized;


                    player.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                }
                break;
            case "YellowGuy":
                if (player.GetComponent<BrickHolder>().blueBrickCounter > collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter)
                {
                    collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter = 0;
                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - collision.gameObject.transform.position;

                    // We then get the opposite (-Vector3) and normalize it
                    dir = -dir.normalized;

                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                }
                else if (player.GetComponent<BrickHolder>().blueBrickCounter == collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter)
                {
                    player.GetComponent<BrickHolder>().blueBrickCounter = 0;
                    collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter = 0;

                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - playerPosition;

                    // We then get the opposite (-Vector3) and normalize it
                    //dir = -dir.normalized;

                    //collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, dir, 1f);

                }
                else
                {
                    player.GetComponentInChildren<BuildBricksTower>().throwGreyBricks = true;

                    player.GetComponent<BrickHolder>().blueBrickCounter = 0;
                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - playerPosition;

                    // We then get the opposite (-Vector3) and normalize it
                    dir = -dir.normalized;


                    player.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                }
                break;
            default:
                ;
                break;
        }
    }
}
