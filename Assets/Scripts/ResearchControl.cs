using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ResearchControl : MonoBehaviour {

    public GameObject welcomeMessage;
    public GameObject firstResearch;
    public GameObject finalResearch;
    public string[] jogador = new string[7];

    //public GameObject jogadorName;
    public GameObject jogadorIdade;
    public GameObject jogadorGenero;
    public GameObject jogadorFormacao;
    public GameObject jogadorFrequenciaHoras;

    public GameObject jogadorPeriodo;
    public GameObject btnEnviar;


    //public GameObject formacaoInput;
    //public GameObject cidadeInput;


    public GameObject emailPanel;
    public GameObject emailInput;
    public GameObject btnEnviarEmail;


    string cdPlayerByEmail;
    public int cdPlayerEmail = 0;
    string urlGetEmail = "http://ec2-54-187-131-63.us-west-2.compute.amazonaws.com/getemail.php?player_email=";


    public int[] aux = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] auxGenero;
    public int count = 0;

    public string email;


    // Use this for initialization
    void Start () {
        welcomeMessage.SetActive(true);
        firstResearch.SetActive(false);
        finalResearch.SetActive(false);
        jogadorPeriodo.SetActive(false);
        btnEnviar.SetActive(false);
        emailPanel.SetActive(false);
        btnEnviarEmail.SetActive(false);
        auxGenero = new int[jogadorGenero.transform.childCount];
   

    }


    // Update is called once per frame
    void Update ()
    {

        if (jogador[6] != null)
        {
            btnEnviar.SetActive(true);
        }
	}

    //alterna entre a tela de login e a tela 1 de pesquisa
    public void EnterResearch()
    {


        if (cdPlayerEmail!=0)
        {
            //print("entrou no if");
            SceneManager.LoadScene("MenuPrincipal");

        }
        else
        {
            //print("entrou no else");
            emailPanel.SetActive(false);
            firstResearch.SetActive(true); //player info
            // finalResearch.SetActive(true);//comeca as perguntas
        }
    }

    // alterna entre a tela de pesquisa 1 e a tela de pesquisa 2
    public void EnterFinalResearch()
    {
        firstResearch.SetActive(false);
        finalResearch.SetActive(true);
    }

    //captura o email do jogador
    public void SetEmail()
    {
        email = emailInput.GetComponent<InputField>().text.Trim();
        if(email != null)
        {
            //print(email);
            StartCoroutine(Coroutine(email));
            
        }
    }


    // Use this for initialization
    public IEnumerator Coroutine(string email)
    {

        GameObject user = GameObject.Find("GetUser");
        CdPlayer getUser = user.GetComponent<CdPlayer>();
        

        WWW cdPlayer = new WWW(urlGetEmail + email);
        yield return cdPlayer;


        cdPlayerByEmail = cdPlayer.text.ToString();

        if (cdPlayerByEmail!="")
        {

            cdPlayerEmail = int.Parse(cdPlayerByEmail);
            getUser.cdPlayer = cdPlayerEmail;
            //print("cdPlayerEmail " + cdPlayerEmail);
            //print(cdPlayerEmail);
            btnEnviarEmail.SetActive(true);
        }
        else
        {

            btnEnviarEmail.SetActive(true);
        }
       
    }


    //alterna entre a tela de boas vindas e tela de login/ email
    public void Login()
    {
        welcomeMessage.SetActive(false);
        emailPanel.SetActive(true);
    }
    

    public void SetIdade(int index)
    {
        
        jogador[1] = jogadorIdade.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
        //print("setIdade" + jogador[1]);
        aux[1] = 1;


        switch (jogador[1])
        {
            case "Entre 0 e 14 anos":
                aux[1] = 1;
                break;
            case "Entre 15 e 19 anos":
                aux[1] = 2;
                break;
            case "Entre 20 e 29 anos":
                aux[1] = 3;
                break;
            case "Entre 30 e 39 anos":
                aux[1] = 4;
                break;
            case "Acima de 40 anos":
                aux[1] = 5;
                break;
        }

        //print("setIdade Aux " + aux[1]);
        jogador[1] = aux[1].ToString();

        //print("setIdade " + jogador[1]);
    }

    public void SetGenero(int index)
    {

        string genero = jogadorGenero.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;

        if (genero == "Feminino")
            jogador[2] = "F";
        else if (genero == "Masculino")
            jogador[2] = "M";
        else
            jogador[2] = "N";
     
        aux[2] = 1;
        //print("setGenero " + jogador[2]);
    }
    public void SetFormacao(int index)
    {
      
       
            jogador[3] = jogadorFormacao.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
            aux[3] = 1;


            //print("formacao " + jogador[3]);

    }



    public void SetFrequencia(int index)
    {
        
        jogador[5] = jogadorFrequenciaHoras.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
        //print("SetFrequencia" + jogador[5]);

        switch (jogador[5])
        {
            case "Sim, com frequencia":
                aux[5] = 1;
                break;
            case "Não, só de vez em quando":
                aux[5] = 2;
                break;
            case "Não, só quando estou com amigos":
                aux[5] = 3;
                break;
            case "Não, eu não curto muito":
                aux[5] = 4;
                break;
        }


        //print("Frequencia Aux" + aux[5]);
        jogador[5] = aux[5].ToString();

        //print("frequencia" + jogador[5]);


    }


    public void SetPeriodo(int index)
    {
       
        jogador[6] = jogadorPeriodo.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
        aux[6] = 1;
        //print("SetPeriodo" + jogador[6]);


        switch (jogador[6])
        {
            case "Entre 1 e 2 horas":
                aux[6] = 1;
                break;
            case "Entre 3 e 4 horas":
                aux[6] = 2;
                break;
            case "Entre 5 e 6 horas":
                aux[6] = 3;
                break;

            case "Entre 7 e 8 horas":
                aux[6] = 4;
                break;
            case "Acima de 9 horas":
                aux[6] = 5;
                break;
        }

       // print("periodo Aux" + aux[6]);
        jogador[6] = aux[6].ToString();

        //print("periodo" + jogador[6]);



    }


}
