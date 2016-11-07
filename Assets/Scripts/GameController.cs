using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	const float speedIncreaseFactor = 0.3f;
	const float speedIncreaseTurboFactor = 0.9f;
	const float speedDecreaseFactor = 0.1f;
	const float outOfBoundsDeathDuration = 1.2f;
	const float minimumSpeed = 0.8f;
	
	GUIController gui;
	
	float currentSpeed = 0;
	float timeLeft = 0;
	float fuel;
	
	
	float testTimer = 0;
	
	TheLevel theLevel;
	
	
	MainTimer mainTimer;
	
	
	Bar bar;
	
	SpeedCounter speedCounter;
	
	OutOfBoundsAudio oobAudio;
	FuelAlarm fuelAlarm;
	
	bool reachedMars = false;
	
	
	public Transform alarmPrefab;
	public Transform asteroidPrefab;
	public Transform fuelCanisterPrefab;
	public Transform speedPowerUpPrefab;
	public Transform powerUpMessagePrefab;
	public Transform marsPrefab;
	
	
	
	
	FrontMessage frontMessage;
		
	
	bool outOfBounds = false;
	bool outOfBoundsTop = false;
	float outOfBoundsTimer = 0;
	bool spawnedDeathAsteroid = false;
	
	
	StarshipController ship;
	

	bool showingUrAGoodMessage = false;
	float urAGoodTimer = 0;
	float urAGoodDuration = 3f;
	
	float oldGamma = 0;
	
	
	bool showingDeadMessage = false;
	
	bool dead = false;
	
	float deadMessageTimer = 0;
	float deadMessageDuration = 2f;
	
	Stars starsNear;
	Stars starsMedium;
	Stars starsFar;
	
	LowFuelText lowFuelText;
	
	
	bool boosterActivated = false;
	float boosterTimer = 0;
	float boosterDuration = 2.2f;
	
	bool outOfTime = false;
	
	
	bool showingOutOfTimeMessage = false;
	float outOfTimeMessageTimer = 0;
	float outOfTimeMessageDuration = 2;
	
	
	float reachedMarsTimer = 0;
	float reachedMarsDuration = 2f;
	
	bool showingWinMessage = false;
	float showingWinMessageTimer = 0;
	float showingWinMessageDuration = 5f;

	// Use this for initialization
	void Start () {
		ship = GetComponentInChildren<StarshipController>();
		gui = GetComponent<GUIController>();
		frontMessage = GetComponentInChildren<FrontMessage>();
		mainTimer = GetComponentInChildren<MainTimer>();
		bar = GetComponentInChildren<Bar>();
		speedCounter = GetComponentInChildren<SpeedCounter>();
		oobAudio = GetComponentInChildren<OutOfBoundsAudio>();
		fuelAlarm = GetComponentInChildren<FuelAlarm>();
		lowFuelText = GetComponentInChildren<LowFuelText>();
		
		timeLeft = 10f; //!!!!
		currentSpeed = minimumSpeed;
		fuel = 100f;
	
		theLevel = GetComponent<TheLevel>();
		
				
		foreach (Transform child in transform) {
			if (child.name == "StarsNear"){
				starsNear = child.GetComponent<Stars>();
			}else if (child.name == "StarsMedium"){
				starsMedium = child.GetComponent<Stars>();
			}else if (child.name == "StarsFar"){
				starsFar = child.GetComponent<Stars>();
			}

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (showingWinMessage){
			showingWinMessageTimer -= Time.deltaTime;
			
			if (showingWinMessageTimer < 0){
				ShowReallyWinMessage();
				
				if (Input.GetMouseButtonDown(0)){
					WinAndGoToMenu();
				}
			}
		}
		
		if (reachedMars && !showingWinMessage){
			reachedMarsTimer -= Time.deltaTime;
			
			if (reachedMarsTimer < 0){
				ShowWinMessage();
			}
		}
		
		if (outOfTime && showingOutOfTimeMessage){
			if (Input.GetMouseButtonDown(0)){
				CleanAndGoToMenu();
			}
		}
	
		if (showingOutOfTimeMessage){
			outOfTimeMessageTimer -= Time.deltaTime;
			
			if (outOfTimeMessageTimer < 0){
				ShowReallyOutOfTimeMessage();
			}
		}
		
		if (boosterActivated){
			boosterTimer -= Time.deltaTime;
			
			if (boosterTimer < 0){
				boosterActivated = false;
				ship.BoosterActive(false);	
			}
		}
		
		
		if (dead && showingDeadMessage){
			if (Input.GetMouseButtonDown(0)){
				CleanAndGoToMenu();
			}
		}
		
		
		if (showingDeadMessage){
			deadMessageTimer -= Time.deltaTime;
			
			if (deadMessageTimer < 0){
				ShowReallyDeadMessage();
			}
		}
		
		
		float gamma = Gamma.GetGamma(currentSpeed);
		
		CheckGammaValue(1f / gamma);
		
		oldGamma = 1f / gamma;
		
		float timePassedThisFrame = Time.deltaTime * gamma;
				
		if (!reachedMars) timeLeft -= timePassedThisFrame;
		gui.UpdateGUI(timeLeft, currentSpeed, fuel, gamma);
		
		starsNear.UpdateScale(1f / gamma);
		starsMedium.UpdateScale(0.65f / gamma);
		starsFar.UpdateScale(0.5f / gamma);
		
		
		
		mainTimer.UpdateTimer(timeLeft);
		speedCounter.UpdateSpeedCounter(currentSpeed);
		
		
		if (timeLeft < 0){
			
			timeLeft = 0;
			RanOutOfTime();	
		}
		
		
		if (outOfBounds){
			outOfBoundsTimer += Time.deltaTime;	
			
			if (outOfBoundsTimer > outOfBoundsDeathDuration){
				if (outOfBoundsTop){
					SpawnAsteroid(3.6f, 80);
					SpawnAsteroid(4.4f, 80);
					SpawnAsteroid(5.2f, 80);
					SpawnAsteroid(6.0f, 80);
					SpawnAsteroid(6.8f, 80);
				}else{
					SpawnAsteroid(-3.6f, 80);
					SpawnAsteroid(-4.4f, 80);
					SpawnAsteroid(-5.2f, 80);
					SpawnAsteroid(-6.0f, 80);
					SpawnAsteroid(-6.8f, 80);
				}
				
				outOfBoundsTimer = 0;
			}
			
		}else if (outOfBoundsTimer > 0){
			outOfBoundsTimer = 0;
			
			oobAudio.StopPlayingOOBSound();
		}
		
				
//		
//		if (Input.GetKeyDown(KeyCode.Alpha1)){
//			SpawnAlarm(2.7f);
//		}
//		
//		if (Input.GetKeyDown(KeyCode.Alpha2)){
//			SpawnAlarm(1.8f);
//		}
//
//		if (Input.GetKeyDown(KeyCode.Alpha3)){
//			SpawnAlarm(0.9f);
//		}
//		
//		if (Input.GetKeyDown(KeyCode.Alpha4)){
//			SpawnAlarm(0);
//		}
//		
//		if (Input.GetKeyDown(KeyCode.Alpha5)){
//			SpawnAlarm(-0.9f);
//		}
//
//		if (Input.GetKeyDown(KeyCode.Alpha6)){
//			SpawnAlarm(-1.8f);
//		}
//
//		if (Input.GetKeyDown(KeyCode.Alpha7)){
//			SpawnAlarm(-2.7f);
//		}
	
		
		
		if (!showingUrAGoodMessage && !showingDeadMessage && !showingOutOfTimeMessage && !showingWinMessage) frontMessage.Active(false);

		outOfBounds = false;
	}
	
	
	
	public bool HaveFuelLeft(){
		if (fuel > 0) return true;
		return false;
	}
	
	public void FuelUsed(float amount){
		fuel -= amount;
		
		if (fuel < 20){
			fuelAlarm.StartAlarm();
			lowFuelText.ShowText();
		}
		
		UpdateFuelBar();
	}
	
	
	public float IncreaseCSpeed(){
		
		float speedIncrease = speedIncreaseFactor * (Mathf.Sqrt(currentSpeed) - currentSpeed) * Time.deltaTime;
		
		currentSpeed += speedIncrease;
		
		
		
		
		return currentSpeed;		
	}
	
	public float IncreaseCSpeedTurbo(){
		
		
		float speedIncrease = speedIncreaseTurboFactor * (Mathf.Sqrt(currentSpeed) - currentSpeed) * Time.deltaTime;
		
		currentSpeed += speedIncrease;
		
		return currentSpeed;		
	}
	
	
	
	
	public float DecreaseSpeed(){
		float speedDecrease = speedDecreaseFactor * (Mathf.Sqrt(currentSpeed) - currentSpeed) * Time.deltaTime;
		
		currentSpeed -= speedDecrease;
		
		if (currentSpeed < minimumSpeed) currentSpeed = minimumSpeed;
		
		return currentSpeed;	
	}
	
	
	public void SpawnAlarm(float y){
		
		//POS Y SHOULD BE BETWEEN -3.5 and 3.5
				
		
		Transform alarmTrans = (Transform) Instantiate(alarmPrefab);
		alarmTrans.GetComponent<Alarm>().Initialize(y, 2);
		
		
		
	}
	
	public void SpawnAsteroid(float y){
		
		SpawnAsteroid(y, 15);

	}
	
	public void SpawnAsteroid(float y, float speed){
		
		
		Transform asteroidTrans = (Transform) Instantiate(asteroidPrefab);
		asteroidTrans.GetComponent<Asteroid>().Initialize(y, speed);

	}
	
	public void SpawnFuelCanister(float y){
		Transform fuelCanisterTrans = (Transform) Instantiate(fuelCanisterPrefab);
		fuelCanisterTrans.GetComponent<PowerUp>().Initialize(y, PowerUp.PowerUpType.FUEL);

	}
	
	public void SpawnSpeedPowerUp(float y){
		Transform speedPowerUpTrans = (Transform) Instantiate(speedPowerUpPrefab);
		speedPowerUpTrans.GetComponent<PowerUp>().Initialize(y, PowerUp.PowerUpType.SPEED);
	}
	
	
	public void SpawnMars(){
		Transform marsTrans = (Transform) Instantiate(marsPrefab);
		marsTrans.GetComponent<Mars>().Initialize();
	}
	
	
	public float GetCSpeed(){
		return currentSpeed;
	}
	
	
	public void PickedUpPowerUp(PowerUp.PowerUpType type, Vector3 position){
		if (dead) return;
		
		if (type == PowerUp.PowerUpType.FUEL){
			fuel += 50;	
			if (fuel > 100) fuel = 100;
			
			fuelAlarm.StopAlarm();
			
			UpdateFuelBar();
			
			lowFuelText.HideText();
			
			ship.PickedUpFuel();
			
			Transform powerUpMessageTrans = (Transform) Instantiate(powerUpMessagePrefab);
			powerUpMessageTrans.GetComponent<PowerUpMessage>().Initialize("+fuel", position);
		}else if (type == PowerUp.PowerUpType.SPEED){
			
			boosterActivated = true;
			
			boosterTimer = boosterDuration;
			
			ship.BoosterActive(true);
			
			ship.PickedUpBooster();
			
			Transform powerUpMessageTrans = (Transform) Instantiate(powerUpMessagePrefab);
			powerUpMessageTrans.GetComponent<PowerUpMessage>().Initialize("+boost", position);
			
		}
		

	}
	
	
	public void HitByAsteroid(){
		dead = true;
		
		Debug.Log("DIE!!!");
		
		ship.HitByAsteriod();
		
		
		
	}
	
	
	public void OutOfBoundsTop(){
		
		if (reachedMars) return;
		
		outOfBounds = true;
		outOfBoundsTop = true;
		frontMessage.Active(true);
		frontMessage.ShowMessage("DANGER!! \n Get back on course", FrontMessage.MessageType.OUT_OF_BOUNDS);
		
		oobAudio.PlayOOBSound();
		
		showingUrAGoodMessage = false;
	}
	
	public void OutOfBoundsBtm(){
		if (reachedMars) return;
		
		outOfBounds = true;
		outOfBoundsTop = false;
		frontMessage.Active(true);
		frontMessage.ShowMessage("DANGER!! \n Get back on course", FrontMessage.MessageType.OUT_OF_BOUNDS);
		
		oobAudio.PlayOOBSound();
		
		showingUrAGoodMessage = false;
		
	}
	
	
	void UpdateFuelBar(){
		bar.UpdateFuelBar(fuel);
	}
	
	
	void CheckGammaValue(float gamma){
		
		if (showingUrAGoodMessage){
			urAGoodTimer -= Time.deltaTime;
			
			if (urAGoodTimer < 0){
				showingUrAGoodMessage = false;	
			}
			
		}else{
						
			if (oldGamma < 2 && gamma >	2){
				frontMessage.Active(true);
				frontMessage.ShowMessage("2 X Time dilation", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}
			
			if (oldGamma < 3 && gamma >	3){
				frontMessage.Active(true);
				frontMessage.ShowMessage("3 X Time dilation!", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}
			
			if (oldGamma < 5 && gamma >	5){
				frontMessage.Active(true);
				frontMessage.ShowMessage("5 X Time dilation!!", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}
			
			if (oldGamma < 8 && gamma >	8){
				frontMessage.Active(true);
				frontMessage.ShowMessage("8 X Time dilation!!!", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}
			
			if (oldGamma < 12 && gamma >	12){
				frontMessage.Active(true);
				frontMessage.ShowMessage("12 X Time dilation!!!!", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}
			
			if (oldGamma < 20 && gamma > 20){
				frontMessage.Active(true);
				frontMessage.ShowMessage("20 X TIME DILATION!!!!!", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}
			
			if (oldGamma < 30 && gamma > 30){
				frontMessage.Active(true);
				frontMessage.ShowMessage("30 X TIME DILATION!!!!!!!!!!", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}
			
			if (oldGamma < 50 && gamma > 50){
				frontMessage.Active(true);
				frontMessage.ShowMessage("50 X TIME DILATION!!!!!!!!!!!!!!!!!", FrontMessage.MessageType.OUT_OF_BOUNDS);
				showingUrAGoodMessage = true;
			}

			

			
			
			if (showingUrAGoodMessage){
				urAGoodTimer = urAGoodDuration;
			}
			
		}
		
		
	}
	
	
	public void ShowDeadMessage(){
		showingDeadMessage = true;
		frontMessage.Active(true);
		frontMessage.ShowMessage("You died...", FrontMessage.MessageType.OUT_OF_BOUNDS);
	
		deadMessageTimer = deadMessageDuration;
	}
	
	
	public void ShowReallyDeadMessage(){
		frontMessage.Active(true);
		frontMessage.ShowMessage("You died... \n \n click to try again", FrontMessage.MessageType.OUT_OF_BOUNDS);
		
	}
	
	
	
	public void ShowOutOfTimeMessage(){
		showingOutOfTimeMessage = true;
		frontMessage.Active(true);
		frontMessage.ShowMessage("Out of time... \n You need to fly faster", FrontMessage.MessageType.OUT_OF_BOUNDS);
		
		outOfTimeMessageTimer = outOfTimeMessageDuration;
	}
	
	public void ShowReallyOutOfTimeMessage(){
		frontMessage.Active(true);
		frontMessage.ShowMessage("Out of time... \n You need to fly faster \n \n click to try again", FrontMessage.MessageType.OUT_OF_BOUNDS);
		
	}
	
	void CleanAndGoToMenu(){
		//CLEAN
		
		
		Application.LoadLevel("MenuScene");
			
		
	}
	
	
	void WinAndGoToMenu(){
		Application.LoadLevel("MenuScene");
		
	}
	
	
	void RanOutOfTime(){
		outOfTime = true;
		
		ship.RanOutOfTime();
		
		
	}
	
	
	public void MadeItToMars(){
		ship.ReachedMars();
		
		reachedMars = true;
		
		reachedMarsTimer = reachedMarsDuration;
	}
	
	
	
	public void ShowWinMessage(){
		showingWinMessage = true;
		frontMessage.Active(true);
		frontMessage.ShowMessage("You made it to mars! \n Humanity is saved because of you.", FrontMessage.MessageType.OUT_OF_BOUNDS);
	
		showingWinMessageTimer = showingWinMessageDuration;
	}
	
	public void ShowReallyWinMessage(){
		frontMessage.Active(true);
		frontMessage.ShowMessage("You made it to mars! \n Humanity is saved because of you. \n \n click to try again", FrontMessage.MessageType.OUT_OF_BOUNDS);
	
	}
	
	
	public void GiveStartBoost(){
		boosterActivated = true;
		
		boosterTimer = boosterDuration;
		
		ship.BoosterActive(true);
		
		ship.PickedUpBooster();
	}
}
