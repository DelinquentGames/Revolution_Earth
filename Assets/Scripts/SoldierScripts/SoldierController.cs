using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

	// Public variables shown in inspector
	
	public float runSpeed = 4.6f;
	public float runStrafeSpeed = 3.07f;
	public float walkSpeed = 1.22f;
	public float walkStrafeSpeed = 1.22f;
	public float crouchRunSpeed = 5f;
	public float crouchRunStrafeSpeed = 5f;
	public float crouchWalkSpeed = 1.8f;
	public float crouchWalkStrafeSpeed = 1.8f;
	public float exhautionMultiplier = 0.1f;

    public GameObject radarObject;
	
	public float maxRotationSpeed = 540f;
	public AudioSource Breathing;
	public GunManager weaponSystem;
	//public float minCarDistance;
	
	static public bool dead;
	
	// Public variables hidden in inspector
	
	[HideInInspector]
	public bool walk;
	
	[HideInInspector]
	public bool crouch;
	
	[HideInInspector]
	public bool inAir;
	
	[HideInInspector]
	public bool fire;
	
	[HideInInspector]
	public bool aim;
	
	[HideInInspector]
	public bool reloading;
	
	[HideInInspector]
	public string currentWeaponName;
	
	[HideInInspector]
	public int currentWeapon;
	
	[HideInInspector]
	public bool grounded;
	
	[HideInInspector]
	public float targetYRotation;
	
	// Private variables
	
	private Transform soldierTransform;
	private CharacterController controller;
	private HeadLookController headLookController;
	private CharacterMotor motor;
	
	private bool firing;
	private float firingTimer;
	public float idleTimer;
	
	public Transform enemiesRef;
	public Transform enemiesShootRef;
	
	static public Transform enemiesReference;
	static public Transform enemiesShootReference;
	
	[HideInInspector]
	public Vector3 moveDir;
    
    private bool _useIK;
	
	private float cnt = 0f;
	private bool canRun = true;
	private GameObject ragdoll;

	void Awake()
	{   
		if(enemiesRef != null) enemiesReference = enemiesRef;
		if(enemiesShootRef != null) enemiesShootReference = enemiesShootRef;
	}
	
	void Start()
	{
		//GameObject ra = GameObject.Find("Radar") as GameObject;
		if (radarObject == null)							
				radarObject = GameObject.Find("Radar") as GameObject;
		
		idleTimer = 0.0f;

		soldierTransform = transform;

		walk = true;
		aim = false;
		reloading = false;

		controller = gameObject.GetComponent<CharacterController>();
        motor = gameObject.GetComponent<CharacterMotor>();
	}
	
	void OnEnable()
	{
        if(radarObject != null)
        {
            foreach (Transform child in radarObject.transform)
            {
                child.gameObject.SetActive(true);
                if (child.childCount > 0)
                {
                    foreach (Transform child2 in child.transform)
                    {
                        child2.gameObject.SetActive(true);
                    }
                }
            }            
        }


        moveDir = Vector3.zero;
        headLookController = gameObject.GetComponent<HeadLookController>();
		headLookController.enabled = true;
        walk = true;
		aim = false;
		reloading = false;
	}
	
	void OnDisable()
	{
        if(radarObject != null)
        {
            foreach (Transform child in radarObject.transform)
            {
                child.gameObject.SetActive(false);
                if (child.childCount > 0)
                {
                    foreach (Transform child2 in child.transform)
                    {
                        child2.gameObject.SetActive(false);
                    }
                }
            }            
        }

        moveDir = Vector3.zero;
		headLookController.enabled = false;
        walk = true;
		aim = false;
		reloading = false;
	}
	
	void Update()
	{
		if( GameManager.scores)//GameManager.menuVisible ||
		{
			moveDir = Vector3.zero;
			motor.canControl = false;
		}
		else
		{
			GetUserInputs();

            if (!motor.canControl)
            {
                motor.canControl = true;
            }
			
			if(!dead)
			{
				moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			}
			else
			{
				moveDir = Vector3.zero;
				motor.canControl = false;
			}
		}
		
		//Check the player move direction
		if (moveDir.sqrMagnitude > 1)
			moveDir = moveDir.normalized;

        motor.inputMoveDirection = transform.TransformDirection(moveDir);
        motor.inputJump = Input.GetButton("Jump") && !crouch;

        motor.movement.maxForwardSpeed = ((walk) ? ((crouch) ? crouchWalkSpeed : walkSpeed) : ((crouch) ? crouchRunSpeed : runSpeed));
        motor.movement.maxBackwardsSpeed = motor.movement.maxForwardSpeed;
        motor.movement.maxSidewaysSpeed = ((walk) ? ((crouch) ? crouchWalkStrafeSpeed : walkStrafeSpeed) : ((crouch) ? crouchRunStrafeSpeed : runStrafeSpeed));
		
		if(moveDir != Vector3.zero)
        {
			idleTimer = 0.0f;
        }
		
		inAir = !motor.grounded;
		
		var currentAngle = soldierTransform.localRotation.eulerAngles.y;
		var delta = Mathf.Repeat ((targetYRotation - currentAngle), 360);
		if (delta > 180)
			delta -= 360;

		soldierTransform.localRotation = Quaternion.Euler(new Vector3(soldierTransform.localRotation.eulerAngles.x,
            Mathf.MoveTowards(currentAngle, currentAngle + delta, Time.deltaTime * maxRotationSpeed),
            soldierTransform.localRotation.eulerAngles.z));
	}
	
	void GetUserInputs()
	{
		//Check if the user if firing the weapon
		fire = Input.GetButton("Fire1") && weaponSystem.currentGun.freeToShoot && !dead && !inAir && !MainCamera.DisableCameraRotate;
		
		//Check if the user is aiming the weapon
		aim = Input.GetButton("Fire2") && !dead && !MainCamera.DisableCameraRotate;
		
		idleTimer += Time.deltaTime;
		
		if(aim || fire)
		{
			firingTimer -= Time.deltaTime;
			idleTimer = 0.0f;
		}
		else
		{
			firingTimer = 0.3f;
		}
		
		firing = (firingTimer <= 0.0 && fire);


        if (weaponSystem.currentGun != null)
        {
            weaponSystem.currentGun.fire = firing;
            reloading = weaponSystem.currentGun.reloading;
            currentWeaponName = weaponSystem.currentGun.gunName;
            currentWeapon = weaponSystem.currentWeapon;

//			if (firing && SoldierTarget.overEnemy && !SoldierTarget.enemyTarget.GetComponent<AdvancedAiEnemy>().isDead){
//				SoldierTarget.enemyTarget.GetComponent<AdvancedAiEnemy>().generalParameters.botHealth = SoldierTarget.enemyTarget.GetComponent<AdvancedAiEnemy>().generalParameters.botHealth - weaponSystem.currentWeaponDamage;
//				Messenger.Broadcast<float>("enemy health update", SoldierTarget.enemyTarget.GetComponent<AdvancedAiEnemy>().generalParameters.botHealth);
//			}
        }
		
		//Check if the user wants the soldier to crouch
		if(Input.GetKeyDown(KeyCode.C))
		{
			crouch = !crouch;
			idleTimer = 0.0f;
		}
		
		crouch |= dead;
		if(canRun && cnt > 99){
			canRun = false;
			
		}
		else if(!canRun && cnt < 1)
			canRun = true;
		
		if (canRun)			//Check if the user wants the soldier to walk
			walk = (!Input.GetKey(KeyCode.LeftShift) && !dead ) || moveDir == Vector3.zero || crouch;
		else
			walk = true;
				
//		if(walk && cnt > 0)
//			cnt = cnt - (1 * exhautionMultiplier);
//		else if(!walk && cnt < 100){
//			Breathing.Play();
//			cnt = cnt + (1 * exhautionMultiplier);
//		}
		//Messenger.Broadcast<float, float>("player vigor update", 100 - cnt, 100 + cnt);	
		//Debug.Log("vigor = " + (100 - cnt));
	}
}