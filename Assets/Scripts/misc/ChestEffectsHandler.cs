/// <summary> 
/// ChestEffectsHandler.cs 
/// November 22, 2013
/// Adam T. Davis
/// 
/// This class handles all of the effects and loot item generation
/// for chests
/// </summary>
///

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(SphereCollider))]
[RequireComponent (typeof(AudioSource))]	
public class ChestEffectsHandler : MonoBehaviour {

    public GameObject[] parts;                  //parts of the chest you want to apply the highlight too
    public static ChestEffectsHandler Instance; //making a reference to itself
	public AnimationClip OpenAnimation;
	public AnimationClip ClosedAnimation;
    public AudioClip OpenSound;                 //audio clip used for opening chest
    public AudioClip CloseSound;                //audio clip used for closing the chest
    public float MaxDistance = 3;

    [HideInInspector]
	public bool InRange = false;               //is the player in range of the chest to open it?

    [HideInInspector]
    public bool InUse = false;                  //is the chest currently in use?

    [HideInInspector]
    public List<Item> Loot = new List<Item>();  //loot items list	
    
    private Color[] _defaultColors;	    		//the default colors for the parts you will be highlighting
    private BoxCollider boxCollider;            //used to trigger possible opening of chest state
	private State _state;		    			//the current state
	private bool _used = false;                 //has the chest been used?    
	
	public enum State{
		open,
		close,
		inbetween
	}

    void Awake() {
        Instance = this;

		_state = ChestEffectsHandler.State.close;
		
        boxCollider = GetComponent("BoxCollider") as BoxCollider;   //setting the box collider object
        _defaultColors = new Color[parts.Length];                   //variable to store the chests default colors

        //store the chests current colors to prepare for highlighting
        if (parts.Length > 0)
            for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                if (parts[cnt].renderer.material != null)
                    _defaultColors[cnt] = parts[cnt].renderer.material.GetColor("_Color");
    }


    void Update(){

        if (InRange && Input.GetKeyDown(KeyCode.E) && !InUse)
        {
            ChestState();                   			        //goto chest state routine
        }                                       
        else if (!InRange && Input.GetKeyDown(KeyCode.E) && InUse)
        {
            MyGUI.Chest.ForceClose();
        }
    }

    public void ChestState() {
        switch (_state)
        {
            case State.open:
                _state = ChestEffectsHandler.State.inbetween;   //set the state to inbetween open and close
                ForceClose();                                   //close the chest
                break;
            case State.close:
                //if the chest is not empty then close it
                if (MyGUI.Chest != null) {
                    MyGUI.Chest.ForceClose();
                }
                _state = ChestEffectsHandler.State.inbetween;   //set the state to inbetween open and close
                StartCoroutine("Open");                         //start the open coroutine
                break;
        }
    }
	
	private IEnumerator Open(){

        //assigning this script to the chest in MyGUI
        MyGUI.Chest = this;

        //mark this chest as being in use
        InUse = true;

        //disabling the targeting reticule for looting mode
        GameManager.Instance.targetReticule.GetComponent<GUITexture>().enabled = false;

		Screen.lockCursor = !Screen.lockCursor;

		if(OpenAnimation != null)
			//play opening animation
			animation.Play(OpenAnimation.name);

		if (OpenSound != null)
			//play opening sound effect
			audio.PlayOneShot(OpenSound);

        //send a message to the GUI to create 3 items and display them in the loot window    
        if(!_used)
            PopulateChest(3);

		if(OpenAnimation != null)
			//wait until the chest is done opening
			yield return new WaitForSeconds(animation[OpenAnimation.name].length); //WaitForSeconds(tempTimer) if no animation
		
		if(OpenAnimation != null)
			//display the beam effect
			EnableBeam();
		
		//change the chest state to open
        _state = State.open;

        Messenger.Broadcast("DisplayLoot");
	}
	
	private IEnumerator Close(){
        //mark this chest as not in use
        InUse = false;
            
        //re-enabling the targeting reticule for combat mode
        GameManager.Instance.targetReticule.GetComponent<GUITexture>().enabled = true;

		if(ClosedAnimation != null)
			//hide the beam effect
			DisableBeam();
        
		if(ClosedAnimation != null)
			//play closing animation
			animation.Play(ClosedAnimation.name);

		if(CloseSound != null)
			//play closing sound effect
			audio.PlayOneShot(CloseSound);

		if(ClosedAnimation != null)
			//wait until the chest is done closing
			yield return new WaitForSeconds(animation[ClosedAnimation.name].length); //WaitForSeconds(tempTimer) if no animation           

        //change the chest state to close
        _state = State.close;

        //if Loot list is empty destroy the chest
        if (Loot.Count == 0)
            Destroy(gameObject);
	}
    
    /// <summary>
    /// Function to populate the chests
    /// </summary>
    /// <param name="x">
    /// integer value representing number of items
    /// to generate
    /// </param>
    /// <Usage>
    /// PopulateChest(x)
    /// </Usage>
    private void PopulateChest(int x)
    {
        for (int cnt = 0; cnt < x; cnt++)
            Loot.Add(ItemGenerator.CreateItem());       //generating items based on the item Generator script

        _used = true;
    }
	
    //function used to force the chests to close based on certain criteria
	public void ForceClose() {
		if (!GameManager.menuVisible)
			Screen.lockCursor = true;

        Messenger.Broadcast("CloseChest");        
        StopCoroutine("Open");
        StartCoroutine("Close");        
    }

    //when player enter collision box
	void OnTriggerEnter(Collider collider)
    {
		if(collider.gameObject.tag == "Player"){
			InRange = true;         //the player is inRange to open the chest so set inRange to true
			HighLight(true);        //since player is inRange highlight the chest
		}
    }

    //when player exits the collision box
    void OnTriggerExit()
    {
        InRange = false;        //the player is no longer inRange to open the chest
        HighLight(false);       //since player is no longer inRange return the chest appearance to normal

        //if the player is no longer inRange and Chest is still open, close it!
        if (!InRange && InUse)
            MyGUI.Chest.ForceClose();
    }

    public void Reset()
    {
        transform.localScale = new Vector3(1, 1, 1);        
    }

    public void EnableBeam() {
        transform.FindChild("BeamEffect").GetComponent<MeshRenderer>().enabled = true;
        (transform.FindChild("BeamEffect").GetComponent("Halo") as Behaviour).enabled = true;
    }
	
    public void DisableBeam() {
        transform.FindChild("BeamEffect").GetComponent<MeshRenderer>().enabled = false;
        (transform.FindChild("BeamEffect").GetComponent("Halo") as Behaviour).enabled = false;
    }

    /// <summary>
    /// Function to make a chest either glow or not glow
    /// </summary>
    /// <param name="glow">
    /// Boolean value
    /// </param>
    /// <Usage>
    /// HighLight(glow)
    /// </Usage>
    /// 
    public void HighLight(bool glow)
    {
        if (glow)
        {
            if (parts.Length > 0)
                for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                    for (int matCount = 0; matCount < parts[cnt].renderer.materials.Length; matCount++)
                        parts[cnt].renderer.materials[matCount].SetColor("_Color", Color.gray + Color.white + Color.white);
        }
        else
        {
            if (parts.Length > 0)
                for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                    for (int matCount = 0; matCount < parts[cnt].renderer.materials.Length; matCount++)
                        parts[cnt].renderer.materials[matCount].SetColor("_Color", _defaultColors[cnt]);
        }
    }
}
