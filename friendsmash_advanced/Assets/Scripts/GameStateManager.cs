using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    static bool endGame = false;
	private GUISkin skin;
//	public GUIStyle style;
    private static GameStateManager instance;

    public static bool ScoringLockout = true;

    public static int StartingLives = 3, StartingScore = 0;
    private int lives, score;
    private int? highScore;

    private string username = null;
    public static Texture UserTexture;
    public static Texture FriendTexture = null;
	public static Texture []FriendTextureEnemys = new Texture[10];
	public int numberFriendID = -1;
	public static int indexFriendEnemy = -1;
    private string friendName = null;
    private string friendID = null;

    public static bool IsFullscreen = false;

    public bool Immortal;
    private static bool immortal;

    public static int ToSmash = -1;

    public static string FriendID
    {
        set { Instance.friendID = value; }
        get { return Instance.friendID; }
    }

    public static string FriendName
    {
        set { Instance.friendName = value; }
        get { return Instance.friendName == null ? "Blue Guy" : Instance.friendName; }
    }

    public static Dictionary<string, Player> leaderboard;

    void Start()
    {
		skin = Resources.Load("GUISkin") as GUISkin;
        lives = StartingLives;
        score = StartingScore;
        immortal = Instance.Immortal;
        ScoringLockout = false;
        Time.timeScale = 1.0f;
    }

    public void StartGame()
    {
        Start();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static GameStateManager Instance { get { return current(); } }
    public static int Score { get { return Instance.score; } }
    public static int HighScore { get { return Instance.highScore.HasValue ? Instance.highScore.Value : 0; } set { Instance.highScore = value; }}
    public static int LivesRemaining { get { return Instance.lives; } }
    public static string Username
    {
        get { return Instance.username; }
        set { Instance.username = value; }
    }
    delegate GameStateManager InstanceStep();

    static InstanceStep init = delegate()
    {
        GameObject container = new GameObject("GameStateManagerManager");
        instance = container.AddComponent<GameStateManager>();
        instance.lives = StartingLives;
        instance.score = StartingScore;
        instance.highScore = null;
        current = then;
        return instance;
    };
    static InstanceStep then = delegate() { return instance; };
    static InstanceStep current = init;

    public static void onFriendDie()
    {
        if (--Instance.lives == 0)
        {
            EndGame();
        }
    }

    public static void onFriendSmash()
    {
        if (!ScoringLockout) ++Instance.score;
    }

    public static void onEnemySmash(GameObject enemy)
    {
        // Instance.fatalEnemy = enemy;
        enemy.AddComponent<EnemyExploder>();
    }

    public static void EndGame()
    {
        if (immortal) return;
        GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject t in friends)
        {
            Destroy(t);
        }
        foreach (GameObject t in enemies)
        {
            Destroy(t);
        }
		endGame = true;
		Time.timeScale = 0.0f;
		FbDebug.Log("EndGame Instance.highScore = " + Instance.highScore + "\nInstance.score = " + Instance.score);

        if (FB.IsLoggedIn && Instance.highScore.HasValue && Instance.highScore < Instance.score) // don't allow high score to be set unless we've read it from FB (-1 check)
        {
            Instance.highScore = Instance.score;
            FbDebug.Log("Player has new high score :" + Instance.score);
            
            var query = new Dictionary<string, string>();
            query["score"] = Instance.score.ToString();
            FB.API("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult r) { FbDebug.Log("Result: " + r.Text); }, query);
        }

        
        if (FB.IsLoggedIn)
        {
            var querySmash = new Dictionary<string, string>();
            querySmash["profile"] = GameStateManager.FriendID;
            FB.API ("/me/" + FB.AppId + ":smash", Facebook.HttpMethod.POST, delegate(FBResult r) { FbDebug.Log("Result: " + r.Text); }, querySmash);
        }

    }
	void OnGUI()
	{
		if(endGame)
		{
			float buttonWidth = Screen.width / 7.0f;
			float buttonHeight = Screen.height / 9.0f;
			
			GUI.skin = skin;
			// Draw a button to start the game
			if (GUI.Button(
				// Center in X, 2/3 of the height in Y
				new Rect(Screen.width  - (buttonWidth ), ( Screen.height ) - (buttonHeight * 1.5f), buttonWidth , buttonHeight * 1.5f),
				"BACK"
				))
			{
				Application.LoadLevel("MainMenu");
				endGame = false;
			}
			if (GUI.Button(
				// Center in X, 2/3 of the height in Y
				new Rect( (Screen.width / 2),Screen.height - (buttonHeight * 1.5f ), buttonWidth , buttonHeight * 1.5f),
				"RATE"
				))
			{
				
				Application.OpenURL("https://itunes.apple.com/us/app/friend-hit/id911697534?ls=1&mt=8");
			}
			if (GUI.Button(
				// Center in X, 2/3 of the height in Y
				new Rect( (buttonWidth / 10),Screen.height - (buttonHeight * 1.5f ), buttonWidth , buttonHeight * 1.5f),
				"SHARE"
				))
			{
				FB.Feed(
					linkCaption: "I just smashed " + GameStateManager.Score.ToString() + " friends! Can you beat it?",
					picture: "",
					linkName: "Checkout my Friend Smash greatness!",
					link: "https://itunes.apple.com/us/app/friend-hit/id911697534?ls=1&mt=8"
					);
			}
		}
	}
}
