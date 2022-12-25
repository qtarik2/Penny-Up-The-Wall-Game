using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using Photon;

public class QMSpawner : MonoBehaviourPunCallbacks
{
    #region Singleton 
    public static QMSpawner instance; private void Awake() { instance = this; }
    
    #endregion
    public GameObject PennyPrefab;
    public GameObject powerBarPrefab, powerBarparent;
    public GameObject spawnPowerBar;
    public GameObject armPrefab, armParent;
    public GameObject spawnArm;
    public GameObject Penny;
    [Header("destroy time in milisecond")]
    public int pennyDestroyTime;
    void Start()
    {
        QMQuickMatch.instance.Penny = gameObject;
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnerObjects();
        }
    }

    public async void SpawnerObjects()
    {
        if (QMQuickMatch.instance.IsMyTurn())
        {
            spawnArm = PhotonNetwork.Instantiate(armPrefab.name, armPrefab.transform.position, Quaternion.identity);
            SpeedoMeter._instance.handPvt=spawnArm.transform;
           // spawnArm.transform.SetParent(SpeedoMeter._instance.handPvt);
            //SpeedoMeter._instance.handPvt=spawnArm.transform;
            spawnPowerBar = PhotonNetwork.Instantiate(powerBarPrefab.name, powerBarparent.transform.position, powerBarparent.transform.rotation);
            spawnPowerBar.transform.parent = powerBarparent.transform;
            await System.Threading.Tasks.Task.Delay(1000);
            //SpeedoMeter._instance.Start();
            SpeedoMeter.anim = spawnPowerBar.GetComponent<Animator>();
            SpeedoMeter.doAnim = spawnPowerBar.GetComponent<DG.Tweening.DOTweenAnimation>();
        }
    }
    public void SpawnPenny(Transform spawnPosition)
    {
        Penny = PhotonNetwork.Instantiate(PennyPrefab.name, spawnPosition.transform.position, spawnPosition.transform.rotation);
    }
    public void OnPenny()
    {
        Penny.transform.position = QMHandControlScript.instance.HandfixPoint.position;
        Penny.transform.rotation = QMHandControlScript.instance.HandfixPoint.rotation;
        Penny.SetActive(true);
    }
    public async void OffPenny()
    {
        await Task.Delay(pennyDestroyTime);
        PhotonNetwork.Destroy(Penny);
        PhotonNetwork.Destroy(spawnArm);
        PhotonNetwork.Destroy(spawnPowerBar);
    }

}
