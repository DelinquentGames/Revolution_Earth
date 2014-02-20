using UnityEngine;
using System.Collections;

public enum MainMenuState
{
	IDLE,
	OPTIONS,
	GRAPHICS,
	ABOUT,
	CHARACTER,
	INVENTORY,
	EQUIPMENT,
	MAINMENU
}

public class MainMenuScreen : MonoBehaviour {

	public Texture2D menuBackground;
	private Rect menuBackgroundRect;
	
	public Texture2D windowBackground;
	private Rect windowBackgroundRect;
	
	public Texture2D playGame;
	public Texture2D playGameOver;
	private Rect playGameRect;
	
	public Texture2D resume;
	public Texture2D resumeOver;
	private Rect resumeRect;

    public Texture2D character;
    public Texture2D characterOver;
    private Rect characterRect;
	
    public Texture2D equipment;
    public Texture2D equipmentOver;
    private Rect equipmentRect;

    public Texture2D inventory;
    public Texture2D inventoryOver;
    private Rect inventoryRect;
	
	public Texture2D options;
	public Texture2D optionsOver;
	private Rect optionsRect;

	public Texture2D about;
	public Texture2D aboutOver;
	private Rect aboutRect;

    public Texture2D exit;
    public Texture2D exitOver;
    private Rect exitRect;
	
	public GUISkin hudSkin;
	
	private GUIStyle panelLeft;
	private Rect panelLeftRect;
	
	private GUIStyle panelRight;
	private Rect panelRightRect;
	
	private GUIStyle menuPanelRight;
	private Rect menuPanelRightRect;
	
	private GUIStyle descriptionStyle;
	private GUIStyle titleStyle;
	private GUIStyle customBox;
	
	private Vector2 mousePos;
	private Vector2 screenSize;
	
	private Event evt;
	
	public static MainMenuState state;
	private float lastMouseTime;
	
	public Texture2D receiveDamageOn;
	public Texture2D receiveDamageOff;
	public Texture2D dontReceiveDamageOn;
	public Texture2D dontReceiveDamageOff;
	private Rect damageRect;
	
	private Rect scrollPosition;
	private Rect scrollView;
	private Vector2 scroll;
	
	public Texture2D black;
	private float alpha;
	static public bool goingToGame;
    static public bool showProgress;
	
	private Vector2 aboutScroll;
	private Vector2 inventoryScroll;
	private Vector2 graphicsScroll;
	private GUIStyle aboutStyle;
	private GUIStyle inventoryStyle;
	
	private bool exitMainMenu;	
	private bool resumeGame;
	public bool visible;
	
	private GUIStyle sliderBackground;
	private GUIStyle sliderButton;
	
	public Texture2D greenBar;
	public Texture2D checkOn;
	public Texture2D checkOff;
	public Texture2D whiteMarker;
	
	private float margin = 30f;

	private Rect questionRect;
	private Rect greenRect;
	private GUIStyle tooltipStyle;
	private GUIStyle questionButtonStyle;
	
	private GUIStyle dgsLogo;
	private GUIStyle unityLogo;
	
	public AudioClip overSound;
	public float overVolume = 0.4f;

	public AudioClip clickSound;
	public float clickVolume = 0.7f;
	
	private bool over;
	private bool hideOptions;
    private bool loadingTutorial;

    public GUIStyle textStyle;
	private float angle;
    public Texture2D loadingBackground;
	
	private string _levelToLoad = "";
	private string _menu = GameConstants.levelNames[0];

    private float _doubleClickTimer = 0;
    private const float DOUBLE_CLICK_TIMER_THRESHHOLD = 0.5f;
    private Item _selectedItem;
    private string _toolTip = "";

	private float native_width = 1600;
	private float native_height = 900;

