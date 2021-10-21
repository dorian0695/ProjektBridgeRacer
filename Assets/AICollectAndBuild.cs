using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AICollectAndBuild : MonoBehaviour
{
    public bool isItFirstPlatfom;
    public bool isItSecondPlatfom;
    public bool isItThirdPlatfom;

    public bool searchFirstStairs;
    public bool searchSecondStairs;
    public bool searchThirdStairs;

    public Rigidbody myRigidBody;

    public GameObject target;

    public List<GameObject> listOfPlatformBricks;
    public List<GameObject> listOfBricksToFollow;
    public List<GameObject> listOfStairsBricks;

    public Brick.BrickType brickTypeToCollect;

    [SerializeField] private Animator m_animator = null;
    //public List<GameObject> listOfBricksInTower;

    [SerializeField]
    public int blueBrickCounter = 0;
    [SerializeField]
    public int redBrickCounter = 0;
    [SerializeField]
    public int yellowBrickCounter = 0;


    //private List<Brick.BrickType> brickList;
    private bool firstTarget = true;

    public float timeForRefresh;
    public float refreshCooldown = 1f;
    public bool buildStairs;
    public bool goToPointer;
    public string pickPointer = "";

    public bool platformChanged;
    public bool stopForKnockback = false;
    int wait = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_animator.SetBool("Grounded", true);
        m_animator.SetFloat("MoveSpeed", 2f);

        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        switch (gameObject.tag)
        {
            case "BlueGuy":
                brickTypeToCollect = Brick.BrickType.Blue;
                searchFirstStairs = true;
                break;
            case "RedGuy":
                brickTypeToCollect = Brick.BrickType.Red;
                searchSecondStairs = true;
                break;
            case "YellowGuy":
                brickTypeToCollect = Brick.BrickType.Yellow;
                searchThirdStairs = true;
                break;
        }

        isItFirstPlatfom = true;
        CreateAListOfPlatformBricks();
        CreateAListOfStairsBricks();
        CreateAListOfBricksToFollow();

    }

    private void CreateAListOfStairsBricks()
    {
        listOfStairsBricks = new List<GameObject>();
        switch (gameObject.tag)
        {

            case "BlueGuy":
                if (isItFirstPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Stairs3Platform1Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "3rdPointerPlatform1";
                    }
                if (isItSecondPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Stairs3Platform2Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "3rdPointerPlatform2";
                    }
                if (isItThirdPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("StairsPlatform3Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "FinalPointerPlatform";
                    }
                break;
            case "RedGuy":
                if (isItFirstPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Stairs2Platform1Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "2ndPointerPlatform1";
                    }
                if (isItSecondPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Stairs2Platform2Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "2ndPointerPlatform2";
                    }
                if (isItThirdPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("StairsPlatform3Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "FinalPointerPlatform";
                    }
                break;
            case "YellowGuy":
                if (isItFirstPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Stairs1Platform1Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "1stPointerPlatform1";
                    }
                if (isItSecondPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Stairs1Platform2Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "1stPointerPlatform2";
                    }
                if (isItThirdPlatfom)
                    foreach (GameObject brick in GameObject.FindGameObjectsWithTag("StairsPlatform3Bricks"))
                    {
                        listOfStairsBricks.Add(brick);
                        pickPointer = "FinalPointerPlatform";
                    }
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeForRefresh)
        {
            CreateAListOfBricksToFollow();
            timeForRefresh = Time.time + refreshCooldown;  

        }

        if (!buildStairs && !goToPointer)
        {
            if(listOfBricksToFollow.Count==0)
            {
                CreateAListOfBricksToFollow();
            }
            if (platformChanged || listOfBricksToFollow.Count==0)
            {
                CreateAListOfPlatformBricks();
                CreateAListOfStairsBricks();
                CreateAListOfBricksToFollow();
                platformChanged = false;
            }
            if (firstTarget)
            {
                FindClosestBrick();
                if (target != null)
                {
                    firstTarget = false;
                }
            }
            if (!target.GetComponent<MeshRenderer>().enabled)
            {
                listOfBricksToFollow.Remove(target);
                FindClosestBrick();
            }
            FindClosestBrick();
        }
        else if(!buildStairs && goToPointer)
        {
            FindPointer();
            if(tag=="RedGuy"&& redBrickCounter==0)
            {
                goToPointer = false;
                FindClosestBrick();
            }
            if (tag == "YellowGuy" && yellowBrickCounter == 0)
            {
                goToPointer = false;
                FindClosestBrick();
            }
        }
        else
        {
            switch(gameObject.tag)
            {
                case "RedGuy":
                    if (redBrickCounter == 0)
                        FindPointer();
                    else
                        FindClosestStairs();
                    break;
                case "YellowGuy":
                    if (yellowBrickCounter == 0)
                        FindPointer();
                    else
                        FindClosestStairs();
                    break;
            }
        }
        var newRotation = Quaternion.LookRotation(target.transform.position - transform.position);//, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);

        if(wait>80)
        {
            stopForKnockback = false;
        }
        if (!stopForKnockback)
        {
            m_animator.SetFloat("MoveSpeed", 2f);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2 * Time.deltaTime);
            wait = 0;
        }
        else
        {
            m_animator.SetFloat("MoveSpeed", 0f);
            wait++;
        }
    }

    void CreateAListOfPlatformBricks()
    {
        listOfPlatformBricks = new List<GameObject>();
        if (isItFirstPlatfom)
            foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Platform1Brick"))
            {
                listOfPlatformBricks.Add(brick);
            }
        if (isItSecondPlatfom)
            foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Platform2Brick"))
            {
                listOfPlatformBricks.Add(brick);
            }
        if (isItThirdPlatfom)
            foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Platform3Brick"))
            {
                listOfPlatformBricks.Add(brick);
            }
    }

    void CreateAListOfBricksToFollow()
    {
        listOfBricksToFollow = new List<GameObject>();
        if (isItFirstPlatfom)
            foreach (GameObject brick in listOfPlatformBricks)
            {
                switch (gameObject.tag)
                {
                    case "BlueGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().gameObject.tag == "BlueBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                    case "RedGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().gameObject.tag == "RedBrick")
                                listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                    case "YellowGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().gameObject.tag == "YellowBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                }
            }
        if (isItSecondPlatfom)
            foreach (GameObject brick in listOfPlatformBricks)
            {
                switch (gameObject.tag)
                {
                    case "BlueGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().tag == "BlueBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                    case "RedGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().gameObject.tag == "RedBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                    case "YellowGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().tag == "YellowBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                }
            }
        if (isItThirdPlatfom)
            foreach (GameObject brick in listOfPlatformBricks)
            {
                switch (gameObject.tag)
                {
                    case "BlueGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().tag == "BlueBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                    case "RedGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().gameObject.tag == "RedBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                    case "YellowGuy":
                        if (brick.GetComponentInChildren<MeshRenderer>().tag == "YellowBrick")
                            listOfBricksToFollow.Add(brick.GetComponentInChildren<MeshRenderer>().gameObject);
                        break;
                }
            }
    }

    void FindClosestBrick()
    {
        target = listOfBricksToFollow.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First();
    }
    void FindClosestStairs()
    {
        target = listOfStairsBricks.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First(go => !go.GetComponent<MeshRenderer>().enabled);
        
    }

    void FindPointer()
    {
        target = GameObject.FindGameObjectWithTag(pickPointer);
    }
        private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == pickPointer)
        {
            switch (gameObject.tag)
            {
                case "RedGuy":
                    if (redBrickCounter == 0)
                    {
                        buildStairs = false;
                        FindClosestBrick();
                    }
                    else
                    {
                        buildStairs = true;
                        goToPointer = false;
                    }
                    break;
                case "YellowGuy":
                    if (yellowBrickCounter == 0)
                    {
                        buildStairs = false;
                        FindClosestBrick();
                    }
                    else
                    {
                        buildStairs = true;
                        goToPointer = false;
                    }
                    break;
            }

        }
        Brick brick = collision.gameObject.GetComponent<Brick>();
        if (brick.GetBrickType() == brickTypeToCollect)
        {
            AddBrick(brick.GetBrickType());
            brick.gameObject.GetComponent<Brick>().brickType = Brick.BrickType.Grey;
            brick.gameObject.GetComponentInChildren<Brick>().gameObject.tag ="GreyBrick";
            brick.gameObject.GetComponent<MeshRenderer>().enabled = false;
            brick.gameObject.GetComponent<BoxCollider>().enabled = false;
            listOfBricksToFollow.Remove(brick.gameObject.GetComponent<Brick>().gameObject);
            FindClosestBrick();

            switch (brickTypeToCollect)
            {
                case Brick.BrickType.Blue:
                    brick.gameObject.GetComponent<Brick>().shouldIExtractBlueValue = true;
                    break;
                case Brick.BrickType.Red:
                    brick.gameObject.GetComponent<Brick>().shouldIExtractRedValue = true;
                    break;
                case Brick.BrickType.Yellow:
                    brick.gameObject.GetComponent<Brick>().shouldIExtractYellowValue = true;
                    break;
            }
        }

    }

    public void AddBrick(Brick.BrickType keyType)
    {
        switch (keyType)
        {
            case Brick.BrickType.Blue:
                blueBrickCounter++;
                break;
            case Brick.BrickType.Red:
                redBrickCounter++;
                if (redBrickCounter > 12)
                {
                    goToPointer = true;
                    FindPointer();  
                }
                break;
            case Brick.BrickType.Yellow:
                if (yellowBrickCounter > 12)
                {
                    goToPointer = true;
                    FindPointer();
                }
                yellowBrickCounter++;
                break;
        }
    }

}
