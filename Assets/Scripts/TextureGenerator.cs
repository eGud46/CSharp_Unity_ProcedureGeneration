using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Linq;
using System;

public static class TextureGenerator
{

    // Height Map Colors
    private static Color DeepColor = new Color(15 / 255f, 30 / 255f, 80 / 255f, 1);
    private static Color ShallowColor = new Color(15 / 255f, 40 / 255f, 90 / 255f, 1);
    private static Color RiverColor = new Color(30 / 255f, 120 / 255f, 200 / 255f, 1);
    private static Color SandColor = new Color(198 / 255f, 190 / 255f, 31 / 255f, 1);
    private static Color GrassColor = new Color(50 / 255f, 220 / 255f, 20 / 255f, 1);
    private static Color ForestColor = new Color(16 / 255f, 160 / 255f, 0, 1);
    private static Color RockColor = new Color(0.5f, 0.5f, 0.5f, 1);
    private static Color SnowColor = new Color(1, 1, 1, 1);
    private static Color IceWater = new Color(210 / 255f, 255 / 255f, 252 / 255f, 1);
    private static Color ColdWater = new Color(119 / 255f, 156 / 255f, 213 / 255f, 1);
    private static Color RiverWater = new Color(65 / 255f, 110 / 255f, 179 / 255f, 1);

    // Height Map Colors
    private static Color Coldest = new Color(0, 1, 1, 1);
    private static Color Colder = new Color(170 / 255f, 1, 1, 1);
    private static Color Cold = new Color(0, 229 / 255f, 133 / 255f, 1);
    private static Color Warm = new Color(1, 1, 100 / 255f, 1);
    private static Color Warmer = new Color(1, 100 / 255f, 0, 1);
    private static Color Warmest = new Color(241 / 255f, 12 / 255f, 0, 1);

    //Moisture map
    private static Color Dryest = new Color(255 / 255f, 139 / 255f, 17 / 255f, 1);
    private static Color Dryer = new Color(245 / 255f, 245 / 255f, 23 / 255f, 1);
    private static Color Dry = new Color(80 / 255f, 255 / 255f, 0 / 255f, 1);
    private static Color Wet = new Color(85 / 255f, 255 / 255f, 255 / 255f, 1);
    private static Color Wetter = new Color(20 / 255f, 70 / 255f, 255 / 255f, 1);
    private static Color Wettest = new Color(0 / 255f, 0 / 255f, 100 / 255f, 1);

    //Biome Map
    private static Color Ice = Color.white;
    private static Color Desert = new Color(238 / 255f, 218 / 255f, 130 / 255f, 1);
    private static Color Savanna = new Color(177 / 255f, 209 / 255f, 110 / 255f, 1);
    private static Color TropicalRainforest = new Color(66 / 255f, 123 / 255f, 25 / 255f, 1);
    private static Color Tundra = new Color(96 / 255f, 131 / 255f, 112 / 255f, 1);
    private static Color TemperateRainforest = new Color(29 / 255f, 73 / 255f, 40 / 255f, 1);
    private static Color Grassland = new Color(164 / 255f, 225 / 255f, 99 / 255f, 1);
    private static Color SeasonalForest = new Color(73 / 255f, 100 / 255f, 35 / 255f, 1);
    private static Color BorealForest = new Color(95 / 255f, 115 / 255f, 62 / 255f, 1);
    private static Color Woodland = new Color(139 / 255f, 175 / 255f, 90 / 255f, 1);

