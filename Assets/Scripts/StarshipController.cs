using UnityEngine;
using System.Collections;

public class StarshipController : MonoBehaviour {
	
	public float maxYVelocity = 2.5f;
	public float maxXVelocity = 0.5f;
	public float maxXBehindVelocity = -0.2f;
	
	public float yThrustVelIncrease = 3f;
	public float xThrustVelIncrease = 1f;
	public float xBehindVelIncrease = 1f;
	
	public float upAndDownFuelCost = 5f;
	public float rightFuelCost = 8f;
	
	
	
	Vector3 velocity = Vector3.zero;
	
	Vector3 startPosition;
	
	Thruster upThruster;
	Thruster downThruster;
	Thruster rightThruster;
	
	GameController game;
	
	bool thrustersOn = false;
	
	bool forwardThrusterOn = false;
	
	float movementOnScreenToCFactor = 0;
	
	public AudioClip fuelPickUpSound;
	public AudioClip boosterPickUpSound;
	public AudioClip explosionSound;
	
	ExplosionAnimation explosion;
	
	bool dead = false;
	
	float deadDuration = 1f;
	float deadTimer = 0;
	
	bool shownDeadMessage = false;
	
	
	bool boosterActive = false;
	
	bool outOfTime = false;
	
	float outOfTimer = 0;
	float outOfDuration = 2f;
	
	bool shownOutOfTimeText = false;
	
	bool reachedMars = false;
	
	
	bool justStarted = false;
	
	
	float currentSpeed = 0;
	
	
	// Use this for initialization
	void Start () {
		
		explosion = GetComponentInChildren<ExplosionAnimation>();
		
		startPosition = transform.position;
		
		game = Camera.mainCamera.GetComponent<GameController>();
		
		foreach (Transform child in transform) {
			if (child.name == "UpThruster"){
				upThruster = child.GetComponent<Thruster>();	
				
			}else if (child.name == "DownThruster"){
				downThruster = child.GetComponent<Thruster>();	
				
			}else if (child.name == "RightThruster"){
				rightThruster = child.GetComponent<Thruster>();	
				
			}
		}
		
		movementOnScreenToCFactor = (transform.position.x / Gamma.GetGamma(game.GetCSpeed()));
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!justStarted){
			justStarted = true;
			game.GiveStartBoost();
			
		}
		
		
		if (reachedMars){
			velocity.x += 5f * Time.deltaTime;
			
			rightThruster.On();
			
			thrustersOn = true;
			
			forwardThrusterOn = true;
			
			audio.volume *= 0.99f;
		}
		
		
		if (outOfTime && !shownOutOfTimeText){
			velocity.x -= 0.5f * Time.deltaTime;
			
			//rightThruster.On();
			thrustersOn = false;
			outOfTimer -= Time.deltaTime;
			
			Debug.Log("outOfTimer,j " + outOfTimer);
			
			if (outOfTimer < 0){
				game.ShowOutOfTimeMessage();
				shownOutOfTimeText = true;
				
				Debug.Log("hgkgh,j");
			}
		}	
		
		if (dead && !shownDeadMessage){
			deadTimer -= Time.deltaTime;
			
			if (deadTimer < 0){
				game.ShowDeadMessage();
				shownDeadMessage = true;
			}
		}
		
		
		if (boosterActive){
					
			thrustersOn = true;
			
			forwardThrusterOn = true;
			
			rightThruster.On();
			
			float currentSpeed = game.IncreaseCSpeedTurbo();
							
			
			Vector3 newPosition = transform.position;
			newPosition.x = Gamma.GetGamma(currentSpeed) * movementOnScreenToCFactor;
			transform.position = newPosition;
			
		}
		
		
		if (!forwardThrusterOn && !outOfTime){
			float currentSpeed = game.DecreaseSpeed();
							
			
			Vector3 newPosition = transform.position;
			newPosition.x = Gamma.GetGamma(currentSpeed) * movementOnScreenToCFactor;
			transform.position = newPosition;
		}
				
		transform.Translate(velocity * Time.deltaTime, Space.World);
		
		
		if (thrustersOn){
			if (!audio.isPlaying){	
				audio.Play();	
			}
		}else{
			
			audio.Stop ();
		}
		
		
		if (dead || outOfTime) return;
		
		
		if (transform.position.y > 3.15){
			game.OutOfBoundsTop();	
		}else if (transform.position.y < -3.15){
			game.OutOfBoundsBtm();	
		}
		

		

		
		forwardThrusterOn = false;
		thrustersOn = false;
	}
	
	
	public void UpPressed(){
		if (dead || outOfTime || reachedMars) return;
		
		if (!game.HaveFuelLeft()){
			//NO FUEL
			return;	
		}
		
		thrustersOn = true;
		
		if (velocity.y < maxYVelocity){
			velocity.y += yThrustVelIncrease * Time.deltaTime;	
		}
		
		downThruster.On();
		
		
		
		
		game.FuelUsed(upAndDownFuelCost * Time.deltaTime);
	}
	
	public void DownPressed(){
		if (dead || outOfTime || reachedMars) return;
		
		if (!game.HaveFuelLeft()){
			//NO FUEL
			return;	
		}
		
		thrustersOn = true;
		
		if (velocity.y > -maxYVelocity){
			velocity.y -= yThrustVelIncrease * Time.deltaTime;	
		}
		
		upThruster.On();
		
		game.FuelUsed(upAndDownFuelCost * Time.deltaTime);

	}
	
	public void RightPressed(){
		if (dead || outOfTime || reachedMars || boosterActive) return;
		
		if (!game.HaveFuelLeft()){
			//NO FUEL
			return;	
		}
		
		thrustersOn = true;
		
		forwardThrusterOn = true;
		
		rightThruster.On();
		
		currentSpeed = game.IncreaseCSpeed();
						
		
		Vector3 newPosition = transform.position;
		newPosition.x = Gamma.GetGamma(currentSpeed) * movementOnScreenToCFactor;
		transform.position = newPosition;
		
//		if (velocity.x < maxXVelocity){
//			velocity.x += xThrustVelIncrease * Time.deltaTime;	
//		}
		
		game.FuelUsed(rightFuelCost * Time.deltaTime);

	}
	
	
	public void PickedUpFuel(){
		audio.PlayOneShot(fuelPickUpSound);
	}
	
	public void PickedUpBooster(){
		audio.PlayOneShot(boosterPickUpSound);
	}
	
				
	public void HitByAsteriod(){
		if (dead) return;
		
		GetComponent<Collider>().enabled = false;
		
		dead = true;
		
		renderer.enabled = false;
		
		explosion.renderer.enabled = true;
		
		explosion.RunAnimation();	
		
		audio.PlayOneShot(explosionSound);
		
		audio.Pause ();
		
		
		deadTimer = deadDuration;
	}
	
	
	public void BoosterActive(bool on){
		boosterActive = on;
		
		
		if (on){
			upThruster.TurboOn();
			downThruster.TurboOn();
			rightThruster.TurboOn();
		}else{
			upThruster.TurboOff();
			downThruster.TurboOff();
			rightThruster.TurboOff();
		}
	}
	
	public void RanOutOfTime(){
		if (outOfTime) return;
		outOfTime = true;
		outOfTimer = outOfDuration;
		GetComponent<Collider>().enabled = false;

	}
	
	
	public void ReachedMars(){
		reachedMars = true;
	}
}
