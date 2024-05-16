using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
{
    #region GameEvents

    public delegate void BeehiveEntered();
    public event BeehiveEntered OnBeehiveEntered;
    public delegate void BeehiveMissed();
    public event BeehiveMissed OnBeehiveMissed;

    #endregion

    public void NotifyBeehiveEntered() {
        OnBeehiveEntered?.Invoke();
    }

    public void NotifyBeehiveMissed() {
        OnBeehiveMissed?.Invoke();
    }
}
