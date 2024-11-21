using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;



public class manazer : MonoBehaviour 

{ 
    public int Speed = 1;
    public int VirusLvl = 1;
    public int MusicType = 0; // premenna, ktora sa bude presuvat scenami 
    public static manazer jedinacik; // STATICKY objekt typu ROVNAKA TRIEDA==singleton 

   
    void Awake() // spusti sa este pred vytvorenim objektu 
    { 
        if (jedinacik != null) // ak uz existuje takyto singleton / objekt... 
        { 
            Destroy(gameObject); // ...tak tuto kopiu zmaz 
        } 
        else // ...inak... 
        { 
            jedinacik = this; // ...ak neexistuje, tak singletonu prirad tento objekt 
            DontDestroyOnLoad(gameObject); // udrzanie objektu pri prechode scenami 
        } 
    } 
    public void Musicsave1()
    {
        MusicType = 1;
    }
    public void Musicsave2()
    {
        MusicType = 2;
    }
    public void Musicsave3()
    {
        MusicType =3;
    }



     public void speedsave1()
    {
        Speed = 1;
    }
    public void speedsave2()
    {
        Speed = 2;
    }
    public void speedsave3()
    {
        Speed = 3;
    }

    //textury na tlačitka
    public void SPEED() 
    {
        
    } 
    
} 