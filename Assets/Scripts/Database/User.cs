using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Table ("User")]
public class User 
{
    [PrimaryKey]
    public string Username { get; set; }

    /// <summary>
    /// DON'T STORE PASSWORDS THIS WAY!!! THIS IS JUST AN EXAMPLE!! 
    /// </summary>
    public string Password { get; set; }

    public static string loggedUserSaveKey = "loggedUserSaveKey";
    public static string LoggedInUsername => PlayerPrefs.GetString(loggedUserSaveKey, string.Empty);

    public static bool IsLoggedIn => !LoggedInUsername.Equals(string.Empty);

    public static void LogIn(string username) => PlayerPrefs.SetString(loggedUserSaveKey, username);
    public static void LogOff() => PlayerPrefs.DeleteKey(loggedUserSaveKey);

}
