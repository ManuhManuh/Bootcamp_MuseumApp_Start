using System.Collections.Generic;

[System.Serializable]
public class WeatherData 
{
    public int visibility;
    public int dt;
    public int timezone;
    public int id;
    public string name;
    public int cod;
    public string @base;
    public Coord coord;
    public List<Weather> weather;
    public Main main;
    public Wind wind;
    public Clouds clouds;
    public Sys sys;
}

[System.Serializable]
public class Coord
{
    public double lon;
    public double lat;
}

[System.Serializable]
public class Weather
{
    public int id;
    public string name;
    public string description;
    public string icon;
}

[System.Serializable]
public class Main
{
    public double temp;
    public double feels_like;
    public double temp_min;
    public double temp_max;
    public double pressure;
    public double humidity;
}

[System.Serializable]
public class Wind
{
    public double speed;
    public int deg;
}

[System.Serializable]
public class Clouds
{
    public int all;
}

[System.Serializable]
public class Sys
{
    public int type;
    public int id;
    public double message;
    public string country;
    public int sunrise;
    public int sunset;
}
