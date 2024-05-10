using UnityEngine;

// SINGLETON CLASS FOR NOTIFYING GAME OBJECT ABOUT EVENTS THAT HAPPEN DURING GAME
public class GameEventsManager : Singleton<GameEventsManager>
{
    #region GameEvents

    public delegate void NextLevel();
    public event NextLevel OnNextLevel;

    #endregion

    public void NotifyNextLevel() {
        OnNextLevel?.Invoke();
    }
}
