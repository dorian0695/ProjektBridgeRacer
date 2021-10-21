using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBricksTower : MonoBehaviour
{
    public int lastValue = 0;
    public Brick.BrickType brickTypeToCollect;

    public GameObject parent;

    private BrickHolder brickHolder;
    public GameObject brick;

    public bool throwGreyBricks = false;
    public GameObject greyBrickToThrow;

    public List<GameObject> listOfBricks;
    // Start is called before the first frame update
    void Start()
    {
        brickHolder = GetComponentInParent<BrickHolder>();
        brickTypeToCollect = brickHolder.brickTypeToCollect;
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("BlueTowerBrick"))
        {
            listOfBricks.Add(brick);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = parent.transform.position + new Vector3(0, 0.5f, 0);
        switch (brickTypeToCollect)
        {
            case Brick.BrickType.Blue:

                if (lastValue != GetComponentInParent<BrickHolder>().blueBrickCounter)
                {
                    if (lastValue < brickHolder.blueBrickCounter)
                    {
                        for (int i = lastValue; i < brickHolder.blueBrickCounter; i++)
                        {
                            var myBrick = Instantiate(brick, parent.transform);//new Vector3(parent.transform.position.x, transform.position.y + i, parent.transform.position.z));
                            myBrick.transform.parent = gameObject.transform;
                            myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                            listOfBricks.Add(myBrick);
                        }
                        lastValue = GetComponentInParent<BrickHolder>().blueBrickCounter;
                    }
                    else
                    {
                        if (throwGreyBricks)
                        {
                            for (int i = lastValue; i > 0; i--)
                            {
                                Debug.Log("Tworze szara cegle");
                                var myBrick = Instantiate(greyBrickToThrow, parent.transform.position, Quaternion.identity);
                                //myBrick.transform.parent = gameObject.transform;
                                myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);
                                //myBrick.GetComponent<DestroyBrick>().timeForDestroy = Time.time;
                                //myBrick.GetComponent<DestroyBrick>().timeForRefresh = Time.time + 2f;
                                //myBrick.GetComponent<DestroyBrick>().LateDestroy();


                            }
                            //lastValue = GetComponentInParent<BrickHolder>().blueBrickCounter;

                            throwGreyBricks = false;

                        }
                        else
                            for (int i = lastValue; i > brickHolder.blueBrickCounter; i--)
                            {
                                //Debug.Log("Wchodze tu i niszcze obiekt" + i);
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);
                                //brickHolder.blueBrickCounter--;
                                //var myBrick = Instantiate(brick, parent.transform);//new Vector3(parent.transform.position.x, transform.position.y + i, parent.transform.position.z));
                                //myBrick.transform.parent = gameObject.transform;
                                //myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                            }
                        lastValue = GetComponentInParent<BrickHolder>().blueBrickCounter;
                    }
                }
                
                break;
        }


    }
}
