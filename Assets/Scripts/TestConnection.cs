using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;


public class TestConnection : MonoBehaviour
{
    MongoClient mongoClient;

    private void Awake()
    {
        //YHTEYDEN MÄÄRITTELY        //ÄLÄ KÄYTÄ NUMEROITA TAI ERIKOISMERKKEJÄ KÄYTTÄJÄNIMESSÄ/SALASANASSA
        mongoClient = new MongoClient("mongodb+srv://testaaja:testaaja@cluster0.37ex2rd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");                                       
        //HAETAAN KAIKKI COLLECTIONIT (testataan yhteyden toimivuus)
        List <string> collections = mongoClient.GetDatabase("scoreDB").ListCollectionNames().ToList();

        //LÄPIKÄYDÄÄN KAIKKI COLLECTIONIT JA TULOSTETAAN NE
        foreach (string col in collections)
        {
            print(col);
        }
 
      //C
      //InsertOne   //INSERT SQL
      //InsertMany

      //R           //SELECT 
      //Find() //hakee ilman paremetreja tai filttereitä kaikki TAI filttereiden mukaan
      
      //U
      //UpdateOne    //UPDATE TABLE SET ...
      //UpdateMany

      //D             //DELETE /DROP ...
      //DeleteOne
      //DeleteMany
        
      //Usein puhutaan CRUD-sovelluksista =  ""Create Read Update Delete""


    }

    


}
