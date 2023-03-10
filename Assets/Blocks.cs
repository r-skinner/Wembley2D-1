using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public struct Block
    {
        public int id;
        public Vector2 topTex;
        public Vector2 bottomTex;
        public Vector2 sidesTex;
        public int fol;
    }

    public Drawer draw;

    public Dictionary<int, Block> blocks = new Dictionary<int, Block>();
    public Block air;
    public Block block;
    public Block stone;
    public Block dirt;

    public Block well;
    public GameObject wellobj;

    public Block trunk;
    public GameObject trunkobj;


    public Block shaft;
    public GameObject shaftobj;

    public Block gearblock;
    public GameObject gear;

    public GameObject doublegear;

    public Block wrenchItem;

    public Block leverItem;
    public GameObject lever;

    public Block pipeItem;
    public GameObject pipe;


    public Block gunItem;
    public GameObject bullet;

    public Block lanternItem;
    public GameObject lantern;

    public Block springPadItem;
    public GameObject springPad;

    public Block wood;

    public Block water;


    public Dictionary<int, GameObject> IDtoModel = new();

    public List<int> modelIDs = new();

    public List<int> itemIDs = new();


    public List<int> clearIDs = new();
    








    public List<Vector3> verts = new();
    public List<int> tris = new();
    public List<Vector2> uvs = new();
    void Start() 
    {
        air = new Block();
        air.id = 0;
        blocks.Add(air.id, air);
        air.fol = 0;

        block = new Block();
        block.id = 1;
        block.topTex = new Vector2(0, 0);
        block.sidesTex = new Vector2(1f/16f, 0);
        block.bottomTex = new Vector2(2f/16f, 0);
        blocks.Add(block.id, block);
        block.fol = 0;

        

        stone = new Block();
        stone.id = 2;
        stone.topTex = new Vector2(7f/16f, 0);
        stone.sidesTex = new Vector2(7f / 16f, 0);
        stone.bottomTex = new Vector2(7f / 16f, 0);
        blocks.Add(stone.id, stone);
        stone.fol = 0;

        dirt = new Block();
        dirt.id = 3;
        dirt.topTex = new Vector2(2f / 16f, 0);
        dirt.sidesTex = new Vector2(2f / 16f, 0);
        dirt.bottomTex = new Vector2(2f / 16f, 0);
        blocks.Add(dirt.id, dirt);
        dirt.fol = 0;

        gearblock = new Block();
        gearblock.id = 4;
        blocks.Add(gearblock.id, gearblock);
        modelIDs.Add(4);
        IDtoModel.Add(4, gear);

        wrenchItem = new Block();
        wrenchItem.id = 5;
        blocks.Add(wrenchItem.id, wrenchItem);
        itemIDs.Add(wrenchItem.id);


        IDtoModel.Add(6, doublegear);
        //6 is double gear



        leverItem = new Block();
        leverItem.id = 7;
        blocks.Add(leverItem.id, leverItem);
        modelIDs.Add(7);
        IDtoModel.Add(7, lever);

        pipeItem = new Block();
        pipeItem.id = 8;
        blocks.Add(pipeItem.id, pipeItem);
        modelIDs.Add(8);
        IDtoModel.Add(8, pipe);

        well = new Block();
        well.id = 9;
        blocks.Add(well.id, well);
        modelIDs.Add(9);
        IDtoModel.Add(well.id, wellobj);

        trunk = new Block();
        trunk.id = 10;
        blocks.Add(trunk.id, trunk);
        modelIDs.Add(10);
        IDtoModel.Add(trunk.id, trunkobj);

        shaft = new Block();
        shaft.id = 11;
        shaft.topTex = new Vector2(0, 2f / 16f);
        shaft.sidesTex = new Vector2(0, 2f / 16f);
        shaft.bottomTex = new Vector2(0, 2f / 16f);
        blocks.Add(shaft.id, shaft);
        modelIDs.Add(11);
        IDtoModel.Add(shaft.id, shaftobj);


        gunItem = new Block();
        gunItem.id = 12;
        blocks.Add(gunItem.id, gunItem);
        itemIDs.Add(gunItem.id);

        lanternItem = new Block();
        lanternItem.id = 13;
        blocks.Add(lanternItem.id, lanternItem);
        modelIDs.Add(13);
        IDtoModel.Add(lanternItem.id, lantern);


        springPadItem = new Block();
        springPadItem.id = 14;
        blocks.Add(springPadItem.id, springPadItem);
        modelIDs.Add(14);
        IDtoModel.Add(springPadItem.id, springPad);

        wood = new Block();
        wood.id = 16;
        wood.topTex = new Vector2(9f/16f, 0);
        wood.sidesTex = new Vector2(9f/16f, 0);
        wood.bottomTex = new Vector2(9f/16f, 0);
        blocks.Add(wood.id, wood);

        water = new Block();
        water.id = 17;
        water.topTex = new Vector2(11f / 16f, 0);
        water.sidesTex = new Vector2(11f / 16f, 0);
        water.bottomTex = new Vector2(11f / 16f, 0);
        blocks.Add(water.id, water);

        clearIDs.Add(air.id);
        clearIDs.Add(water.id);

        //building the mesh for lever

        verts.Add(new Vector3(0, 0, 0)); //0
        verts.Add(new Vector3(0, 0.2f, 0)); //1
        verts.Add(new Vector3(1, 0.2f, 0)); //2
        verts.Add(new Vector3(1, 0, 0)); //3

        verts.Add(new Vector3(0, 0.2f, 1)); //4
        verts.Add(new Vector3(0, 0, 1)); //5

        verts.Add(new Vector3(1, 0.2f, 1)); //6
        verts.Add(new Vector3(1, 0, 1)); //7

        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0.5f, 1));
        uvs.Add(new Vector2(0.5f, 1));

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 2f / 16f));

        uvs.Add(new Vector2(0.5f, 0));
        uvs.Add(new Vector2(0.5f, 2f / 16f));



        tris.Add(0);// front
        tris.Add(1);
        tris.Add(2);
        tris.Add(0);
        tris.Add(2);
        tris.Add(3);

        tris.Add(5); // left
        tris.Add(4);
        tris.Add(1);
        tris.Add(5); 
        tris.Add(1);
        tris.Add(0);

        tris.Add(1); // top
        tris.Add(4);
        tris.Add(2);
        tris.Add(2);
        tris.Add(4);
        tris.Add(6);

        tris.Add(3); //right
        tris.Add(2);
        tris.Add(6);
        tris.Add(3);
        tris.Add(6);
        tris.Add(7);

        tris.Add(7); // back
        tris.Add(6);
        tris.Add(4);
        tris.Add(7);
        tris.Add(4);
        tris.Add(5);
        Mesh mesh = new Mesh();

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        lever.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh = mesh;
        lever.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

        springPad.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh = mesh;
        springPad.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;


        verts.Clear();
        tris.Clear();
        uvs.Clear();

        verts.Add(new Vector3(0, 0, 0)); //0
        verts.Add(new Vector3(0, 1, 0)); //1
        verts.Add(new Vector3(0, 0, 0.5f)); //2
        verts.Add(new Vector3(0, 1, 0.5f)); //3

        verts.Add(new Vector3(0.5f, 0, 0)); //4
        verts.Add(new Vector3(0.5f, 1, 0)); //5
        verts.Add(new Vector3(0.5f, 0, 0.5f)); //6
        verts.Add(new Vector3(0.5f, 1, 0.5f)); //7

        uvs.Add(new Vector2(0.5f, 0));
        uvs.Add(new Vector2(0.5f, 1));
        uvs.Add(new Vector2(0.5f+(2f/16f), 0));
        uvs.Add(new Vector2(0.5f + (2f / 16f), 1));
        uvs.Add(new Vector2(0.5f + (2f / 16f), 0));
        uvs.Add(new Vector2(0.5f + (2f / 16f), 1));
        uvs.Add(new Vector2(0.5f, 0));
        uvs.Add(new Vector2(0.5f, 1));

        tris.Add(0); //front
        tris.Add(1);
        tris.Add(5);
        tris.Add(0);
        tris.Add(5);
        tris.Add(4);

        tris.Add(2); //left
        tris.Add(3);
        tris.Add(1);
        tris.Add(2);
        tris.Add(1);
        tris.Add(0);

        tris.Add(4);
        tris.Add(5); //right
        tris.Add(7);
        tris.Add(4);
        tris.Add(7);
        tris.Add(6);

        tris.Add(6);
        tris.Add(7); //back
        tris.Add(3);
        tris.Add(6);
        tris.Add(3);
        tris.Add(2);

        tris.Add(1);
        tris.Add(3);
        tris.Add(7); //top
        tris.Add(1);
        tris.Add(7);
        tris.Add(5);

        Mesh mesh1 = new Mesh();
        mesh1.vertices = verts.ToArray();
        mesh1.triangles = tris.ToArray();
        mesh1.uv = uvs.ToArray();
        mesh1.RecalculateNormals();
        lever.transform.GetChild(1).GetChild(0).gameObject.GetComponent<MeshFilter>().mesh = mesh1;
        lever.transform.GetChild(1).GetChild(0).gameObject.GetComponent<MeshCollider>().sharedMesh = mesh1;


        for (int i = -20; i < 20; i++)
        {
            for (int j = -20; j < 20; j++)
            {
                draw.GenerateNewWorldDataChunk(i, j, 0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
