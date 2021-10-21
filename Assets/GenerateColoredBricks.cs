using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateColoredBricks : MonoBehaviour
{
    bool[] isBrickGenerated = new bool[72];
    public List<GameObject> listOfPlatformBricks;
    public List<GameObject> randomListofPlatformBricks;

    public int howManyBlue = 0;
    public int howManyRed = 0;
    public int howManyYellow = 0;

    private bool increasedByBlue;
    private bool increasedByRed;
    private bool increasedByYellow;
    private bool triggeredToChange;


    public int howManyBricks = 0;
    private int howManyColors = 0;
    public int randomizer = 3;
    public List<int> availableColors = new List<int>();

    [SerializeField]
    private List<Material> brickColor;

    public GameObject trigger;

    public float timeForRefresh;
    public float refreshCooldown = 10f;
    public int tempIndex;
    //int[] whichBricksAreEmpty = new int[72];
    // Start is called before the first frame update
    void Start()
    {
        CreateAListOfBricks();
        CreateARandomListOfBricks();
        GenerateBricks();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "SecondPlatform"|| gameObject.tag == "ThirdPlatform")
        {
            if (trigger.GetComponent<TriggerColorToSpawn>().blueIsReadyToSpawn == true && !increasedByBlue)
            {
                howManyBricks += 24;
                availableColors.Add(0);
                howManyColors += 1;
                randomizer--;
                CreateARandomListOfBricks();
                increasedByBlue = true;
                triggeredToChange = true;

            }
            if (trigger.GetComponent<TriggerColorToSpawn>().redIsReadyToSpawn == true && !increasedByRed)
            {
                howManyBricks += 24;
                availableColors.Add(1);
                howManyColors += 1;
                randomizer--;
                CreateARandomListOfBricks();
                increasedByRed = true;
                triggeredToChange = true;

            }
            if (trigger.GetComponent<TriggerColorToSpawn>().yellowIsReadyToSpawn == true && !increasedByYellow)
            {
                howManyBricks += 24;
                availableColors.Add(2);
                howManyColors += 1;
                randomizer--;
                CreateARandomListOfBricks();
                increasedByYellow = true;
                triggeredToChange = true;
            }
        }
        else
        {
            howManyBricks = 72;
            howManyColors = 3;
            randomizer = 1;
            availableColors.Add(0);
            availableColors.Add(1);
            availableColors.Add(2);
        }
        if (Time.time > timeForRefresh || triggeredToChange)
        {
            GenerateBricks();
            timeForRefresh = Time.time + refreshCooldown;
            triggeredToChange = false;
        }
    }
    void CreateAListOfBricks()
    {
        if (gameObject.tag == "FirstPlatform")
            foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Platform1Brick"))
            {
                listOfPlatformBricks.Add(brick);
            }
        if (gameObject.tag == "SecondPlatform")
            foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Platform2Brick"))
            {
                listOfPlatformBricks.Add(brick);
            }
        if (gameObject.tag == "ThirdPlatform")
            foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Platform3Brick"))
            {
                listOfPlatformBricks.Add(brick);
            }
        
    }

    void CreateARandomListOfBricks()
    {

        //randomListofPlatformBricks = new List<GameObject>();
        if (gameObject.tag == "SecondPlatform"|| gameObject.tag == "ThirdPlatform")
        {
            if (howManyColors == 3)
            {
                randomListofPlatformBricks = new List<GameObject>();
            }
            for (int i = randomListofPlatformBricks.Count; i < howManyBricks; i++)
            {
                if (howManyColors < 3)
                {
                    int randomMultiplier = UnityEngine.Random.Range(0, (randomizer * 24) + 1);
                    if (i - randomMultiplier > 0 && i%2==0)
                    {
                        tempIndex = i - randomMultiplier;
                        //randomMultiplier = UnityEngine.Random.Range(0, (randomizer * 24) + 1);
                    }
                    else if (i + randomMultiplier<72)
                    {
                        tempIndex = i + randomMultiplier;
                        //randomMultiplier = 0;
                    }
                    else
                    {
                        tempIndex = i;
                    }
                }
                else
                {
                    tempIndex = i;
                }
                if (!randomListofPlatformBricks.Contains(listOfPlatformBricks[tempIndex]))
                {
                    randomListofPlatformBricks.Add(listOfPlatformBricks[tempIndex]);
                }
                else
                {
                    i--;
                }
            }
        }
        //if (gameObject.tag == "ThirdPlatform")
        //    for (int i = randomListofPlatformBricks.Count; i < howManyBricks; i++)
        //    {
        //        if (howManyColors < 3)
        //        {
        //            int randomMultiplier = UnityEngine.Random.Range(0, (randomizer * 24) + 1);
        //            if (i %2== 0)
        //            {
        //                tempIndex = i + randomMultiplier;
        //                //randomMultiplier = UnityEngine.Random.Range(0, (randomizer * 24) + 1);
        //            }
        //            else if(i - randomMultiplier>0)
        //            {
        //                tempIndex = i - randomMultiplier;
        //                //randomMultiplier = 0;
        //            }
        //            else
        //            {
        //                tempIndex = i;
        //            }
        //            //tempIndex = i + randomMultiplier;
        //        }
        //        else
        //        {
        //            tempIndex = i;
        //        }
        //        if (!randomListofPlatformBricks.Contains(listOfPlatformBricks[tempIndex]))
        //        {
        //            randomListofPlatformBricks.Add(listOfPlatformBricks[tempIndex]);
        //        }
        //        else
        //        {
        //            i--;
        //        }
        //    }

    }


    private void GenerateBricks()
    {
        if (gameObject.tag == "FirstPlatform")
        {
            if (availableColors.Count > 0)
                for (int i = 0; i < howManyBricks; i++)
                {
                    if (listOfPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractBlueValue == true)
                    {
                        howManyBlue--;
                        listOfPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractBlueValue = false;
                    }
                    if (listOfPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractRedValue == true)
                    {
                        howManyRed--;
                        //Debug.Log("Odejmuje od czerwonej wartosci, zostalo " + howManyRed);
                        listOfPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractRedValue = false;

                    }
                    if (listOfPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractYellowValue == true)
                    {
                        howManyYellow--;
                        listOfPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractYellowValue = false;

                    }

                    while (listOfPlatformBricks[i].GetComponentInChildren<Brick>().brickType == Brick.BrickType.Grey)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, availableColors.Count);


                        switch (availableColors[randomNumber])
                        {
                            case 0:
                                if (howManyBlue < 24)
                                {
                                    howManyBlue++;
                                    listOfPlatformBricks[i].GetComponentInChildren<Brick>().brickType = Brick.BrickType.Blue;
                                    listOfPlatformBricks[i].GetComponentInChildren<Renderer>().material = brickColor[0];
                                    listOfPlatformBricks[i].GetComponentInChildren<Brick>().tag = "BlueBrick";
                                    //isBrickGenerated[i] = true;
                                }
                                break;
                            case 1:
                                if (howManyRed < 24)
                                {

                                    howManyRed++;
                                    //Debug.Log("Dodaje do czerwonej wartosci, jest " + howManyRed);

                                    listOfPlatformBricks[i].GetComponentInChildren<Brick>().brickType = Brick.BrickType.Red;
                                    listOfPlatformBricks[i].GetComponentInChildren<Renderer>().material = brickColor[1];
                                    listOfPlatformBricks[i].GetComponentInChildren<Brick>().tag = "RedBrick";

                                    //isBrickGenerated[i] = true;
                                }
                                break;
                            case 2:
                                if (howManyYellow < 24)
                                {
                                    howManyYellow++;
                                    listOfPlatformBricks[i].GetComponentInChildren<Brick>().brickType = Brick.BrickType.Yellow;
                                    listOfPlatformBricks[i].GetComponentInChildren<Renderer>().material = brickColor[2];
                                    listOfPlatformBricks[i].GetComponentInChildren<Brick>().tag = "YellowBrick";

                                    //isBrickGenerated[i] = true;
                                }
                                break;
                        }
                        listOfPlatformBricks[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                        listOfPlatformBricks[i].GetComponentInChildren<BoxCollider>().enabled = true;

                    }

                }
        }
        else
        {
            if (randomListofPlatformBricks.Count>howManyBricks-2)
                for (int i = 0; i < howManyBricks; i++)
                {
                    if (randomListofPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractBlueValue == true)
                    {
                        howManyBlue--;
                        randomListofPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractBlueValue = false;
                    }
                    if (randomListofPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractRedValue == true)
                    {
                        howManyRed--;
                        //Debug.Log("Odejmuje od czerwonej wartosci, zostalo " + howManyRed);
                        randomListofPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractRedValue = false;

                    }
                    if (randomListofPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractYellowValue == true)
                    {
                        howManyYellow--;
                        randomListofPlatformBricks[i].GetComponentInChildren<Brick>().shouldIExtractYellowValue = false;

                    }

                    while (randomListofPlatformBricks[i].GetComponentInChildren<Brick>().brickType == Brick.BrickType.Grey)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, availableColors.Count);

                        if(randomListofPlatformBricks.Count !=howManyBricks)
                        {
                            break;
                        }
                        if(howManyBlue==24&&howManyRed == 24 && howManyYellow==24)
                        {
                            howManyBlue--;
                            howManyRed--;
                            howManyYellow--;
                        }
                        switch (availableColors[randomNumber])
                        {
                            case 0:
                                if (howManyBlue < 24)
                                {
                                    howManyBlue++;
                                    randomListofPlatformBricks[i].GetComponentInChildren<Brick>().brickType = Brick.BrickType.Blue;
                                    randomListofPlatformBricks[i].GetComponentInChildren<Renderer>().material = brickColor[0];
                                    randomListofPlatformBricks[i].GetComponentInChildren<Brick>().tag = "BlueBrick";
                                    //isBrickGenerated[i] = true;
                                }
                                break;
                            case 1:
                                if (howManyRed < 24)
                                {

                                    howManyRed++;
                                    //Debug.Log("Dodaje do czerwonej wartosci, jest " + howManyRed);

                                    randomListofPlatformBricks[i].GetComponentInChildren<Brick>().brickType = Brick.BrickType.Red;
                                    randomListofPlatformBricks[i].GetComponentInChildren<Renderer>().material = brickColor[1];
                                    randomListofPlatformBricks[i].GetComponentInChildren<Brick>().tag = "RedBrick";

                                    //isBrickGenerated[i] = true;
                                }
                                break;
                            case 2:
                                if (howManyYellow < 24)
                                {
                                    howManyYellow++;
                                    randomListofPlatformBricks[i].GetComponentInChildren<Brick>().brickType = Brick.BrickType.Yellow;
                                    randomListofPlatformBricks[i].GetComponentInChildren<Renderer>().material = brickColor[2];
                                    randomListofPlatformBricks[i].GetComponentInChildren<Brick>().tag = "YellowBrick";

                                    //isBrickGenerated[i] = true;
                                }
                                break;
                        }
                        randomListofPlatformBricks[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                        randomListofPlatformBricks[i].GetComponentInChildren<BoxCollider>().enabled = true;

                    }

                }
        }
    }
}
