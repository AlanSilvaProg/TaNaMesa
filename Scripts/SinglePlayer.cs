using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Resources;

[System.Serializable]
public class DicasControll
{
    public string[] dicasUsed;
}

public enum StateGameSingle
{
    INGREDIENTEFASE,
    DISCASFASE,
    RECEITAFASE,
    PONTUACAOFASE
}


public class SinglePlayer : MonoBehaviour
{

    [Header("Habilitar para teste sem erros")]
    public bool testando;

    public StateGameSingle stateGame;

    public Receita actualReceita;

    public Button[] textButtonsGame;
    public Button[] textIngre;

    private GameObject[] dicasCatch;
    public GameObject[] dicaSlots;
    private int dicasLiberadas;

    public GameObject dicasCanvas, ingredientesCanvas, receitaCanvas, pontuacaoCanvas;
    public Image[] stars;
    public Text pontuacaoText, fimPontuacao;
    public Button nextLevel;

    public SpriteRenderer bgMinas;

    public Text textoNextButton;

    private GameObject[] bonusCatch;
    private GameObject[] receitaCatch;
    private Receita[] receitaAvaible;
    private IngredienteInfo ingredienteCatch;
    private List<string> receitaUsed;
    private int valueRandom;


    private int ingredienteCount;

    private bool contandoPonto;
    private float countStars;
    private float pontuacao; // temporario
    private float pontuacaoContador; // temporario
    private int randomCatch;
    private bool changeTexts;
    private int correctButtonIngre; // 0 a 3
    private int correctButton; // 0 a 3
    private int actualIngrediente;

    private bool lastIngre;

    [SerializeField]
    private Sprite Calopsita1, Calopsita2, Gato1, Gato2, Vaca1, Vaca2, Mexico1, Mexico2, VacaWrong, MexicoWrong;

    [SerializeField]
    private Image NPCSlot, PlayerSlot, NPCSlotWrong;

    private int currentRecipe = 0;

    // Use this for initialization

    private void Awake()
    {

       
        if (PlayerPrefs.GetString("Modo") == "Rapido" || testando)
        {
            receitaCatch = Resources.LoadAll<GameObject>("All");
        }
        else if (PlayerPrefs.GetString("Modo") == "Campanha")
        {
            receitaCatch = Resources.LoadAll<GameObject>("Paises/" + PlayerPrefs.GetString("PaisAtual"));
        }
        receitaAvaible = new Receita[receitaCatch.Length];
        valueRandom = receitaAvaible.Length;
        for (int i = 0; i < receitaCatch.Length; i++)
        {
            receitaAvaible[i] = receitaCatch[i].GetComponent<Receitas>().receita;
        }
        receitaUsed = new List<string>();

    }


