using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Carte : MonoBehaviour
{

    public void ClickCarte()
    {
        GameObject carteAJouer = this.gameObject;

        Joueur joueur = carteAJouer.transform.parent.parent.GetComponent<Joueur>();
        joueur.PlayCard(carteAJouer);
    }

}
