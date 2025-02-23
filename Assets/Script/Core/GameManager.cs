using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    #region Singleton
        public static DungeonManager Instance {get; private set;} //singleton
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

    /*Todo:
    
    */
    [SerializeField] public int currentStage {get; set;} = 1;
    [SerializeField] public int currentLevel {get; set;} = 1;

 
}