	void Start()
	{
        angle = 0.0f;
		over = false;
        loadingTutorial = false;
        showProgress = false;
		hideOptions = Application.loadedLevelName != "TutorialIsland";

		questionButtonStyle = hudSkin.GetStyle("QuestionButton");
		tooltipStyle = hudSkin.GetStyle("TooltipStyle");
		dgsLogo = hudSkin.GetStyle("DGSLogo");
		unityLogo = hudSkin.GetStyle("UnityLogo");
		questionRect = new Rect(0, 0, 11, 11);
		
		alpha = 1.0f;
		goingToGame = false;
		resumeGame = false;
		exitMainMenu = false;
		
		state = MainMenuState.IDLE;
		
		descriptionStyle = hudSkin.GetStyle("Description");
		titleStyle = hudSkin.GetStyle("Titles");
		customBox = hudSkin.GetStyle("CustomBox");
		panelLeft = hudSkin.GetStyle("PanelLeft");
		panelRight = hudSkin.GetStyle("PanelRight");
		aboutStyle = hudSkin.GetStyle("About");
		
		menuBackgroundRect = new Rect(0, 0, menuBackground.width, menuBackground.height);
		windowBackgroundRect = new Rect(0, 0, windowBackground.width, windowBackground.height);
		panelLeftRect = new Rect(0, 0, 235, 240);
		panelRightRect = new Rect(0, 0, 235, 240);
		playGameRect = new Rect(0, 0, playGame.width * 0.49f, playGame.height * 0.49f);
		optionsRect = new Rect(0, 0, options.width, options.height);
		aboutRect = new Rect(0, 0, about.width, about.height);
		resumeRect = new Rect(0, 0, resume.width, resume.height);
		exitRect = new Rect(0, 0, exit.width, exit.height);
		damageRect = new Rect(0, 0, receiveDamageOn.width * 0.7f, receiveDamageOn.height * 0.7f);
		characterRect = new Rect(0, 0, character.width, character.height);
        equipmentRect = new Rect(0, 0, equipment.width, equipment.height);
        inventoryRect = new Rect(0, 0, inventory.width, inventory.height);
	}
	
	void Update()
	{
		if(goingToGame)
		{
            alpha += Time.deltaTime;
			
            if(alpha >= 1.0)
            {
                if(!loadingTutorial)
                {
                    loadingTutorial = true;
                    Application.LoadLevelAsync("TutorialIsland");
                }
            }
		}
		else
		{
			if(alpha > 0)
			{
				alpha -= Time.deltaTime * 0.5f;
			}
		}
		
        if(Time.timeScale == 0.0 || GameManager.menuVisible)
        {
		    lastMouseTime -= 0.01f;
        }

        if(showProgress)
        {
            angle -= Time.deltaTime * 360;

            if(angle < -360.0f)
            {
                angle += 360.0f;
            }
        }
	}
	
	public void DrawGUI(Event e)
	{
		evt = e;
		screenSize = new Vector2(Screen.width, Screen.height);
		mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		
		if(visible)
		{
			//set up scaling
			float rx = Screen.width / native_width;
			float ry = Screen.height / native_height;
			GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 

			windowBackgroundRect.x = menuBackgroundRect.x + menuBackgroundRect.width;
			windowBackgroundRect.y = (screenSize.y - windowBackgroundRect.height);
			
			GUI.DrawTexture(windowBackgroundRect, windowBackground);

            if (state == MainMenuState.CHARACTER || state == MainMenuState.EQUIPMENT || state == MainMenuState.INVENTORY || state == MainMenuState.OPTIONS || state == MainMenuState.ABOUT)
			{
				panelLeftRect.width = 445;
				panelLeftRect.height = 497;
				panelLeftRect.x = windowBackgroundRect.x + 51;
				panelLeftRect.y = windowBackgroundRect.y + 85;
				GUI.Box(panelLeftRect, "", panelLeft);
				
			}

            if (state == MainMenuState.CHARACTER)
            {
                DrawCharacter();
            }
            else if(state == MainMenuState.EQUIPMENT)
			{
				DrawEquipment();
			} 
            else if(state == MainMenuState.INVENTORY)
			{
				DrawInventory();
			}else if(state == MainMenuState.OPTIONS)
			{
				DrawGraphicOptions();
			}			
			else if(state == MainMenuState.ABOUT)
			{
				DrawAbout();
			}
			
			DrawMenu();
		}

        if(showProgress)
        {   
            float currentProgress = IndustryLoader.industryProgress;//Application.GetStreamProgressForLevel("demo_industry");
            currentProgress *= 100.0f;
            float aux = currentProgress;
            currentProgress = aux;

            if(currentProgress < 1.0)
            {
                GUIUtility.RotateAroundPivot(angle, new Vector2(Screen.width - 28, Screen.height - 28));
                GUI.DrawTexture(new Rect(Screen.width - 56, Screen.height - 56, 56, 56), loadingBackground, ScaleMode.ScaleToFit, true, 0);

                GUI.matrix = Matrix4x4.identity;
                GUI.Label(new Rect(Screen.width - 52, Screen.height - 36, 50, 20), currentProgress.ToString(), textStyle);
            }
        }
        		
        if(alpha > 0.0)
        {
		    Color c = GUI.color;
		    Color d = c;
		    d.a = alpha;
		    GUI.color = d;
		
		    GUI.DrawTexture(new Rect(0, 0, screenSize.x, screenSize.y), black);

            if(goingToGame)
            {
		        GUI.Label(new Rect(Screen.width - 120, Screen.height - 40, 90, 20), "Loading...", textStyle);
            }
		    GUI.color = c;
        }
	}
	
