using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mutetoggle : MonoBehaviour
{

[SerializeField] AudioMixer master;
   public void muter(bool flag){

if(flag){
master.SetFloat("MasterVolume",-79);

}else{

master.SetFloat("MasterVolume",-30);

}

}
}
