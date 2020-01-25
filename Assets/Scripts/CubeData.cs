using UnityEngine;

public class CubeData
{
    public Vector3 pos;
    public bool frontSide = false;
    public bool backSide = false;
    public bool leftSide = false;
    public bool rightSide = false;
    public bool topSide = true;
    public bool bottomSide = false;
    public float size = 1;
    public CubeType cubeType = CubeType.dirt;
    public CubeFeature cubeFeature = CubeFeature.none;
    public Animal standingAnimal;
    public Plant standingPlant;
}

public enum CubeType
{
    dirt,
    grass,
    sand,
    water
}

public enum CubeFeature
{
    none,
    tree,
    plant1,
    plant2,
    plant3
}