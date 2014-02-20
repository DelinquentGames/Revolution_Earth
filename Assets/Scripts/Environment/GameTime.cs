/// <Summary>
/// GameTime.cs
/// Adam T. Davis
/// 07/08/2013
/// 
/// This class is responsible for keeping track of in game time. it will also rotate the suns and moons in the sky according to the current in game time.
/// This class will also change the skybox from the day skybox to the night skybox as time progresses in game.
/// </Summary>
using UnityEngine;
using System.Collections;

[AddComponentMenu("Revolution Earth/Environments/Day Night Cycle/Timed Lighting")]
public class GameTime : MonoBehaviour {    
    public Transform[] sun;                     //an array to hold all of the suns/moons    
    public float dayCycleInMinutes = 60;         //how many real time minutes an in game day will last

    public float sunRise;                       //the time of day that we start the sunrise
    public float sunSet;                        //the time of day that we start the sunset
    public float skyboxBlendModifier;           //the speed at which the textures in the skybox blend

    private Sun[] _sunScript;                   //an array to hold all of the Sun.cs scripts attached to our sun
    private float _degreeRotation;              //how many degrees we rotate each Unit of time
    public float _timeOfDay;                   //track the passage of time through out the day

    private float _dayCycleInSeconds;           //the number of real time seconds in an in game day

    private const float SECOND = 1;                        //constant for 1 second
    private const float MINUTE = 60 * SECOND;              //constant for how many seconds in a minute
    private const float HOUR = 60 * MINUTE;                //constant for how many seconds in an hour
    private const float DAY = 24 * HOUR;                   //constant for how many seconds in a day
    private const float DEGREES_PER_SECOND = 360 / DAY;    //constant for how many degrees we have to rotate per second a day to do 360 degrees

    private TimeOfDay _tod;

	// Use this for initialization
	void Start () {

        _tod = TimeOfDay.Idle;

        //get the number of real time seconds in an in game day
        _dayCycleInSeconds = dayCycleInMinutes * MINUTE;

        RenderSettings.skybox.SetFloat("_Blend", 0);
        
        _sunScript = new Sun[sun.Length];
        for (int cnt = 0; cnt < sun.Length; cnt++) {
            Sun temp = sun[cnt].GetComponent<Sun>();

            if (temp == null) {
                Debug.Log("Sun script not found. Adding it.");
                sun[cnt].gameObject.AddComponent<Sun>();
                temp = sun[cnt].GetComponent<Sun>();
            }
            _sunScript[cnt] = temp;
        }
        _timeOfDay = 0;
        _degreeRotation = DEGREES_PER_SECOND * DAY / (_dayCycleInSeconds);

        sunRise *= _dayCycleInSeconds;
        sunSet *= _dayCycleInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
        for (int cnt = 0; cnt < sun.Length; cnt++)
            sun[cnt].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);

        _timeOfDay += Time.deltaTime;

        if (_timeOfDay > _dayCycleInSeconds)
            _timeOfDay -= _dayCycleInSeconds;
        
        GameObject moon = GameObject.FindGameObjectWithTag("Moon");

        if (_timeOfDay >sunSet)
            moon.GetComponent<Light>().enabled = true;
        else if (_timeOfDay > sunRise)
            moon.GetComponent<Light>().enabled = false;
       
        //Debug.Log(_timeOfDay);
        if (_timeOfDay < sunSet) {
            sun[0].gameObject.GetComponent<Light>().intensity = (_timeOfDay / 10000) * 1f;           
        }else {
            sun[0].gameObject.GetComponent<Light>().intensity = (_timeOfDay / 10000) * -1f;            
        }



        if (_timeOfDay > sunRise && _timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1) {
            _tod = TimeOfDay.SunRise;
            BlendSkyBox();
        }else if(_timeOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend") > 0){
            _tod = GameTime.TimeOfDay.SunSet;
            BlendSkyBox();
        }else {
            _tod = GameTime.TimeOfDay.Idle;
        }
        
	}

    private void BlendSkyBox() {
        float temp = 0;

        switch (_tod) {
            case TimeOfDay.SunRise:
                temp = (_timeOfDay - sunRise) / _dayCycleInSeconds * skyboxBlendModifier;
                break;

            case TimeOfDay.SunSet:
                temp = (_timeOfDay - sunSet) / _dayCycleInSeconds * skyboxBlendModifier;
                temp = 1 - temp;
                break;
        } 
        
        //Debug.Log(temp);
        RenderSettings.skybox.SetFloat("_Blend", temp);
    }
	
	public enum TimeOfDay {
        Idle,
        SunRise,
        SunSet
	}
}