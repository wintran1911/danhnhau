 using UnityEngine;
 using System.Collections;
 
 public class TransformFormat : MonoBehaviour {
 
     [SerializeField]private GameObject[] SpritesBackgorund;
    [SerializeField] private Camera maincam;
    private void Start()
    {
        scaleBackGroundFitScreenSize();
    }
    private void scaleBackGroundFitScreenSize()
    {
        //step1 get device screen
        Vector2 devecieSecreenResolution = new Vector2(Screen.width, Screen.height);
        print(devecieSecreenResolution);
        float scrHeight = Screen.height;
        float scrWidth = Screen.width;

        float DECECIE_SCREEN_ASPECT = scrWidth / scrHeight;
        print("DECECIE_SCREEN_ASPECT" + DECECIE_SCREEN_ASPECT.ToString());

        //stwp 2 set maincamera
        maincam.aspect = DECECIE_SCREEN_ASPECT;
        //step 3 scale back ground
        float camHeight = 100.0f * maincam.orthographicSize * 2.0f;
        float camWidth = camHeight *DECECIE_SCREEN_ASPECT;
        print("camHeght" + camHeight.ToString());
    }    

}
