using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public GameObject chunk;
    public Dictionary<Vector3, GameObject> chunks = new();
    public Blocks blockstore;

    public Transform player;





    public int dimension = 0;







    public List<GameObject> remeshQueue = new();
    private float remeshTimer = 0;
    public Dictionary<int, Dictionary<Vector2, Dictionary<Vector3, Blocks.Block>>> worldAllChunks = new();





    public Dictionary<int, Dictionary<Vector3, MachineNode>> worldMachines = new();
    
    public struct MachineNode
    {
        public int id;
        public GameObject linkedObject;
        public bool active;
        public float level;
    }
    public GameObject tree;
    // Start is called before the first frame update
    public static float Perlin3D(float x, float y, float z, float scale)
    {
        float XY = Mathf.PerlinNoise(x, y);
        float YZ = Mathf.PerlinNoise(y, z);
        float ZX = Mathf.PerlinNoise(z, x);

        float YX = Mathf.PerlinNoise(y, z);
        float ZY = Mathf.PerlinNoise(z, y);
        float XZ = Mathf.PerlinNoise(x, z);

        float val = (XY + YZ + ZX + YX + ZY + XZ) / 6f;
        return val * scale;
    }
    void Start()
    {
        for (int i = 0; i < 17; i++)
        {
            for(int j = 0; j < 17; j++)
            {
                //GenerateNewWorldDataChunk(i, j);
                GameObject c = Instantiate(chunk, Vector3.zero + new Vector3(i * 16, 0, j * 16), Quaternion.identity);
                chunks.Add(new Vector3(i, 0, j), c);
            }
        }

       /* for(int i = 0; i < 40; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                if(Random.Range(0, 20) < 2)
                {
                    GameObject t = Instantiate(tree, new Vector3(i*20, Random.Range(-20, -10), j*20), Quaternion.identity);
                    t.SetActive(true);
                }
            }
        }*/
        

    }

    private float rtime = 0.015f;
    void remesh()
     {

        if(remeshQueue.Count > 2)
        {
            if(remeshTimer > rtime)
            {
                /*if (Vector3.Distance(player.position, remeshQueue[0].transform.position) < 128)
                {*/
                    remeshQueue[0].GetComponent<ChunkTerrain>().RebuildMesh();
                    remeshQueue[0].GetComponent<ChunkTerrain>().isInRemeshQueue = false;
                    remeshQueue.RemoveAt(0);
                    remeshTimer = 0;
                /*} else
                {
                    var t = remeshQueue[0];
                    remeshQueue.Add(t);
                    remeshQueue.RemoveAt(0); //move it to the end 
                    remeshTimer = 0;
                }*/
                
            } else
            {
                remeshTimer += Time.deltaTime;
            }
        }
    }

    Vector2 cp = new();
    Vector3 vec = new();
    public void GenerateNewWorldDataChunk(int x, int z, int dimen)
    {
        cp.x = x;
        cp.y = z;
        
        var thechunk = new Dictionary<Vector3, Blocks.Block>();

        /*for (int i = 0; i < 16; i++)
        {
            for (int k = 0; k < 16; k++)
            {
                vec.x = i;
                vec.z = k;
                for (int y = 0; y < 30; y++)
                {
                    vec.y = y;
                    thechunk.Add(vec, blockstore.stone);
                }
                var perl = Mathf.PerlinNoise(((x * 16) + vec.x) / 40f, ((z * 16) + vec.z) / 40f);
                    
                for (int h = 0; h < 10 * perl; h++)
                {
                    vec.y = 30 + h;
                    thechunk.Add(vec, blockstore.block);
                    vec.y = 29 + h;
                    thechunk[vec] = blockstore.stone;
                }

            }
        }*/
        if (dimen == 0)
        {
            for (int i = 0; i < 16; i++)
            {

                for (int k = 0; k < 16; k++)
                {
                    for (int j = 0; j < 65; j++)
                    {
                        vec.x = i;
                        vec.y = j;
                        vec.z = k;
                        if (j < (Mathf.PerlinNoise((dimension * 100)+(float)((x * 16) + vec.x) / 30f, (dimension * 100)+(float)((z * 16) + vec.z) / 30f) * 2))
                        {
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.block;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.block);
                            }
                            vec.y = j - 1;
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.dirt;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.dirt);
                            }
                        }
                        else
                        {
                            thechunk.Add(vec, blockstore.air);
                        }

                    }
                }
            }
        } else
        {
            for (int i = 0; i < 16; i++)
            {

                for (int k = 0; k < 16; k++)
                {
                    for (int j = 0; j < 65; j++)
                    {
                        vec.x = i;
                        vec.y = j;
                        vec.z = k;
                        if (j < (Mathf.PerlinNoise((dimension*100)+(float)((x * 16) + vec.x) / 3f, (dimension * 100) + (float)((z * 16) + vec.z) / 3f) * 4))
                        {
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.stone;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.stone);
                            }
                            vec.y = j - 1;
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.dirt;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.dirt);
                            }
                        }
                        else
                        {
                            thechunk.Add(vec, blockstore.air);
                        }

                    }
                }
            }
        }
        if (this.worldAllChunks.ContainsKey(dimen))
        {
            this.worldAllChunks[dimen].Add(cp, thechunk);
        } else
        {
            this.worldAllChunks.Add(dimen, new Dictionary<Vector2, Dictionary<Vector3, Blocks.Block>>());
            this.worldAllChunks[dimen].Add(cp, thechunk);
        }
    }
    bool thisdone = false;
    // Update is called once per frame
    void Update()
    {
        remesh();
        /*if(Time.time > 0.5f && !thisdone)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    chunks[new Vector3(i, 0, j)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            thisdone = true;
        }*/
    }
}