    void Start()
    {

        AtualizarReceita();
        bonusCatch = Resources.LoadAll<GameObject>("Bonus");
        dicasCatch = Resources.LoadAll<GameObject>("Dicas");
        changeTexts = true;
        correctButtonIngre = (int)Random.Range(0, 4);
        correctButton = (int)Random.Range(0, 4);
        pontuacao = 300;
        pontuacaoContador = 0;
        countStars = 0;

        if (PlayerPrefs.GetString("PaisAtual") == "Brasil")
        {
            NPCSlotWrong.sprite = VacaWrong;
        }
        else if (PlayerPrefs.GetString("PaisAtual") == "Mexico")
        {
            NPCSlotWrong.sprite = MexicoWrong;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetString("PoP") == "Sim")
        {
            MakePopUp();
        }

        if (stateGame == StateGameSingle.DISCASFASE)
        {
            changeTexts = true;
            if (dicasCanvas.activeSelf == false)
            {
                //gameObject.SendMessage("DicasVisit");
                dicasCanvas.SetActive(true);
                ingredientesCanvas.SetActive(false);
                receitaCanvas.SetActive(false);
                pontuacaoCanvas.SetActive(false);
            }

            if (changeTexts)
            {
                ChangeTexts();
            }

        }

        if (stateGame == StateGameSingle.INGREDIENTEFASE)
        {
            if (ingredientesCanvas.activeSelf == false)
            {
                //gameObject.SendMessage("IngreVisit");
                ingredientesCanvas.SetActive(true);
                dicasCanvas.SetActive(false);
                receitaCanvas.SetActive(false);
                pontuacaoCanvas.SetActive(false);
            }

            for (int i = 0; i < textButtonsGame.Length; i++)
            {
                if (i == 0)
                {
                    textButtonsGame[i].gameObject.SetActive(true);
                    textButtonsGame[i].interactable = false;
                }
                else if (i < 4)
                {
                    textButtonsGame[i].gameObject.SetActive(false);
                }

                if (i == 4)
                {
                    if (actualIngrediente == 0)
                    {
                        textButtonsGame[i].interactable = false;
                    }
                    else
                    {
                        textButtonsGame[i].interactable = true;
                    }
                }
                if (i == 5)
                {
                    textButtonsGame[i].interactable = true;
                }

            }

            if (changeTexts)
            {
                ChangeTexts();
            }

        }

        if (stateGame == StateGameSingle.RECEITAFASE)
        {
            if (receitaCanvas.activeSelf == false)
            {
                ingredientesCanvas.SetActive(false);
                dicasCanvas.SetActive(false);
                receitaCanvas.SetActive(true);
                pontuacaoCanvas.SetActive(false);
            }

            for (int i = 0; i < textButtonsGame.Length; i++)
            {
                textButtonsGame[i].gameObject.SetActive(true);
                if (i == 5)
                {
                    textButtonsGame[i].interactable = false;
                }
                else
                {
                    textButtonsGame[i].interactable = true;
                }
            }
            if (changeTexts)
            {
                ChangeTexts();
            }
        }

        if (stateGame == StateGameSingle.PONTUACAOFASE)
        {

            ingredientesCanvas.SetActive(false);
            dicasCanvas.SetActive(false);
            receitaCanvas.SetActive(false);
            pontuacaoCanvas.SetActive(true);

            if (!contandoPonto)
            {
                ContaPonto();
                fimPontuacao.text = "";
                nextLevel.gameObject.SetActive(false);
            }
        }
        else
        {
            pontuacaoCanvas.SetActive(false);
        }

    }

    public void AtualizarReceita()
    {
        if (receitaAvaible.Length != receitaUsed.Count)
        {
            randomCatch = (int)Random.Range(0, valueRandom);
        }
        else
        {
            return;
        }

        for (int f = 0; f < receitaUsed.Count; f++)
        {
            for (int i = 0; i < receitaAvaible.Length; i++)
            {
                if (receitaAvaible[i].NomeDaReceita == receitaUsed[f] && i == randomCatch)
                {
                    AtualizarReceita();
                    return;
                }
            }
        }

        Receita receitaCatch;
        receitaCatch = receitaAvaible[randomCatch];
        actualReceita = receitaCatch;
        receitaUsed.Add(actualReceita.NomeDaReceita);
        dicasLiberadas = 0;



    }

    public void ChangeTexts()
    {
        if (stateGame == StateGameSingle.DISCASFASE)
        {
            for (int i = 0; i < dicaSlots.Length; i++)
            {                

                for (int y = 0; y < dicasCatch.Length; y++)
                {
                    if (dicasCatch[y].GetComponent<Dicas>().dicas.ingredienteName == actualReceita.Ingredientes[actualIngrediente])
                    {
                        dicaSlots[i].GetComponentInChildren<Text>().text = dicasCatch[y].GetComponent<Dicas>().dicas.dicas[i];
                    }
                }

            }

            for (int i = 0; i < dicaSlots.Length; i++)
            {
                if (i >= dicasLiberadas)
                {
                    if (i == 0)
                    {
                        if (dicasLiberadas == 0)
                        {

                        }
                    }
                    else
                    {
                        dicaSlots[i].GetComponentInChildren<Text>().text = "Revelar Dica";
                    }
                }
            }

        }

        if (stateGame == StateGameSingle.INGREDIENTEFASE)
        {
            for (int i = 0; i < textButtonsGame.Length; i++)
            {
                if (i == correctButtonIngre)
                {
                    textIngre[i].GetComponentInChildren<Text>().text = actualReceita.Ingredientes[actualIngrediente];
                }
                else if (i < 4)
                {
                    GameObject ingredientesRandom = Resources.Load<GameObject>("Ingredientes/" + actualReceita.Ingredientes[actualIngrediente]);
                    print(ingredientesRandom.name);
                    IngredienteInfo random = ingredientesRandom.GetComponent<Ingrediente>().ingredienteInfo;
                    textIngre[i].GetComponentInChildren<Text>().text = random.randomIngre[i];
                }
            }
        }

        if (stateGame == StateGameSingle.RECEITAFASE)
        {
            for (int i = 0; i < textButtonsGame.Length; i++)
            {
                if (i == correctButton)
                {
                    textButtonsGame[i].GetComponentInChildren<Text>().text = actualReceita.NomeDaReceita;
                }
                else if (i < 4)
                {
                    textButtonsGame[i].GetComponentInChildren<Text>().text = actualReceita.random[i];
                }
            }
        }

        changeTexts = false;

    }

