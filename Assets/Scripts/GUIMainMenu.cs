//check to see if we have some saved data in the playerprefs
//check the version of the saved data
//if the saved version of the data is not the current version
//<do something>
//else if the saved version is the current version
//check to see if they have a character saved - check for a character name
//if they do not have a character saved, load the character generation scene
//else if they do have a character saved
//if they want to load the character - load the character and go to level 1
//if they want to delete the character - delete the character and go to the character generation scene

using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour {

    public bool clearPrefs = false;
	public UISlider percentLoaded;
	public UIButton continueButton;

	[HideInInspector]
	public string LevelToLoad = GameConstants.levelNames[1];

    private string _characterGeneration = GameConstants.levelNames[1];
    private string _firstLevel = GameConstants.levelNames[2];

    private bool _hasCharacter = false;
    private float _percentLoaded = 0;
//    private bool _displayOptions = true;

    /******************************/
    /* Menu Window Variables */
    /******************************/
    private bool _displayInventoryWindow = false;
    private const int MENU_WINDOW_ID = 4;
    private Rect _menuWindowRect = new Rect(Screen.width * 0.5f - 180, Screen.height * 0.5f - 100, 361.5f, 350);  //Screen.width * 0.5f - (284f * 0.5f)

	void Awake(){
		continueButton.isEnabled = false;
	}
	
	void Start () {
		Screen.lockCursor = false;
        if (clearPrefs)
            PlayerPrefs.DeleteAll();
		//yield return LoadLevelWithProgress(_levelToLoad);
        if (PlayerPrefs.HasKey("ver"))
        {
            Debug.Log("There is a ver key");
            if (PlayerPrefs.GetFloat("ver") != GameConstants.VERSION_NUMBER)
            {
                //Debug.Log("Saved version is not the same as current version");
                /* Upgrade playerprefs here */
            }
            else
            {
				//Debug.Log(PlayerPrefs.HasKey);
                //Debug.Log("Saved version is the same as the current version");
                if (PlayerPrefs.HasKey("Players Alias"))
                {
                    Debug.Log("There is an Alias tag");
                    if (PlayerPrefs.GetString("Players Alias") == "")
                    {
                        Debug.Log("The Player First Name key does not have anything in it");
                        //CreateCharacter();
                    }
                    else
                    {
                        Debug.Log("The Player First Name key has a value");
                        _hasCharacter = true;
						continueButton.isEnabled = true;

                    }
                }
                else
                {
                    //Debug.Log("There is no Player First Name key");
                    //CreateCharacter();
                }
            }
        }
        else
        {
            //Debug.Log("There is no ver key");
            CreateCharacter();
        }
    }

	// adjusts the Progressbar with the amount loaded
	public IEnumerator LoadLevelWithProgress(string levelToLoad){
		if (levelToLoad == "")
			yield break;

		var async = Application.LoadLevelAsync(levelToLoad);
		while(!async.isDone){
			percentLoaded.value = async.progress;
			yield return null;
		}
	}
   

    private void CreateCharacter() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("ver", GameConstants.VERSION_NUMBER);		
        LevelToLoad = _characterGeneration;
    }
}
