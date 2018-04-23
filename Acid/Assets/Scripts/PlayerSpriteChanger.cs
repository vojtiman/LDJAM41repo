using UnityEngine;

public class PlayerSpriteChanger : MonoBehaviour {
    public Sprite[] sprites;

    public void UpdateSprite(int level)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Mathf.Clamp(Mathf.FloorToInt(level / 5), 0, sprites.Length - 1)];
    }
}
