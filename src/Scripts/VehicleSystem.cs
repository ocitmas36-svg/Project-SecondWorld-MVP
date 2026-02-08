using UnityEngine;
using Mirror;

public class VehicleSystem : NetworkBehaviour
{
    [SyncVar] // Sinkronisasi status mobil (dipakai atau tidak) ke semua pemain
    public bool isOccupied = false;

    public Transform seatPosition;
    private GameObject driver;

    // Fungsi ini dipanggil saat Player menekan tombol 'E' dekat mobil
    [Command(requiresAuthority = false)]
    public void CmdEnterVehicle(GameObject player)
    {
        if (isOccupied) return;

        driver = player;
        isOccupied = true;
        
        // Pindahkan posisi player ke dalam mobil
        RpcSetPlayerInside(player);
    }

    [ClientRpc]
    void RpcSetPlayerInside(GameObject player)
    {
        player.transform.SetParent(this.transform);
        player.transform.position = seatPosition.position;
        player.GetComponent<CharacterController>().enabled = false;
        // Tambahkan logika menonaktifkan script jalan kaki disini
    }
}
