using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class ApiKeyData
{
    public string apiKeyNomicAI;
    public string apiKeyGoogle;
}

public class RecommendAI : MonoBehaviour
{
    public static RecommendAI INSTANCE;
    private void Awake()
    {
        INSTANCE = this;
    }
    private void Start()
    {

        DontDestroyOnLoad(gameObject);
    }
    public void CompareItems(string itemADescription, string itemBDescription, string weapon1Name, string weapon2Name)
    {
        StartCoroutine(SendToNomicAndCompare(itemADescription, itemBDescription, weapon1Name, weapon2Name));
    }

    IEnumerator SendToNomicAndCompare(string descA, string descB, string weapon1Name, string weapon2Name)
    {
        string apiUrl = "https://api-atlas.nomic.ai/v1/embedding/text";


        var payload = new NomicRequest()
        {
            model = "nomic-embed-text-v1.5",
            task_type = "search_document",
            texts = new string[] { descA, descB }
        };
        string jsonBody = JsonUtility.ToJson(payload);
        ApiKeyData keys = LoadApiKeys();
        // UnityWebRequest setup
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + keys.apiKeyNomicAI);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;

                NomicResponse response = JsonConvert.DeserializeObject<NomicResponse>(json);

                float[] vecA = response.Embeddings[0];
                float[] vecB = response.Embeddings[1];

                float similarity = CosineSimilarity(vecA, vecB);
                string suggestion = similarity > 0.98f ?
    $"{weapon1Name} and {weapon2Name} are almost the same! Either would work perfectly." :
    similarity < 0.85f ?
    $"{weapon1Name} and {weapon2Name} are quite different. Pick the one you like best!" :
    $"{weapon2Name} is not too similar to {weapon1Name}, but still good options. Choose whichever feels right!";

                Talking.INSTANCE.Talk(suggestion);
                Debug.Log($"Cosine similarity: {similarity:F4}\nRecommend: {suggestion}");
            }
            else
            {
                Talking.INSTANCE.Talk(weapon2Name);
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

    public static ApiKeyData LoadApiKeys()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "apikey.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ApiKeyData data = JsonUtility.FromJson<ApiKeyData>(json);
            return data;
        }
        else
        {
            Debug.LogError("API Key file not found!");
            return null;
        }
    }

    public void ThinkAboutTwoWeapon(string item1Description, string item2Description, string weapon1Name, string weapon2Name)
    {
        StartCoroutine(CallGeminiAPI($"calculate how many dame 2 weapon can deal per sec with given attribute, and only give me the SHORTEST conclusion (no more than 15 words) is which weapon seem better to which due to what, and ansswer sound more like a human thinking like Hmmm... which is better : {weapon2Name}, {item2Description} and {weapon1Name}, {item1Description}"));
    }

    IEnumerator CallGeminiAPI(string text)
    {
        string apiURL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-lite:generateContent?key=";
        string jsonPayload = $"{{\"contents\": [{{\"parts\":[{{\"text\": \"{text}\"}}]}}]}}";
        ApiKeyData keys = LoadApiKeys();

        UnityWebRequest request = new UnityWebRequest(apiURL + keys.apiKeyGoogle, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            GeminiResponse geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(request.downloadHandler.text);
            string resultText = geminiResponse?.candidates?[0]?.content?.parts?[0]?.text;
            string cleanedText = resultText?.Replace("\n", "");
            Talking.INSTANCE.Talk(cleanedText);
        }
        
    }
    public class Part
    {
        public string text { get; set; }
    }

    public class Content
    {
        public List<Part> parts { get; set; }
        public string role { get; set; }
    }

    public class Candidate
    {
        public Content content { get; set; }
        public string finishReason { get; set; }
        public double avgLogprobs { get; set; }
    }

    public class UsageMetadata
    {
        public int promptTokenCount { get; set; }
        public int candidatesTokenCount { get; set; }
        public int totalTokenCount { get; set; }
    }

    public class GeminiResponse
    {
        public List<Candidate> candidates { get; set; }
        public UsageMetadata usageMetadata { get; set; }
        public string modelVersion { get; set; }
    }

}