    public void InputButtonGame(int indiceButton)
    {

        //if (!TutorialScript.onTuto)
        //{
        changeTexts = true;

        if (stateGame == StateGameSingle.DISCASFASE)
        {
            if (indiceButton < 4)
            {
                if (indiceButton > dicasLiberadas || indiceButton == 3 && dicasLiberadas == 3)
                {
                    dicasLiberadas++;
                }
            }
            else if (indiceButton == 4)
            {
                stateGame = StateGameSingle.INGREDIENTEFASE;
            }else if(indiceButton == 5)
            {
                actualIngrediente++;
                dicasLiberadas = 0;
            }else if(indiceButton == 6)
            {
                if(actualIngrediente > 0)
                {
                    actualIngrediente--;
                    dicasLiberadas = 0;
                }
            }

        }
        else if (stateGame == StateGameSingle.INGREDIENTEFASE)
        {
            if (indiceButton == correctButtonIngre)
            {
                if (lastIngre == true)
                {
                    //gameObject.SendMessage("AdiviVisit");
                    stateGame = StateGameSingle.RECEITAFASE;
                    actualIngrediente = 0;
                    return;
                }
                else
                {
                    correctButtonIngre = (int)Random.Range(0, 4);
                    stateGame = StateGameSingle.DISCASFASE;
                    dicasLiberadas = 0;
                    actualIngrediente++;
                    if (actualIngrediente == actualReceita.Ingredientes.Length - 1)
                    {
                        lastIngre = true;
                    }
                    return;
                }
            }
            else if (indiceButton < 4)
            {
                print("Errou");
                return;
            }

            if (indiceButton == 4)
            {
                correctButtonIngre = Random.Range(0, 4);
                stateGame = StateGameSingle.DISCASFASE;
                return;
            }

            if (actualIngrediente >= actualReceita.Ingredientes.Length - 1)
            {
                correctButtonIngre = Random.Range(0, 4);
                stateGame = StateGameSingle.RECEITAFASE;
                actualIngrediente = 0;
            }
            else
            {
                actualIngrediente++;
                if (actualIngrediente > ingredienteCount)
                {
                    ingredienteCount++;
                }
            }

        }
        else
        {
            if (stateGame == StateGameSingle.RECEITAFASE)
            {
                if (indiceButton == correctButton)
                {
                    if (PlayerPrefs.GetString("PaisAtual") == "Brasil")
                    {
                        if (currentRecipe == 0)
                        {
                            NPCSlot.sprite = Vaca1;
                        }
                        else
                        {
                            NPCSlot.sprite = Vaca2;
                        }
                    }
                    else if (PlayerPrefs.GetString("PaisAtual") == "Mexico")
                    {
                        if (currentRecipe == 0)
                        {
                            NPCSlot.sprite = Mexico1;
                        }
                        else
                        {
                            NPCSlot.sprite = Mexico2;
                        }
                    }
                    if (PlayerPrefs.GetInt("Personagem") == 2)
                    {
                        if (currentRecipe == 0)
                        {
                            PlayerSlot.sprite = Calopsita1;
                        }
                        else
                        {
                            PlayerSlot.sprite = Calopsita2;
                        }
                    }
                    else
                    {
                        if (currentRecipe == 0)
                        {
                            PlayerSlot.sprite = Gato1;
                        }
                        else
                        {
                            PlayerSlot.sprite = Gato2;
                        }
                    }
                    stateGame = StateGameSingle.PONTUACAOFASE;
                }
                else
                {
                    if (indiceButton < 4)
                    {
                        pontuacao -= 50;
                    }
                }

                if (indiceButton == 4)
                {
                    stateGame = StateGameSingle.INGREDIENTEFASE;
                    actualIngrediente = actualReceita.Ingredientes.Length - 1;
                }
            }
        }
    }

