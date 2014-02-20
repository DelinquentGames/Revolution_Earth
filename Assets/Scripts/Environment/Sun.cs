using UnityEngine;
using System.Collections;

[AddComponentMenu("Revolution Earth/Environments/Day Night Cycle/Sun")]
public class Sun : MonoBehaviour {

    public float maxLightBrightness;        //maximum light brightness we want the sun/moon to be at
    public float minLightBrightness;        //minimum light brightness we want the sun/moon to be at
    public float maxFlareBrightness;        //maximum flare brightness we want the sun/moon to be at
    public float minFlareBrightness;        //minimum flare brightness we want the sun/moon to be at

    public bool isMoon = false;             //Is this a moon or sun?
    public bool giveLight = false;          //does this give off light or not?

    void Start() {
        if (GetComponent<Light>() != null)
            giveLight = true;
    }
}
