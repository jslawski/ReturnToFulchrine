using UnityEngine;
using UnityEngine.UI;


//This is awful, delete this once you've captured the footage you need
public class TempCreatureUIManager : MonoBehaviour {

    public Creature owningCreature;
    public Text hpText;
    public GameObject status;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.hpText.text = "HP: " + this.owningCreature.hitPoints;

        if (this.gameObject.GetComponentInParent<InflictedStatusEffect>() != null)
        {
            this.status.SetActive(true);
        }
        else
        {
            this.status.SetActive(false);
        }
	}
}
