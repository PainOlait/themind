using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Joueur : MonoBehaviour
{
    public GameObject cartePrefab;
    public Transform carteParent;

    public TheMind controller;

    // Distribue les cartes aux joueurs
    public void Distribution(List<int> _Cartes)
    {
        _Cartes.Sort();
        if (carteParent == null) carteParent = transform.GetChild(0).transform;
        for (int i = 0; i < _Cartes.Count; i++)
        {
            // Instancie une carte et lui donne sa valeur
            GameObject carte = Instantiate(cartePrefab, carteParent);
            carte.name = _Cartes[i].ToString();
            Transform contenu = carte.transform.GetChild(0);
            TMP_Text valeur = contenu.GetComponent<TMP_Text>();
            valeur.text = _Cartes[i].ToString();

            //Ajoute les cartes distribuées au controller de la zone de jeu
            controller.cartesDeManche.Add(_Cartes[i]);
        }
    }

    // Joue une carte
    public void PlayCard(GameObject carte)
    {
        Debug.Log(gameObject.name + " joue la carte " + carte.transform.GetChild(0).GetComponent<TMP_Text>().text);

        Transform zoneDeJeu = GameObject.Find("Zone de jeu").transform;

        // Destruction des cartes existantes dans la zone de jeu
        if (zoneDeJeu.childCount != 0)
        {
            foreach (Transform child in zoneDeJeu)
            {
                Destroy(child.gameObject);
            }
        }

        // Fait checker la carte jouée par le controller de la zone de jeu
        controller.CheckCarte(carte);
        controller.CheckEndLevel();
    }

}