    public void ContaPonto()
    {
        contandoPonto = true;
        float pontuacaoAtual = pontuacao + pontuacaoContador;
        StartCoroutine("Contagem", pontuacaoAtual);
    }

    IEnumerator Contagem(float pointAtual)
    {
        yield return new WaitForEndOfFrame();
        pontuacaoContador++;
        countStars += 0.01f;
        pontuacaoText.text = "" + (int)pontuacaoContador;


        if (countStars > 0 && countStars < 1.1f)
        {
            stars[0].fillAmount = countStars;
        }
        else if (countStars > 1 && countStars < 2.1f)
        {
            stars[1].fillAmount = (countStars - 1);
        }
        else if (countStars > 2 && countStars < 3.1f)
        {
            stars[2].fillAmount = (countStars - 2);
        }


        if (pontuacaoContador < pointAtual)
        {
            StartCoroutine("Contagem", pointAtual);
        }
        else
        {
            FimPonto();
        }
    }

    public void FimPonto()
    {

        //Fanfarra is love
        //Fanfarra is Life
        //Faça Fanfarra aqui


        if (PlayerPrefs.GetString("Modo") == "Campanha")
        {
            for (int i = 0; i < bonusCatch.Length; i++)
            {
                if (bonusCatch[i].GetComponent<ReceitasBonus>().info.nomeReceita == actualReceita.nomeReceitaBonus)
                {
                    if (countStars >= bonusCatch[i].GetComponent<ReceitasBonus>().info.estrelas)
                    {
                        currentRecipe++;
                        if (PlayerPrefs.GetString(actualReceita.nomeReceitaBonus) != "Obtido")
                        {

                            PlayerPrefs.SetInt("Notificacao", 1);
                        }
                        PlayerPrefs.SetString(actualReceita.nomeReceitaBonus, "Obtido");
                        PlayerPrefs.SetString("Atualizar", "Sim");
                    }
                }
            }
        }

        if (receitaAvaible.Length == receitaUsed.Count)
        {
            textoNextButton.text = "Voltar para o Menu";
        }
        else
        {
            textoNextButton.text = "Próxima Receita";
        }

        fimPontuacao.text = "Sua Pontuação Atual";
        nextLevel.gameObject.SetActive(true);
    }

    public void NextLevel()
    {

        if (receitaAvaible.Length == receitaUsed.Count)
        {
            if (PlayerPrefs.GetString("Atualizar") == "Sim")
            {
                BookCook();
            }
            else if (PlayerPrefs.GetString("Modo") == "Campanha")
            {
                PlayerPrefs.SetString("Over", "Go");
            }
            SceneManager.LoadScene(0);
            return;
        }

        correctButton = (int)Random.Range(0, 4);
        correctButtonIngre = (int)Random.Range(0, 4);
        randomCatch = (int)Random.Range(0, 5);
        dicasLiberadas = 0;
        AtualizarReceita();
        ResetVariaveis();
        ResetStars();

    }

    public void ResetVariaveis()
    {

        contandoPonto = false;
        pontuacaoCanvas.SetActive(false);
        ingredientesCanvas.SetActive(false);
        receitaCanvas.SetActive(false);
        dicasCanvas.SetActive(true);
        stateGame = StateGameSingle.DISCASFASE;
        actualIngrediente = 0;
        ingredienteCount = 0;
        pontuacao = 300;
        countStars = 0;
        dicasLiberadas = 0;

    }

    public void ResetStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].fillAmount = 0;
        }
    }

    public void MakePopUp()
    {
        //Fazer trigger true do pop up 
        // Pop Up tem que ser um botão, para quando clicar ele executar a Função BookCook
    }

    public void BookCook()
    {
        PlayerPrefs.SetString("Book", "Go");
        SceneManager.LoadScene("0");
    }


}
