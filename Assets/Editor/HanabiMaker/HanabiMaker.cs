using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.VFX;


public class HanabiMaker : EditorWindow
{
    [Header("UIElements")]
    //Father Box
    private VisualElement HanabiScene;
    private VisualElement AllIn;
    //ObjectField
    private ObjectField Hanabi;
    private ObjectField Audience;
    //Properties
    private FloatField _allInTime;
    private FloatField _delayTime;
    private IntegerField _allInRate;
    //Scripts
    private HanabiTakaiManager _manager;
    
    
    [MenuItem("Window/UI Toolkit/HanabiMaker")]
    public static void ShowExample()
    {
        HanabiMaker wnd = GetWindow<HanabiMaker>();
        wnd.titleContent = new GUIContent("HanabiMaker");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/HanabiMaker/HanabiMaker.uxml");
        visualTree.CloneTree(root);
        GetElements(root);
        DataBinding();
    }

    private void GetElements(VisualElement root)
    {
        //先获取两个父盒子
        HanabiScene=root.Q<VisualElement>("HanabiScene");
        AllIn=root.Q<VisualElement>("AllIn");
        //获取到ObjectField
        Hanabi=HanabiScene.Q<ObjectField>("Hanabi");
        Hanabi.objectType = typeof(Hanabi);
        Audience=HanabiScene.Q<ObjectField>("Audience");
        Audience.objectType = typeof(Audience);
        //获取IntegerField
        _allInTime = AllIn.Q<FloatField>("AllInTime");
        _delayTime = AllIn.Q<FloatField>("DelayTime");
        _allInRate = AllIn.Q<IntegerField>("AllInRate");
    }

    private void DataBinding()
    {
        FindManager(out _manager);
        if (_manager != null)
        {
            var so =new SerializedObject(_manager);
            Hanabi.Bind(so);
            Audience.Bind(so);
            _allInTime.Bind(so);
            _delayTime.Bind(so);
            _allInRate.Bind(so);
        }
    }

    private void FindManager(out HanabiTakaiManager manager)
    {
        manager=FindObjectOfType<HanabiTakaiManager>() != null ? FindObjectOfType<HanabiTakaiManager>() : null;
    }
}