    public static Texture2D GetHeightMapTexture(int width, int height, Tile[,] tiles)
    {
        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        var pixels = new Color[width * height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                switch (tiles[x, y].HeightType)
                {
                    case HeightType.DeepWater:
                        pixels[width - x - 1 + y * width] = DeepColor;
                        break;
                    case HeightType.ShallowWater:
                        pixels[width - x - 1 + y * width] = ShallowColor;
                        break;
                    case HeightType.Sand:
                        pixels[width - x - 1 + y * width] = SandColor;
                        break;
                    case HeightType.Grass:
                        pixels[width - x - 1 + y * width] = GrassColor;
                        break;
                    case HeightType.Forest:
                        pixels[width - x - 1 + y * width] = ForestColor;
                        break;
                    case HeightType.Rock:
                        pixels[width - x - 1 + y * width] = RockColor;
                        break;
                    case HeightType.Snow:
                        pixels[width - x - 1 + y * width] = SnowColor;
                        break;
                    case HeightType.River:
                        pixels[width - x - 1 + y * width] = RiverColor;
                        break;
                }

                /*float value = tiles[x, y].HeightValue;

                //Set color range, 0 = black, 1 = white
                pixels[width - x - 1 + y * width] = Color.Lerp(Color.black, Color.white, value);
                */
                //darken the color if a edge tile
                if ((int)tiles[x, y].HeightType > 2 && tiles[x, y].Bitmask != 15)
                    pixels[width - x - 1 + y * width] = Color.Lerp(pixels[width - x - 1 + y * width], Color.black, 0.4f);

            }
        }
        texture.SetPixels(pixels);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }

    public static Texture2D GetHeatMapTexture(int width, int height, Tile[,] tiles)
    {
        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        var pixels = new Color[width * height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                switch (tiles[x, y].HeatType)
                {
                    case HeatType.Coldest:
                        pixels[width - x - 1 + y * width] = Coldest;
                        break;
                    case HeatType.Colder:
                        pixels[width - x - 1 + y * width] = Colder;
                        break;
                    case HeatType.Cold:
                        pixels[width - x - 1 + y * width] = Cold;
                        break;
                    case HeatType.Warm:
                        pixels[width - x - 1 + y * width] = Warm;
                        break;
                    case HeatType.Warmer:
                        pixels[width - x - 1 + y * width] = Warmer;
                        break;
                    case HeatType.Warmest:
                        pixels[width - x - 1 + y * width] = Warmest;
                        break;
                }

                //darken the color if a edge tile
                if ((int)tiles[x, y].HeightType > 2 && tiles[x, y].Bitmask != 15)
                    pixels[width - x - 1 + y * width] = Color.Lerp(pixels[width - x - 1 + y * width], Color.black, 0.4f);
            }
        }

        texture.SetPixels(pixels);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }

    public static Texture2D GetMoistureMapTexture(int width, int height, Tile[,] tiles)
    {
        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        var pixels = new Color[width * height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                Tile t = tiles[x, y];

                if (t.MoistureType == MoistureType.Dryest)
                    pixels[width - x - 1 + y * width] = Dryest;
                else if (t.MoistureType == MoistureType.Dryer)
                    pixels[width - x - 1 + y * width] = Dryer;
                else if (t.MoistureType == MoistureType.Dry)
                    pixels[width - x - 1 + y * width] = Dry;
                else if (t.MoistureType == MoistureType.Wet)
                    pixels[width - x - 1 + y * width] = Wet;
                else if (t.MoistureType == MoistureType.Wetter)
                    pixels[width - x - 1 + y * width] = Wetter;
                else
                    pixels[width - x - 1 + y * width] = Wettest;

                //darken the color if a edge tile
                //				if ((int)tiles[x,y].HeightType > 2 && tiles[x,y].Bitmask != 15)
                //					pixels[width - x - 1 + y * width] = Color.Lerp(pixels[width - x - 1 + y * width], Color.black, 0.4f);
            }
        }

        texture.SetPixels(pixels);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }

    public static Texture2D GetBiomeMapTexture(int width, int height, Tile[,] tiles, float coldest, float colder, float cold)
    {
        var texture = new Texture2D(width, height);
        var pixels = new Color[width * height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                BiomeType value = tiles[x, y].BiomeType;

                switch (value)
                {
                    case BiomeType.Ice:
                        pixels[width - x - 1 + y * width] = Ice;
                        break;
                    case BiomeType.BorealForest:
                        pixels[width - x - 1 + y * width] = BorealForest;
                        break;
                    case BiomeType.Desert:
                        pixels[width - x - 1 + y * width] = Desert;
                        break;
                    case BiomeType.Grassland:
                        pixels[width - x - 1 + y * width] = Grassland;
                        break;
                    case BiomeType.SeasonalForest:
                        pixels[width - x - 1 + y * width] = SeasonalForest;
                        break;
                    case BiomeType.Tundra:
                        pixels[width - x - 1 + y * width] = Tundra;
                        break;
                    case BiomeType.Savanna:
                        pixels[width - x - 1 + y * width] = Savanna;
                        break;
                    case BiomeType.TemperateRainforest:
                        pixels[width - x - 1 + y * width] = TemperateRainforest;
                        break;
                    case BiomeType.TropicalRainforest:
                        pixels[width - x - 1 + y * width] = TropicalRainforest;
                        break;
                    case BiomeType.Woodland:
                        pixels[width - x - 1 + y * width] = Woodland;
                        break;
                }

                // Тайлы воды
                if (tiles[x, y].HeightType == HeightType.DeepWater)
                {
                    pixels[width - x - 1 + y * width] = DeepColor;
                }
                else if (tiles[x, y].HeightType == HeightType.ShallowWater)
                {
                    pixels[width - x - 1 + y * width] = ShallowColor;
                }

                // рисуем реки
                if (tiles[x, y].HeightType == HeightType.River)
                {
                    float heatValue = tiles[x, y].HeatValue;

                    if (tiles[x, y].HeatType == HeatType.Coldest)
                        pixels[width - x - 1 + y * width] = Color.Lerp(IceWater, ColdWater, (heatValue) / (coldest));
                    else if (tiles[x, y].HeatType == HeatType.Colder)
                        pixels[width - x - 1 + y * width] = Color.Lerp(ColdWater, RiverWater, (heatValue - coldest) / (colder - coldest));
                    else if (tiles[x, y].HeatType == HeatType.Cold)
                        pixels[width - x - 1 + y * width] = Color.Lerp(RiverWater, ShallowColor, (heatValue - colder) / (cold - colder));
                    else
                        pixels[width - x - 1 + y * width] = ShallowColor;
                }


                // добавляем контур
                if (tiles[x, y].HeightType >= HeightType.Shore && tiles[x, y].HeightType != HeightType.River)
                {
                    if (tiles[x, y].BiomeBitmask != 15)
                        pixels[width - x - 1 + y * width] = Color.Lerp(pixels[width - x - 1 + y * width], Color.black, 0.35f);
                }
            }
        }

        texture.SetPixels(pixels);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }
}
