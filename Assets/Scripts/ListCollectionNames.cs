using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class ListCollectionNames : MonoBehaviour
{

    Connection conn;

    void Awake()
    {
        conn = new Connection("sample_mflix", "movies");      
    }


    //synkroninen versio (jos tuloksen hakemisessa menisi kauan, niin ohjelma olisi "blokattu" siihen asti
    void Start()
    {
        List<string> collections = conn.db.ListCollectionNames().ToList();

        foreach (string coll in collections)
        {
            print(coll);
        }
    }

    //asynkroninen versio (ei blokkaa ohjelmaa, sallii useita samanaikaisia käyttäjiä/operaatioita
    //async void Start()
    //{
    //    List<string> collections = await conn.db.ListCollectionNames().ToListAsync();

    //    foreach (string coll in collections)
    //    {
    //        print(coll);
    //    }

    //    print(string.Join(",", collections)); //sama kuin foreach, mutta samalla rivillä pilkulla eroteltuna
    //}

}
