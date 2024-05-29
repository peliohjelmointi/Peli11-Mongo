using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;


public class InsertOneDocument : MonoBehaviour
{

    Connection conn;

    IMongoCollection<BsonDocument> collection;

    void Awake()
    {
        conn = new Connection("scoreDB", "scoreCollection");
        collection = conn.collection;
        //InsertOneFromJSON();
        InsertOneFromClass();
    }

    private void Start()
    {
        //1) Dokumentin lisäys, jos luo oman luokan
        //2) Dokumentin lisäys suoraan JSON-muodossa

        //InsertOneFromClass();      

    }

    async void InsertOneFromClass()
    {
        var newHighscore = new Highscore
        {
            //omaID = ObjectId.GenerateNewId(),
            name = "Hessu Hopo00",
            score = 150
        };

        
        BsonDocument bsonHighscore = newHighscore.ToBsonDocument(); //konvertoidaan luokka BsonDocumentiksi
        await collection.InsertOneAsync(bsonHighscore);
        
    }

    async void InsertOneFromJSON() {
        var newBsonDocument = new BsonDocument
        {
            {"name", "Taikaviitta 33" },
            {"score", 2232 }
        };
        await collection.InsertOneAsync(newBsonDocument);
    }

}

public class Highscore
{
    public ObjectId omaID;
    public string name;
    public int score;
}
