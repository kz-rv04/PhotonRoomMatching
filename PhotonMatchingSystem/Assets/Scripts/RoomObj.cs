﻿using UnityEngine;
using UnityEngine.UI;


/// ルーム  
/// </summary>  
public class RoomObj : MonoBehaviour
{
    // 部屋名  
    [SerializeField]
    private Text _name;
    // 人数  
    [SerializeField]
    private Text _count;


    // Use this for initialization  
    void Start()
    {
        // ボタンにコールバック登録  
        GetComponent<Button>().onClick.AddListener(OnTapped);
        this._name = this.transform.Find("Name").GetComponent<Text>();
        this._count = this.transform.Find("Count").GetComponent<Text>();
    }

    // Update is called once per frame  
    void Update()
    {
        // 部屋一覧からこの部屋の情報を取得  
        var roomlist = PhotonNetwork.GetRoomList();
        RoomInfo room = null;
        foreach(RoomInfo r in roomlist)
        {
            if (r.Name == this._name.text) {
                room = r;
                break;
            }

        }
        if (room != null)
        {
            // 取得した情報から人数を取得  
            _count.text = room.PlayerCount + "/" + room.MaxPlayers;
        }
        else
        {
            // 一覧にない = 0人、0人の部屋は削除される  
            _count.text = 0 + "/" + 2;
        }

    }

    /// <summary>  
    /// タップ時  
    /// </summary>  
    public void OnTapped()
    {
        // 部屋設定  
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.isOpen = true;     // 部屋を開くか  
        roomOptions.isVisible = true;  // 一覧に表示するか  
        roomOptions.maxPlayers = 2;        // 最大参加人数  

        // 部屋に参加、存在しない時作成して参加  
        PhotonNetwork.JoinOrCreateRoom(_name.text, roomOptions, new TypedLobby());
    }
}