	void DrawGameOptions()
    {
		panelLeftRect.width = 235;
		panelLeftRect.x = windowBackgroundRect.x + 15;
		panelLeftRect.y = windowBackgroundRect.y + 55;

		panelRightRect.x = panelLeftRect.x + 5 + panelLeftRect.width;
		panelRightRect.y = panelLeftRect.y;
		
		damageRect.x = panelLeftRect.x + ((panelLeftRect.width - damageRect.width) * 0.5f);
		damageRect.y = panelLeftRect.y + ((panelLeftRect.height - damageRect.height) * 0.5f);
		
		var dRect = damageRect;
		dRect.x = panelRightRect.x + ((panelRightRect.width - damageRect.width) * 0.5f);
		
		if(Event.current.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
		{
			if(damageRect.Contains(mousePos))
			{
				if(!GameManager.receiveDamage)
				{
					audio.volume = clickVolume;
					audio.PlayOneShot(clickSound);
					GameManager.receiveDamage = true;	
					lastMouseTime = Time.time;
				}
			}
			else if(dRect.Contains(mousePos))
			{
				if(GameManager.receiveDamage)
				{
					audio.volume = clickVolume;
					audio.PlayOneShot(clickSound);
					GameManager.receiveDamage = false;
					lastMouseTime = Time.time;
				}
			}
		}
		
		if(GameManager.receiveDamage)
		{
			GUI.DrawTexture(damageRect, receiveDamageOn);
			GUI.DrawTexture(dRect, dontReceiveDamageOff);
		}
		else
		{
			GUI.DrawTexture(damageRect, receiveDamageOff);
			GUI.DrawTexture(dRect, dontReceiveDamageOn);
		}
		
		GUI.Label(new Rect(windowBackgroundRect.x + 20, windowBackgroundRect.y + 15, 200, 20), "GAME OPTIONS", titleStyle);	
	}
	
	
	private SceneSettings sceneConf;
	void GetSceneRef()
	{
		//var currentScene : int = Application.loadedLevel;
		int currentScene = Application.loadedLevel;
		
        if(Application.loadedLevelName == "demo_start_cutscene")
        {
            currentScene = 0;
        }
        else if(Application.loadedLevelName == "TutorialIsland")
        {
            currentScene = 1;
        }
        else if(Application.loadedLevelName == "demo_industry")
        {
            currentScene = 2;
        }
		//Debug.Log(GameQualitySettings.scenes.Length.ToString());
		if(GameQualitySettings.scenes != null)
		{
			if(currentScene >= 0 && currentScene < GameQualitySettings.scenes.Length)
			{
				sceneConf = GameQualitySettings.scenes[currentScene];		
			}
			else
			{
				currentScene = -1;
			}
		}
		else
		{
			currentScene = -1;
		}
	}
	
	private void DrawSliderOverlay (Rect rect,float val)
	{
		rect.width = Mathf.Clamp(val * 199.0f, 0.0f, 199.0f);
		GUI.DrawTexture (rect, greenBar);
	}
		
	private float SettingsSlider (string name, int nameLen, string tooltip, float v, float vmin, float vmax, string dispname, float dispmul, float dispadd)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(margin);
		GUILayout.BeginVertical();
		GUILayout.Label (name);
				
		questionRect.x = margin + nameLen;
		questionRect.y += 39;

		GUI.Button(questionRect, new GUIContent(string.Empty, tooltip), questionButtonStyle);
		
		v = GUILayout.HorizontalSlider(v, vmin, vmax, GUILayout.Width(210));
		greenRect.y += 39;
		DrawSliderOverlay (greenRect, Mathf.InverseLerp (vmin, vmax, v));
		
		var disp = v * dispmul + dispadd;
		GUI.Label(new Rect(greenRect.x + 220, greenRect.y - 7, 200, 20), dispname + disp.ToString("0.00"));
		
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		
		return v;
	}
	
	private int SettingsIntSlider (string name, int nameLen, string tooltip, int v, int vmin, int vmax, string[] dispnames)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(margin);
		GUILayout.BeginVertical();
		GUILayout.Label (name);
				
