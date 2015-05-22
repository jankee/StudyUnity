//--------------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------------
public class GameManager : MonoBehaviour
{
	//--------------------------------------
	//Access to single instance
	public static GameManager Instance
	{
		get {
				if(!instance) instance = new GameManager();
				return instance;
			}
	}
	//--------------------------------------
	//Access to level complete flag
	public bool LevelCompleted
	{
		get{return bLevelCompleted;}
		set{
				bLevelCompleted = value;

				if(value)
				{
					//Disable input
					bAcceptInput=false;

					//Start win wait interval
					StartCoroutine(MoveToNextLevel());

					//Play Audio
					if(!GetComponent<AudioSource>().isPlaying)
						GetComponent<AudioSource>().Play();
				}
			}
	}
	//--------------------------------------
	//Can accept input - set to false to disable input
	public bool bAcceptInput = true;
	
	//Win wait interval
	public float WinWaitInterval = 2.0f;
	
	//Reference to level completed graphic
	public Sprite LevelCompleteGraphic = null;

	//Reference to next level
	public int NextLevel = 0;

	//Internal reference to singleton
	private static GameManager instance = null;

	//Internal reference to texture display pos
	private Rect WinDisplayPos = new Rect();
	private Rect WinTexCoords = new Rect();

	//Internal reference to all crates in scene
	private Crate[] Crates = null;

	//Flag indicating whether level is completed
	private bool bLevelCompleted = false;
	//--------------------------------------
	// Use this for initialization
	void Awake () 
	{
		//If there is already an instance of this class, then remove
		if(instance) {DestroyImmediate(this);return;}

		//Assign this instance as singleton
		instance = this;

		//Get texture coordinates
		WinTexCoords.x = LevelCompleteGraphic.rect.x/LevelCompleteGraphic.texture.width;
		WinTexCoords.y = LevelCompleteGraphic.rect.y/LevelCompleteGraphic.texture.height;
		WinTexCoords.width = (LevelCompleteGraphic.rect.x + LevelCompleteGraphic.rect.width) / LevelCompleteGraphic.texture.width;
		WinTexCoords.height = (LevelCompleteGraphic.rect.y + LevelCompleteGraphic.rect.height) / LevelCompleteGraphic.texture.height;

		//Get all crates in scene
		Crates = GameObject.FindObjectsOfType<Crate>();
	}
	//--------------------------------------
	//Function to restart current level
	public void RestartLevel()
	{
		//Restart current level
		Application.LoadLevel(Application.loadedLevel);
	}
	//--------------------------------------
	//Show level completed GUI graphic
	void OnGUI()
	{
		//If not completed then don't show win
		if(!bLevelCompleted)return;
	
		GUI.DrawTextureWithTexCoords(WinDisplayPos, LevelCompleteGraphic.texture, WinTexCoords);
	}
	//--------------------------------------
	//Check scene for win condition and update
	public bool CheckForWin()
	{
		//If already won, then exit
		if(bLevelCompleted) return true;

		//Checks scene for win condition - returns true if game is won
		foreach(Crate C in Crates)
		{
			if(!C.bIsOnDestination) 
				return false; //If there is one or more crates not on destination then exit with false - no win situation
		}
	
		//If reached here, then we have a win situation
		LevelCompleted = true;

		//Level completed
		return true;
	}
	//--------------------------------------
	//Count down to next level
	public IEnumerator MoveToNextLevel()
	{
		//Wait for win interval
		yield return new WaitForSeconds(WinWaitInterval);

		//Now load next level
		Application.LoadLevel(NextLevel);
	}
	//--------------------------------------
	// Update is called once per frame
	void Update ()
	{
		//Update win display pos
		WinDisplayPos.x = Screen.width/2 - LevelCompleteGraphic.rect.width/2;
		WinDisplayPos.y = Screen.height/2 - LevelCompleteGraphic.rect.height/2;
		WinDisplayPos.width = LevelCompleteGraphic.rect.width;
		WinDisplayPos.height = LevelCompleteGraphic.rect.height;

		//Check for win
		CheckForWin();
	}
}