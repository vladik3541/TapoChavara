using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 50)]

public class Ability : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _Name;
    [SerializeField] private string _Description;
    [SerializeField] private long price;
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool isDamagePerSeconde;
    [SerializeField] private int damage;

    public string Name{ get { return _Name; }}
    public string Description { get { return _Description; }}
    public Sprite Sprite{ get { return _sprite; } }
    public long Price { get { return price; } }
    public GameObject Projectile { get { return projectile; } }
    public bool IsDamagePerSeconde { get { return isDamagePerSeconde; } }
    public int Damage { get { return damage; } }
}
