using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    #region Singleton
        public static DungeonManager Instance {get; private set;}
        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
    #endregion

    public void RoomCleared(Room room)
    {
        Destroy(room.barrierReference);
    }    
}