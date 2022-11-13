using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<ItemSlot> myInv = new();
    public Sprite[] sprites;
    public Texture2D texture;
    public Texture2D texture2;

    public Vector3 velocity = Vector3.zero;
    public Ray ray = new Ray();
    private RaycastHit hit;
    public Drawer draw;
    public Blocks blockstore;

    private float walktime = 0;

    public Ray walkray = new Ray();
    public Ray walkrayleft = new Ray();
    public Ray walkrayright = new Ray();
    public Ray walkrayback = new Ray();
    public Ray finalray = new Ray();
    private RaycastHit walkhit;
    private RaycastHit walkhitleft;
    private RaycastHit walkhitright;
    private RaycastHit walkhitback;
    private RaycastHit finalhit;
    public CharacterController cc;

    private Ray r;
    private RaycastHit h;
    public GameObject selectedCube;
    void DoSelected()
    {
        r.origin = (transform.position + transform.up) + (Camera.main.transform.forward * 0.5f);
        r.direction = Camera.main.transform.forward;
        Physics.Raycast(r, out h, 30f);
        if(h.collider != null)
        {
            var thing = h.point + (r.direction * .1f);
            selectedCube.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            selectedCube.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            selectedCube.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            selectedCube.transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            selectedCube.transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            selectedCube.transform.GetChild(0).transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            selectedCube.transform.position = new Vector3(Mathf.FloorToInt(thing.x), Mathf.FloorToInt(thing.y), Mathf.FloorToInt(thing.z));
        } else
        {
            //selectedCube.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    void Start()
    {
        cc = this.gameObject.GetComponent<CharacterController>();
        for(int i = 0; i < 35; i++)
        {
            ItemSlot slot = new();
            slot.id = 0;
            slot.amt = 0;
            myInv.Add(slot);
        }
        texture = new Texture2D(16, 16);
        texture2 = new Texture2D(16, 16);

        texture.filterMode = FilterMode.Point;
        
        texture2.filterMode = FilterMode.Point;
        Cursor.lockState = CursorLockMode.Locked;
        myInv[0].id = 2;
        myInv[0].amt = 128;
        myInv[1].id = 4;
        myInv[1].amt = 99;
        myInv[2].id = 5;
        myInv[2].amt = 1;
        myInv[3].id = 7;
        myInv[3].amt = 20;
        myInv[4].id = 8;
        myInv[4].amt = 99;
        myInv[5].id = 9;
        myInv[5].amt = 5;
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }




    Rect position = new(); 
    public void DrawItemSlot(ItemSlot slot, Vector2 pos)
    {
            position.x = pos.x;
            position.y = pos.y;
            position.width = Screen.width / 32;
            position.height = Screen.width / 32;
            var pixels1 = sprites[0].texture.GetPixels((int)sprites[0].textureRect.x,
                                                    (int)sprites[0].textureRect.y,
                                                    (int)sprites[0].textureRect.width,
                                                    (int)sprites[0].textureRect.height);
            texture.SetPixels(pixels1);
            texture.Apply(); 

        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
        if (slot.id != 0)
        {
            position.x = pos.x+7;
            position.y = pos.y+7;
            position.width = Screen.width / 40;
            position.height = Screen.width / 40;
            var pixels = sprites[slot.id].texture.GetPixels((int)sprites[slot.id].textureRect.x,
                                                    (int)sprites[slot.id].textureRect.y,
                                                    (int)sprites[slot.id].textureRect.width,
                                                    (int)sprites[slot.id].textureRect.height);
            texture2.SetPixels(pixels);
            texture2.Apply();
            GUI.skin.box.normal.background = texture2;
            GUI.Box(position, GUIContent.none);
            position.width = 100;
            position.y += Screen.width / 40;
            GUI.Label(position, slot.amt.ToString());
        }
        
    }
    public Sprite selectedSp;
    public void DrawSelectedSquare(Vector2 pos)
    {
        position.x = pos.x;
        position.y = pos.y;
        position.width = Screen.width / 32;
        position.height = Screen.width / 32;
        var pixels1 = selectedSp.texture.GetPixels((int)selectedSp.textureRect.x,
                                                (int)selectedSp.textureRect.y,
                                                (int)selectedSp.textureRect.width,
                                                (int)selectedSp.textureRect.height);
        texture.SetPixels(pixels1);
        texture.Apply();

        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);

    }

    public GUISkin skin;
    private void OnGUI()
    {
        GUI.skin = skin;
        for(int i = 0; i < 7; i++)
        {
            DrawItemSlot(myInv[i], new Vector2(i * (Screen.width / 24), Screen.height - (Screen.height / 6)));
            if(selected == i)
            {
                DrawSelectedSquare(new Vector2(i * (Screen.width / 24), Screen.height - (Screen.height / 6)));
            }
        }
    }

    Ray placeray = new();
    RaycastHit placehit = new();
    float protectiveTimer = 0;
    bool machine = false;
    bool placed = false;
    Vector3 myblockpos;
    void DoBlockPlaceStuff()
    {
        myblockpos.x = (int)transform.position.x;
        myblockpos.y = (int)transform.position.y;
        myblockpos.z = (int)transform.position.z;
        if(Input.GetMouseButtonDown(1)) //right click
        {
            placed = false;
                placeray.origin = (transform.position + transform.up) + (Camera.main.transform.forward*0.5f);
                placeray.direction = Camera.main.transform.forward;
                Physics.Raycast(placeray, out placehit, 4f);
                while (placehit.collider == null)
                {
                    if (protectiveTimer < 2f)
                    {
                        protectiveTimer += Time.deltaTime;
                        placeray.origin += placeray.direction / 4;
                        Physics.Raycast(placeray, out placehit, 4f);
                    }
                    else
                    {
                        protectiveTimer = 0;
                        break;
                    }
                }
                machine = false;
                if (placehit.collider != null)
                {
                    protectiveTimer = 0;
                    Vector3 thing = placehit.point - (placeray.direction * .1f);
                if (blockstore.itemIDs.Contains(myInv[selected].id) || Input.GetKey(KeyCode.LeftShift) || myInv[selected].id == 0)
                {
                    thing += (placeray.direction * .2f); //go into the block if its an item
                }
                    Vector3 machspot = new Vector3(Mathf.FloorToInt(thing.x), Mathf.FloorToInt(thing.y), Mathf.FloorToInt(thing.z));
                    
                    if (SetBlock((int)machspot.x, (int)machspot.y, (int)machspot.z, myInv[selected].id))
                    {
                    RecheckSurrounders((int)machspot.x, (int)machspot.y, (int)machspot.z);
                        if (myInv[selected].amt > 1)
                        {
                            myInv[selected].amt--;

                        }
                        else
                        {
                            myInv[selected].amt = 0;
                            myInv[selected].id = 0;
                        }
                    }
                    Debug.Log(thing.x + " " + thing.y + " " + thing.z);
                }

        }

        if (Input.GetMouseButtonDown(0)) //left click
        {
                placeray.origin = (transform.position + transform.up) + (Camera.main.transform.forward * 0.5f);
                placeray.direction = Camera.main.transform.forward;
                Physics.Raycast(placeray, out placehit, 4f);
                while (placehit.collider == null)
                {
                    if (protectiveTimer < 2f)
                    {
                        protectiveTimer += Time.deltaTime;
                        placeray.origin += placeray.direction / 4;
                        Physics.Raycast(placeray, out placehit, 4f);
                    }
                    else
                    {
                        protectiveTimer = 0;
                        break;
                    }
                }
                if (placehit.collider != null)
                {
                    protectiveTimer = 0;
                Vector3 thing = placehit.point + (placeray.direction*.5f) ;
                Debug.Log("Is this");
                BreakBlock(Mathf.FloorToInt(thing.x), Mathf.FloorToInt(thing.y), Mathf.FloorToInt(thing.z));
                RecheckSurrounders((int)thing.x, (int)thing.y, (int)thing.z);
            }
        }
    }

    void RecheckSurrounders(int x, int y, int z)
    {
        Vector3 thing = new(x, y, z);
        Vector3[] vecs =
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
        };
        foreach(Vector3 vec in vecs)
        {
            if(draw.worldMachines.ContainsKey(thing + vec))
            {
                if (draw.worldMachines[thing + vec].id == 8)
                {
                    draw.worldMachines[thing + vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }

    }

    void BreakBlock(int x, int y, int z)
    {
        bool ismachine = false;
        Vector3 thing = new(x, y, z);
        Blocks.Block block;
        if(draw.worldMachines.ContainsKey(thing))
        {
            ismachine = true;
            if (draw.worldMachines[thing].id == 6)
            {
                AddToInventory(myInv, blockstore.gearblock.id, 2);
            }
            else
            {
                AddToInventory(myInv, draw.worldMachines[thing].id, 1);
            }
            Destroy(draw.worldMachines[thing].linkedObject);
            draw.worldMachines.Remove(thing);
        } else
        if (thing.y > 0)
        {
            if (thing.x < 0 && thing.z > 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))];
                if (AddToInventory(myInv, block.id, 1))
                {
                    Debug.Log("Added 1 " + block.id + " to inventory");
                }
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x < 0 && thing.z < 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, 15 + (int)(thing.z % 16))];
                if (AddToInventory(myInv, block.id, 1))
                {
                    Debug.Log("Added 1 " + block.id + " to inventory");
                }
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, 15 + (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x > 0 && thing.z < 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 15 + (int)(thing.z % 16))];
                if (AddToInventory(myInv, block.id, 1))
                {
                    Debug.Log("Added 1 " + block.id + " to inventory");
                }
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 15 + (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x > 0 && thing.z > 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))];
                if (AddToInventory(myInv, block.id, 1))
                {
                    Debug.Log("Added 1 " + block.id + " to inventory");
                }
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.air;
            }

        }

        //block = blockstore.air;
        if (!ismachine)
        {
            draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
            if (thing.x > 0 && thing.z > 0)
            {
                if ((int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
            if (thing.x < 0 && thing.z < 0)
            {
                if (15 + (int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
            if (thing.x > 0 && thing.z < 0)
            {
                if ((int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
            if (thing.x < 0 && thing.z > 0)
            {
                if (15 + (int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
        }
        Debug.Log(thing.x + " " + thing.y + " " + thing.z);
    }

    bool AddToInventory(List<ItemSlot> inv, int type, int amount)
    {
        int slottoadd = -1;

        for(int i = 0; i < inv.Count; i++)
        {
            if (inv[i].id == 0 || inv[i].id == type)
            {
                slottoadd = i;
                break;
            } else
            {
                continue;
            }
        }
        if(slottoadd == -1)
        {
            return false;
        } else
        {
            
            inv[slottoadd].id = type;
            inv[slottoadd].amt += amount;
            return true;
        }

    }



    void MovementStuff()
    {
        selected += (int)Input.mouseScrollDelta.y;
        selected = Mathf.Abs(selected) % 7;
        DoBlockPlaceStuff();




        if (Time.time > 20)
        {
            if (!cc.isGrounded && this.transform.position.y > 1)
            {
                timefalling += Time.deltaTime * 230;
                velocity.y -= Time.deltaTime * (15 + timefalling);
                cc.Move(velocity / 100);
            }
        }



        if (velocity != Vector3.zero)
        {

            velocity -= velocity / 15;
            var thing = transform.position + transform.up;
            Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));

        }

        if (cc.isGrounded || this.transform.position.y <= 1)
        {
            timefalling = 0;
            velocity.y = Mathf.Clamp(velocity.y, 0, 10);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y += 20f;
            }
        }

        rotation.x = -Input.GetAxis("Mouse Y");
        rotation.y = Input.GetAxis("Mouse X");
        rotation.z = 0;
        var rot = transform.rotation;
        var rot2 = Camera.main.transform.rotation;
        this.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y + rotation.y, rot.eulerAngles.z + 0);
        Camera.main.transform.rotation = Quaternion.Euler(rot2.eulerAngles.x + rotation.x, rot2.eulerAngles.y + rotation.y, rot2.eulerAngles.z + 0);



        if (Input.GetKey(KeyCode.W))
        {

            walkray.origin = this.transform.position;
            walkray.direction = this.transform.forward;
            Physics.Raycast(walkray, out walkhit, 1f);
            if (walkhit.collider == null)
            {
                movement += (this.transform.forward * Input.GetAxis("Vertical")) / 4;
            }
            if (velocity == Vector3.zero)
            {
                var thing = transform.position + transform.up;
                walktime += Time.deltaTime;
                Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
            }
        }
        if (Input.GetKey(KeyCode.S))
        {

            walkrayback.origin = this.transform.position;
            walkrayback.direction = -1 * this.transform.forward;
            Physics.Raycast(walkrayback, out walkhitback, 1f);
            if (walkhitback.collider == null)
            {
                movement += (this.transform.forward * Input.GetAxis("Vertical")) / 4;
            }
            if (velocity == Vector3.zero)
            {
                var thing = transform.position + transform.up;
                walktime += Time.deltaTime;
                Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
            }
        }
        if (Input.GetKey(KeyCode.A))
        {

            walkrayleft.origin = this.transform.position;
            walkrayleft.direction = -1 * this.transform.right;
            Physics.Raycast(walkrayleft, out walkhitleft, 1f);
            if (walkhitleft.collider == null)
            {
                movement += (this.transform.right * Input.GetAxis("Horizontal")) / 4;
            }
            if (velocity == Vector3.zero)
            {
                var thing = transform.position + transform.up;
                walktime += Time.deltaTime;
                Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
            }
        }
        if (Input.GetKey(KeyCode.D))
        {

            walkrayright.origin = this.transform.position;
            walkrayright.direction = this.transform.right;
            Physics.Raycast(walkrayright, out walkhitright, 1f);
            if (walkhitright.collider == null)
            {
                movement += (this.transform.right * Input.GetAxis("Horizontal")) / 4;

            }
            if (velocity == Vector3.zero)
            {
                var thing = transform.position + transform.up;
                walktime += Time.deltaTime;
                Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
            }
        }
        finalray.origin = transform.position;
        finalray.direction = Vector3.Normalize(movement);
        Physics.Raycast(finalray, out finalhit, 1f);
        if (finalhit.collider == null)
        {
            cc.Move((movement * Time.deltaTime) * 20);
        }
        else
        {
            movement = Vector3.zero;
        }
        movement /= 4;
    }



    private int selected = 0;
    float timefalling = 0;
    Vector3 rotation = new();
    Vector3 movement = new();
    public List<Vector2> neededChunks = new();
    void Update()
    {
        DoSelected();
        MovementStuff();
        if ((int)transform.position.x > 0 && (int)transform.position.z > 0)
        {
            for (int i = ((int)transform.position.x - ((int)transform.position.x%16)) - (6 * 16); i < ((int)transform.position.x - ((int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - ((int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - ((int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }
        }
        else
            if ((int)transform.position.x < 0 && (int)transform.position.z > 0)
        {

            for (int i = ((int)transform.position.x - (15 + (int)transform.position.x % 16)) - (6 * 16); i < ((int)transform.position.x - (15 + (int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - ((int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - ((int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }
        }
        else
            if ((int)transform.position.x < 0 && (int)transform.position.z < 0)
        {
            for (int i = ((int)transform.position.x - (15 + (int)transform.position.x % 16)) - (6 * 16); i < ((int)transform.position.x - (15 + (int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - (15 + (int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - (15 + (int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }

        }
        else if ((int)transform.position.x > 0 && (int)transform.position.z < 0)
        {
            for (int i = ((int)transform.position.x - ((int)transform.position.x % 16)) - (6 * 16); i < ((int)transform.position.x - ((int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - (15 + (int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - (15 + (int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }

        }
    }


    bool SetBlock(int x, int y, int z, int id)
    {
        Vector3 thing = new Vector3(x, y, z);
        Vector3 machspot = thing;
        if (!blockstore.modelIDs.Contains(id) && !blockstore.itemIDs.Contains(id) && myblockpos != machspot && id != 0)
        {
            machine = false;
            if (thing.x < 0 && thing.z > 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.blocks[id];
            }
            if (thing.x < 0 && thing.z < 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, 15 + (int)(thing.z % 16))] = blockstore.blocks[id];
            }
            if (thing.x > 0 && thing.z < 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 15 + (int)(thing.z % 16))] = blockstore.blocks[id];
            }
            if (thing.x > 0 && thing.z > 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.blocks[id];
            }
        }
        else if (!blockstore.itemIDs.Contains(id) && blockstore.modelIDs.Contains(id))
        {
            machine = true;
            if (!draw.worldMachines.ContainsKey(machspot))
            {
                GameObject g = Instantiate(blockstore.IDtoModel[id], machspot, Quaternion.identity);
                Drawer.MachineNode thismach = new Drawer.MachineNode();
                thismach.id = id;
                thismach.linkedObject = g;
                draw.worldMachines.Add(machspot, thismach);

            } else 
            {
                if (draw.worldMachines[machspot].id == blockstore.gearblock.id)
                {
                    if (id == 4)
                    {
                        Destroy(draw.worldMachines[machspot].linkedObject);
                        draw.worldMachines.Remove(machspot);
                        GameObject g = Instantiate(blockstore.IDtoModel[6], machspot, Quaternion.identity);
                        Drawer.MachineNode thismach = new Drawer.MachineNode();
                        thismach.id = 6;
                        thismach.linkedObject = g;
                        draw.worldMachines.Add(machspot, thismach);
                    }
                }
            }
            
            return true;
        }
        else if (blockstore.itemIDs.Contains(id))
        {
            if (id == blockstore.wrenchItem.id)
            {
                if (draw.worldMachines.ContainsKey(thing))
                {
                    if (draw.worldMachines[thing].id == 4)
                    {
                        var t = draw.worldMachines[thing].linkedObject.GetComponent<GearController>().rotationIndex;
                        draw.worldMachines[thing].linkedObject.GetComponent<GearController>().UpdateRotIndex((t + 1) % 6);
                    }
                    if (draw.worldMachines[thing].id == 6)
                    {
                        var t = draw.worldMachines[thing].linkedObject.GetComponent<DoubleGearController>().rotationIndex;
                        var t2 = draw.worldMachines[thing].linkedObject.GetComponent<DoubleGearController>().rotationIndex2;
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            draw.worldMachines[thing].linkedObject.GetComponent<DoubleGearController>().UpdateRot2Index((t2 + 1) % 6);
                        }
                        else
                        {
                            draw.worldMachines[thing].linkedObject.GetComponent<DoubleGearController>().UpdateRot1Index((t + 1) % 6);
                        }
                    }
                }
                
            }
            return false;
        }
        else if(id == 0)
        {
            if (draw.worldMachines.ContainsKey(machspot))
            {
                if (draw.worldMachines[machspot].id == blockstore.leverItem.id)
                {
                    draw.worldMachines[machspot].linkedObject.GetComponent<LeverWorker>().Toggle();
                }
            }
            return false;
        }

        if (!machine)
        {
            draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
            if ((int)(thing.x % 16) == 15)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.x % 16) == 0)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.z % 16) == 15)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.z % 16) == 0)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            return true;
        }
        return false;
       
    }
}