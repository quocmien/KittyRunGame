using UnityEngine;
using System.Collections;

public class D3Item : MonoBehaviour {
	
	public float scoreAdd; //add money if item = coin
	public int decreaseLife; //decrease life if item = obstacle 
	[HideInInspector] public int itemID; //item id
	public float speedMove; //speed move for moving obstacle
	public float duration; // duration item
	public float itemEffectValue; // effect value(if item star = speed , if item multiply = multiply number)
	public D3ItemCoinRotate itemRotate; // rotate item
	public GameObject effectHit; // effect when hit item

	

	//Lights
    public GameObject listLights;
    public AudioClip audioHorn;
    [Range(0, 100)]
    public int percentPlay = 100;
    [HideInInspector] public  bool statusPlay = true;


     public bool itemActive;

	bool Mesh = true;

	[HideInInspector] public bool isEditing;
	[HideInInspector] public Object targetPref;
	[HideInInspector] public string scenePath;


	[HideInInspector] public Color colorPattern =  new Color(Color.white.r, Color.white.g, Color.white.b, Color.white.a);
	
	public enum TypeItem{
		Null, Coin, Obstacle, Obstacle_Roll, ItemJump, ItemSprint, ItemMagnet, ItemMultiply, Moving_Obstacle, ItemShield, ItemSpecial,Enemy
    }
	
	public TypeItem typeItem;
	
	[HideInInspector]
	public bool useAbsorb = false;
	
	public static D3Item instance;

	bool IsMoving = false;
	void Start(){
		instance = this;
        Mesh = true;
    }

	float count = 8;

    void OnTriggerEnter(Collider col)
    {
		if(GetComponent<Collider>() && typeItem == TypeItem.Moving_Obstacle && itemActive)
		{
            if (col.tag == "Item")
            {
                if (col.GetComponent<D3Item>())
                {
                    col.GetComponent<D3Item>().HitObstacle();
                }
            }
        }
        
    }


    //Set item effect
    public void ItemGet(float AddSprintTime, float AddSpecialTime, float AddMultiplyTime, float AddMagnetTime, float AddShieldTime)
    {
		float SprintTime = AddSprintTime;
		float SpecialTime = AddSpecialTime;
		float MultiplierTime = AddMultiplyTime;
		float MagnetTime = AddMagnetTime;
		float PowerTime = AddShieldTime;

		if(D3GameAttribute.gameAttribute.deleyDetect == false && itemActive)
        {
			if(typeItem == TypeItem.Coin){
				HitCoin();
				//Play sfx when get coin
				D3SoundManager.instance.PlayingSound("GetCoin");
			}else if(typeItem == TypeItem.Obstacle){
				HitObstacle();
				//Play sfx when get hit
				D3SoundManager.instance.PlayingSound("HitOBJ");
			}else if(typeItem == TypeItem.Obstacle_Roll){
				if(D3Controller.instace.isRoll == false){
					HitObstacle();
					//Play sfx when get hit
					D3SoundManager.instance.PlayingSound("HitOBJ");
				}
			}else if(typeItem == TypeItem.ItemSprint){
				float sum = duration + SprintTime;
                D3Controller.instace.Sprint(itemEffectValue,sum);
				//Play sfx when get item
				D3SoundManager.instance.PlayingSound("GetItem");
				HideObj();
				initEffect(effectHit);
			}else if(typeItem == TypeItem.ItemMagnet){
				float sum = duration + MagnetTime;
                D3Controller.instace.Magnet(sum);
				//Play sfx when get item
				D3SoundManager.instance.PlayingSound("GetItem");
				HideObj();
				initEffect(effectHit);
			}else if(typeItem == TypeItem.ItemJump){
				D3Controller.instace.JumpDouble(duration);
				//Play sfx when get item
				D3SoundManager.instance.PlayingSound("GetItem");
				HideObj();
				initEffect(effectHit);
			}else if(typeItem == TypeItem.ItemMultiply){
				D3Controller.instace.Multiply(duration);
				float sum = itemEffectValue + MultiplierTime;
                D3GameAttribute.gameAttribute.multiplyValue = sum;
				//Play sfx when get item
				D3SoundManager.instance.PlayingSound("GetItem");
				HideObj();
				initEffect(effectHit);
			}else if(typeItem == TypeItem.Moving_Obstacle){
				HitObstacle();
				//Play sfx when get hit
				D3SoundManager.instance.PlayingSound("HitOBJ");
			} else if (typeItem == TypeItem.ItemShield)
            {
				float sum = duration + PowerTime;
                D3Controller.instace.ItemImmortality(sum);
                D3SoundManager.instance.PlayingSound("GetItem");
                HideObj();
                initEffect(effectHit);
            }
            else if (typeItem == TypeItem.ItemSpecial)
            {
				float sum = duration + SpecialTime;
                D3Controller.instace.ItemSpecial(itemEffectValue, sum);
                D3SoundManager.instance.PlayingSound("GetItem");
                HideObj();
                initEffect(effectHit);
            }
        }
	}

