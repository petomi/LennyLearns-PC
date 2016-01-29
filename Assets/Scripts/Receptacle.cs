using UnityEngine;
using System.Collections;

public class Receptacle : MonoBehaviour {

	[HideInInspector]
	bool NearReceptacle = false;
	public int score  = 0;

	public AudioSource[] AudioClips = null;

	void Update(){

        //hooking into inventory script to check if it's full later
        GameObject Player = GameObject.Find("Player");
        Inventory inventoryScript = Player.GetComponent<Inventory>();

        if (NearReceptacle == true){

           
			if(Input.GetKeyDown(KeyCode.E) && inventoryScript.inventoryFull){

				Debug.Log ("placed item in bin");

				//read item description from inventory bools
				//another example of changing/accessing variables in other scripts
				//GameObject Player = GameObject.Find ("Player");
			//	Inventory inventoryScript = Player.GetComponent<Inventory>();

				if (inventoryScript.isPickUp == true){ 			//choose correct receptacle behaviour based on bools in inventory
					Debug.Log("It was the stupid bird!");

					//place whatever bool here as needed, or whatever sfx/animation hooks
		
					inventoryScript.isPickUp = false; 			//reset inventory state as item is now removed
					inventoryScript.inventoryFull = false;


					//place whatever sfx/animation here for celebration
					AudioClips[0].Play ();
					//AudioClips[1].Play ();
					//when another receptacle is added, add audio source for the wrong placement sound to that
					//then switch around the properties

					GameObject PickUp = GameObject.Find ("pickup placeholder"); //reset state of item as picked up
					PickedUpObject pickUpScript = PickUp.GetComponent<PickedUpObject>();
					pickUpScript.isPickedUp = false;
					score++;
				}

				if(inventoryScript.isKaz == true){
					//taken out because this is the bird bin:
//					Debug.Log ("This burger tastes like A HOT COCK!");
//					inventoryScript.isKaz = false; 				//reset inventory state to empty as item is now removed
//					inventoryScript.inventoryFull = false;

					//place whatever sfx/animation hooks/new variable assignments here as needed
//					AudioClips[0].Play ();
					AudioClips[1].Play ();


//					GameObject Kaz = GameObject.Find ("kaz burger"); //reset state of item as picked up
//					PickedUpObject kazScript = Kaz.GetComponent<PickedUpObject>();
//					kazScript.isPickedUp = false;
				}

//				score++; //makes game score variable go up by one. moved to inside proper brackets
			



				///////////////////////////

				//(OBSOLETE)another example of changing/accessing variables in other scripts
				//PickUpItem pickUpItem = Player.GetComponent<PickUpItem>();
				//int itemNumber = pickUpItem.itemNumber;

				//Debug.Log (itemNumber);

				//change to refer to specific object in array of pickups

				//GameObject[] thePickups = GameObject.FindGameObjectsWithTag("PickUp");
				//GameObject correctObject = thePickups[itemNumber];
				//PickedUpObject objectScript = correctObject.GetComponent<PickedUpObject>();
				//objectScript.isPickedUp = false;
				// //
				/////////////////////////////

			}
		}

		//debug testing states of items
		if(Input.GetKeyDown (KeyCode.X)){
		
			GameObject thePlayer = GameObject.Find ("Player");
			PickUpItem pickUpItem = thePlayer.GetComponent<PickUpItem>();
			int itemNumber = pickUpItem.itemNumber;
			
			GameObject[] thePickups = GameObject.FindGameObjectsWithTag("PickUp");
			GameObject correctObject = thePickups[itemNumber];
			PickedUpObject objectScript = correctObject.GetComponent<PickedUpObject>();
			Debug.Log("Stupid bird is in inventory: " + objectScript.isPickedUp);

			GameObject Kaz = GameObject.Find ("kaz burger");
			PickedUpObject kazScript = Kaz.GetComponent<PickedUpObject>();
			Debug.Log ("burger in hand?: " + kazScript.isPickedUp);
		}

		
	}
	
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			NearReceptacle = true;

			
			//Debug.Log("near object"); 	//testing proximity
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			NearReceptacle = false;

			
			//Debug.Log ("left object"); 		//testing proximity
		}
		
	}

	//create "score" GUI element
	void OnGUI(){
		GUI.Box (new Rect ((Screen.width)/2 - (Screen.width)/32 ,(Screen.height)/32,(Screen.width)/8,(Screen.height)/16), "Score: " + score); 
	}

}

