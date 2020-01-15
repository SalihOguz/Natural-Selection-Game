public class CubeData
{
    public bool frontSide;
    public bool backSide;
    public bool leftSide;
    public bool rightSide;
    public bool topSide;
    public bool bottomSide;
    public float size = 1;
    public CubeType cubeType;
}

public enum CubeType
{
    dirt,
    grass,
    sand,
    water
}