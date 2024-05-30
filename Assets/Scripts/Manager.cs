using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField scoreField;
    public Button getScoreButton;
    public Button setScoreButton;

    Connection conn;

    public IMongoCollection<BsonDocument> collection;


    void Awake()
    {
        conn = new Connection("scoreDB", "scoreCollection");
        collection = conn.collection;        
    }

    async public void GetScore(string name) //HUOM. esim. listaa ei voi async-metodi palauttaa
    {
        var filter = Builders<BsonDocument>.Filter.Eq("name", name);
        //var results = await collection.Find(filter).ToListAsync(); //listana palautus
        var results = await collection.Find(filter).FirstOrDefaultAsync();


        //jos palautti listan ja haluaa 1.tietueen:
        //nameField.text = results[0]["name"].ToString();

        nameField.text = results["name"].ToString(); 
        scoreField.text = results["score"].ToString();
       
    }

    async public void UpdateScore()
    {
        print("updating");
        string name = nameField.text;
        int score = int.Parse(scoreField.text);

        print($"name is {name} and score is {score}");

        var filter = Builders<BsonDocument>.Filter.Eq("name",name);
        var update = Builders<BsonDocument>.Update.Set("score", score);

        //await collection.UpdateOneAsync(filter, update); //HUOM! collectionissa voi tällä hetkellä olla usea
                                                         // saman niminen pelaaja. Nyt päivitetään vain ensimmäinen
                                                         // löytynyt. (koska kyseessä vain harjoitus)

        await collection.UpdateManyAsync(filter, update); //päivittäisi kaikki scoret, joiden nimenä annettu parametri                                              
    }


}
