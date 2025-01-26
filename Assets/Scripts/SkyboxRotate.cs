using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;

    private void Update()
    {
        // Отримуємо поточне обертання skybox
        float rotation = RenderSettings.skybox.GetFloat("_Rotation");

        // Збільшуємо обертання
        rotation += rotationSpeed * Time.deltaTime;

        // Забезпечуємо, щоб обертання залишалося в межах 0-360 градусів
        rotation %= 360f;

        // Застосовуємо нове значення обертання
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}
