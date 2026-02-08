using UnityEngine;
using Mirror;

/*
 * Script ini mengatur logika dasar Server dan Client.
 * Mewarisi NetworkManager dari library Mirror.
 */
public class SimpleNetworkManager : NetworkManager
{
    [Header("Pengaturan Lokasi Muncul (Spawn)")]
    public Vector3 spawnPosition = new Vector3(0, 1, -50); // Di depan Monas

    // Diaktifkan saat Server pertama kali jalan
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("--- Server Dunia Kedua Aktif! ---");
    }

    // Diaktifkan saat ada pemain (Client) yang masuk
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // 1. Tentukan titik spawn
        // Jika ada NetworkStartPosition di Map, dia akan pakai itu. 
        // Jika tidak ada, dia pakai posisi default di bawah ini.
        Transform startPos = GetStartPosition();
        Vector3 pos = startPos != null ? startPos.position : spawnPosition;

        // 2. Buat objek Player dari Prefab yang sudah didaftarkan di Inspector
        GameObject player = Instantiate(playerPrefab, pos, Quaternion.identity);

        // 3. Kasih tahu server kalau objek ini adalah milik koneksi (pemain) tersebut
        NetworkServer.AddPlayerForConnection(conn, player);

        Debug.Log($"Pemain bergabung! Total pemain: {numPlayers}");
    }

    // Diaktifkan saat pemain keluar/terputus
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        Debug.Log("Seorang pemain telah keluar dari dunia.");
        
        // Panggil fungsi dasar untuk menghapus objek player dari server
        base.OnServerDisconnect(conn);
    }

    // Fungsi tambahan untuk Client saat berhasil konek ke Server
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("Berhasil terhubung ke Server Dunia Kedua!");
    }
}
