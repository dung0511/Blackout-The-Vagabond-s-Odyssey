using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class ApiKeyData
{
    public string apiKey;
}

public class NomicAI : MonoBehaviour
{
    public static NomicAI INSTANCE;
    private void Awake()
    {
        INSTANCE = this;
    }
    public void CompareItems(string itemADescription, string itemBDescription)
    {
        StartCoroutine(SendToNomicAndCompare(itemADescription, itemBDescription));
    }

    IEnumerator SendToNomicAndCompare(string descA, string descB)
    {
        string apiUrl = "https://api-atlas.nomic.ai/v1/embedding/text";


        var payload = new NomicRequest()
        {
            model = "nomic-embed-text-v1.5",
            task_type = "search_document",
            texts = new string[] { descA, descB }
        };
        string jsonBody = JsonUtility.ToJson(payload);

        // UnityWebRequest setup
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + LoadApiKey());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;

                NomicResponse response = JsonConvert.DeserializeObject<NomicResponse>(json);

                float[] vecA = response.Embeddings[0];
                float[] vecB = response.Embeddings[1];

                float similarity = CosineSimilarity(vecA, vecB);
                string suggestion = similarity > 0.98f ?
    "They're almost the same! Either would work perfectly." :
    similarity < 0.85f ?
    "These are quite different. Pick the one you like best!" :
    "Not too similar, but still good options. Choose whichever feels right!";

                //resultText.text = $"Cosine similarity: {similarity:F4}\nRecommend: {suggestion}";
                Debug.Log($"Cosine similarity: {similarity:F4}\nRecommend: {suggestion}");
            }
            else
            {
                Debug.Log("Error: " + request.error + "\n" + request.downloadHandler.text);
            }
        }
    }

    float CosineSimilarity(float[] a, float[] b)
    {
        float dot = 0f, magA = 0f, magB = 0f;
        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }
        return dot / (Mathf.Sqrt(magA) * Mathf.Sqrt(magB));
    }

    // Structs for JSON Serialization
    [Serializable]
    public class NomicRequest
    {
        public string model;
        public string task_type;
        public string[] texts;
    }

    [Serializable]
    public class NomicResponse
    {
        [JsonProperty("embeddings")]
        public float[][] Embeddings { get; set; }
    }

    [Serializable]
    public class Embedding
    {
        public float[] values;
    }

    public static string LoadApiKey()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "apikey.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ApiKeyData data = JsonUtility.FromJson<ApiKeyData>(json);
            return data.apiKey;
        }
        else
        {
            Debug.LogError("API Key file not found!");
            return null;
        }
    }
}
