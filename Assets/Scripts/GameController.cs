using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController controlador;

	public Inventory inventario;
	public Database database;
	public GamepadController gamepadController;

	public int LinternaID = 1;
	public int PilaID = 2;
	public int AlicatesID = 4;

    public float timer;
    public Text timerText;
	public Text timerBombText;
	public Text batteryText;
	public GameObject batteryPanel;
    private bool timerStarted = false;

	private Animator playerAnimator;

    public delegate void TextToSpeak(string text);
    public static event TextToSpeak TextToSpeakPlay;
	public ApiAiModuleMod ai;
    public GameObject panelMac;
    
	public GameObject frontPanel; 
	public Text frontText;

	private bool inicio = false;


    public delegate void Escaleras();
    public static event Escaleras SubirEscaleras;
    public static event Escaleras BajarEscaleras;

    public delegate void Luces(string @string);
    public static event Luces LucesOnOff;

    public delegate void ControlLuces();
    public static event ControlLuces AllDown;
    public static event ControlLuces Up;
    public static event ControlLuces Apagon;

    public GameObject fireWorks;
	public GameObject bombExplosion;
	public GameObject camExplosion;

	private void Awake()
	{
		if (controlador == null)
		{
			controlador = this;
			DontDestroyOnLoad(this);
		}
		else if (controlador != this)
		{
			Destroy(gameObject);
		}
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if (scene.name == "ScapeRoom") {
			Debug.Log ("Escena Cargada");
			SceneManager.UnloadSceneAsync ("Inicio");
			Destroy (GameObject.Find ("Player1"));
			playerAnimator = GameObject.Find ("Player").GetComponent<Animator> ();
			playerAnimator.SetBool ("Start", true);
		}
	}

	private void Start(){
		timer *= 60;
		gamepadController = new GamepadController ();
		gamepadController.Init ();
	}

    public static bool isInventario(int id)
    {
        if (controlador.inventario.FindItemInInventory(id) != null)
        {
            return true;
        }
        return false;
        
    }

	public int bateria = 100;
	public float bat_timer = 100.0f;
	public bool bat_encendida = false;

	private float endGameTimer = 20.0f;
	private bool eGTimerStarter = false;

    void Update(){

		/*************** Linterna ******************/

		if (inventario.IsEquiped (LinternaID) && bat_encendida) {
			bat_timer -= Time.deltaTime;
			bateria = Mathf.RoundToInt (bat_timer);
			batteryText.text = bateria.ToString() + "%";
		}

		/*************** TIMERS ******************/
		if (timerStarted) {
			if (timer > 0.009f) {
				timer -= Time.deltaTime;

				string minutes = Mathf.Floor (timer / 60).ToString ("00");
				string seconds = (timer % 60).ToString ("00");

				timerText.text = string.Format ("{0}:{1}", minutes, seconds);
				timerBombText.text = string.Format ("{0}:{1}", minutes, seconds);
				if (timer <= 0.999f) {
					GameOver ();
				}
			}
		}
		/****************************************/

		/*************** TIMER END GAME ******************/
		if (eGTimerStarter) {
			endGameTimer -= Time.deltaTime;
			if (endGameTimer <= 0) {
				EsceneEndGame ();
			}
		}


		/*************** Inventario *****************/
		/*if (Input.GetKeyDown (gamepadController.Inventario)) {
			ShowFrontPanel ("Accediendo al inventario");
			inventario.Spawn ();
		}*/
    }

    public void StartGame(){
        timerStarted = true;

		ShowFrontPanel("¡Buena suerte!");
    }

    public void GameOver(){
		//TODO:
		ShowFrontPanel("Game Over...");
		bombExplosion.SetActive (true);
		camExplosion.SetActive (true);

		timerStarted = false;

		endGameTimer = 1.5f;
		eGTimerStarter = true;
		
		Debug.Log("HAS PERDIDO");
    }

	public void Win(){
        //TODO:
		ShowFrontPanel("¡Enhorabuena!");
		endGameTimer = 15.0f;
		eGTimerStarter = true;
		
        fireWorks.SetActive(true);
		Debug.Log("HAS GANADO");
	}

	private void EsceneEndGame(){
		//TODO:
		SceneManager.LoadScene("Fin" , LoadSceneMode.Single);
		Destroy(gameObject);
	}

	/*************** LUCES ******************/

    public void ApagonFunction(){
        Apagon();
    }

    public void OffAll()
    {
        if (AllDown != null)
        {
            AllDown();
        }
    }

    public void UpEnable()
    {
        if (Up != null)
        {
            Up();
        }
    }

    public void OnOff(string tag)
    {
        if (LucesOnOff != null)
        {
            LucesOnOff(tag);
        }
    }

	/****************************************/

	/*************** ESCALERAS ******************/

    public static void Subir()
    {
        if (SubirEscaleras != null)
        {
            SubirEscaleras();
            
        }
    }

    public static void Bajar()
    {
        if (BajarEscaleras != null)
        {
            BajarEscaleras();
        }
    }

	/****************************************/

	/*************** DIALOG FLOW ******************/

    public void SendTextToAi(string msg){
        string result = ai.SendText(msg);
        ShowFrontPanel(result);
    }

    public void ShowHidePanelMacListening(){
        
        panelMac.SetActive(!panelMac.activeSelf);
    }

    public void TextToSpeakFunc(string text){
        TextToSpeakPlay(text);
    }


	/****************************************/

	public Animator cajonAnimator;
	/*************** BOMBA ******************/

	private string cable = "amarillo";
	private bool cortado = false;

	public void CortarCable(string cable_){
		ShowFrontPanel("Has cortado el cable " + cable_);
		if (!cortado) {
			cortado = true;
			if (cable == cable_) {
				cajonAnimator.SetBool ("Abrir", true);
			} else {
				GameOver ();
			}
		}
	}
    
	/****************************************/

	/*************** UI ******************/
    public void ShowFrontPanel(string msg){
        frontPanel.SetActive(true);
        frontText.text = msg;
        StartCoroutine(HideCanvas());
    }

    private IEnumerator HideCanvas(){
        yield return new WaitForSeconds(10.0f);
        frontPanel.SetActive(false);
    }
	/****************************************/

	

}
