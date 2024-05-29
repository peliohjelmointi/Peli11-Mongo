using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;


public class Connection
{                                                          
    MongoClient mongoClient = new MongoClient("mongodb+srv://USERNAME:PASSWORD@cluster0.37ex2rd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");

    public IMongoDatabase db;
    IMongoCollection<BsonDocument> collection;

    //konstruktori (kutsutaan, kun olio Connection-luokasta luodaan kahdella string-parametrilla)
    public Connection(string dbName, string dbCollection)
    {
        db = mongoClient.GetDatabase(dbName); //asetetaan muuttujaan parametrina saatu tietokannan nimi
        collection = db.GetCollection<BsonDocument>(dbCollection);
        Debug.Log($"Connection-olio luotiin tietokannalle {dbName}, collectionille {dbCollection}");
    }
    
}
