using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollisionHandler : MonoBehaviour
{
    public GameObject me;
    public float force = 5;
    public ForceMode forceMode = ForceMode.Impulse;
    int brickCounter;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != gameObject.tag && (collision.gameObject.tag == "BlueGuy")|| (collision.gameObject.tag == "RedGuy") || (collision.gameObject.tag == "YellowGuy") )
        switch (tag)
        {
            case "BlueGuy":
                brickCounter = me.GetComponent<AICollectAndBuild>().blueBrickCounter;
                HandleCollision(brickCounter, collision);
                break;
            case "RedGuy":
                brickCounter = me.GetComponent<AICollectAndBuild>().redBrickCounter;
                HandleCollision(brickCounter, collision);
                break;
            case "YellowGuy":
                brickCounter = me.GetComponent<AICollectAndBuild>().yellowBrickCounter;
                HandleCollision(brickCounter, collision);
                break;
        }



    }

    private void HandleCollision(int brickCounter, Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "BlueGuy":
                if (brickCounter > collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter)
                {
                    collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter = 0;
                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - collision.gameObject.transform.position;
                    dir = -dir.normalized;

                    collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                }
                else if (brickCounter == collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter)
                {
                    brickCounter = 0;
                    collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter = 0;

                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - playerPosition;

                    collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, dir, 1f);

                }
                else
                {
                    if (collision.gameObject.tag.Contains("Guy"))
                    {
                        me.GetComponentInChildren<AIBuildBrickTower>().throwGreyBricks = true;

                        brickCounter = 0;
                        ContactPoint contactPoint = collision.GetContact(0);
                        Vector3 playerPosition = transform.position;
                        Vector3 dir = contactPoint.point - playerPosition;

                        dir = -dir.normalized;

                        me.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                    }
                }
                break;
            case "RedGuy":
                if (brickCounter > collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter)
                {
                    collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter = 0;
                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - collision.gameObject.transform.position;

                    dir = -dir.normalized;

                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                }
                else if (brickCounter == collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter)
                {
                    brickCounter = 0;
                    collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter = 0;

                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - playerPosition;

                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, dir, 1f);

                }
                else
                {
                    if (collision.gameObject.tag.Contains("Guy"))
                    {
                        me.GetComponentInChildren<AIBuildBrickTower>().throwGreyBricks = true;

                        brickCounter = 0;
                        ContactPoint contactPoint = collision.GetContact(0);
                        Vector3 playerPosition = transform.position;
                        Vector3 dir = contactPoint.point - playerPosition;

                        dir = -dir.normalized;

                        me.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                    }
                }
                break;
            case "YellowGuy":
                if (brickCounter > collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter)
                {
                    collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter = 0;
                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - collision.gameObject.transform.position;
                    dir = -dir.normalized;
                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                }
                else if (brickCounter == collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter)
                {
                    brickCounter = 0;
                    collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter = 0;

                    ContactPoint contactPoint = collision.GetContact(0);
                    Vector3 playerPosition = transform.position;
                    Vector3 dir = contactPoint.point - playerPosition;
                    collision.gameObject.GetComponent<AICollectAndBuild>().stopForKnockback = true;
                    collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, dir, 1f);

                }
                else
                {
                    if (collision.gameObject.tag.Contains("Guy"))
                    {
                        me.GetComponentInChildren<AIBuildBrickTower>().throwGreyBricks = true;

                        brickCounter = 0;
                        ContactPoint contactPoint = collision.GetContact(0);
                        Vector3 playerPosition = transform.position;
                        Vector3 dir = contactPoint.point - playerPosition;
                        dir = -dir.normalized;
                        me.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, forceMode);
                    }
                }
                break;
            default:
                me.GetComponentInChildren<AIBuildBrickTower>().throwGreyBricks = false;
                break;
        }
    }
}
