// STATIC LASS FOR STORING DATA RELEVANT FOR SINGLE LEVEL

public static class LevelManager
{
    public static bool ShowToDoCanvas = true;
    public static int NumberOfEdgeCollisions = 0;

    public static void Reset()
    {
        ShowToDoCanvas = true;
        NumberOfEdgeCollisions = 0;
    }
}
