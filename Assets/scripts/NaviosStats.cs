using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pieza { Uno, Dos, Tres, MaxPiezas_2D }
public class NaviosStats 
{
    public enum TypeAtk { TERR, AER, DUAL, MAXTYPE } 
    [System.Serializable]
    public  class Navios
    {  
        public string Name;
        public int ATK, DEF, VEL, SON, HP,MaxHP;
        public TypeAtk typeAtk;
        public Pieza typePieza;
        public GameObject pf;
    }
    public static List<Navios> AllNavios = new List<Navios>()
    {
        //Destructor
        new Navios
        {
            Name="Destructor",
            ATK=3,
            DEF=2,
            VEL=2,
            SON=2,
            MaxHP=2,
            typeAtk=TypeAtk.DUAL,
            typePieza= Pieza.Uno
        },
        //Fragata
        new Navios
        {
           Name="Fragata",
            ATK=1,
            DEF=1,
            VEL=3,
            SON=1,
            MaxHP=2,
            typeAtk=TypeAtk.TERR,
            typePieza= Pieza.Uno
        },
        //Submarino
        new Navios
        {
            Name="Submarino",
            ATK=2,
            DEF=1,
            VEL=2,
            SON=3,
            MaxHP=6,
            typeAtk=TypeAtk.TERR,
            typePieza= Pieza.Dos
        },
        //Acorazado
        new Navios
        {
            Name="Acorazado",
            ATK=2,
            DEF=3,
            VEL=1,
            SON=2,
            MaxHP=6,
            typeAtk=TypeAtk.TERR,
            typePieza= Pieza.Dos
        },
        //Portaviones
        new Navios
        {
            Name="Portaviones",
            ATK=1,
            DEF=3,
            VEL=1,
            SON=3,
            MaxHP=10,
            typeAtk=TypeAtk.DUAL,
            typePieza= Pieza.Tres
        }


    };
    
   
} 