	public void UseMovingItem(){

        if (listLights != null && statusPlay && itemActive)
        {
            listLights.SetActive(false);
            statusPlay = false;
        }
        StartCoroutine (MovingItem ());
	}

    public void ShowMesh()
    {
        if (!Mesh)
        {
            Mesh = true;
            foreach (Transform tran in transform) tran.gameObject.SetActive(true);
        }
    }

    public void HideMesh()
    {
        if (Mesh)
        {
            Mesh = false;
            foreach (Transform tran in transform) tran.gameObject.SetActive(false);
        }
    }

    IEnumerator MovingItem(){
		while (itemActive && !D3GameAttribute.gameAttribute.pause) {
			transform.Translate(Vector3.back * speedMove * Time.deltaTime);
			yield return 0;
		}
	}

    private void FixedUpdate()
    {
        if (count <= 0 && itemActive && D3GameController.instace.Player && !statusPlay && !D3Controller.instace.isSprint && !D3Controller.instace.IsSpecial && typeItem == TypeItem.Moving_Obstacle)
        {
            float PlayerDist = Vector3.Distance(D3GameController.instace.Player.transform.position, transform.position);

            if (PlayerDist < 12)
            {
                int ran = Random.Range(0, 100);
                if (ran < percentPlay)
                {
                    if (audioHorn != null) D3SoundManager.instance.SFXSound.PlayOneShot(audioHorn);
                    if (listLights != null) listLights.SetActive(true);
                    statusPlay = true;
                }
            }
        }
    }
	private void Update()
	{
		if (count > 0 && typeItem == TypeItem.Moving_Obstacle && itemActive)
		{
			count -= Time.deltaTime;
		}

		if (itemActive)
		{
            ShowMesh();
        }
        if (!itemActive)
        {
            HideMesh();
        }


        if (D3GameAttribute.gameAttribute && itemActive)
		{
			if (D3GameAttribute.gameAttribute.pause)
			{
				if (!IsMoving)
				{
                    StopAllCoroutines();
					IsMoving = true;
                }
			}
			else {
				if (IsMoving)
				{
                    StartCoroutine(MovingItem());
					IsMoving= false;
                }
                
            }
		}
    }


    //Coin method
    private void HitCoin(){
		if(D3Controller.instace.isMultiply == false){
			D3GameAttribute.gameAttribute.coin += scoreAdd;
		}else{
			D3GameAttribute.gameAttribute.coin += (scoreAdd)* D3GameAttribute.gameAttribute.multiplyValue;
		}
        initEffectCoin(effectHit);
		HideObj();
	}
	
	//Obstacle method
	private void HitObstacle(){
        statusPlay = false;
        if (listLights != null) listLights.SetActive(false);
        count = 50;
        if (D3GameAttribute.gameAttribute.ageless == false){
			
			if(D3Controller.instace.timeSprint <= 0 && !D3Controller.instace.IsHoverboard && !D3Controller.instace.IsImmortality)
			{
				D3GameAttribute.gameAttribute.life -= decreaseLife;
				D3GameAttribute.gameAttribute.ActiveShakeCamera();
			}

			if (D3Controller.instace.IsHoverboard && !D3Controller.instace.IsImmortality)
			{
				HideObj();
				D3Controller.instace.DisebleHoverboard();
				D3GameAttribute.gameAttribute.ActiveShakeCamera();
			}
			if (D3Controller.instace.timeSprint > 0)
			{
				HideObj();
				D3GameAttribute.gameAttribute.ActiveShakeCamera();
			}
            if (D3Controller.instace.IsImmortality)
            {
                HideObj();
                D3GameAttribute.gameAttribute.ActiveShakeCamera();
            }

        }
	}
	
