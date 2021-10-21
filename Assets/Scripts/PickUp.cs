using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   // public Transform theDest;

    //public Rigidbody rb;
    //public BoxCollider coll;
    public Transform player, theDest, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool holdingSomething;


    GameObject grabbedObject;
    
     
    GameObject GetMouseHoverObject(float range)
    {
        Vector3 pos = gameObject.transform.position;
        RaycastHit raycastHit;
        Vector3 target = pos + Camera.main.transform.forward * range;
        if(Physics.Linecast(pos,target,out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
        return null;
    }

    void TryGrabObject(GameObject grabObject)
    {
        if (grabObject == null || !CanGrab(grabObject))
            return;

        grabbedObject = grabObject;
        holdingSomething = true;
        grabbedObject.GetComponent<Rigidbody>().detectCollisions = false;


    }

    bool CanGrab(GameObject candidate)
    {
        return candidate.GetComponent<Rigidbody>() != null;
    }
 
    void DropObject()
    {
        if (grabbedObject == null)
            return;
        if (grabbedObject.GetComponent<Rigidbody>() != null)
        {
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            grabbedObject.GetComponent<Rigidbody>().rotation = Quaternion.identity;
            holdingSomething = false;
        }
        grabbedObject.GetComponent<Rigidbody>().detectCollisions = true;

        grabbedObject = null;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (grabbedObject == null)
            {
                TryGrabObject(GetMouseHoverObject(5));
            }
            else
                DropObject();
        }

        if(grabbedObject!=null)
        {
            grabbedObject.transform.position= theDest.transform.position;
        }
    }


    //void OnTriggerStay(Collider other) // If ammo object collides with any collider...
    //{

    //    if ((other.gameObject.tag == "Player") && Vector3.Distance(player.position, transform.position) <= pickUpRange && (Input.GetKeyDown(KeyCode.G)))
    //    {
    //        PickUpItem();
    //    }
    //    else if(Input.GetKeyDown(KeyCode.G))
    //    {
    //        Drop();
    //    }
    //}

    //private void Update()
    //{

    //    //Check if player is in range and "E" is pressed
    //    Vector3 distanceToPlayer = player.position - transform.position;
    //    if (Vector3.Distance(player.position, transform.position) <= pickUpRange && Input.GetKeyDown(KeyCode.G)) PickUpItem();

    //    //Drop if equipped and "Q" is pressed
    //    if (theDest.position == this.transform.position && Input.GetKeyDown(KeyCode.G)) Drop();
    //}

    //private void PickUpItem()
    //{
    //    GetComponent<BoxCollider>().enabled = false;
    //    GetComponent<Rigidbody>().useGravity = false;
    //    this.transform.position = theDest.position;
    //    this.transform.parent = GameObject.Find("Destination").transform;
    //}

    //private void Drop()
    //{

    //    this.transform.parent = null;
    //    GetComponent<Rigidbody>().useGravity = true;
    //    GetComponent<BoxCollider>().enabled = true;

    //    //Gun carries momentum of player
    //    rb.velocity = player.GetComponent<Rigidbody>().velocity;

    //    //AddForce
    //    rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
    //    rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
    //    //Add random rotation
    //    float random = Random.Range(-1f, 1f);
    //    rb.AddTorque(new Vector3(random, random, random) * 10);
    //}




    //private void Update()
    //{
    //   RaycastHit hit;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Grabable"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.G))
    //        {
    //            GetComponent<BoxCollider>().enabled = false;
    //            GetComponent<Rigidbody>().useGravity = false;
    //            this.transform.position = theDest.position;
    //            this.transform.parent = GameObject.Find("Destination").transform;
    //        }
    //        if (Input.GetKeyUp(KeyCode.G))
    //        {
    //            this.transform.parent = null;
    //            GetComponent<Rigidbody>().useGravity = true;
    //            GetComponent<BoxCollider>().enabled = true;
    //        }
    //    }
    //}

    //}

}
