using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToLevel : MonoBehaviour
{
    [SerializeField] private int idLevel;

    private void OnTriggerEnter(Collider other)
    {
        //если игрок попадает в триггер
        if (other.gameObject.tag == "Player" && !other.isTrigger)
        {
            
            //если айди уровня, куда ведет портал, больше на 1, чем айди уровня,
            //на котором игрок уже был, то загружаем сцену с этим уровнем
            if (idLevel == PlayerPrefs.GetInt("IDLevel") + 1)
                SceneManager.LoadScene(GameInformation.GetNameOfLevelByID(idLevel));
        }
    }
}