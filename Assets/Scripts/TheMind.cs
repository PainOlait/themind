using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheMind : MonoBehaviour
{
    const int NBCARTES = 100;

    public List<int> deck;
    public List<int> cartesDeManche;
    public List<Joueur> joueurs;

    int level;
    int levelMax;
    int nbVie;
    int nbShuriken;
    int nbJoueur;

    public GameObject compteurVie;
    public GameObject compteurShuriken;
    public GameObject compteurNiveau;

    public GameObject endScreen;
    public GameObject victoryScreen;

    Transform zoneDeJeu;

    [SerializeField]
    GameConfig config;

    private void Start()
    {
        if (endScreen == null) endScreen = GameObject.Find("End game screen");
        zoneDeJeu = GameObject.Find("Zone de jeu").transform;
    }

    public void NewGame()
    {
        nbJoueur = config.nbJoueur;
        levelMax = config.nbLevels;
        nbVie = config.nbVie; ;
        nbShuriken = config.nbShuriken;
        level = 1;
        int i = 0;
        while (i <= nbJoueur-1)
        {
            joueurs[i].gameObject.SetActive(true);
            i++;
        }
        StartGame();
    }

    public void StartGame()
    {
        SetCompteurs();
        deck.Clear();

        if (cartesDeManche != null)
        {
            cartesDeManche.Clear();
        }

        foreach (Carte carte in FindObjectsOfType<Carte>())
        {
            Destroy(carte.gameObject);
        }

        // Crée un deck de 100 cartes
        for (int i = 0; i < NBCARTES; i++)
        {
            deck.Add(i + 1);
        }

        // Mélange le deck
        for (int i = 0; i < NBCARTES; i++)
        {
            int temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        // Distribue les cartes
        for (int i = 0; i < nbJoueur; i++)
        {
            joueurs[i].Distribution(deck.GetRange(level * i, level));
            Debug.Log("Distribution de " + level + " cartes au Joueur " + (i+1));
        }
        cartesDeManche.Sort();
    }

    public void SetCompteurs()
    {
        compteurVie.GetComponent<TMP_Text>().text = "Vie <br><br>" + nbVie.ToString();
        compteurShuriken.GetComponent<TMP_Text>().text = "Shuriken <br><br>" + nbShuriken.ToString();
        compteurNiveau.GetComponent<TMP_Text>().text = "Niveau " + level + " / " + levelMax;
    }

    public void CheckCarte(GameObject carte)
    {   
        int valeurCarte = int.Parse(carte.transform.GetChild(0).GetComponent<TMP_Text>().text);
        if (valeurCarte > cartesDeManche[0])
        {
            WrongCard();
            int len = cartesDeManche.Count;
            for (int i = 0; i < len; i++)
            {
                if (valeurCarte >= cartesDeManche[i])
                {
                    GameObject.Find(cartesDeManche[i].ToString()).transform.SetParent(zoneDeJeu);
                    cartesDeManche.Remove(cartesDeManche[i]);
                    i--;
                    len--;
                }
            }
        }
        else if (valeurCarte <= cartesDeManche[0])
        {
            GameObject.Find(cartesDeManche[0].ToString()).transform.SetParent(zoneDeJeu);
            cartesDeManche.Remove(cartesDeManche[0]);
        }
        Debug.Log("cartes restantes : ");
        foreach (int a in cartesDeManche)
        {
            Debug.Log(a);
        }
    }

    public void WrongCard()
    {
        nbVie--;
        SetCompteurs();

        if (nbVie < 1)
        {
            GameLost();
        }
    }

    public void GameLost()
    {
        endScreen.SetActive(true);
    }

    public void CheckEndLevel()
    {
        int nbCartes = 0;
        for (int i = 0; i < nbJoueur-1; i++)
        {
            nbCartes += joueurs[i].transform.GetChild(0).transform.childCount;
        }

        if (nbCartes == 0)
        {
            LevelCompleted();
            level ++;
            StartGame();
        }
    }

    public void LevelCompleted()
    {
        if (level == levelMax) GameWon();
        if (level == 2) nbShuriken++; 
        if (level == 3) nbVie++;
        if (level == 5) nbShuriken++;
        if (level == 6) nbVie++;
        if (level == 8) nbShuriken++;
        if (level == 9) nbVie++;
        StartGame();
    }
    void GameWon()
    {
        victoryScreen.SetActive(true);
    }
    public void ActivateShuriken()
    {
        for (int i = 0; i < nbJoueur; i++)
        {
            joueurs[i].transform.GetChild(0).GetChild(0).SetParent(zoneDeJeu);
            cartesDeManche.Remove(cartesDeManche[0]);
            CheckEndLevel();
        }
    }
}


