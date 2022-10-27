using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "TheMind/GameConfig")]

public class GameConfig : ScriptableObject
{
    public int nbJoueur;
    public int nbLevels;
    public int nbVie;
    public int nbShuriken;
}
