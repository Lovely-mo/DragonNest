using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleShow : MonoBehaviour
{
    static string[] Allconstitute;
    static string path = "Equipments/Player/";
    static SkinnedMeshRenderer skinnd;
    static GameObject play;
    static Texture2D[] texture2;
    static List<MySelfConstitute> MySelfconstitute = new List<MySelfConstitute>
    {
        new MySelfConstitute("ZJ_zhanshi_SkinnedMesh","wa_face", "wa_hair01",  "wa_body", "wa_boots", "wa_glove", "wa_leg", "wa_second", "wa_weapon","Point001_zhanshi"),
        new MySelfConstitute("Player_archer_SkinnedMesh","ar_face", "ar_hair",  "ar_body", "ar_boots", "ar_glove", "ar_leg", "ar_second", "ar_weapon","BoxBone01_archer"),
        new MySelfConstitute("Player_sorceress_SkinnedMesh", "so_face", "so_hair_01", "so_body", "so_boots", "so_glove", "so_leg", "so_second", "so_weapon","BoxBone01_sorceress"),
        new MySelfConstitute("Player_cleric_SkinnedMesh","cl_face", "cl_hair_01",  "cl_body", "cl_boots", "cl_glove", "cl_leg", "cl_second", "cl_weapon","BoxBone01_Cleric"),
        new MySelfConstitute("Player_academic_SkinnedMesh", "ac_face", "ac_hair", "ac_body", "ac_boots", "ac_glove", "ac_leg", "ac_second", "ac_weapon","~BoxBone01_academic"),
        new MySelfConstitute("Player_assassin_SkinnedMesh","as_face", "as_hair01",  "as_body", "as_boots", "as_glove", "as_leg", "as_second", "as_weapon","BoxBone01_assassin"),
        new MySelfConstitute("Player_kali_SkinnedMesh","ka_face", "ka_hair",  "ka_body", "ka_boots", "ka_glove", "ka_leg", "ka_second", "ka_weapon","BoxBone02_kali"),
    };
    // Start is called before the first frame update

    public static void Init(int index)
    {
        print(index);
        print(MySelfconstitute[6].Skeleton);
        Allconstitute = new string[10];
        Allconstitute[0] = MySelfconstitute[index].Skeleton;
        Allconstitute[1] = MySelfconstitute[index].Face;
        Allconstitute[2] = MySelfconstitute[index].Hair;
        Allconstitute[3] = MySelfconstitute[index].Body;
        Allconstitute[4] = MySelfconstitute[index].Boots;
        Allconstitute[5] = MySelfconstitute[index].Glove;
        Allconstitute[6] = MySelfconstitute[index].Leg;
        Allconstitute[7] = MySelfconstitute[index].Second;
        Allconstitute[8] = MySelfconstitute[index].Wenpon;
        Allconstitute[9] = MySelfconstitute[index].WEaponPoint;
        step1();
    }
    public static void step1()
    {
        if (play != null)
        {
            GameObject.Destroy(play.gameObject);
        }
        play = Instantiate(Resources.Load<GameObject>("Prefabs/" + Allconstitute[0]));
        skinnd = play.GetComponent<SkinnedMeshRenderer>();
        GameObject wep = Instantiate(Resources.Load<GameObject>(path + Allconstitute[Allconstitute.Length - 2]));
        wep.transform.SetParent(play.transform.Find(Allconstitute[Allconstitute.Length - 1]).transform);
        wep.transform.localPosition = Vector3.zero;
        play.transform.position = new Vector3(0, 0.5f, -8);
        play.transform.Rotate(new Vector3(0, 180, 0));
        Mesh[] mesh = new Mesh[Allconstitute.Length - 3];
        texture2 = new Texture2D[Allconstitute.Length - 3];
        int index = 0;
        for (int i = 1; i < Allconstitute.Length - 2; i++)
        {
            Mesh mesh1;
            mesh1 = Resources.Load<Mesh>(path + Allconstitute[i]);
            mesh[index] = mesh1;
            texture2[index] = Resources.Load<Texture2D>(path + Allconstitute[i]);
            index++;

        }
        step2(mesh);
    }
    public static void step2(Mesh[] meshes)
    {
        CombineInstance[] combines = new CombineInstance[meshes.Length];
        for (int i = 0; i < meshes.Length; i++)
        {
            combines[i] = new CombineInstance();
            combines[i].subMeshIndex = 0;
            combines[i].mesh = meshes[i];
        }
        Mesh newmesh = new Mesh();
        newmesh.CombineMeshes(combines, true, false);
        skinnd.sharedMesh = newmesh;

        skinnd.sharedMaterial = Resources.Load<Material>("Materials/Char/RimSorcer");
        for (int i = 0; i < texture2.Length; i++)
        {
            skinnd.sharedMaterial.SetTexture("_Tex" + i, texture2[i]);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class MySelfConstitute
{
    string skeleton;//骨骼
    string face;//脸
    string hair;//头发

    string body;//身体
    string boots;//鞋子
    string glove;//手套
    string helmet;//头盔
    string leg;//腿
    string second;//副武器
    string wenpon;//主武器
    string WeaponPoint;//主武器挂载点

    public MySelfConstitute(string skeleton, string face, string hair, string body, string boots, string glove, /*string helmet,*/ string leg, string second, string wenpon, string WeaponPoint)
    {
        this.skeleton = skeleton;
        this.hair = hair;
        this.face = face;
        this.body = body;
        this.boots = boots;
        this.glove = glove;
        //this.helmet = helmet;
        this.leg = leg;
        this.second = second;
        this.wenpon = wenpon;
        this.WeaponPoint = WeaponPoint;
    }

    public string Skeleton { get => skeleton; set => skeleton = value; }
    public string Hair { get => hair; set => hair = value; }
    public string Face { get => face; set => face = value; }
    public string Body { get => body; set => body = value; }
    public string Boots { get => boots; set => boots = value; }
    public string Glove { get => glove; set => glove = value; }
    public string Helmet { get => helmet; set => helmet = value; }
    public string Leg { get => leg; set => leg = value; }
    public string Second { get => second; set => second = value; }
    public string Wenpon { get => wenpon; set => wenpon = value; }
    public string WEaponPoint { get => WeaponPoint; set => WeaponPoint = value; }
}
