
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "MyGame/Create ParameterTable", fileName = "ParameterTable")]
public class EnemyParamTable : ScriptableObject {

    private static readonly string RESOURCE_PATH = "ParameterTable";

    private static EnemyParamTable s_instance = null;
    public static EnemyParamTable Instance
    {
        get
        {
            if(s_instance == null)
            {
                var asset = Resources.Load(RESOURCE_PATH) as EnemyParamTable;
                if (asset == null)
                {
                    Debug.AssertFormat(false, "Missing EnemyParameterTable! path={0}", RESOURCE_PATH);
                    asset = CreateInstance<EnemyParamTable>();
                }
                s_instance = asset;
            }
            return s_instance;
        }
    }

    [SerializeField]
    public List <CloseAttacks> closeAttacks;
    [SerializeField]
    public List <BehindAttacks> behindAttacks;
    [SerializeField]
    public List<RuntoAttack> runtoAttacks;
    [SerializeField]
    public GetParried getParried;
    [SerializeField]
    public ParriedDamage parriedDamage;
    [SerializeField]
    public ShrinkDamage shrinkDamage;
    [SerializeField]
    public MoveAnimation MoveAnimation;

}
