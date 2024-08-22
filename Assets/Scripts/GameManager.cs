using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }
    public MissileLauncher MissileLauncher { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public InputManager InputManager { get; private set; }
    //public AdManager AdManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LevelManager = GetComponent<LevelManager>();
        MissileLauncher = GetComponent<MissileLauncher>();
        EnemyManager = GetComponent<EnemyManager>();
        UIManager = GetComponent<UIManager>();
        InputManager = GetComponent<InputManager>();
        //AdManager = GetComponent<AdManager>();
    }

    private void Start()
    {
        LevelManager.Init();
        MissileLauncher.Init();
        EnemyManager.Init();
        UIManager.Init();
        InputManager.Init();
        //AdManager.Init();
    }
}
