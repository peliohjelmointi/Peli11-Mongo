using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver; 
using MongoDB.Bson;

public class FindAllDocuments : MonoBehaviour
{
    Connection conn;

    public IMongoCollection<BsonDocument> collection;

    void Awake()
    {
        conn = new Connection("scoreDB", "scoreCollection");
        collection = conn.collection;
    }

    private void Start()
    {
        //FindAllAsync(); 
        //FindAll();
        //FindAllDocumentsUsingFilter(500);
        //FindAllLessThan(300);
        //FindScoreBetween(30, 300);
        FindUsingAggregate();
    }


    async void FindAllAsync()
    {
        var documents = collection.FindAsync(new BsonDocument());
        var documentsAwaited = await documents;

        foreach (BsonDocument doc in documentsAwaited.ToList())
        {
            //print(doc); //jokainen dokumentin tieto 
            print(doc["plot"]);
        }
    }

    void FindAll()
    {
        var documents = collection.Find(new BsonDocument());        

        foreach (BsonDocument doc in documents.ToList())
        {
            //print(doc); //jokainen dokumentin tieto 
            print(doc["plot"]); //mik‰ tieto halutaan hakea dokumentista
        }
    }

    async void FindAllDocumentsUsingFilter(int score) //SQL WHERE -> Mongo : Filter
    {
        var filter = Builders<BsonDocument>.Filter.Eq("score", score); //score equals 500
        //var filter2 = Builders<BsonDocument>.Filter.Eq("name", "Goofy");
        //var results = collection.Find(filter & filter2)
        var results = await collection.Find(filter).ToListAsync();

        //Debug.Log(string.Join(",", results[0]["name"]));

        foreach (var res in results) //var = BsonDocument, sekin k‰y
        {
            Debug.Log(res["name"]);
        }

    }


    async void FindAllLessThan(int num) //Hae scoret, jotka < 300     ! Lt (Less than)
    {
        var filter = Builders<BsonDocument>.Filter.Lt("score", num);
        var results = await collection.Find(filter).ToListAsync();
        
        foreach (var res in results) 
        {
            Debug.Log(res["name"]);
        }
    }


    async void FindScoreBetween(int num1, int num2)
    {
        var filter = Builders<BsonDocument>.Filter.And(
            Builders<BsonDocument>.Filter.Gt("score", num1),
            Builders<BsonDocument>.Filter.Lt("score", num2));

        var results = await collection.Find(filter).ToListAsync();
        foreach (var res in results)
        {
            Debug.Log(res["name"]);
        }
    }

    async void FindUsingAggregate()
    {
        var filter = Builders<BsonDocument>.Filter.Gt("score", 30);

        var sort = Builders<BsonDocument>.Sort.Descending("score");

        //k‰ytet‰‰n aggregatea (3-vaiheinen haku):
        var results = await collection.Aggregate()
            .Match(filter) // 1
            .Sort(sort)    // 2
            .Limit(10)     // 3
            .ToListAsync();

        foreach (var res in results)
        {
            Debug.Log(res["score"]);
        }

    }
}

