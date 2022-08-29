using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Table("UserRating")]
public class UserRating 
{
    [PrimaryKey]
    [AutoIncrement]
    public int RatingID { get; set; }
    public string RatingUser { get; set; }
    public string AttractionID { get; set; }
    public int Rating { get; set; }
    
    
    
}
