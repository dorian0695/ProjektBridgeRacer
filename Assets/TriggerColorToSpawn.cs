using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColorToSpawn : MonoBehaviour
{
    public bool blueIsReadyToSpawn;
    public bool redIsReadyToSpawn;
    public bool yellowIsReadyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "BlueGuy":
                blueIsReadyToSpawn = true;
                //other.gameObject.GetComponent<BrickHolder>().
                break;
            case "RedGuy":
                redIsReadyToSpawn = true;
                other.gameObject.GetComponent<AICollectAndBuild>().buildStairs = false;
                other.gameObject.GetComponent<AICollectAndBuild>().goToPointer = false;
                if (gameObject.tag == "TriggerPlatform2")
                {
                    other.gameObject.GetComponent<AICollectAndBuild>().isItSecondPlatfom = true;
                    other.gameObject.GetComponent<AICollectAndBuild>().isItFirstPlatfom = false;
                }
                else
                {
                    other.gameObject.GetComponent<AICollectAndBuild>().isItSecondPlatfom = false;
                    other.gameObject.GetComponent<AICollectAndBuild>().isItThirdPlatfom = true;
                }

                other.gameObject.GetComponent<AICollectAndBuild>().platformChanged = true;
                break;
            case "YellowGuy":
                yellowIsReadyToSpawn = true;
                other.gameObject.GetComponent<AICollectAndBuild>().buildStairs = false;
                other.gameObject.GetComponent<AICollectAndBuild>().goToPointer = false;
                if (gameObject.tag == "TriggerPlatform2")
                {
                    other.gameObject.GetComponent<AICollectAndBuild>().isItSecondPlatfom = true;
                    other.gameObject.GetComponent<AICollectAndBuild>().isItFirstPlatfom = false;
                }
                else
                {
                    other.gameObject.GetComponent<AICollectAndBuild>().isItSecondPlatfom = false;
                    other.gameObject.GetComponent<AICollectAndBuild>().isItThirdPlatfom = true;
                }

                other.gameObject.GetComponent<AICollectAndBuild>().platformChanged = true;


                break;
        }
    }
}
