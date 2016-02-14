using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    public Sprite DamageSprite;
    public int HitPoints = 4;

    private SpriteRenderer _spriteRenderer;

	void Awake ()
	{
	    _spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void DamageWall(int loss)
    {
        _spriteRenderer.sprite = DamageSprite;
        HitPoints -= loss;

        if (HitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
