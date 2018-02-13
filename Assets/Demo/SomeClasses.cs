using System.Collections;
using System.Collections.Generic;
using pointcache.SerializedType;
using UnityEngine;


[ExposeType("deliberateGibberishHereCanBeUsedOrStandardGUIDGenerator")]
public class Weapon {
    public Weapon() {
        Debug.Log("Im just a weapon, nothing to see here");
    }
}

public class Ammo { }

[ExposeType("item_ammo_freezing")]
public class AmmoFreezing : Ammo {
    public AmmoFreezing() {
        Debug.Log("Yay im a freezing ammo");
    }
}

[ExposeType("item_ammo_exposive")]
public class AmmoExplosive : Ammo {
    public AmmoExplosive() {
        Debug.Log("Yay im a explosive ammo");
    }
}

public class NPC { }

[ExposeType("732647823")]
public class SpecialBoss : NPC {
    public SpecialBoss() {
        Debug.Log("Blow it out your ass");
    }
}