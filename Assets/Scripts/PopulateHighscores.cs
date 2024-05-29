using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class PopulateHighscores : MonoBehaviour
{
    public List<TextMeshProUGUI> HighscoreList = new List<TextMeshProUGUI>();

    Connection conn;

    FilterDefinition<BsonDocument> filter;
    List<BsonDocument> results;

    int i = 0;

    private void Awake()
    {
        conn = new Connection("scoreDB", "scoreCollection");
        filter = Builders<BsonDocument>.Filter.Empty;
        var sort = Builders<BsonDocument>.Sort.Descending("score");
        results = conn.collection.Aggregate().Match(filter).Sort(sort).ToList();
    }

    void Start()
    {
        foreach (var entry in HighscoreList)
        {
            if (i < results.Count)
            {

                if (results[i]["score"] != null)
                {
                    entry.text = results[i]["score"].ToString()+"\t\t"+results[i]["name"];
                }
                else
                {
                    entry.text = "---no score yet set----";
                }

            }
            else
            {
                entry.text = "---no score yet set----";
            }
            i++;



        }


    }
}
