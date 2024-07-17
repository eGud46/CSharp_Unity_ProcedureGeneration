using UnityEngine;
using System.Collections.Generic;
using TinkerWorX.AccidentalNoiseLibrary;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{

    int Seed;

    // Adjustable variables for Unity Inspector
    [Header("Generator Values")]
    [SerializeField]
    int Width = 512;
    [SerializeField]
    int Height = 512;
    [SerializeField]
    int HeightZ32 = 32;

    [Header("Height Map")]
    [SerializeField]
    float scaleHeight78 = 1.1f;
    [SerializeField]
    float scaleHeight6 = 1.05f;
    [SerializeField]
    float scaleHeight123459 = 1f;
    [SerializeField]
    int TerrainOctaves = 6;
    [SerializeField]
    double TerrainFrequency = 1.25;
    [SerializeField]
    double TerrainLacunarity = 2;
    [SerializeField]
    float DeepWater = 0.2f;
    [SerializeField]
    float ShallowWater = 0.4f;
    [SerializeField]
    float Sand = 0.5f;
    [SerializeField]
    float Grass = 0.7f;
    [SerializeField]
    float Forest = 0.8f;
    [SerializeField]
    float Rock = 0.9f;

    [Header("Heat Map")]
    [SerializeField]
    int HeatOctaves = 4;
    [SerializeField]
    double HeatFrequency = 3.0;
    [SerializeField]
    double HeatLacunarity = 2;
    [SerializeField]
    float ColdestValue = 0.05f;
    [SerializeField]
    float ColderValue = 0.18f;
    [SerializeField]
    float ColdValue = 0.4f;
    [SerializeField]
    float WarmValue = 0.6f;
    [SerializeField]
    float WarmerValue = 0.8f;

    [Header("Moisture Map")]
    [SerializeField]
    int MoistureOctaves = 4;
    [SerializeField]
    double MoistureFrequency = 3.0;
    [SerializeField]
    double MoistureLacunarity = 2;
    [SerializeField]
    float DryerValue = 0.27f;
    [SerializeField]
    float DryValue = 0.4f;
    [SerializeField]
    float WetValue = 0.6f;
    [SerializeField]
    float WetterValue = 0.8f;
    [SerializeField]
    float WettestValue = 0.9f;

    [Header("Rivers")]
    [SerializeField]
    int RiverCount = 40;
    [SerializeField]
    float MinRiverHeight = 0.6f;
    [SerializeField]
    int MaxRiverAttempts = 1000;
    [SerializeField]
    int MinRiverTurns = 18;
    [SerializeField]
    int MinRiverLength = 20;
    [SerializeField]
    int MaxRiverIntersections = 2;


    //ObjectPlacement
    [Header("ObjectPlacement")]

    [Header("Desert")]
    [SerializeField]
    GameObject objDesert;
    [SerializeField]
    GameObject objDesert2;
    [SerializeField]
    GameObject objDesert3;
    [SerializeField]
    GameObject objDesert4;
    [SerializeField]
    GameObject objDesert5;
    [SerializeField]
    GameObject objDesert6;
    [SerializeField]
    GameObject objDesert7;
    [SerializeField]
    GameObject objDesert8;
    [Range(0f, 1f)]
    public float densityObjDesert;
    [Range(0.01f, 2f)]
    public float scaleObjDesert;
    List<Vector3> SpawnDotDesert = new List<Vector3>();

    [Header("Savanna")]
    [SerializeField]
    GameObject objSavanna;
    [SerializeField]
    GameObject objSavanna2;
    [SerializeField]
    GameObject objSavanna3;
    [SerializeField]
    GameObject objSavanna4;
    [SerializeField]
    GameObject objSavanna5;
    [SerializeField]
    GameObject objSavanna6;
    [SerializeField]
    GameObject objSavanna7;
    [SerializeField]
    GameObject objSavanna8;
    [Range(0f, 1f)]
    public float densityObjSavanna;
    [Range(0.01f, 2f)]
    public float scaleObjSavanna;
    List<Vector3> SpawnDotSavanna = new List<Vector3>();

    [Header("Tropical Rainforest")]
    [SerializeField]
    GameObject objTropical;
    [SerializeField]
    GameObject objTropical2;
    [SerializeField]
    GameObject objTropical3;
    [SerializeField]
    GameObject objTropical4;
    [SerializeField]
    GameObject objTropical5;
    [SerializeField]
    GameObject objTropical6;
    [SerializeField]
    GameObject objTropical7;
    [SerializeField]
    GameObject objTropical8;
    [Range(0f, 1f)]
    public float densityObjTropical;
    [Range(0.01f, 2f)]
    public float scaleObjTropical;
    List<Vector3> SpawnDotTropical = new List<Vector3>();

    [Header("Grassland")]
    [SerializeField]
    GameObject objGrassland;
    [SerializeField]
    GameObject objGrassland2;
    [SerializeField]
    GameObject objGrassland3;
    [SerializeField]
    GameObject objGrassland4;
    [SerializeField]
    GameObject objGrassland5;
    [SerializeField]
    GameObject objGrassland6;
    [SerializeField]
    GameObject objGrassland7;
    [SerializeField]
    GameObject objGrassland8;
    [Range(0f, 1f)]
    public float densityObjGrassland;
    [Range(0.01f, 2f)]
    public float scaleObjGrassland;
    List<Vector3> SpawnDotGrassland = new List<Vector3>();

    [Header("Woodland")]
    [SerializeField]
    GameObject objWoodland;
    [SerializeField]
    GameObject objWoodland2;
    [SerializeField]
    GameObject objWoodland3;
    [SerializeField]
    GameObject objWoodland4;
    [SerializeField]
    GameObject objWoodland5;
    [SerializeField]
    GameObject objWoodland6;
    [SerializeField]
    GameObject objWoodland7;
    [SerializeField]
    GameObject objWoodland8;
    [Range(0f, 1f)]
    public float densityObjWoodland;
    [Range(0.01f, 2f)]
    public float scaleObjWoodland;
    List<Vector3> SpawnDotWoodland = new List<Vector3>();

    [Header("Seasonal Forest")]
    [SerializeField]
    GameObject objSeasonal;
    [SerializeField]
    GameObject objSeasonal2;
    [SerializeField]
    GameObject objSeasonal3;
    [SerializeField]
    GameObject objSeasonal4;
    [SerializeField]
    GameObject objSeasonal5;
    [SerializeField]
    GameObject objSeasonal6;
    [SerializeField]
    GameObject objSeasonal7;
    [SerializeField]
    GameObject objSeasonal8;
    [Range(0f, 1f)]
    public float densityObjSeasonal;
    [Range(0.01f, 2f)]
    public float scaleObjSeasonal;
    List<Vector3> SpawnDotSeasonal = new List<Vector3>();

    [Header("Temperate Rainforest")]
    [SerializeField]
    GameObject objTemperate;
    [SerializeField]
    GameObject objTemperate2;
    [SerializeField]
    GameObject objTemperate3;
    [SerializeField]
    GameObject objTemperate4;
    [SerializeField]
    GameObject objTemperate5;
    [SerializeField]
    GameObject objTemperate6;
    [SerializeField]
    GameObject objTemperate7;
    [SerializeField]
    GameObject objTemperate8;
    [Range(0f, 1f)]
    public float densityObjTemperate;
    [Range(0.01f, 2f)]
    public float scaleObjTemperate;
    List<Vector3> SpawnDotTemperate = new List<Vector3>();

    [Header("Boreal Forest")]
    [SerializeField]
    GameObject objBoreal;
    [SerializeField]
    GameObject objBoreal2;
    [SerializeField]
    GameObject objBoreal3;
    [SerializeField]
    GameObject objBoreal4;
    [SerializeField]
    GameObject objBoreal5;
    [SerializeField]
    GameObject objBoreal6;
    [SerializeField]
    GameObject objBoreal7;
    [SerializeField]
    GameObject objBoreal8;
    [Range(0f, 1f)]
    public float densityObjBoreal;
    [Range(0.01f, 2f)]
    public float scaleObjBoreal;
    List<Vector3> SpawnDotBoreal = new List<Vector3>();

    [Header("Tundra")]
    [SerializeField]
    GameObject objTundra;
    [SerializeField]
    GameObject objTundra2;
    [SerializeField]
    GameObject objTundra3;
    [SerializeField]
    GameObject objTundra4;
    [SerializeField]
    GameObject objTundra5;
    [SerializeField]
    GameObject objTundra6;
    [SerializeField]
    GameObject objTundra7;
    [SerializeField]
    GameObject objTundra8;
    [Range(0f, 1f)]
    public float densityObjTundra;
    [Range(0.01f, 2f)]
    public float scaleObjTundra;
    List<Vector3> SpawnDotTundra = new List<Vector3>();

    [Header("Ice")]
    [SerializeField]
    GameObject objIce;
    [SerializeField]
    GameObject objIce2;
    [SerializeField]
    GameObject objIce3;
    [SerializeField]
    GameObject objIce4;
    [SerializeField]
    GameObject objIce5;
    [SerializeField]
    GameObject objIce6;
    [SerializeField]
    GameObject objIce7;
    [SerializeField]
    GameObject objIce8;
    [Range(0f, 1f)]
    public float densityObjIce;
    [Range(0.01f, 2f)]
    public float scaleObjIce;
    List<Vector3> SpawnDotIce = new List<Vector3>();

    // private variables
    ImplicitFractal HeightMap;
    ImplicitCombiner HeatMap;
    ImplicitFractal MoistureMap;
    

    MapData HeightData;
    MapData HeatData;
    MapData MoistureData;

    Tile[,] Tiles;

    List<TileGroup> Waters = new List<TileGroup>();
    List<TileGroup> Lands = new List<TileGroup>();

    List<River> Rivers = new List<River>();
    List<RiverGroup> RiverGroups = new List<RiverGroup>();

    // Our texture output gameobject
    MeshRenderer HeightMapRenderer;
    MeshRenderer HeatMapRenderer;
    MeshRenderer MoistureMapRenderer;
    MeshRenderer BiomeMapRenderer;


    BiomeType[,] BiomeTable = new BiomeType[6, 6] {   
        //COLDEST        //COLDER          //COLD                  //HOT                          //HOTTER                       //HOTTEST
        { BiomeType.Ice, BiomeType.Tundra, BiomeType.Grassland,    BiomeType.Desert,              BiomeType.Desert,              BiomeType.Desert },              //DRYEST
        { BiomeType.Ice, BiomeType.Tundra, BiomeType.Grassland,    BiomeType.Desert,              BiomeType.Desert,              BiomeType.Desert },              //DRYER
        { BiomeType.Ice, BiomeType.Tundra, BiomeType.Woodland,     BiomeType.Woodland,            BiomeType.Savanna,             BiomeType.Savanna },             //DRY
        { BiomeType.Ice, BiomeType.Tundra, BiomeType.BorealForest, BiomeType.Woodland,            BiomeType.Savanna,             BiomeType.Savanna },             //WET
        { BiomeType.Ice, BiomeType.Tundra, BiomeType.BorealForest, BiomeType.SeasonalForest,      BiomeType.TropicalRainforest,  BiomeType.TropicalRainforest },  //WETTER
        { BiomeType.Ice, BiomeType.Tundra, BiomeType.BorealForest, BiomeType.TemperateRainforest, BiomeType.TropicalRainforest,  BiomeType.TropicalRainforest }   //WETTEST
    };

    void Start()
    {
        Seed = UnityEngine.Random.Range(0, int.MaxValue);

        HeightMapRenderer = transform.Find("HeightTexture").GetComponent<MeshRenderer>();
        HeatMapRenderer = transform.Find("HeatTexture").GetComponent<MeshRenderer>();
        MoistureMapRenderer = transform.Find("MoistureTexture").GetComponent<MeshRenderer>();
        BiomeMapRenderer = transform.Find("BiomeTexture").GetComponent<MeshRenderer>();

        Initialize();
        GetData();
        LoadTiles();

        UpdateNeighbors();

        GenerateRivers();
        BuildRiverGroups();
        DigRiverGroups();
        AdjustMoistureMap();

        UpdateBitmasks();
        FloodFill();

        GenerateBiomeMap();
        UpdateBiomeBitmask();
        

        Texture2D HeightTexture = TextureGenerator.GetHeightMapTexture(Width, Height, Tiles);
        Texture2D HeatTexture = TextureGenerator.GetHeatMapTexture(Width, Height, Tiles);
        Texture2D MoistureTexture = TextureGenerator.GetMoistureMapTexture(Width, Height, Tiles);
        Texture2D BiomeTexture = TextureGenerator.GetBiomeMapTexture(Width, Height, Tiles, ColdestValue, ColderValue, ColdValue);

        HeightMapRenderer.materials[0].mainTexture = HeightTexture;
        HeatMapRenderer.materials[0].mainTexture = HeatTexture;
        MoistureMapRenderer.materials[0].mainTexture = MoistureTexture;
        BiomeMapRenderer.materials[0].mainTexture = BiomeTexture;

        SaveTextureToPNG(HeightTexture, "HeightTexture");
        SaveTextureToPNG(HeatTexture, "HeatTexture");
        SaveTextureToPNG(MoistureTexture, "MoistureTexture");
        SaveTextureToPNG(BiomeTexture, "BiomeTexture");

        //GetImageOnMapToM
        GetMapToM(BiomeTexture);

        //Terrain
        GenerateTerrain(Width, Height, HeightZ32, Tiles);

        //PlacementObjects
        PlaceObjects();
    }

    void Update()
    {

        // Refresh with inspector data 
        //!!!Нужно добавить удаление объектов перед их новым расположением!!!
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ClearScene();

            Seed = UnityEngine.Random.Range(0, int.MaxValue);

            HeightMapRenderer = transform.Find("HeightTexture").GetComponent<MeshRenderer>();
            HeatMapRenderer = transform.Find("HeatTexture").GetComponent<MeshRenderer>();
            MoistureMapRenderer = transform.Find("MoistureTexture").GetComponent<MeshRenderer>();
            BiomeMapRenderer = transform.Find("BiomeTexture").GetComponent<MeshRenderer>();

            Initialize();
            GetData();
            LoadTiles();

            UpdateNeighbors();

            GenerateRivers();
            BuildRiverGroups();
            DigRiverGroups();
            AdjustMoistureMap();

            UpdateBitmasks();
            FloodFill();

            GenerateBiomeMap();
            UpdateBiomeBitmask();


            Texture2D HeightTexture = TextureGenerator.GetHeightMapTexture(Width, Height, Tiles);
            Texture2D HeatTexture = TextureGenerator.GetHeatMapTexture(Width, Height, Tiles);
            Texture2D MoistureTexture = TextureGenerator.GetMoistureMapTexture(Width, Height, Tiles);
            Texture2D BiomeTexture = TextureGenerator.GetBiomeMapTexture(Width, Height, Tiles, ColdestValue, ColderValue, ColdValue);

            HeightMapRenderer.materials[0].mainTexture = HeightTexture;
            HeatMapRenderer.materials[0].mainTexture = HeatTexture;
            MoistureMapRenderer.materials[0].mainTexture = MoistureTexture;
            BiomeMapRenderer.materials[0].mainTexture = BiomeTexture;

            SaveTextureToPNG(HeightTexture, "HeightTexture");
            SaveTextureToPNG(HeatTexture, "HeatTexture");
            SaveTextureToPNG(MoistureTexture, "MoistureTexture");
            SaveTextureToPNG(BiomeTexture, "BiomeTexture");

            //GetImageOnMapToM
            GetMapToM(BiomeTexture);

            //Terrain
            GenerateTerrain(Width, Height, HeightZ32, Tiles);

            //PlacementObjects
            PlaceObjects();
            /*
            Initialize();
            GetData();
            LoadTiles();
            UpdateNeighbors();
            UpdateBitmasks();
            FloodFill();
            UpdateBiomeBitmask();

            Texture2D HeightTexture = TextureGenerator.GetHeightMapTexture(Width, Height, Tiles);
            Texture2D HeatTexture = TextureGenerator.GetHeatMapTexture(Width, Height, Tiles);
            Texture2D MoistureTexture = TextureGenerator.GetMoistureMapTexture(Width, Height, Tiles);

            HeightMapRenderer.materials[0].mainTexture = HeightTexture;
            HeatMapRenderer.materials[0].mainTexture = HeatTexture;
            MoistureMapRenderer.materials[0].mainTexture = MoistureTexture;

            SaveTextureToPNG(HeightTexture, "HeightTexture");
            SaveTextureToPNG(HeatTexture, "HeatTexture");
            SaveTextureToPNG(MoistureTexture, "MoistureTexture");*/
        }
    }

    //ClearScene
    private void ClearScene()
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        string[] dangerObjects = {"Generator", "Main Camera", "Directional Light", "Global Volume", "WaterBlock_50m", "EventSystem",
        "Canvas", "Image", "HeightTexture", "HeatTexture", "MoistureTexture", "BiomeTexture", "Terrain", "Water", "Marker"};

        SpawnDotDesert.Clear();
        SpawnDotSavanna.Clear();
        SpawnDotTropical.Clear();
        SpawnDotGrassland.Clear();
        SpawnDotWoodland.Clear();
        SpawnDotSeasonal.Clear();
        SpawnDotTemperate.Clear();
        SpawnDotBoreal.Clear();
        SpawnDotTundra.Clear();
        SpawnDotIce.Clear();

        foreach (GameObject obj in allGameObjects)
        {
            if (!dangerObjects.Contains(obj.name))
                Destroy(obj);
        }
    }

    //GetImageOnMapToM
    private void GetMapToM(Texture2D mapTexture)
    {
        Image img = transform.Find("Canvas").Find("Image").gameObject.GetComponent<Image>();
        img.sprite = Sprite.Create(mapTexture, new Rect(0.0f, 0.0f, mapTexture.width, mapTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

    }

    //ObjectPlacement
    private void PlaceObjects()
    {
        
        int numObjDesert = (int)(densityObjDesert * SpawnDotDesert.Count);
        int numObjSavanna = (int)(densityObjSavanna * SpawnDotSavanna.Count);
        int numObjTropical = (int)(densityObjTropical * SpawnDotTropical.Count);
        int numObjGrassland = (int)(densityObjGrassland * SpawnDotGrassland.Count);
        int numObjWoodland = (int)(densityObjWoodland * SpawnDotWoodland.Count);
        int numObjSeasonal = (int)(densityObjSeasonal * SpawnDotSeasonal.Count);
        int numObjTemperate = (int)(densityObjTemperate * SpawnDotTemperate.Count);
        int numObjBoreal = (int)(densityObjBoreal * SpawnDotBoreal.Count);
        int numObjTundra = (int)(densityObjTundra * SpawnDotTundra.Count);
        int numObjIce = (int)(densityObjIce * SpawnDotIce.Count);



        for (int i = 0; i < numObjDesert; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objDesert != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
                case 1:
                    if (objDesert2 != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert2, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
                case 2:
                    if (objDesert3 != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert3, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
                case 3:
                    if (objDesert4 != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert4, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
                case 4:
                    if (objDesert5 != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert5, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
                case 5:
                    if (objDesert6 != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert6, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
                case 6:
                    if (objDesert7 != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert7, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
                case 7:
                    if (objDesert8 != null && densityObjDesert != 0)
                    {
                        Vector3 tmpDesertDot = SpawnDotDesert[UnityEngine.Random.Range(0, SpawnDotDesert.Count)];
                        Instantiate(objDesert8, tmpDesertDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjDesert, scaleObjDesert, scaleObjDesert);
                        SpawnDotDesert.Remove(tmpDesertDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjSavanna; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objSavanna != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
                case 1:
                    if (objSavanna2 != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna2, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
                case 2:
                    if (objSavanna3 != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna3, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
                case 3:
                    if (objSavanna4 != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna4, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
                case 4:
                    if (objSavanna5 != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna5, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
                case 5:
                    if (objSavanna6 != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna6, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
                case 6:
                    if (objSavanna7 != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna7, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
                case 7:
                    if (objSavanna8 != null && densityObjSavanna != 0)
                    {
                        Vector3 tmpSavannaDot = SpawnDotSavanna[UnityEngine.Random.Range(0, SpawnDotSavanna.Count)];
                        Instantiate(objSavanna8, tmpSavannaDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSavanna, scaleObjSavanna, scaleObjSavanna);
                        SpawnDotSavanna.Remove(tmpSavannaDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjTropical; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objTropical != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
                case 1:
                    if (objTropical2 != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical2, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
                case 2:
                    if (objTropical3 != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical3, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
                case 3:
                    if (objTropical4 != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical4, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
                case 4:
                    if (objTropical5 != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical5, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
                case 5:
                    if (objTropical6 != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical6, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
                case 6:
                    if (objTropical7 != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical7, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
                case 7:
                    if (objTropical8 != null && densityObjTropical != 0)
                    {
                        Vector3 tmpTropicalDot = SpawnDotTropical[UnityEngine.Random.Range(0, SpawnDotTropical.Count)];
                        Instantiate(objTropical8, tmpTropicalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTropical, scaleObjTropical, scaleObjTropical);
                        SpawnDotTropical.Remove(tmpTropicalDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjGrassland; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objGrassland != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
                case 1:
                    if (objGrassland2 != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland2, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
                case 2:
                    if (objGrassland3 != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland3, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
                case 3:
                    if (objGrassland4 != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland4, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
                case 4:
                    if (objGrassland5 != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland5, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
                case 5:
                    if (objGrassland6 != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland6, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
                case 6:
                    if (objGrassland7 != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland7, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
                case 7:
                    if (objGrassland8 != null && densityObjGrassland != 0)
                    {
                        Vector3 tmpGrasslandDot = SpawnDotGrassland[UnityEngine.Random.Range(0, SpawnDotGrassland.Count)];
                        Instantiate(objGrassland8, tmpGrasslandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjGrassland, scaleObjGrassland, scaleObjGrassland);
                        SpawnDotGrassland.Remove(tmpGrasslandDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjWoodland; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objWoodland != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
                case 1:
                    if (objWoodland2 != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland2, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
                case 2:
                    if (objWoodland3 != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland3, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
                case 3:
                    if (objWoodland4 != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland4, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
                case 4:
                    if (objWoodland5 != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland5, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
                case 5:
                    if (objWoodland6 != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland6, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
                case 6:
                    if (objWoodland7 != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland7, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
                case 7:
                    if (objWoodland8 != null && densityObjWoodland != 0)
                    {
                        Vector3 tmpWoodlandDot = SpawnDotWoodland[UnityEngine.Random.Range(0, SpawnDotWoodland.Count)];
                        Instantiate(objWoodland8, tmpWoodlandDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjWoodland, scaleObjWoodland, scaleObjWoodland);
                        SpawnDotWoodland.Remove(tmpWoodlandDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjSeasonal; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objSeasonal != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
                case 1:
                    if (objSeasonal2 != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal2, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
                case 2:
                    if (objSeasonal3 != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal3, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
                case 3:
                    if (objSeasonal4 != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal4, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
                case 4:
                    if (objSeasonal5 != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal5, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
                case 5:
                    if (objSeasonal6 != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal6, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
                case 6:
                    if (objSeasonal7 != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal7, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
                case 7:
                    if (objSeasonal8 != null && densityObjSeasonal != 0)
                    {
                        Vector3 tmpSeasonalDot = SpawnDotSeasonal[UnityEngine.Random.Range(0, SpawnDotSeasonal.Count)];
                        Instantiate(objSeasonal8, tmpSeasonalDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjSeasonal, scaleObjSeasonal, scaleObjSeasonal);
                        SpawnDotSeasonal.Remove(tmpSeasonalDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjTemperate; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objTemperate != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
                case 1:
                    if (objTemperate2 != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate2, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
                case 2:
                    if (objTemperate3 != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate3, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
                case 3:
                    if (objTemperate4 != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate4, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
                case 4:
                    if (objTemperate5 != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate5, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
                case 5:
                    if (objTemperate6 != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate6, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
                case 6:
                    if (objTemperate7 != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate7, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
                case 7:
                    if (objTemperate8 != null && densityObjTemperate != 0)
                    {
                        Vector3 tmpTemperateDot = SpawnDotTemperate[UnityEngine.Random.Range(0, SpawnDotTemperate.Count)];
                        Instantiate(objTemperate8, tmpTemperateDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTemperate, scaleObjTemperate, scaleObjTemperate);
                        SpawnDotTemperate.Remove(tmpTemperateDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjBoreal; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objBoreal != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
                case 1:
                    if (objBoreal2 != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal2, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
                case 2:
                    if (objBoreal3 != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal3, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
                case 3:
                    if (objBoreal4 != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal4, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
                case 4:
                    if (objBoreal5 != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal5, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
                case 5:
                    if (objBoreal6 != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal6, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
                case 6:
                    if (objBoreal7 != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal7, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
                case 7:
                    if (objBoreal8 != null && densityObjBoreal != 0)
                    {
                        Vector3 tmpBorealDot = SpawnDotBoreal[UnityEngine.Random.Range(0, SpawnDotBoreal.Count)];
                        Instantiate(objBoreal8, tmpBorealDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjBoreal, scaleObjBoreal, scaleObjBoreal);
                        SpawnDotBoreal.Remove(tmpBorealDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjTundra; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objTundra != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
                case 1:
                    if (objTundra2 != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra2, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
                case 2:
                    if (objTundra3 != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra3, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
                case 3:
                    if (objTundra4 != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra4, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
                case 4:
                    if (objTundra5 != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra5, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
                case 5:
                    if (objTundra6 != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra6, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
                case 6:
                    if (objTundra7 != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra7, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
                case 7:
                    if (objTundra8 != null && densityObjTundra != 0)
                    {
                        Vector3 tmpTundraDot = SpawnDotTundra[UnityEngine.Random.Range(0, SpawnDotTundra.Count)];
                        Instantiate(objTundra8, tmpTundraDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjTundra, scaleObjTundra, scaleObjTundra);
                        SpawnDotTundra.Remove(tmpTundraDot);
                    }
                    break;
            }
        }

        for (int i = 0; i < numObjIce; i++)
        {
            int RandomNum = UnityEngine.Random.Range(0, 8);

            switch (RandomNum)
            {
                case 0:
                    if (objIce != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
                case 1:
                    if (objIce2 != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce2, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
                case 2:
                    if (objIce3 != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce3, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
                case 3:
                    if (objIce4 != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce4, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
                case 4:
                    if (objIce5 != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce5, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
                case 5:
                    if (objIce6 != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce6, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
                case 6:
                    if (objIce7 != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce7, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
                case 7:
                    if (objIce8 != null && densityObjIce != 0)
                    {
                        Vector3 tmpIceDot = SpawnDotIce[UnityEngine.Random.Range(0, SpawnDotIce.Count)];
                        Instantiate(objIce8, tmpIceDot, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)).transform.localScale = new Vector3(scaleObjIce, scaleObjIce, scaleObjIce);
                        SpawnDotIce.Remove(tmpIceDot);
                    }
                    break;
            }
        }
    }

    //Terrain
    private void GenerateTerrain(int width, int height, int heightZ32,Tile[,] tiles)
    {
        Transform water = transform.Find("Water").GetComponent<Transform>();
        water.position =new Vector3(water.position.x, heightZ32 / 32 * 12, water.position.z);

        Terrain terrain = transform.Find("Terrain").GetComponent<Terrain>();
        float[,] heights = new float[width, height];
        //var pixels = new Color[width * height];
        terrain.terrainData.size = new Vector3(width, heightZ32, height); // Устанавливаем размер нашей карты
        terrain.terrainData.heightmapResolution = height + 1; // Задаём разрешение (кол-во высот)
        terrain.terrainData.alphamapResolution = height;
        terrain.terrainData.baseMapResolution = 2 * height;
        float[,,] alphaData = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                //Mountain x2
                //!!!if change scale height - change and in ObjectPLacment in down(GenerateBiomeMap)!!!
                //!!!else spawn obj not goode)))
                //
                if (tiles[x, y].HeightType == (HeightType)7 || tiles[x, y].HeightType == (HeightType)8)
                {
                        heights[x, y] = tiles[x, y].HeightValue * scaleHeight78;
                }
                else if(tiles[x, y].HeightType == (HeightType)6)
                {
                        heights[x, y] = tiles[x, y].HeightValue * scaleHeight6;
                }
                else
                {
                    heights[x, y] = tiles[x, y].HeightValue * scaleHeight123459;
                }
                

                switch (tiles[x, y].BiomeType)
                {
                    case BiomeType n when (n == (BiomeType)0):
                        alphaData[x, y, 0] = 1; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)1):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 1; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)2):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 1; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)3):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 1; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)4):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 1; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)5):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 1; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)6):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 1; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)7):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 1; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)8):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 1; //Tundra
                        alphaData[x, y, 9] = 0; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                    case BiomeType n when (n == (BiomeType)9):
                        alphaData[x, y, 0] = 0; //Desert
                        alphaData[x, y, 1] = 0; //Savanna
                        alphaData[x, y, 2] = 0; //TropicalRainforest
                        alphaData[x, y, 3] = 0; //Grassland
                        alphaData[x, y, 4] = 0; //Woodland
                        alphaData[x, y, 5] = 0; //SeasonalForest
                        alphaData[x, y, 6] = 0; //TemperateRainforest
                        alphaData[x, y, 7] = 0; //BorealForest
                        alphaData[x, y, 8] = 0; //Tundra
                        alphaData[x, y, 9] = 1; //Ice
                        alphaData[x, y, 10] = 0; //Seabed
                        alphaData[x, y, 11] = 0; //River
                        break;
                }

                if (tiles[x, y].HeightType < (HeightType)4)
                {
                    alphaData[x, y, 0] = 0; //Desert
                    alphaData[x, y, 1] = 0; //Savanna
                    alphaData[x, y, 2] = 0; //TropicalRainforest
                    alphaData[x, y, 3] = 0; //Grassland
                    alphaData[x, y, 4] = 0; //Woodland
                    alphaData[x, y, 5] = 0; //SeasonalForest
                    alphaData[x, y, 6] = 0; //TemperateRainforest
                    alphaData[x, y, 7] = 0; //BorealForest
                    alphaData[x, y, 8] = 0; //Tundra
                    alphaData[x, y, 9] = 0; //Ice
                    alphaData[x, y, 10] = 1; //Seabed
                    alphaData[x, y, 11] = 0; //River
                }

                if (tiles[x, y].HeightType == HeightType.River)
                {
                    alphaData[x, y, 0] = 0; //Desert
                    alphaData[x, y, 1] = 0; //Savanna
                    alphaData[x, y, 2] = 0; //TropicalRainforest
                    alphaData[x, y, 3] = 0; //Grassland
                    alphaData[x, y, 4] = 0; //Woodland
                    alphaData[x, y, 5] = 0; //SeasonalForest
                    alphaData[x, y, 6] = 0; //TemperateRainforest
                    alphaData[x, y, 7] = 0; //BorealForest
                    alphaData[x, y, 8] = 0; //Tundra
                    alphaData[x, y, 9] = 0; //Ice
                    alphaData[x, y, 10] = 0; //Seabed
                    alphaData[x, y, 11] = 1; //River
                }
              

            }
        }

        terrain.terrainData.SetHeights(0, 0, heights); // И, наконец, применяем нашу карту высот (heights)
        terrain.terrainData.SetAlphamaps(0, 0, alphaData);
    }
    
    private void SaveTextureToPNG(Texture2D texture, string name)
    {
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = "C:\\Users\\Eugene\\Downloads\\ProcGen3D_URP\\Assets\\";
        File.WriteAllBytes(dirPath + name + ".png", bytes);
    }
    private void Initialize()
    {
        // Initialize the HeightMap Generator
        HeightMap = new ImplicitFractal(FractalType.Multi,
                                       BasisType.Simplex,
                                       InterpolationType.Quintic);
        HeightMap.Octaves = TerrainOctaves;
        HeightMap.Frequency = TerrainFrequency;
        HeightMap.Lacunarity = TerrainLacunarity;
        HeightMap.Seed = Seed;


        // Initialize the Heat map
        ImplicitGradient gradient = new ImplicitGradient(1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        ImplicitFractal heatFractal = new ImplicitFractal(FractalType.Multi,
                                                          BasisType.Simplex,
                                                          InterpolationType.Quintic);
        heatFractal.Octaves = HeatOctaves;
        heatFractal.Frequency = HeatFrequency;
        heatFractal.Lacunarity = HeatLacunarity;
        heatFractal.Seed = Seed;

        HeatMap = new ImplicitCombiner(CombinerType.Multiply);
        HeatMap.AddSource(gradient);
        HeatMap.AddSource(heatFractal);

        //moisture map
        MoistureMap = new ImplicitFractal(FractalType.Multi,
                                           BasisType.Simplex,
                                           InterpolationType.Quintic);
        MoistureMap.Octaves = MoistureOctaves;
        MoistureMap.Frequency = MoistureFrequency;
        MoistureMap.Lacunarity = MoistureLacunarity;
        MoistureMap.Seed = Seed;
    }

    public BiomeType GetBiomeType(Tile tile)
    {
        return BiomeTable[(int)tile.MoistureType, (int)tile.HeatType];
    }

    private void GenerateBiomeMap()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {

                if (!Tiles[x, y].Collidable) continue;

                Tile t = Tiles[x, y];
                t.BiomeType = GetBiomeType(t);

                
                //ObjectPlacement
                //
                Vector3 tmpDesertDot = new Vector3();
                Vector3 tmpSavannaDot = new Vector3();
                Vector3 tmpTropicalDot = new Vector3();
                Vector3 tmpGrasslandDot = new Vector3();
                Vector3 tmpWoodlandDot = new Vector3();
                Vector3 tmpSeasonalDot = new Vector3();
                Vector3 tmpTemperateDot = new Vector3();
                Vector3 tmpBorealDot = new Vector3();
                Vector3 tmpTundraDot = new Vector3();
                Vector3 tmpIceDot = new Vector3();

                //!!!scale height!!!
                //float scaleHeight78 = 1.1f;
                //float scaleHeight6 = 1.05f;
                //float scaleHeight123459 = 1f;

                switch (t.BiomeType)
                {
                    case BiomeType n when (n == (BiomeType)0):
                        tmpDesertDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpDesertDot.y = HeightZ32;
                            else
                                tmpDesertDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpDesertDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpDesertDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpDesertDot.x = t.Y;
                        SpawnDotDesert.Add(tmpDesertDot);
                        break;
                    case BiomeType n when (n == (BiomeType)1):
                        tmpSavannaDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpSavannaDot.y = HeightZ32;
                            else
                                tmpSavannaDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpSavannaDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpSavannaDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpSavannaDot.x = t.Y;
                        SpawnDotSavanna.Add(tmpSavannaDot);
                        break;
                    case BiomeType n when (n == (BiomeType)2):
                        tmpTropicalDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpTropicalDot.y = HeightZ32;
                            else
                                tmpTropicalDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpTropicalDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpTropicalDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpTropicalDot.x = t.Y;
                        SpawnDotTropical.Add(tmpTropicalDot);
                        break;
                    case BiomeType n when (n == (BiomeType)3):
                        tmpGrasslandDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpGrasslandDot.y = HeightZ32;
                            else
                                tmpGrasslandDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpGrasslandDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpGrasslandDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpGrasslandDot.x = t.Y;
                        SpawnDotGrassland.Add(tmpGrasslandDot);
                        break;
                    case BiomeType n when (n == (BiomeType)4):
                        tmpWoodlandDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpWoodlandDot.y = HeightZ32;
                            else
                                tmpWoodlandDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpWoodlandDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpWoodlandDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpWoodlandDot.x = t.Y;
                        SpawnDotWoodland.Add(tmpWoodlandDot);
                        break;
                    case BiomeType n when (n == (BiomeType)5):
                        tmpSeasonalDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpSeasonalDot.y = HeightZ32;
                            else
                                tmpSeasonalDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpSeasonalDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpSeasonalDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpSeasonalDot.x = t.Y;
                        SpawnDotSeasonal.Add(tmpSeasonalDot);
                        break;
                    case BiomeType n when (n == (BiomeType)6):
                        tmpTemperateDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpTemperateDot.y = HeightZ32;
                            else
                                tmpTemperateDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpTemperateDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpTemperateDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpTemperateDot.x = t.Y;
                        SpawnDotTemperate.Add(tmpTemperateDot);
                        break;
                    case BiomeType n when (n == (BiomeType)7):
                        tmpBorealDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpBorealDot.y = HeightZ32;
                            else
                                tmpBorealDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpBorealDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpBorealDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpBorealDot.x = t.Y;
                        SpawnDotBoreal.Add(tmpBorealDot);
                        break;
                    case BiomeType n when (n == (BiomeType)8):
                        tmpTundraDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpTundraDot.y = HeightZ32;
                            else
                                tmpTundraDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpTundraDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpTundraDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpTundraDot.x = t.Y;
                        SpawnDotTundra.Add(tmpTundraDot);
                        break;
                    case BiomeType n when (n == (BiomeType)9):
                        tmpIceDot.z = t.X;
                        if (t.HeightType == (HeightType)7 || t.HeightType == (HeightType)8)
                            if (t.HeightValue * HeightZ32 * scaleHeight78 > HeightZ32)
                                tmpIceDot.y = HeightZ32;
                            else
                                tmpIceDot.y = t.HeightValue * HeightZ32 * scaleHeight78;
                        else if (t.HeightType == (HeightType)6)
                            tmpIceDot.y = t.HeightValue * HeightZ32 * scaleHeight6;
                        else
                            tmpIceDot.y = t.HeightValue * HeightZ32 * scaleHeight123459;
                        tmpIceDot.x = t.Y;
                        SpawnDotIce.Add(tmpIceDot);
                        break;
                }   
            }
        }
    }

    private void AddMoisture(Tile t, int radius)
    {
        int startx = MathHelper.Mod(t.X - radius, Width);
        int endx = MathHelper.Mod(t.X + radius, Width);
        Vector2 center = new Vector2(t.X, t.Y);
        int curr = radius;

        while (curr > 0)
        {

            int x1 = MathHelper.Mod(t.X - curr, Width);
            int x2 = MathHelper.Mod(t.X + curr, Width);
            int y = t.Y;

            AddMoisture(Tiles[x1, y], 0.025f / (center - new Vector2(x1, y)).magnitude);

            for (int i = 0; i < curr; i++)
            {
                AddMoisture(Tiles[x1, MathHelper.Mod(y + i + 1, Height)], 0.025f / (center - new Vector2(x1, MathHelper.Mod(y + i + 1, Height))).magnitude);
                AddMoisture(Tiles[x1, MathHelper.Mod(y - (i + 1), Height)], 0.025f / (center - new Vector2(x1, MathHelper.Mod(y - (i + 1), Height))).magnitude);

                AddMoisture(Tiles[x2, MathHelper.Mod(y + i + 1, Height)], 0.025f / (center - new Vector2(x2, MathHelper.Mod(y + i + 1, Height))).magnitude);
                AddMoisture(Tiles[x2, MathHelper.Mod(y - (i + 1), Height)], 0.025f / (center - new Vector2(x2, MathHelper.Mod(y - (i + 1), Height))).magnitude);
            }
            curr--;
        }
    }

    private void AddMoisture(Tile t, float amount)
    {
        MoistureData.Data[t.X, t.Y] += amount;
        t.MoistureValue += amount;
        if (t.MoistureValue > 1)
            t.MoistureValue = 1;

        //set moisture type
        if (t.MoistureValue < DryerValue) t.MoistureType = MoistureType.Dryest;
        else if (t.MoistureValue < DryValue) t.MoistureType = MoistureType.Dryer;
        else if (t.MoistureValue < WetValue) t.MoistureType = MoistureType.Dry;
        else if (t.MoistureValue < WetterValue) t.MoistureType = MoistureType.Wet;
        else if (t.MoistureValue < WettestValue) t.MoistureType = MoistureType.Wetter;
        else t.MoistureType = MoistureType.Wettest;
    }

    private void AdjustMoistureMap()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {

                Tile t = Tiles[x, y];
                if (t.HeightType == HeightType.River)
                {
                    AddMoisture(t, (int)60);
                }
            }
        }
    }

    private void DigRiverGroups()
    {
        for (int i = 0; i < RiverGroups.Count; i++)
        {

            RiverGroup group = RiverGroups[i];
            River longest = null;

            //Find longest river in this group
            for (int j = 0; j < group.Rivers.Count; j++)
            {
                River river = group.Rivers[j];
                if (longest == null)
                    longest = river;
                else if (longest.Tiles.Count < river.Tiles.Count)
                    longest = river;
            }

            if (longest != null)
            {
                //Dig out longest path first
                DigRiver(longest);

                for (int j = 0; j < group.Rivers.Count; j++)
                {
                    River river = group.Rivers[j];
                    if (river != longest)
                    {
                        DigRiver(river, longest);
                    }
                }
            }
        }
    }

    private void BuildRiverGroups()
    {
        //loop each tile, checking if it belongs to multiple rivers
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Tile t = Tiles[x, y];

                if (t.Rivers.Count > 1)
                {
                    // multiple rivers == intersection
                    RiverGroup group = null;

                    // Does a rivergroup already exist for this group?
                    for (int n = 0; n < t.Rivers.Count; n++)
                    {
                        River tileriver = t.Rivers[n];
                        for (int i = 0; i < RiverGroups.Count; i++)
                        {
                            for (int j = 0; j < RiverGroups[i].Rivers.Count; j++)
                            {
                                River river = RiverGroups[i].Rivers[j];
                                if (river.ID == tileriver.ID)
                                {
                                    group = RiverGroups[i];
                                }
                                if (group != null) break;
                            }
                            if (group != null) break;
                        }
                        if (group != null) break;
                    }

                    // existing group found -- add to it
                    if (group != null)
                    {
                        for (int n = 0; n < t.Rivers.Count; n++)
                        {
                            if (!group.Rivers.Contains(t.Rivers[n]))
                                group.Rivers.Add(t.Rivers[n]);
                        }
                    }
                    else   //No existing group found - create a new one
                    {
                        group = new RiverGroup();
                        for (int n = 0; n < t.Rivers.Count; n++)
                        {
                            group.Rivers.Add(t.Rivers[n]);
                        }
                        RiverGroups.Add(group);
                    }
                }
            }
        }
    }

    private void GenerateRivers()
    {
        int attempts = 0;
        int rivercount = RiverCount;
        Rivers = new List<River>();

        // Generate some rivers
        while (rivercount > 0 && attempts < MaxRiverAttempts)
        {

            // Get a random tile
            int x = UnityEngine.Random.Range(0, Width);
            int y = UnityEngine.Random.Range(0, Height);
            Tile tile = Tiles[x, y];

            // validate the tile
            if (!tile.Collidable) continue;
            if (tile.Rivers.Count > 0) continue;

            if (tile.HeightValue > MinRiverHeight)
            {
                // Tile is good to start river from
                River river = new River(rivercount);

                // Figure out the direction this river will try to flow
                river.CurrentDirection = tile.GetLowestNeighbor();

                // Recursively find a path to water
                FindPathToWater(tile, river.CurrentDirection, ref river);

                // Validate the generated river 
                if (river.TurnCount < MinRiverTurns || river.Tiles.Count < MinRiverLength || river.Intersections > MaxRiverIntersections)
                {
                    //Validation failed - remove this river
                    for (int i = 0; i < river.Tiles.Count; i++)
                    {
                        Tile t = river.Tiles[i];
                        t.Rivers.Remove(river);
                    }
                }
                else if (river.Tiles.Count >= MinRiverLength)
                {
                    //Validation passed - Add river to list
                    Rivers.Add(river);
                    tile.Rivers.Add(river);
                    rivercount--;
                }
            }
            attempts++;
        }
    }

    // Dig river based on a parent river vein
    private void DigRiver(River river, River parent)
    {
        int intersectionID = 0;
        int intersectionSize = 0;

        // determine point of intersection
        for (int i = 0; i < river.Tiles.Count; i++)
        {
            Tile t1 = river.Tiles[i];
            for (int j = 0; j < parent.Tiles.Count; j++)
            {
                Tile t2 = parent.Tiles[j];
                if (t1 == t2)
                {
                    intersectionID = i;
                    intersectionSize = t2.RiverSize;
                }
            }
        }

        int counter = 0;
        int intersectionCount = river.Tiles.Count - intersectionID;
        int size = UnityEngine.Random.Range(intersectionSize, 5);
        river.Length = river.Tiles.Count;

        // randomize size change
        int two = river.Length / 2;
        int three = two / 2;
        int four = three / 2;
        int five = four / 2;

        int twomin = two / 3;
        int threemin = three / 3;
        int fourmin = four / 3;
        int fivemin = five / 3;

        // randomize length of each size
        int count1 = UnityEngine.Random.Range(fivemin, five);
        if (size < 4)
        {
            count1 = 0;
        }
        int count2 = count1 + UnityEngine.Random.Range(fourmin, four);
        if (size < 3)
        {
            count2 = 0;
            count1 = 0;
        }
        int count3 = count2 + UnityEngine.Random.Range(threemin, three);
        if (size < 2)
        {
            count3 = 0;
            count2 = 0;
            count1 = 0;
        }
        int count4 = count3 + UnityEngine.Random.Range(twomin, two);

        // Make sure we are not digging past the river path
        if (count4 > river.Length)
        {
            int extra = count4 - river.Length;
            while (extra > 0)
            {
                if (count1 > 0) { count1--; count2--; count3--; count4--; extra--; }
                else if (count2 > 0) { count2--; count3--; count4--; extra--; }
                else if (count3 > 0) { count3--; count4--; extra--; }
                else if (count4 > 0) { count4--; extra--; }
            }
        }

        // adjust size of river at intersection point
        if (intersectionSize == 1)
        {
            count4 = intersectionCount;
            count1 = 0;
            count2 = 0;
            count3 = 0;
        }
        else if (intersectionSize == 2)
        {
            count3 = intersectionCount;
            count1 = 0;
            count2 = 0;
        }
        else if (intersectionSize == 3)
        {
            count2 = intersectionCount;
            count1 = 0;
        }
        else if (intersectionSize == 4)
        {
            count1 = intersectionCount;
        }
        else
        {
            count1 = 0;
            count2 = 0;
            count3 = 0;
            count4 = 0;
        }

        // dig out the river
        for (int i = river.Tiles.Count - 1; i >= 0; i--)
        {

            Tile t = river.Tiles[i];

            if (counter < count1)
            {
                t.DigRiver(river, 4);
            }
            else if (counter < count2)
            {
                t.DigRiver(river, 3);
            }
            else if (counter < count3)
            {
                t.DigRiver(river, 2);
            }
            else if (counter < count4)
            {
                t.DigRiver(river, 1);
            }
            else
            {
                t.DigRiver(river, 0);
            }
            counter++;
        }
    }

    // Dig river
    private void DigRiver(River river)
    {
        int counter = 0;

        // How wide are we digging this river?
        int size = UnityEngine.Random.Range(1, 5);
        river.Length = river.Tiles.Count;

        // randomize size change
        int two = river.Length / 2;
        int three = two / 2;
        int four = three / 2;
        int five = four / 2;

        int twomin = two / 3;
        int threemin = three / 3;
        int fourmin = four / 3;
        int fivemin = five / 3;

        // randomize lenght of each size
        int count1 = UnityEngine.Random.Range(fivemin, five);
        if (size < 4)
        {
            count1 = 0;
        }
        int count2 = count1 + UnityEngine.Random.Range(fourmin, four);
        if (size < 3)
        {
            count2 = 0;
            count1 = 0;
        }
        int count3 = count2 + UnityEngine.Random.Range(threemin, three);
        if (size < 2)
        {
            count3 = 0;
            count2 = 0;
            count1 = 0;
        }
        int count4 = count3 + UnityEngine.Random.Range(twomin, two);

        // Make sure we are not digging past the river path
        if (count4 > river.Length)
        {
            int extra = count4 - river.Length;
            while (extra > 0)
            {
                if (count1 > 0) { count1--; count2--; count3--; count4--; extra--; }
                else if (count2 > 0) { count2--; count3--; count4--; extra--; }
                else if (count3 > 0) { count3--; count4--; extra--; }
                else if (count4 > 0) { count4--; extra--; }
            }
        }

        // Dig it out
        for (int i = river.Tiles.Count - 1; i >= 0; i--)
        {
            Tile t = river.Tiles[i];

            if (counter < count1)
            {
                t.DigRiver(river, 4);
            }
            else if (counter < count2)
            {
                t.DigRiver(river, 3);
            }
            else if (counter < count3)
            {
                t.DigRiver(river, 2);
            }
            else if (counter < count4)
            {
                t.DigRiver(river, 1);
            }
            else
            {
                t.DigRiver(river, 0);
            }
            counter++;
        }
    }

    private void FindPathToWater(Tile tile, Direction direction, ref River river)
    {
        if (tile.Rivers.Contains(river))
            return;

        // check if there is already a river on this tile
        if (tile.Rivers.Count > 0)
            river.Intersections++;

        river.AddTile(tile);

        // get neighbors
        Tile left = GetLeft(tile);
        Tile right = GetRight(tile);
        Tile top = GetTop(tile);
        Tile bottom = GetBottom(tile);

        float leftValue = int.MaxValue;
        float rightValue = int.MaxValue;
        float topValue = int.MaxValue;
        float bottomValue = int.MaxValue;

        // query height values of neighbors
        if (left.GetRiverNeighborCount(river) < 2 && !river.Tiles.Contains(left))
            leftValue = left.HeightValue;
        if (right.GetRiverNeighborCount(river) < 2 && !river.Tiles.Contains(right))
            rightValue = right.HeightValue;
        if (top.GetRiverNeighborCount(river) < 2 && !river.Tiles.Contains(top))
            topValue = top.HeightValue;
        if (bottom.GetRiverNeighborCount(river) < 2 && !river.Tiles.Contains(bottom))
            bottomValue = bottom.HeightValue;

        // if neighbor is existing river that is not this one, flow into it
        if (bottom.Rivers.Count == 0 && !bottom.Collidable)
            bottomValue = 0;
        if (top.Rivers.Count == 0 && !top.Collidable)
            topValue = 0;
        if (left.Rivers.Count == 0 && !left.Collidable)
            leftValue = 0;
        if (right.Rivers.Count == 0 && !right.Collidable)
            rightValue = 0;

        // override flow direction if a tile is significantly lower
        if (direction == Direction.Left)
            if (Mathf.Abs(rightValue - leftValue) < 0.1f)
                rightValue = int.MaxValue;
        if (direction == Direction.Right)
            if (Mathf.Abs(rightValue - leftValue) < 0.1f)
                leftValue = int.MaxValue;
        if (direction == Direction.Top)
            if (Mathf.Abs(topValue - bottomValue) < 0.1f)
                bottomValue = int.MaxValue;
        if (direction == Direction.Bottom)
            if (Mathf.Abs(topValue - bottomValue) < 0.1f)
                topValue = int.MaxValue;

        // find mininum
        float min = Mathf.Min(Mathf.Min(Mathf.Min(leftValue, rightValue), topValue), bottomValue);

        // if no minimum found - exit
        if (min == int.MaxValue)
            return;

        //Move to next neighbor
        if (min == leftValue)
        {
            if (left.Collidable)
            {
                if (river.CurrentDirection != Direction.Left)
                {
                    river.TurnCount++;
                    river.CurrentDirection = Direction.Left;
                }
                FindPathToWater(left, direction, ref river);
            }
        }
        else if (min == rightValue)
        {
            if (right.Collidable)
            {
                if (river.CurrentDirection != Direction.Right)
                {
                    river.TurnCount++;
                    river.CurrentDirection = Direction.Right;
                }
                FindPathToWater(right, direction, ref river);
            }
        }
        else if (min == bottomValue)
        {
            if (bottom.Collidable)
            {
                if (river.CurrentDirection != Direction.Bottom)
                {
                    river.TurnCount++;
                    river.CurrentDirection = Direction.Bottom;
                }
                FindPathToWater(bottom, direction, ref river);
            }
        }
        else if (min == topValue)
        {
            if (top.Collidable)
            {
                if (river.CurrentDirection != Direction.Top)
                {
                    river.TurnCount++;
                    river.CurrentDirection = Direction.Top;
                }
                FindPathToWater(top, direction, ref river);
            }
        }
    }

    // Extract data from a noise module
    private void GetData()
    {
        HeightData = new MapData(Width, Height);
        HeatData = new MapData(Width, Height);
        MoistureData = new MapData(Width, Height);

        // loop through each x,y point - get height value
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {

                // WRAP ON BOTH AXIS
                // Noise range
                float x1 = 0, x2 = 2;
                float y1 = 0, y2 = 2;
                float dx = x2 - x1;
                float dy = y2 - y1;

                // Sample noise at smaller intervals
                float s = x / (float)Width;
                float t = y / (float)Height;

                // Calculate our 4D coordinates
                float nx = x1 + Mathf.Cos(s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
                float ny = y1 + Mathf.Cos(t * 2 * Mathf.PI) * dy / (2 * Mathf.PI);
                float nz = x1 + Mathf.Sin(s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
                float nw = y1 + Mathf.Sin(t * 2 * Mathf.PI) * dy / (2 * Mathf.PI);




                float heightValue = (float)HeightMap.Get(nx, ny, nz, nw);
                float heatValue = (float)HeatMap.Get(nx, ny, nz, nw);
                float moistureValue = (float)MoistureMap.Get(nx, ny, nz, nw);

                // keep track of the max and min values found
                if (heightValue > HeightData.Max) HeightData.Max = heightValue;
                if (heightValue < HeightData.Min) HeightData.Min = heightValue;

                if (heatValue > HeatData.Max) HeatData.Max = heatValue;
                if (heatValue < HeatData.Min) HeatData.Min = heatValue;

                if (moistureValue > MoistureData.Max) MoistureData.Max = moistureValue;
                if (moistureValue < MoistureData.Min) MoistureData.Min = moistureValue;

                HeightData.Data[x, y] = heightValue;
                HeatData.Data[x, y] = heatValue;
                MoistureData.Data[x, y] = moistureValue;
            }
        }

    }

    // Build a Tile array from our data
    private void LoadTiles()
    {
        Tiles = new Tile[Width, Height];

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Tile t = new Tile();
                t.X = x;
                t.Y = y;

                //set heightmap value
                float heightValue = HeightData.Data[x, y];
                heightValue = (heightValue - HeightData.Min) / (HeightData.Max - HeightData.Min);
                t.HeightValue = heightValue;


                if (heightValue < DeepWater)
                {
                    t.HeightType = HeightType.DeepWater;
                    t.Collidable = false;
                }
                else if (heightValue < ShallowWater)
                {
                    t.HeightType = HeightType.ShallowWater;
                    t.Collidable = false;
                }
                else if (heightValue < Sand)
                {
                    t.HeightType = HeightType.Sand;
                    t.Collidable = true;
                }
                else if (heightValue < Grass)
                {
                    t.HeightType = HeightType.Grass;
                    t.Collidable = true;
                }
                else if (heightValue < Forest)
                {
                    t.HeightType = HeightType.Forest;
                    t.Collidable = true;
                }
                else if (heightValue < Rock)
                {
                    t.HeightType = HeightType.Rock;
                    t.Collidable = true;
                }
                else
                {
                    t.HeightType = HeightType.Snow;
                    t.Collidable = true;
                }


                //adjust moisture based on height
                if (t.HeightType == HeightType.DeepWater)
                {
                    MoistureData.Data[t.X, t.Y] += 8f * t.HeightValue;
                }
                else if (t.HeightType == HeightType.ShallowWater)
                {
                    MoistureData.Data[t.X, t.Y] += 3f * t.HeightValue;
                }
                else if (t.HeightType == HeightType.Shore)
                {
                    MoistureData.Data[t.X, t.Y] += 1f * t.HeightValue;
                }
                else if (t.HeightType == HeightType.Sand)
                {
                    MoistureData.Data[t.X, t.Y] += 0.2f * t.HeightValue;
                }

                //Moisture Map Analyze	
                float moistureValue = MoistureData.Data[x, y];
                moistureValue = (moistureValue - MoistureData.Min) / (MoistureData.Max - MoistureData.Min);
                t.MoistureValue = moistureValue;

                //set moisture type
                if (moistureValue < DryerValue) t.MoistureType = MoistureType.Dryest;
                else if (moistureValue < DryValue) t.MoistureType = MoistureType.Dryer;
                else if (moistureValue < WetValue) t.MoistureType = MoistureType.Dry;
                else if (moistureValue < WetterValue) t.MoistureType = MoistureType.Wet;
                else if (moistureValue < WettestValue) t.MoistureType = MoistureType.Wetter;
                else t.MoistureType = MoistureType.Wettest;


                // Adjust Heat Map based on Height - Higher == colder
                if (t.HeightType == HeightType.Forest)
                {
                    HeatData.Data[t.X, t.Y] -= 0.1f * t.HeightValue;
                }
                else if (t.HeightType == HeightType.Rock)
                {
                    HeatData.Data[t.X, t.Y] -= 0.25f * t.HeightValue;
                }
                else if (t.HeightType == HeightType.Snow)
                {
                    HeatData.Data[t.X, t.Y] -= 0.4f * t.HeightValue;
                }
                else
                {
                    HeatData.Data[t.X, t.Y] += 0.01f * t.HeightValue;
                }

                // Set heat value
                float heatValue = HeatData.Data[x, y];
                heatValue = (heatValue - HeatData.Min) / (HeatData.Max - HeatData.Min);
                t.HeatValue = heatValue;

                // set heat type
                if (heatValue < ColdestValue) t.HeatType = HeatType.Coldest;
                else if (heatValue < ColderValue) t.HeatType = HeatType.Colder;
                else if (heatValue < ColdValue) t.HeatType = HeatType.Cold;
                else if (heatValue < WarmValue) t.HeatType = HeatType.Warm;
                else if (heatValue < WarmerValue) t.HeatType = HeatType.Warmer;
                else t.HeatType = HeatType.Warmest;

                Tiles[x, y] = t;
            }
        }
    }

    private void UpdateNeighbors()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Tile t = Tiles[x, y];

                t.Top = GetTop(t);
                t.Bottom = GetBottom(t);
                t.Left = GetLeft(t);
                t.Right = GetRight(t);
            }
        }
    }

    private void UpdateBitmasks()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Tiles[x, y].UpdateBitmask();
            }
        }
    }

    private void UpdateBiomeBitmask()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Tiles[x, y].UpdateBiomeBitmask();
            }
        }
    }

    private void FloodFill()
    {
        // Use a stack instead of recursion
        Stack<Tile> stack = new Stack<Tile>();

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {

                Tile t = Tiles[x, y];

                //Tile already flood filled, skip
                if (t.FloodFilled) continue;

                // Land
                if (t.Collidable)
                {
                    TileGroup group = new TileGroup();
                    group.Type = TileGroupType.Land;
                    stack.Push(t);

                    while (stack.Count > 0)
                    {
                        FloodFill(stack.Pop(), ref group, ref stack);
                    }

                    if (group.Tiles.Count > 0)
                        Lands.Add(group);
                }
                // Water
                else
                {
                    TileGroup group = new TileGroup();
                    group.Type = TileGroupType.Water;
                    stack.Push(t);

                    while (stack.Count > 0)
                    {
                        FloodFill(stack.Pop(), ref group, ref stack);
                    }

                    if (group.Tiles.Count > 0)
                        Waters.Add(group);
                }
            }
        }
    }

    private void FloodFill(Tile tile, ref TileGroup tiles, ref Stack<Tile> stack)
    {
        // Validate
        if (tile.FloodFilled)
            return;
        if (tiles.Type == TileGroupType.Land && !tile.Collidable)
            return;
        if (tiles.Type == TileGroupType.Water && tile.Collidable)
            return;

        // Add to TileGroup
        tiles.Tiles.Add(tile);
        tile.FloodFilled = true;

        // floodfill into neighbors
        Tile t = GetTop(tile);
        if (!t.FloodFilled && tile.Collidable == t.Collidable)
            stack.Push(t);
        t = GetBottom(tile);
        if (!t.FloodFilled && tile.Collidable == t.Collidable)
            stack.Push(t);
        t = GetLeft(tile);
        if (!t.FloodFilled && tile.Collidable == t.Collidable)
            stack.Push(t);
        t = GetRight(tile);
        if (!t.FloodFilled && tile.Collidable == t.Collidable)
            stack.Push(t);
    }

    private Tile GetTop(Tile t)
    {
        return Tiles[t.X, MathHelper.Mod(t.Y - 1, Height)];
    }
    private Tile GetBottom(Tile t)
    {
        return Tiles[t.X, MathHelper.Mod(t.Y + 1, Height)];
    }
    private Tile GetLeft(Tile t)
    {
        return Tiles[MathHelper.Mod(t.X - 1, Width), t.Y];
    }
    private Tile GetRight(Tile t)
    {
        return Tiles[MathHelper.Mod(t.X + 1, Width), t.Y];
    }


}
