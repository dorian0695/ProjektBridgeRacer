using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuildBrickTower : MonoBehaviour
{
    public int lastValue = 0;
    public Brick.BrickType brickTypeToCollect;

    public GameObject parent;

    private AICollectAndBuild aICollectAndBuild;
    public GameObject brick;

    public List<GameObject> listOfBricks;

    public bool throwGreyBricks = false;
    public GameObject greyBrickToThrow;
    // Start is called before the first frame update
    void Start()
    {
        aICollectAndBuild = GetComponentInParent<AICollectAndBuild>();
        brickTypeToCollect = aICollectAndBuild.brickTypeToCollect;
    }

    // Update is called once per frame
    void Update()
    {
        switch (brickTypeToCollect)
        {
            case Brick.BrickType.Blue:
                if (lastValue != GetComponentInParent<AICollectAndBuild>().blueBrickCounter)
                {
                    if (lastValue < aICollectAndBuild.blueBrickCounter)
                    {
                        for (int i = lastValue; i < aICollectAndBuild.blueBrickCounter; i++)
                        {
                            var myBrick = Instantiate(brick, parent.transform);//new Vector3(parent.transform.position.x, transform.position.y + i, parent.transform.position.z));
                            myBrick.transform.parent = gameObject.transform;
                            myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                            listOfBricks.Add(myBrick);
                        }
                    }
                    else
                    {
                        if (throwGreyBricks)
                        {
                            for (int i = listOfBricks.Count; i > 0; i--)
                            {
                                Debug.Log("Tworze szara cegle");
                                var myBrick = Instantiate(greyBrickToThrow, parent.transform.position, Quaternion.identity);
                                myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);

                            }
                            //lastValue = GetComponentInParent<BrickHolder>().blueBrickCounter;

                            throwGreyBricks = false;

                        }
                        else
                            for (int i = lastValue; i > aICollectAndBuild.blueBrickCounter; i--)
                            {
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);
                            }
                        lastValue = GetComponentInParent<AICollectAndBuild>().blueBrickCounter;
                    }
                }
                break;
            case Brick.BrickType.Red:
                if (lastValue != GetComponentInParent<AICollectAndBuild>().redBrickCounter)
                {
                    if (lastValue < aICollectAndBuild.redBrickCounter)
                    {
                        for (int i = lastValue; i < aICollectAndBuild.redBrickCounter; i++)
                        {
                            var myBrick = Instantiate(brick, parent.transform);//new Vector3(parent.transform.position.x, transform.position.y + i, parent.transform.position.z));
                            myBrick.transform.parent = gameObject.transform;
                            myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                            listOfBricks.Add(myBrick);
                        }
                        //lastValue = GetComponentInParent<AICollectAndBuild>().redBrickCounter;
                    }
                    else
                    {
                        if (throwGreyBricks)
                        {
                            for (int i = listOfBricks.Count; i > 0; i--)
                            {
                                Debug.Log("Tworze szara cegle");
                                var myBrick = Instantiate(greyBrickToThrow, parent.transform.position, Quaternion.identity);
                                myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);                            }
                            //lastValue = GetComponentInParent<AICollectAndBuild>().redBrickCounter;

                            throwGreyBricks = false;

                        }
                        else
                            for (int i = lastValue; i > aICollectAndBuild.redBrickCounter; i--)
                            {
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);
                            }
                    }
                    lastValue = GetComponentInParent<AICollectAndBuild>().redBrickCounter;
                }
                break;
            case Brick.BrickType.Yellow:
                if (lastValue != GetComponentInParent<AICollectAndBuild>().yellowBrickCounter)
                {
                    if (lastValue < aICollectAndBuild.yellowBrickCounter)
                    {
                        for (int i = lastValue; i < aICollectAndBuild.yellowBrickCounter; i++)
                        {
                            var myBrick = Instantiate(brick, parent.transform);
                            myBrick.transform.parent = gameObject.transform;
                            myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                            listOfBricks.Add(myBrick);
                        }
                        lastValue = GetComponentInParent<AICollectAndBuild>().yellowBrickCounter;
                    }
                    else
                    {
                        if (throwGreyBricks)
                        {
                            for (int i = listOfBricks.Count; i > 0; i--)
                            {
                                Debug.Log("Tworze szara cegle");
                                var myBrick = Instantiate(greyBrickToThrow, parent.transform.position, Quaternion.identity);
                                myBrick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + lastValue * 0.045f, gameObject.transform.position.z);
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);
                            }
                            throwGreyBricks = false;
                        }
                        else
                            for (int i = lastValue; i > aICollectAndBuild.yellowBrickCounter; i--)
                            {
                                Destroy(listOfBricks[i - 1]);
                                listOfBricks.RemoveAt(i - 1);
                            }
                    }
                    lastValue = GetComponentInParent<AICollectAndBuild>().yellowBrickCounter;
                }
                break;
        }


    }
}
