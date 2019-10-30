using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<GameObject> listEnemysLv1 = new List<GameObject>();
    public GameObject gameOver;
    public GameObject win;
    public string gameState = "play";

    FloorGenerator script;

    public static int enemysTotal = 0;
    public int enemysLeft = 0;

    public GameObject energy;
    public GameObject heal;
    public GameObject enemysCount;
    public GameObject curretnFloor;

    public AudioClip energySound;
    public AudioClip breakSound;
    public AudioClip boxBreakSound;
    public AudioClip healSound;

    bool isWaiting = false;
    GameObject save;
    GameObject player;


    public List<GameObject> icons = new List<GameObject>();

    //public Dictionary<string,GameObject> shootList = new Dictionary<string,GameObject>();
    public List<GameObject> shootList = new List<GameObject>();


    public List<GameObject> skillList = new List<GameObject>();


    // Use this for initialization
    void Start () {
        
        gameOver.SetActive(false);
        win.SetActive(false);
        GameObject obj = GameObject.Find("FloorGeneratorObj");
        script = obj.GetComponent<FloorGenerator>();
        save = GameObject.Find("Save");
        player = GameObject.Find("Blitz");

        enemysTotal = 0;
        foreach(GameObject floor in script.floor)
        {
            enemysTotal += floor.GetComponent<MapConfig>().enemysLeft;
        }

        foreach(GameObject skill in skillList)
        {
            skill.GetComponent<Skill>().purchased = false;
        }

        enemysLeft = enemysTotal;

        skillList = save.GetComponent<Save>().skillList;

        save.GetComponent<Save>().numberOfCapture = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if(enemysTotal <= 0)
        {
            gameState = "win";
        }

        enemysCount.GetComponent<Text>().text = "Inimigos Restantes: " + enemysTotal + " / " + enemysLeft;
        curretnFloor.GetComponent<Text>().text = "Andar: " + save.GetComponent<Save>().floor;

        if(Input.GetButtonDown("A") && gameState.Equals("gameover"))
        {
            SceneManager.LoadScene("Floor_1");
        }

        if (Input.GetButtonDown("B") && gameState.Equals("gameover"))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }

        if (gameState.Equals("win") && !isWaiting)
        {
            win.SetActive(true);
            StartCoroutine(DelayTime());
        }

        //print(enemysTotal.ToString());

    }

    void PlayerEnergySound()
    {
        GetComponent<AudioSource>().PlayOneShot(energySound, musicControl.soundVolume);
    }

    

    void showGameOver()
    {
        if(menuPrincipal.initialLanguage == 0)
        {
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "REINICIAR O JOGO";
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "REINICIAR O JOGO";

            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "VOLTAR PARA O MENU";
            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLTAR PARA O MENU";
        }
        if(menuPrincipal.initialLanguage == 1)
        {
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "RESTART THE GAME";
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "RESTART THE GAME";

            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "BACK TO MENU";
            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "BACK TO MENU";
        }
        gameState = "gameover";
        gameOver.SetActive(true);
    }

    void BoxBreakSound()
    {
        GetComponent<AudioSource>().PlayOneShot(boxBreakSound, musicControl.soundVolume);
    }

    void JarBreakSound()
    {
        GetComponent<AudioSource>().PlayOneShot(breakSound, musicControl.soundVolume);
    }

    void PlayHealSound()
    {
        GetComponent<AudioSource>().PlayOneShot(healSound, musicControl.soundVolume);
    }

    public void RestartLevel()
    {
        enemysTotal = 0;
        SceneManager.LoadScene("Floor_1");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    IEnumerator DelayTime()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2.5f);
        gameState = "play";
        isWaiting = false;
        save.GetComponent<Save>().floor++;
        save.GetComponent<Save>().xp = player.GetComponent<Player>().xp;
        save.GetComponent<Save>().skill = player.GetComponent<Player>().skillEquiped;
        save.GetComponent<Save>().shoot = player.GetComponent<Player>().shoot;
        save.GetComponent<Save>().classify = player.GetComponent<Player>().knnClass;
        save.GetComponent<Save>().skillList = skillList;
        RestartLevel();
    }

    public void PassaFase()
    {
        enemysTotal = 0;
    }


}
