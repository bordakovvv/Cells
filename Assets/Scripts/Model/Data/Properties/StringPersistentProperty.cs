﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringPersistentProperty : PrefsPersistentProperty<string>
{
    public StringPersistentProperty(string defaultValue, string key) : base(defaultValue, key)
    {
        Init();
    }

    protected override void Write(string value)
    {
        PlayerPrefs.SetString(Key, value);
    }

    protected override string Read(string defaulValue)
    {
        return PlayerPrefs.GetString(Key, defaulValue);
    }    
}
