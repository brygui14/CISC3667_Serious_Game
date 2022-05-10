using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControll : MonoBehaviour
{
[SerializeField] AudioMixer master;

public void SetVolume(float level){
Debug.Log(Mathf.Log10(level)*20);
master.SetFloat("MasterVolume",Mathf.Log10(level)*20);
}

}
