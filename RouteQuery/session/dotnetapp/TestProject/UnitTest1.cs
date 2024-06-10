using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;

public class Tests
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
    }

    [Test, Order(1)]
    public async Task ArtworkController_8080_PostArtwork()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueTitle = $"Title_{uniqueId}";

        string requestBody = $"{{\"title\": \"{uniqueTitle}\", \"artist\": \"Artist Name\", \"year\": 2023, \"medium\": \"Oil\", \"description\": \"Description of the artwork\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/artworks", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(2)]
    public async Task ArtworkController_8080_GetArtworks()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/artworks");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test, Order(3)]
    public async Task ArtworkController_8080_PostArtwork_and_GetArtworkById()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueTitle = $"Title_{uniqueId}";
        string requestBody = $"{{\"title\": \"{uniqueTitle}\", \"artist\": \"Artist Name\", \"year\": 2023, \"medium\": \"Oil\", \"description\": \"Description of the artwork\"}}";

        // Send POST request to create a new artwork
        HttpResponseMessage response = await _httpClient.PostAsync("/api/artworks", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
        
        // Check if artwork creation was successful
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        // Parse the ID from the JSON response
        JObject jsonResponse = JObject.Parse(responseString);
        int createdArtworkId = jsonResponse["artworkId"].Value<int>(); // Assuming the ID field in the response is named "artworkId"
        Console.WriteLine(createdArtworkId);

        // Send GET request to retrieve the created artwork by ID
        HttpResponseMessage prodresponse = await _httpClient.GetAsync($"/api/artworks/{createdArtworkId}");
        Console.WriteLine(await prodresponse.Content.ReadAsStringAsync());

        // Check if GET request was successful
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test, Order(4)]
    public async Task ArtworkController_8080_Post_and_GetArtworksByArtist()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueTitle = $"Title_{uniqueId}";
        string artist = "Van Gogh";

        string requestBody = $"{{\"title\": \"{uniqueTitle}\", \"artist\": \"{artist}\", \"year\": 2023, \"medium\": \"Oil\", \"description\": \"Description of the artwork\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/artworks", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        HttpResponseMessage prodresponse = await _httpClient.GetAsync($"/api/artworks/filter?artist={artist}");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test]
    public void Artwork_Properties_ArtworkId_ReturnExpectedDataTypes_int()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Artwork";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("ArtworkId");
        Assert.IsNotNull(propertyInfo, "The property 'ArtworkId' was not found on the Artwork class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(int), propertyType, "The data type of 'ArtworkId' property is not as expected (int).");
    }

    [Test]
    public void Artwork_Properties_Year_ReturnExpectedDataTypes_int()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Artwork";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Year");
        Assert.IsNotNull(propertyInfo, "The property 'Year' was not found on the Artwork class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(int), propertyType, "The data type of 'Year' property is not as expected (int).");
    }

    [Test]
    public void Artwork_Properties_Title_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Artwork";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Title");
        Assert.IsNotNull(propertyInfo, "The property 'Title' was not found on the Artwork class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Title' property is not as expected (string).");
    }

    [Test]
    public void Artwork_Properties_Description_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Artwork";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Description");
        Assert.IsNotNull(propertyInfo, "The property 'Description' was not found on the Artwork class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Description' property is not as expected (string).");
    }
}
