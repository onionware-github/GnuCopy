﻿using System.Collections.Generic;

public class Settings
{
    public bool Clearaftercopy { get; set; }
    public bool Clearforcopy { get; set; }
    public bool? Listingart { get; set; }
    public bool Savelastpaths { get; set; }
    public string Pathfrom { get; set; }
    public string Pathto { get; set; }
    public int Packageformat { get; set; }
    public bool Overrite { get; set; }
    
    public bool DateAsName { get; set; }
    
    public string? ZipName { get; set; }
    
    public string? TempfolderPath { get; set; }
    
    public bool MultipleSources { get; set; }
    public List<string> Sources { get; set; } = new();
    public bool? CreateOwnFolder { get; set; }
    public string? OwnFolderName { get; set; }
    public bool OwnFolderDate { get; set; }
}