	//Spawn effect method
	private void initEffect(GameObject prefab){
		GameObject go = (GameObject) Instantiate(prefab, D3Controller.instace.transform.position, Quaternion.identity);
		go.transform.parent = D3Controller.instace.transform;
		go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y+0.5f, go.transform.localPosition.z);	
	}

    //Spawn effect method
    private void initEffectCoin(GameObject prefab)
    {
		if (D3Controller.instace.magnetCollider.GetComponent<D3Magnet>())
		{
			if (D3Controller.instace.magnetCollider.GetComponent<D3Magnet>())
			{
				if (D3Controller.instace.magnetCollider.GetComponent<D3Magnet>())
				{
					if (D3Controller.instace.magnetCollider.GetComponent<D3Magnet>().ObjectMagnet && D3Controller.instace.magnetCollider.activeInHierarchy)
					{
                        GameObject go = (GameObject)Instantiate(prefab, D3Controller.instace.magnetCollider.GetComponent<D3Magnet>().ObjectMagnet.transform.position, Quaternion.identity);
                        go.transform.parent = D3Controller.instace.transform;

                    }
                    else
                    {
                        initEffect(prefab);
                    }

                }
                else
                {
                    initEffect(prefab);
                }
            }
            else
            {
                initEffect(prefab);
            }
        }
		else {
            initEffect(prefab);
        }
    }

    //Magnet method
    public IEnumerator UseAbsorb(GameObject targetObj){
		bool isLoop = true;
		useAbsorb = true;
		while(isLoop){
			this.transform.position = Vector3.Lerp(this.transform.position, targetObj.transform.position, D3GameAttribute.gameAttribute.speed*2f * Time.smoothDeltaTime);
			if(Vector3.Distance(this.transform.position, targetObj.transform.position) < 0.6f){
				isLoop = false;
				D3SoundManager.instance.PlayingSound("GetCoin");
				HitCoin();
			}
			yield return 0;
		}
		Reset();
		StopCoroutine("UseAbsorb");
		yield return 0;
	}
	
	public void HideObj(){
		if(useAbsorb == false){
			this.transform.localPosition = new Vector3(-100,-100,-100);
            
        }
	}
	
	public void Reset(){
		itemActive = false;
		this.transform.position = new Vector3(-100,-100,-100);
		useAbsorb = false;
		statusPlay = false;
        if (listLights != null) listLights.SetActive(false);
    }

	private bool isSelect = false;
	[HideInInspector] public GameObject point1, point2, point3;
	[HideInInspector] public GameObject textZ, textY;
	[HideInInspector] public Vector3 position1, position2, position3;

	[HideInInspector] public float distanceZ = 1, distanceY = 1;

	#if UNITY_EDITOR
	[ExecuteInEditMode]
	public void OnDrawGizmos(){
		if(Application.isPlaying == false && isEditing == true){
			if (UnityEditor.Selection.Contains (gameObject) && isSelect == false) {
				Debug.Log("Select");
				CreatePointSelect();
				isSelect = true;
			}else if(!UnityEditor.Selection.Contains (gameObject) && !UnityEditor.Selection.Contains (point1) 
			         && !UnityEditor.Selection.Contains (point2) && !UnityEditor.Selection.Contains (point3)
			         && !UnityEditor.Selection.Contains (textZ) && !UnityEditor.Selection.Contains (textY) && isSelect == true){
				Debug.Log("Discount Select");
				DistroyPointSelect();
				isSelect = false;
			}
			
			if(point1 != null && point2 != null && point3 != null){
				Gizmos.color = Color.blue;
				point2.transform.position = new Vector3(transform.position.x, transform.position.y, point2.transform.position.z);
				Gizmos.DrawLine (point1.transform.position, point2.transform.position);
				Gizmos.color = Color.green;
				point3.transform.position = new Vector3(transform.position.x, point3.transform.position.y, transform.position.z);
				Gizmos.DrawLine (point1.transform.position, point3.transform.position);
				Gizmos.color = Color.green;
				Gizmos.DrawLine (point3.transform.position, new Vector3(point2.transform.position.x, point3.transform.position.y, point2.transform.position.z));

				position1 = point1.transform.position;
				position2 = point2.transform.position;
				position3 = point3.transform.position;


				textZ.transform.rotation = UnityEditor.SceneView.lastActiveSceneView.rotation;
				textY.transform.rotation = UnityEditor.SceneView.lastActiveSceneView.rotation;

				textZ.transform.position = point1.transform.position + ((point2.transform.position - point1.transform.position).normalized * Vector3.Distance(point1.transform.position, point2.transform.position))/2;
				textY.transform.position = point1.transform.position + ((point3.transform.position - point1.transform.position).normalized * Vector3.Distance(point1.transform.position, point3.transform.position))/2;
				distanceZ = Vector3.Distance (point2.transform.position, point1.transform.position);
				distanceY = Vector3.Distance (point3.transform.position, point1.transform.position);
				textZ.GetComponent<TextMesh> ().text = Vector3.Distance (point2.transform.position, point1.transform.position).ToString ("0.00");
				textY.GetComponent<TextMesh> ().text = Vector3.Distance (point3.transform.position, point1.transform.position).ToString ("0.00");
			}
		}
	}

	private void CreatePointSelect(){
		point1 = new GameObject ("Point1");
		point1.transform.parent = transform;
		if(position1.x == 0 && position1.y == 0 && position1.z == 0){
			point1.transform.position = transform.position;
		}else{
			point1.transform.position = position1;
		}
		point1.transform.localRotation = Quaternion.identity;

		point2 = new GameObject ("Point2");
		point2.transform.parent = transform;
		if(position2.x == 0 && position2.y == 0 && position2.z == 0){
			point2.transform.position = transform.position + transform.forward;
		}else{
			point2.transform.position = position2;
		}
		point2.transform.localRotation = Quaternion.identity;

		point3 = new GameObject ("Point3");
		point3.transform.parent = transform;
		if(position3.x == 0 && position3.y == 0 && position3.z == 0){
			point3.transform.position = transform.position + transform.up;
		}else{
			point3.transform.position = position3;
		}
		point3.transform.localRotation = Quaternion.identity;

		textZ = (GameObject)Instantiate ((Object)Resources.Load ("TextMesh"), Vector3.zero, Quaternion.identity);
		textY = (GameObject)Instantiate ((Object)Resources.Load ("TextMesh"), Vector3.zero, Quaternion.identity);

		textZ.transform.parent = transform;
		textY.transform.parent = transform;

		textZ.transform.position = (point2.transform.position - point1.transform.position) / 2;
		textY.transform.position = (point3.transform.position - point1.transform.position) / 2;

		textZ.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		textY.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);


		textZ.GetComponent<TextMesh> ().text = Vector3.Distance (point2.transform.position, point1.transform.position).ToString ("0.00");
		textY.GetComponent<TextMesh> ().text = Vector3.Distance (point3.transform.position, point1.transform.position).ToString ("0.00");

		textZ.GetComponent<TextMesh> ().fontSize = 100;
		textY.GetComponent<TextMesh> ().fontSize = 100;

		textZ.GetComponent<TextMesh> ().anchor = TextAnchor.UpperCenter;
		textY.GetComponent<TextMesh> ().anchor = TextAnchor.UpperCenter;

		point1.AddComponent<D3PointSelection> ();
		point2.AddComponent<D3PointSelection> ();
		point3.AddComponent<D3PointSelection> ();
		point1.GetComponent<D3PointSelection> ().color =  (Color.yellow);
		point2.GetComponent<D3PointSelection> ().color = (Color.blue);
		point3.GetComponent<D3PointSelection> ().color = (Color.green);
	}

	private void DistroyPointSelect(){
		if(point1 != null && point2 != null && point3 != null){
			DestroyImmediate (point1.gameObject);
			DestroyImmediate (point2.gameObject);
			DestroyImmediate (point3.gameObject);
			DestroyImmediate (textZ.gameObject);
			DestroyImmediate (textY.gameObject);
		}
	}
	#endif
}
