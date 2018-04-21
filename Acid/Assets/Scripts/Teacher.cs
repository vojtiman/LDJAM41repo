using UnityEngine.UI;
using UnityEngine;

public class Teacher : MonoBehaviour {
    public GameObject teacherPanel;
    public float maxDistance = 200;

    public Text strength;
    public Text stamina;
    public Text luck;
    public Text weaponLevel;
    public Text armorLevel;

    [Header("Price Scaling")]
    public float priceScalingStrength;
    public float priceScalingStamina;
    public float priceScalingLuck;
    public float priceScalingWeapon;
    public float priceScalingArmor;

    [Header("Price of")]
    public int priceOfStrength;
    public int priceOfStamina;
    public int priceOfLuck;
    public int priceOfWeapon;
    public int priceOfArmor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckDistance();
	}

    public void OpenTeacherPanel()
    {
        teacherPanel.SetActive(true);
        UpdateTeacherPanel();
    }

    void UpdateTeacherPanel()
    {
        PlayerStats ps = PlayerStats.instance;
        strength.text = ps.strength.ToString();
        stamina.text = ps.stamina.ToString();
        luck.text = ps.luck.ToString();
        armorLevel.text = ps.armorLevel.ToString();
        weaponLevel.text = ps.weaponLevel.ToString();
    }

    void CheckDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, player.transform.position - transform.position, maxDistance + 200);
        if (hitInfo.transform == null)
            return;
        if (hitInfo.distance > maxDistance)
            teacherPanel.SetActive(false);
    }
}
