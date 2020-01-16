public class CubeData
{
    public bool frontSide = false;
    public bool backSide = false;
    public bool leftSide = false;
    public bool rightSide = false;
    public bool topSide = true;
    public bool bottomSide = false;
    public float size = 1;
    public CubeType cubeType = CubeType.dirt;
}

public enum CubeType
{
    dirt,
    grass,
    sand,
    water
}