		questionRect.x = margin + nameLen;
		questionRect.y += 39;
		GUI.Button(questionRect, new GUIContent(string.Empty, tooltip), questionButtonStyle);
		
		v = (int)GUILayout.HorizontalSlider(v, vmin, vmax, GUILayout.Width(210));
		greenRect.y += 39;
		DrawSliderOverlay (greenRect, Mathf.InverseLerp (vmin, vmax, v));
		
		GUI.Label(new Rect(greenRect.x + 220, greenRect.y - 7, 200, 20), dispnames == null ? v.ToString() : dispnames[v]);
		
		if (Mathf.Abs(vmin-vmax) < 8)
			DrawMarker (greenRect.y + 5, Mathf.Abs(vmin-vmax));
		
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		
		return v;
	}
	
	private void SettingsSpace (int pix)
	{
		GUILayout.Space (pix);
		questionRect.y += pix;
		greenRect.y += pix;
	}
	
	private bool SettingsToggle (bool v, string name, int nameLen, string tooltip)
	{
		GUILayout.BeginVertical();
		GUILayout.Space(7);
		v = GUILayout.Toggle (v, v ? checkOn : checkOff, GUILayout.Width(14), GUILayout.Height(14));
		GUILayout.EndVertical();
		GUILayout.Space(5);
		GUILayout.Label(name);
		questionRect.x = margin + nameLen;
		GUI.Button (questionRect, new GUIContent(string.Empty, tooltip), questionButtonStyle);
		return v;
	}
	
	private void BeginToggleRow ()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(margin);
		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal(GUILayout.Width(400));
		questionRect.y += 21;
	}
	
	private void EndToggleRow (int pix)
	{
		GUILayout.Space (pix);		
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}
	
	void DrawGraphicOptions()
	{
		GetSceneRef();

        int currentQualityLevel = QualitySettings.GetQualityLevel();//QualitySettings.currentLevel;
        Color originalColor = GUI.color;

		if(sceneConf == null) return;
		
		GUI.Label(new Rect(windowBackgroundRect.x + 50, windowBackgroundRect.y + 55, 200, 20), "OPTIONS", titleStyle);	
		
		Rect graphicRect = new Rect(panelLeftRect.x - 10, panelLeftRect.y + 30, panelLeftRect.width + 10, panelLeftRect.height - 35);
		
		GUISkin cSkin = GUI.skin;
		GUI.skin = hudSkin;
		
		greenRect = new Rect(margin, 0, 210, 5);

		GUILayout.BeginArea(graphicRect);
		graphicsScroll = GUILayout.BeginScrollView(graphicsScroll, GUILayout.Width(graphicRect.width));
		
		Rect boxRect = new Rect(17, 0, 430, 0);
		// overall level
		boxRect.height = 18 + 39;
		GUI.Box(boxRect, "", customBox);
		// post-fx
		boxRect.y += 10 + boxRect.height;
		boxRect.height = 93;
		GUI.Box(boxRect, "", customBox);
		// distances
		boxRect.y += 10 + boxRect.height;
		boxRect.height = 18 + 39;
		GUI.Box(boxRect, "", customBox);
		// shadow distance
		boxRect.y += 10 + boxRect.height;
		boxRect.height = 18 + 39;
		GUI.Box(boxRect, "", customBox);
		// texture limit
		boxRect.y += 10 + boxRect.height;
		boxRect.height = 18 + 39;
		GUI.Box(boxRect, "", customBox);
		// terrain
		boxRect.y += 10 + boxRect.height;
		boxRect.height = 18 + 39 * 7;
		GUI.Box(boxRect, "", customBox);
		
		GUILayout.BeginVertical();
		questionRect.y = -31;
		greenRect.y = -9;

        string[] tempstring = {"QUALITY: FASTEST", "QUALITY: FAST", "QUALITY: SIMPLE", "QUALITY: GOOD", "QUALITY: BEAUTIFUL", "QUALITY: FANTASTIC"};
		
		GameQualitySettings.overallQuality = SettingsIntSlider (
			"Overall Quality Level", 125, "Overall quality level of the game.",
			GameQualitySettings.overallQuality, 0, 5, tempstring);
				
		GUILayout.Space(29);
		questionRect.y += 47;
		
		BeginToggleRow ();
		GameQualitySettings.anisotropicFiltering = SettingsToggle (GameQualitySettings.anisotropicFiltering, "Anisotropic Filtering", 153, "Anisotropic filtering for the textures.");
		GUILayout.FlexibleSpace();
		GameQualitySettings.ambientParticles = SettingsToggle (GameQualitySettings.ambientParticles, "Ambient Particles", 355, "Smoke & dust particles.");
		EndToggleRow (50);

		BeginToggleRow ();
		GameQualitySettings.colorCorrection = SettingsToggle (GameQualitySettings.colorCorrection, "Color Correction", 128, "Color correction image effect.");
        GUILayout.FlexibleSpace();
		GameQualitySettings.bloomAndFlares = SettingsToggle (GameQualitySettings.bloomAndFlares, "Bloom & Flares", 336, "Bloom & Lens Flares image effect.");
		EndToggleRow (71);

		BeginToggleRow ();
		GameQualitySettings.sunShafts = SettingsToggle (GameQualitySettings.sunShafts, "Sun Shafts", 100, "Sun Shafts image effect.");
		GUILayout.FlexibleSpace();
		GameQualitySettings.depthOfFieldAvailable = SettingsToggle (GameQualitySettings.depthOfFieldAvailable, "Depth Of Field", 336, "Depth Of Field image effect while aiming.");
		EndToggleRow (73);

		BeginToggleRow ();	
		bool ssaoEnable = SettingsToggle (GameQualitySettings.ssao, "SSAO", 60, "Screen Space Ambient Ccclusion image effect.");
		if(GameQualitySettings.overallQuality > 3)	
			 GameQualitySettings.ssao = ssaoEnable;
		GUILayout.FlexibleSpace();
		EndToggleRow (0);
		
        greenRect.y += 113;
		questionRect.y -= 18;
		
		SettingsSpace (25);
		
		GameQualitySettings.dynamicObjectsFarClip = SettingsSlider (
			"Dynamic Objects Far Distance", 180, "Drawing distance of dynamic objects.",
			GameQualitySettings.dynamicObjectsFarClip, 0.0f, 1.0f, "DYNAMIC: ",
			GameQualitySettings._dynamicLayersRange.y - GameQualitySettings._dynamicLayersRange.x, GameQualitySettings._dynamicLayersRange.x);
		
		SettingsSpace (27);
		
		GameQualitySettings.shadowDistance = SettingsSlider (
			"Shadow Distance", 108, "Realtime shadows drawing distance.",
			GameQualitySettings.shadowDistance, 0.0f, 30.0f, "",
			1.0f, 0.0f);
			
		SettingsSpace (28);

        string[] tempResolution = {"FULL RESOLUTION", "HALF RESOLUTION", "QUARTER RESOLUTION", "1/8 RESOLUTION"};
		
		GameQualitySettings.masterTextureLimit = SettingsIntSlider (
			"Texture Quality", 100, "Overall texture detail.",
			GameQualitySettings.masterTextureLimit, 3, 0, tempResolution);
		
		SettingsSpace (27);
		
		sceneConf.detailObjectDensity = SettingsSlider (
			"Terrain Grass Density", 136, "Grass density.",
			sceneConf.detailObjectDensity, 0.0f, 1.0f, "",
			1.0f, 0.0f);
			
		sceneConf.detailObjectDistance = SettingsSlider (
			"Terrain Grass Distance", 141, "Grass drawing distance.",
			sceneConf.detailObjectDistance, 0.0f, 50.0f, "",
			1.0f, 0.0f);
			
		sceneConf.nearTerrainPixelError = SettingsSlider (
			"Terrain Pixel Error", 146, "Set terrain pixel error.",
			sceneConf.nearTerrainPixelError, 50.0f, 5.0f, "",
			1.0f, 0.0f);
		
		sceneConf.treeDistance = SettingsSlider (
			"Terrain Tree Distance", 137, "Tree drawing distance.",
			sceneConf.treeDistance, 200.0f, 400.0f, "",
			1.0f, 0.0f);
		
        string[] tempRes = {"FULL RESOLUTION", "QUARTER RESOLUTION", "1/16 RESOLUTION"};
		sceneConf.heightmapMaximumLOD = SettingsIntSlider (
			"Terrain Level of Detail", 139, "Overall terrain Level of Detail.",
			sceneConf.heightmapMaximumLOD, 2, 0, tempRes);
		
		sceneConf.terrainTreesBillboardStart = SettingsSlider (
			"Terrain Billboard Start", 140, "Distance from the camera where trees will be rendered as billboards.",
			sceneConf.terrainTreesBillboardStart, 10.0f, 70.0f, "",
			1.0f, 0.0f);
			
		sceneConf.maxMeshTrees = SettingsIntSlider (
			"Max Mesh Trees", 100, "Set the maximum number of trees rendered at full LOD.",
			sceneConf.maxMeshTrees, 5, 60,
			null);
		
		GUILayout.Space(20);
			
		GUILayout.EndVertical();
	
		GUILayout.EndScrollView();
		GUILayout.EndArea();
		
		if(GUI.tooltip != "")
		{
			GUI.Label(new Rect(mousePos.x + 15, mousePos.y - 60, 150, 70), GUI.tooltip, tooltipStyle);
		}
		
		GUI.skin = cSkin;
	}
	
	void DrawMarker(float y,int steps)
	{
		Rect markerRect = new Rect(margin, y + 2, 1, 5);
		float aux;
		float s = steps;
		
		for(int i = 0; i <= steps; i++)
		{
			aux = i;
			aux 	/= s;
			markerRect.x = margin + 5 + aux * 196;
			
			GUI.DrawTexture(markerRect, whiteMarker);
		}
	}
	
	void DrawAbout()
	{
		GUI.Label(new Rect(windowBackgroundRect.x + 50, windowBackgroundRect.y + 55, 200, 20), "ABOUT", titleStyle);	
		
		Rect abRect = new Rect(panelLeftRect.x + 7, panelLeftRect.y + 30, panelLeftRect.width - 12, panelLeftRect.height - 40);

		GUISkin cSkin = GUI.skin;
		GUI.skin = hudSkin;

		GUILayout.BeginArea(abRect);
		//aboutScroll = GUILayout.BeginScrollView(aboutScroll, GUILayout.Width(abRect.width));
		
		GUILayout.BeginHorizontal();
		GUILayout.Space(17);
		GUILayout.BeginVertical();
		GUILayout.Label("Developed by Delinquent Games", aboutStyle, GUILayout.Width(423));
		GUILayout.Space(5);
		string names;
		names = "Adam Troy Davis, ";
		names += "Damion McCoy, ";
		names += "Drew Daleo, ";
		names += "Jennifer McLeod, ";
		names += "Robby Carrington Jr., ";
		names += "Jeremy Harris Jones";
		GUILayout.Label(names, GUILayout.Width(400));		
		GUILayout.Space(70);
		GUI.DrawTexture(new Rect(170, 350, 339 * 0.75f, 94 * 0.75f), dgsLogo.normal.background);
		
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		
		//GUILayout.EndScrollView();
		
		GUILayout.EndArea();

		GUI.skin = cSkin;
	}

    void DrawCharacter()
    {
        GUI.Label(new Rect(windowBackgroundRect.x + 50, windowBackgroundRect.y + 55, 200, 20), "CHARACTER", titleStyle);

        Rect abRect = new Rect(panelLeftRect.x + 7, panelLeftRect.y + 30, panelLeftRect.width - 12, panelLeftRect.height - 40);

        GUISkin cSkin = GUI.skin;
        GUI.skin = hudSkin;

        GUILayout.BeginArea(abRect);
        
        GUILayout.EndArea();

        GUI.skin = cSkin;
    }
	
	void DrawInventory()
	{
		GUI.Label(new Rect(windowBackgroundRect.x + 50, windowBackgroundRect.y + 55, 200, 20), "INVENTORY", titleStyle);
        Rect inventoryRect = new Rect(panelLeftRect.x + 15, panelLeftRect.y + 25, panelLeftRect.width - 20, panelLeftRect.height - 35);

		GUISkin cSkin = GUI.skin;
		GUI.skin = hudSkin;

        greenRect = new Rect(margin, 0, 210, 5);

		GUILayout.BeginArea(inventoryRect);
        inventoryScroll = GUILayout.BeginScrollView(inventoryScroll, GUILayout.Width(inventoryRect.width));
        

        GUILayout.BeginVertical();
        greenRect.y += 113;
        questionRect.y -= 18;

        int cnt = 0;
        
        for (int y = 0; y < 5; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < 3; x++)
            {
                if (cnt < PlayerData.Instance.Inventory.Count)
                {                    
                    if (GUILayout.Button(new GUIContent(PlayerData.Instance.Inventory[cnt].Icon, PlayerData.Instance.Inventory[cnt].ToolTip()), "InventorySlotCommon"))
                    {
                        if (_doubleClickTimer != 0 && _selectedItem != null)
                        {
                            if (Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHHOLD)
                            {
                                //Debug.Log("Double Click: " + PlayerCharacter.Inventory[cnt].Name);

                                if (PlayerData.Instance.EquipedWeapon == null)
                                {
                                    PlayerData.Instance.EquipedWeapon = PlayerData.Instance.Inventory[cnt];
                                    PlayerData.Instance.Inventory.RemoveAt(cnt);
                                }
                                else
                                {
                                    Item temp = PlayerData.Instance.EquipedWeapon;
                                    PlayerData.Instance.EquipedWeapon = PlayerData.Instance.Inventory[cnt];
                                    PlayerData.Instance.Inventory[cnt] = temp;
                                }


                                _doubleClickTimer = 0;
                                _selectedItem = null;
                            }
                            else
                            {
                                //Debug.Log("Reset the double click timer!");
                                _doubleClickTimer = Time.time;
                            }
                        }
                        else
                        {
                            _doubleClickTimer = Time.time;
                            _selectedItem = PlayerData.Instance.Inventory[cnt];
                        }
                    }
                }
                else
                {
                    GUILayout.Label("", "InventorySlotEmpty");
                }

                cnt++;
            }
            GUILayout.EndHorizontal();
        }
        
        GUILayout.EndVertical();

        GUILayout.EndScrollView();
        GUILayout.EndArea();

        if (GUI.tooltip != "")
        {
            GUI.Label(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 200, 100), GUI.tooltip, "TooltipStyle2");
        }

		GUI.skin = cSkin;
	}

    void DrawEquipment()
    {
        GUI.Label(new Rect(windowBackgroundRect.x + 50, windowBackgroundRect.y + 55, 200, 20), "EQUIPMENT", titleStyle);

        Rect abRect = new Rect(panelLeftRect.x + 7, panelLeftRect.y + 30, panelLeftRect.width - 12, panelLeftRect.height - 40);

        GUISkin cSkin = GUI.skin;
        GUI.skin = hudSkin;

        GUILayout.BeginArea(abRect);
        

        GUILayout.EndArea();

        GUI.skin = cSkin;
    }
	
	void DrawMenu()
	{
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 

		menuBackgroundRect.x = 0;
		menuBackgroundRect.y = (screenSize.y - menuBackgroundRect.height) * 0.5f;

        resumeRect.x = menuBackgroundRect.x + 105;
        resumeRect.y = menuBackgroundRect.y + 10;

        characterRect.x = menuBackgroundRect.x + 60;
        characterRect.y = menuBackgroundRect.y + 40;

        equipmentRect.x = menuBackgroundRect.x + 62;
        equipmentRect.y = menuBackgroundRect.y + 55;

        inventoryRect.x = menuBackgroundRect.x + 52;
        inventoryRect.y = menuBackgroundRect.y + 95;

        optionsRect.x = menuBackgroundRect.x + 78;
        optionsRect.y = menuBackgroundRect.y + 110;

        aboutRect.x = menuBackgroundRect.x + 84;
        aboutRect.y = menuBackgroundRect.y + 153;

        exitRect.x = menuBackgroundRect.x + 102;
        exitRect.y = menuBackgroundRect.y + 166;
		
		GUI.DrawTexture(menuBackgroundRect, menuBackground);
		
		var overButton = false;
	
		//handle resume menu item
		if(resumeRect.Contains(mousePos))
		{
			overButton = true;
			
			if(!over)
			{
				over = true;
				audio.volume = overVolume;
				audio.PlayOneShot(overSound);
			}
			
			GUI.DrawTexture(resumeRect, resumeOver);
			
			if(alpha <= 0.0 && GameManager.menuVisible)
			{
				if(evt.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
				{
					audio.volume = clickVolume;
					audio.PlayOneShot(clickSound);
					GameManager.menuVisible = false;					
					visible = false;
					if(!MyGUI.LootWindowOpen)
						Screen.lockCursor = true;
                    //re-enabling the targeting reticule for combat mode
                    GameManager.Instance.targetReticule.GetComponent<GUITexture>().enabled = !visible;
					lastMouseTime = Time.time;
				}
			}
		}
		else
		{
			GUI.DrawTexture(resumeRect, resume);
		}

        //handle character menu item
        if (characterRect.Contains(mousePos))
        {
            overButton = true;

            if (!over)
            {
                over = true;
                audio.volume = overVolume;
                audio.PlayOneShot(overSound);
            }

            GUI.DrawTexture(characterRect, characterOver);

            if (alpha <= 0.0 && !goingToGame)
            {
                if (evt.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
                {
                    audio.volume = clickVolume;
                    audio.PlayOneShot(clickSound);

                    if (state != MainMenuState.CHARACTER)
                    {
                        state = MainMenuState.CHARACTER;
                    }
                    else
                    {
                        state = MainMenuState.IDLE;
                    }

                    lastMouseTime = Time.time;
                }
            }
        }
        else
        {
            GUI.DrawTexture(characterRect, character);
        }

        //handle equipment menu item
        if (equipmentRect.Contains(mousePos))
        {
            overButton = true;

            if (!over)
            {
                over = true;
                audio.volume = overVolume;
                audio.PlayOneShot(overSound);
            }

            GUI.DrawTexture(equipmentRect, equipmentOver);

            if (alpha <= 0.0 && !goingToGame)
            {
                if (evt.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
                {
                    audio.volume = clickVolume;
                    audio.PlayOneShot(clickSound);

                    if (state != MainMenuState.EQUIPMENT)
                    {
                        state = MainMenuState.EQUIPMENT;
                    }
                    else
                    {
                        state = MainMenuState.IDLE;
                    }

                    lastMouseTime = Time.time;
                }
            }
        }
        else
        {
            GUI.DrawTexture(equipmentRect, equipment);
        }

        //handle inventory menu item
        if (inventoryRect.Contains(mousePos))
        {
            overButton = true;

            if (!over)
            {
                over = true;
                audio.volume = overVolume;
                audio.PlayOneShot(overSound);
            }

            GUI.DrawTexture(inventoryRect, inventoryOver);

            if (alpha <= 0.0 && !goingToGame)
            {
                if (evt.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
                {
                    audio.volume = clickVolume;
                    audio.PlayOneShot(clickSound);

                    if (state != MainMenuState.INVENTORY)
                    {
                        state = MainMenuState.INVENTORY;
                    }
                    else
                    {
                        state = MainMenuState.IDLE;
                    }

                    lastMouseTime = Time.time;
                }
            }
        }
        else
        {
            GUI.DrawTexture(inventoryRect, inventory);
        }
		
        //handle options menu item
        if(optionsRect.Contains(mousePos))
        {
            overButton = true;
				
            if(!over)
            {
                over = true;
                audio.volume = overVolume;
                audio.PlayOneShot(overSound);
            }
				
            GUI.DrawTexture(optionsRect, optionsOver);
				
            if(alpha <= 0.0 && !goingToGame)
            {
                if(evt.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
                {
                    audio.volume = clickVolume;
                    audio.PlayOneShot(clickSound);
						
                    if(state != MainMenuState.OPTIONS)
                    {
                        state = MainMenuState.OPTIONS;
                    }
                    else
                    {
                        state = MainMenuState.IDLE;
                    }
						
                    lastMouseTime = Time.time;
                }
            }
        }
        else
        {
            GUI.DrawTexture(optionsRect, options);
        }
		
        //handle about menu item
		if(aboutRect.Contains(mousePos))
		{
			overButton = true;
			
			if(!over)
			{
				over = true;
				audio.volume = overVolume;
				audio.PlayOneShot(overSound);
			}
			
			GUI.DrawTexture(aboutRect, aboutOver);
			
			if(alpha <= 0.0 && !goingToGame)
			{
				if(evt.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
				{
					audio.volume = clickVolume;
					audio.PlayOneShot(clickSound);
					
					if(state != MainMenuState.ABOUT)
					{
						state = MainMenuState.ABOUT;
					}
					else
					{
						state = MainMenuState.IDLE;
					}
					
					lastMouseTime = Time.time;
				}
			}
		}
		else 
		{
			GUI.DrawTexture(aboutRect, about);
		}

        //handle exit menu item
        if (exitRect.Contains(mousePos))
        {
            overButton = true;

            if (!over)
            {
                over = true;
                audio.volume = overVolume;
                audio.PlayOneShot(overSound);
            }

            GUI.DrawTexture(exitRect, exitOver);

            if (alpha <= 0.0 && GameManager.menuVisible)
            {
                if (evt.type == EventType.MouseUp && evt.button == 0 && Time.time > lastMouseTime)
                {
                    audio.volume = clickVolume;
                    audio.PlayOneShot(clickSound);
                    GameManager.menuVisible = false;
                    GameManager.running = false;
                    Application.LoadLevel(_menu);
                    visible = false;
                    lastMouseTime = Time.time;
                }
            }
        }
        else
        {
            GUI.DrawTexture(exitRect, exit);
        }

		if(!overButton)
		{
			over = false;
		}
	}
}
