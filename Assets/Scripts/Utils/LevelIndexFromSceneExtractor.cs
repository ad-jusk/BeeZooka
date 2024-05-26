using System;

// STATIC CLASS FOR DETERMINING LEVEL INDEX FROM SCENE NAME
public class LevelIndexFromSceneExtractor
{
    public static int GetLevelIndex(string sceneName)
    {
        try
        {
            int levelIndex = int.Parse(sceneName);
            return levelIndex;
        }
        catch (FormatException)
        {
            return -1;
        }
    }
}
