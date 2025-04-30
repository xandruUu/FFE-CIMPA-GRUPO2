using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changescene : MonoBehaviour
{
    public bool endgame = false;

    public void ChangeSceneNivel(string sceneName)
    {
        if (endgame == true){
            SceneManager.LoadScene("creditos");
        }else{
        SceneManager.LoadScene(sceneName);
        }
        //Cuando se reproduzca, carga la escena que se meta por parametro en el inspector.
    }

    public void setEnd(bool value){
        endgame = value;
    }

    
}
 