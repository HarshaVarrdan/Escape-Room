using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightmapController : MonoBehaviour
{
    LightmapData[] lightmap_data;

    // Use this for initialization
    void Start()
    {
        // Save reference to existing scene lightmap data.
        lightmap_data = LightmapSettings.lightmaps;
        disableLightmaps();
    }

    public void disableLightmaps()
    {
        // Disable lightmaps in scene by removing the lightmap data references
        LightmapSettings.lightmaps = new LightmapData[] { };
    }

    public void enableLightmaps()
    {
        // Reenable lightmap data in scene.
        LightmapSettings.lightmaps = lightmap_data;
    }

}
