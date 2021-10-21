using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsBrickScript : MonoBehaviour
{

    [SerializeField]
    private List<Material> brickColor;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "BlueGuy":
                if (collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter >= 1)
                {
                    if (meshRenderer.enabled == false)
                        meshRenderer.enabled = true;
                    if (GetComponent<Brick>().GetBrickType() != Brick.BrickType.Blue)
                    {
                        GetComponent<Brick>().brickType = Brick.BrickType.Blue;
                        GetComponent<Renderer>().material = brickColor[0];
                        //collision.gameObject.GetComponent<BrickHolder>().listOfBricksInTower[collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter - 1].SetActive(false);

                        collision.gameObject.GetComponent<BrickHolder>().blueBrickCounter--;
                    }
                }
                break;
            case "RedGuy":
                if (collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter >= 1)
                {
                    if (meshRenderer.enabled == false)
                        meshRenderer.enabled = true;
                    if (GetComponent<Brick>().GetBrickType() != Brick.BrickType.Red)
                    {
                        GetComponent<Brick>().brickType = Brick.BrickType.Red;
                        GetComponent<Renderer>().material = brickColor[1];
                        //collision.gameObject.GetComponent<AICollectAndBuild>().listOfBricksInTower[collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter - 1].SetActive(false);
                        collision.gameObject.GetComponent<AICollectAndBuild>().redBrickCounter--;
                    }
                }
                break;
            case "YellowGuy":
                if (collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter >= 1)
                {
                    if (meshRenderer.enabled == false)
                        meshRenderer.enabled = true;
                    if (GetComponent<Brick>().GetBrickType() != Brick.BrickType.Yellow)
                    {
                        GetComponent<Brick>().brickType = Brick.BrickType.Yellow; 
                        GetComponent<Renderer>().material = brickColor[2];
                        //collision.gameObject.GetComponent<AICollectAndBuild>().listOfBricksInTower[collision.gameObject.GetComponent<BrickHolder>().yellowBrickCounter - 1].SetActive(false);
                        collision.gameObject.GetComponent<AICollectAndBuild>().yellowBrickCounter--;
                    }
                }
                break;

        }
    }
}
