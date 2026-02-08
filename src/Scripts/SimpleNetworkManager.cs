using UnityEngine;
using Mirror;

public class SimpleNetworkManager : NetworkManager
{
    // Kamu bisa kustomisasi apa yang terjadi saat player join di sini
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Debug.Log("Pemain baru bergabung dari IP: " + conn.address);
    }
}
