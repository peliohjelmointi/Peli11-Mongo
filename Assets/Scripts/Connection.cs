using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System;


public class Connection
{
    MongoClient mongoClient = new MongoClient("mongodb+srv://USER:PASSWORD@cluster0.37ex2rd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");

    public IMongoDatabase db;
    IMongoCollection<BsonDocument> collection;

    //konstruktori
    public Connection(string dbName, string dbCollection)
    {
        db = mongoClient.GetDatabase(dbName); //asetetaan muuttujaan parametrina saatu tietokannan nimi
        collection = db.GetCollection<BsonDocument>(dbCollection);
        Debug.Log($"Connection-olio luotiin tietokannalle {dbName}, collectionille {dbCollection}");

        try
        {
            //Pingataan yhteys, jotta tiedet‰‰n, saatiinko yhteys vai ei:
            var result = db.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Debug.Log($"Ping OK to DB {dbName}");
        }
        catch (Exception e)
        {
            Debug.Log($"Cannot connect. Error: {e}");
        }
    }
    
}
