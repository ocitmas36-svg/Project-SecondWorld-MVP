using UnityEngine;

public class MonasBuilder : MonoBehaviour
{
    void Start()
    {
        BuildMonas();
    }

    void BuildMonas()
    {
        // 1. Dasar Monas (Pelataran bawah)
        GameObject basePlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        basePlatform.transform.position = new Vector3(0, 2.5f, 0);
        basePlatform.transform.localScale = new Vector3(45, 5, 45);
        basePlatform.name = "Monas_Base";

        // 2. Cawan (Pelataran atas)
        GameObject cawan = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cawan.transform.position = new Vector3(0, 7f, 0);
        cawan.transform.localScale = new Vector3(20, 4, 20);
        cawan.name = "Monas_Cawan";

        // 3. Tugu (Tiang panjang)
        GameObject tugu = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tugu.transform.position = new Vector3(0, 60f, 0);
        tugu.transform.localScale = new Vector3(8, 110, 8);
        tugu.name = "Monas_Tugu";

        // 4. Lidah Api (Emas di atas)
        GameObject api = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        api.transform.position = new Vector3(0, 118f, 0);
        api.transform.localScale = new Vector3(10, 15, 10);
        api.name = "Monas_ApiEmas";
        
        // Memberi warna emas pada api
        api.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
