using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheLevel : MonoBehaviour{
	
	public Vector2[] meteors;
	public Vector2[] fuelCanisters;
	public Vector2[] speedPowerUps;
	
	LinkedList<Vector2> meteorList;
	LinkedList<Vector2> fuelCanisterList;
	LinkedList<Vector2> speedPowerUpList;
	
	
	GameController game;
	
	float levelTimer = 0;
	float levelDuration = 90;
	
	bool spawnedMars = false;
	
	void Start(){
		game = GetComponent<GameController>();
		
		meteorList = new LinkedList<Vector2>();
		fuelCanisterList = new LinkedList<Vector2>();
		speedPowerUpList = new LinkedList<Vector2>();
		
		foreach (Vector2 item in meteors) {			
			meteorList.AddLast(item);
		}
		foreach (Vector2 item in fuelCanisters) {			
			fuelCanisterList.AddLast(item);
		}
		foreach (Vector2 item in speedPowerUps) {			
			speedPowerUpList.AddLast(item);
		}
		
		
		
		
	}
	
	void Update(){
		levelTimer += Time.deltaTime;
		
		
		
		
		if (meteorList.Count != 0 && meteorList.First.Value.x < levelTimer){
			game.SpawnAlarm(meteorList.First.Value.y);
			meteorList.RemoveFirst();
		}
		
		if (fuelCanisterList.Count != 0 && fuelCanisterList.First.Value.x < levelTimer){
			game.SpawnFuelCanister(fuelCanisterList.First.Value.y);
			fuelCanisterList.RemoveFirst();
		}
		
		if (speedPowerUpList.Count != 0 && speedPowerUpList.First.Value.x < levelTimer){
			game.SpawnSpeedPowerUp(speedPowerUpList.First.Value.y);
			speedPowerUpList.RemoveFirst();
		}
		
		
		if (!spawnedMars && levelTimer > 46){
			game.SpawnMars();
			spawnedMars = true;
		}
	}
	

	
}
