using UnityEngine;

public class SticksMotion : MonoBehaviour
{
    float fadeTimer = 0f;
    private Color targetEmissionColor;
    public float fadeDuration = 1.5f;
    public HeartRateManager hrManager;
    public float minHR = 40f;
    public float maxHR = 200f;
    public float minSpeed = 2.0f;
    public float maxSpeed = 20.0f;
    public float speedSmoothness = 2.0f;


    public Vector3 movementDirection = Vector3.back;
    //Wo verschwinden die Sticks
    public float resetZPositon = 30.0f;
    //Wo sie erscheinen
    public float spawnZPositon = 60.0f;
    //Wie weit links/rechts sie spawnen
    public float horizontalSpawnSpread = 15.0f;
    //Verschiebung nach oben
    public float baseYPosition = 3.0f;
    public float verticalSpawnSpread = 0.0f;
    //Korridor um Menschen
    public float minHorizontalOffset = 1.5f;

    private float currentSpeed;
    private Material targetMaterial;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        targetMaterial = rend.material;
        targetMaterial.EnableKeyword("_EMISSION");
        //Liest die in Unity eingestellte EmissionsFarbe aus
        if (targetMaterial.HasProperty("_EmissionColor"))
        {
            targetEmissionColor = targetMaterial.GetColor("_EmissionColor");
        }
        else
        {   //Fallback auf standard Farbe
            targetEmissionColor = targetMaterial.color;
        }
        targetMaterial.SetColor("_EmissionColor", Color.black);
        
        currentSpeed = minSpeed;
        // Verteilt die Sticks beim Start zufällig auf der Strecke zwischen Spawn und Reset
        float initialZ = Random.Range(spawnZPositon, resetZPositon);
        RandomizePosition(initialZ);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaterialFade();

        UpdateSpeedHR();

        transform.Translate(movementDirection * currentSpeed*Time.unscaledDeltaTime, Space.World);
        if(transform.position.z <= resetZPositon)
        {
           float respawnZ = spawnZPositon - Random.Range(0f, 10f);
           RandomizePosition(respawnZ);
           //Fade in für neuen Lauf zurücksetzen
           ResetFade();
        }
    }
    public void UpdateMaterialFade()
    {
        if(targetMaterial==null||fadeTimer >= fadeDuration) return;
        fadeTimer += Time.unscaledDeltaTime;
        float t= Mathf.Clamp01(fadeTimer/fadeDuration);
        //Blendet von Scharz zur Zielfarbe über
        Color currentFadeColor = Color.Lerp(Color.black, targetEmissionColor, t);
        targetMaterial.SetColor("_EmissionColor", currentFadeColor);

    }

    public void ResetFade()
    {
        fadeTimer = 0f;
        targetMaterial.SetColor("_EmissionColor", Color.black);
    }


    public void UpdateSpeedHR()
    {
        float currentHR = minHR;
        currentHR = hrManager.GetHeartRate();
        float hrNormalized = Mathf.InverseLerp(minHR,maxHR, currentHR);
        float targetSpeed = Mathf.Lerp(minSpeed,maxSpeed, hrNormalized);
        //glättet Sprünge bei Pulsschwankung
        currentSpeed = Mathf.Lerp(currentSpeed,targetSpeed, Time.unscaledDeltaTime * speedSmoothness);
    }
    public void RandomizePosition(float zPos)
    {
        //Links oder Rechts vom Menschen
        float side = (Random.value <0.5f) ? -1f : 1f;
        float randomX = Random.Range(minHorizontalOffset, horizontalSpawnSpread)* side;
        float randomY = baseYPosition + Random.Range(-verticalSpawnSpread, verticalSpawnSpread);
        transform.position = new Vector3(randomX, randomY, zPos);
    }
}
