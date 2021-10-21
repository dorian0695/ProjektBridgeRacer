using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHolder : MonoBehaviour
{
    public event EventHandler OnKeysChanged;
    private List<Brick.BrickType> brickList;

    private List<Brick.BrickType> blueBrickList;
    private List<Brick.BrickType> redBrickkList;
    private List<Brick.BrickType> yellowBrickList;

    public Brick.BrickType brickTypeToCollect;

    [SerializeField]
    public int blueBrickCounter = 0;
    [SerializeField]
    public int redBrickCounter = 0;
    [SerializeField]
    public int yellowBrickCounter = 0;

    //public List<GameObject> listOfBricksInTower;

    private void Awake()
    {
        brickList = new List<Brick.BrickType>();        
    }
    private void Start()
    {
        switch (gameObject.tag)
        {
            case "BlueGuy":
                brickTypeToCollect = Brick.BrickType.Blue;
                break;
            case "RedGuy":
                brickTypeToCollect = Brick.BrickType.Red;
                break;
            case "YellowGuy":
                brickTypeToCollect = Brick.BrickType.Yellow;
                break;
        }

    }

        public List<Brick.BrickType> GetKeyList()
    {
        return brickList;
    }

    public void AddBrick(Brick.BrickType keyType)
    {
        switch(keyType)
        {
            case Brick.BrickType.Blue:
                //blueBrickList.Add(keyType);
                //listOfBricksInTower[blueBrickCounter].SetActive(true);
                blueBrickCounter++;
                break;
            case Brick.BrickType.Red:
                //blueBrickList.Add(keyType);
                //listOfBricksInTower[redBrickCounter].SetActive(true);
                redBrickCounter++;
                break;
            case Brick.BrickType.Yellow:
                //listOfBricksInTower[yellowBrickCounter].SetActive(true);
                //blueBrickList.Add(keyType);
                yellowBrickCounter++;
                break;
        }
        brickList.Add(keyType);
        
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveKey(Brick.BrickType keyType)
    {
        brickList.Remove(keyType);
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool ContainsKey(Brick.BrickType keyType)
    {
        return brickList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Brick brick = collision.gameObject.GetComponent<Brick>();
        if (brick.GetBrickType() == brickTypeToCollect)
        {
            AddBrick(brick.GetBrickType());
            brick.gameObject.GetComponent<Brick>().brickType = Brick.BrickType.Grey;
            brick.gameObject.GetComponentInChildren<Brick>().gameObject.tag = "GreyBrick";
            brick.gameObject.GetComponent<MeshRenderer>().enabled = false;
            brick.gameObject.GetComponent<BoxCollider>().enabled = false;
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
            //Destroy(brick.gameObject);
        }

        //KeyDoor keyDoor = collision.gameObject.GetComponent<KeyDoor>();
        //if (keyDoor != null)
        //{
        //    if (ContainsKey(keyDoor.GetKeyType())) 
        //    {
        //        RemoveKey(keyDoor.GetKeyType());
        //        keyDoor.OpenDoor(collision.gameObject.gameObject.transform);
        //    }
        //}
    }
}
