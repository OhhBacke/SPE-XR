using UnityEngine;

public class HeartRateCamera : MonoBehaviour
{
    //traget = das Objekt, welchem die Kamera fogen soll
    public Transform target; 
    // hrManager = das Onjekt, welches die HF ausgibt
    public HeartRateManager hrManager;
    //Untere HF Grenze
    public float minHR = 40f;
    //Obere HF Grenze
    public float maxHR = 200f;
    // Entspricht der Distanz bei HF40
    public float minDistance = 2f; 
    //Entspricht der Distanz bei HF200
    public float maxDistance = 8f; 
    //Wie smoth die Kamera sich bewegt
    public float smoothSpeed = 3f;
    //lokale Variable um den aktuellen Abstand zu speichern
    private float currentDistance;
    
    void Start()
    {
        currentDistance = minDistance;
    }

    // LateUpdate wird nach allen anderen Updates aufgerufen (ideal für Kameras)
    void LateUpdate()
    {
        //Camera blockieren falls Spiel noch nicht gestartet wurde
        if(!StartMenuManager.isProgamStarted) return;
        // Abbrechen falls ein Objekt im Inspector nicht zugewiesen wurde
        if (target == null || hrManager == null) return;
        // aktuelle HF erhalten
        float currentHR = hrManager.GetHeartRate();
        //Prozentualen Anteil an HF vom Min berechnen
        float t = Mathf.InverseLerp(minHR, maxHR, currentHR);
        // Ziel-Distanz berechnen
        float targetDistance = Mathf.Lerp(minDistance, maxDistance, t);

        // Sanfter Übergang zur neuen Distanz (damit die Kamera nicht springt)
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * smoothSpeed);

        // Richtung von der Kamera zum Charakter berechnen
        // transform mit kleinem t ist immer automatisch das Objekt auf welchem das Skript ausgeführt wird
        Vector3 direction = (transform.position - target.position).normalized;
        if (direction == Vector3.zero) direction = -target.forward; // Sicherheitseinstellung

        // Kamera an der neuen Position platzieren
        transform.position = target.position + direction * currentDistance;
        //ausrichten auf den Kopf
        transform.LookAt(target);
    }
}
