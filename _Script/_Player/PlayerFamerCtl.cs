using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerFamerCtl : MyObj
{
    [SerializeField]protected BoxCollider2D boxCollider2D;
    [SerializeField]protected CropScripTble cropScripTble;
    [SerializeField]protected FarmeManager farmeManager;
    [SerializeField]protected List<Vector3Int> ListSelpos = new List<Vector3Int>();
    [SerializeField]protected TileBase FarmrTileBase;
    [SerializeField]protected TileBase Tb_Plant;
    [SerializeField]protected TileBase Tb_Seed;
    [SerializeField]protected TileBase Tb_Harvest;
    [SerializeField]protected Tilemap Tm_Ground;
    [SerializeField]protected Tilemap Tm_Grass;
    [SerializeField]protected Tilemap Tm_Plant;
    [SerializeField]protected float CurrentTime;
    [SerializeField]protected bool IsSeed;
    [SerializeField]protected bool CanHarvest;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTileBase();
        this.LoadTileMap();
        this.LoadSelPos();
        this.LoadCollider();
        this.LoadFarmeManager();
    }

    protected virtual void LoadFarmeManager()
    {
        if(this.farmeManager == null) this.farmeManager = transform.parent.GetComponentInParent<FarmeManager>();
    }

    protected virtual void LoadSelPos()
    {
        if(this.ListSelpos.Count > 0) return;
        for(int i = 1; i <= 18; i++)
        {
            for(int o = 6; o <= 10; o++)
            {
                Vector3Int Pos = new Vector3Int(i,o,0);
                this.ListSelpos.Add(Pos);
            }
        }
    } 

    protected virtual void LoadCollider()
    {
        if(this.boxCollider2D == null) this.boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }
    protected virtual void FixedUpdate()
    {
        this.TimeHarvest();
    }

    protected virtual void LoadTileMap()
    {
        GameObject Obj = GameObject.FindGameObjectWithTag("Farme");
        foreach(Transform trans in Obj.transform)
        {
            if(trans.name == "Ground")
            {
                this.Tm_Ground = trans.GetComponent<Tilemap>();
            }
            if(trans.name ==  "Grass")
            {
                this.Tm_Grass = trans.GetComponent<Tilemap>();
            }
            if(trans.name ==  "Plant")
            {
                this.Tm_Plant = trans.GetComponent<Tilemap>();
            }
        }
    }
    protected virtual void LoadTileBase()
    {
        
        if(this.FarmrTileBase == null) this.FarmrTileBase = Resources.Load<TileBase>("_Farme/TX Tileset Grass Flower 2");
        if(this.Tb_Plant == null) this.Tb_Plant = Resources.Load<TileBase>("_Farme/TX Tileset Ground_4");
        if(this.Tb_Seed == null) this.Tb_Seed = Resources.Load<TileBase>("_Farme/TX Tileset Grass Flower 3");
        if(this.Tb_Harvest == null) this.Tb_Harvest = Resources.Load<TileBase>("_Farme/TX Tileset Grass Flower 4");
        if(cropScripTble == null) this.cropScripTble  = Resources.Load<CropScripTble>("_Farme/_Crop/Flower");
        
    }
    protected virtual void OnTriggerStay2D(Collider2D coll)
    {
        Vector3Int MyPOs = this.Tm_Ground.WorldToCell(transform.position);
        TileBase CurrentTile = this.Tm_Grass.GetTile(MyPOs);
        TileBase CurrentPlant = this.Tm_Plant.GetTile(MyPOs);
        if (coll.gameObject.CompareTag("LoadScene"))
        {
            SceneManager.LoadScene(coll.name);
            transform.position= new Vector3(0f,0f,0f);
        }
        if (coll.gameObject.CompareTag("CropHarvest"))
        {
            if (InputManger.Instance.Dig && this.IsSeed == false)
            {
                foreach(Vector3Int vector3Int in this.ListSelpos)
                {
                    this.Tm_Grass.SetTile(vector3Int,null);                
                    
                }
            }
            if (InputManger.Instance.Plant && this.IsSeed == false)
            {
                this.IsSeed = true;
                this.CurrentTime = 0;
                foreach(Vector3Int vector3Int in this.ListSelpos)
                {
                    this.Tm_Grass.SetTile(vector3Int,Tb_Seed);                
                    
                }
            }
            if(this.CanHarvest == true)
            {
                if (InputManger.Instance.Harvest)
                {
                    this.IsSeed = false;

                    foreach(Vector3Int vector3Int in this.ListSelpos)
                    {
                        this.Tm_Grass.SetTile(vector3Int,null);                
                    
                    }
                    this.farmeManager.farme_ScipTble.CurrentXP+=this.cropScripTble.Xp;
                    this.farmeManager.farme_ScipTble.Flower+=this.cropScripTble.count;
                    this.CanHarvest = false;

                    FarmeSaveDate date = new FarmeSaveDate
                    {
                        Meat = this.farmeManager.farme_ScipTble.Meat,
                        Fur = this.farmeManager.farme_ScipTble.Fur,
                        Flower = this.farmeManager.farme_ScipTble.Flower,
                        FarmeLevel = this.farmeManager.farme_ScipTble.FarmeLv,
                        CurretXp = this.farmeManager.farme_ScipTble.CurrentXP
                    };

                    this.farmeManager.LocalsaveManager.SaveToJson(date);
                }
                
            }
        }
    }

    protected virtual void TimeHarvest()
    {
        if(this.CurrentTime <= this.cropScripTble.TimeHarvest)
        {
            this.CurrentTime += Time.fixedDeltaTime;
        }
        if(this.CurrentTime >= this.cropScripTble.TimeHarvest)
        {
            this.CanHarvest = true;
            if(this.IsSeed == true)
            {
                foreach(Vector3Int vector3Int in this.ListSelpos)
                {
                    this.Tm_Grass.SetTile(vector3Int,Tb_Harvest);                
                }
            }
            
        }
    }

    protected virtual void ResetFarme()
    {
        //lam thoi gian qua ngay o chua gieo hat sex trow vef tbGrass
        ///if(time){
        //if (grassTilemap.GetTile(cellPos) == null)
        {
            // Đặt lại cỏ cũ
           //grassTilemap.SetTile(cellPos, Tm_farmtilebase);
            //Debug.Log("Cỏ đã mọc lại tại tọa độ: " + cellPos);
        }
    }
}
