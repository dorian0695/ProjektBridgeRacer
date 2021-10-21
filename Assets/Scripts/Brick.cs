using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public bool shouldIExtractBlueValue;
    public bool shouldIExtractRedValue;
    public bool shouldIExtractYellowValue;

    public BrickType brickType;
    public enum BrickType
    {
        Blue,
        Red,
        Yellow,
        Grey
    }
    public BrickType GetBrickType()
    {
        return brickType;
    }
}