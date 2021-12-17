-- 我是提示的东西
Help = {
    [ [[
        -- 动画组件获取
        self.animator = self.FindComponent:GetComponent(self.player.name, typeof(Animator));
        -- 动画的Trigger
        self.animator:SetTrigger("Hit");
        -- 动画的Bool
        self.animator:SetBool("Run",false);
    ]]] = "",
    [ [[
        -- 相机移动
        local CameraMove = BaseClass("CameraMove", Singleton)

        function CameraMove:__init()
            if self.Update ~= nil then
                self.__update_handle = BindCallback(self, self.Update)
                UpdateManager:GetInstance():AddUpdate(self.__update_handle)
            end
        
            self.speed = 50 -- 滑动速度
            self.scrollSpeed = 60 -- 滚轮速度
            self.space = 50 -- 空间
            self.Min_X = -100 -- X最小
            self.Max_X = 35 -- X最大
            self.Min_Y = 30 -- Y最小
            self.Max_Y = 120 -- Y最大
            self.Min_Z = -50 -- Z最小
            self.Max_Z = 50 -- Z最大
        
            self.Camera = Camera.main
        end
        
        local function IsInScreenSpace()
            return (Input.mousePosition.x >= 0 and Input.mousePosition.x < Screen.width and Input.mousePosition.y >= 0 and
                       Input.mousePosition.y <= Screen.height)
        end
        
        function CameraMove:Update()
            if Input.GetKey(KeyCode.W) or
                (IsInScreenSpace() and Input.mousePosition.y > Screen.height - self.space and Input.mousePosition.y <=
                    Screen.height) then
                self.Camera.transform.position = self.Camera.transform.position + Vector3.back * self.speed * Time.deltaTime
            end
            if Input.GetKey(KeyCode.S) or
                (IsInScreenSpace() and Input.mousePosition.y < self.space and Input.mousePosition.y >= 0) then
                self.Camera.transform.position = self.Camera.transform.position + Vector3.forward * self.speed * Time.deltaTime
            end
            if Input.GetKey(KeyCode.A) or
                (IsInScreenSpace() and Input.mousePosition.x < self.space and Input.mousePosition.x >= 0) then
                self.Camera.transform.position = self.Camera.transform.position + Vector3.right * self.speed * Time.deltaTime
            end
            if Input.GetKey(KeyCode.D) or
                (IsInScreenSpace() and Input.mousePosition.x > Screen.width - self.space and Input.mousePosition.x <=
                    Screen.width) then
                self.Camera.transform.position = self.Camera.transform.position + Vector3.left * self.speed * Time.deltaTime
            end
            local csall = Input.GetAxis("Mouse ScrollWheel")
            if csall ~= 0 then
                self.Camera.transform.position = self.Camera.transform.position + Vector3.up * csall * self.scrollSpeed *
                                                     Time.deltaTime
            end
            local pos = self.Camera.transform.position
            pos.x = Mathf.Clamp(pos.x, self.Min_X, self.Max_X)
            pos.y = Mathf.Clamp(pos.y, self.Min_Y, self.Max_Y)
            pos.z = Mathf.Clamp(pos.z, self.Min_Z, self.Max_Z)
            self.Camera.transform.position = pos
        end
        
        return CameraMove
        
    ]]] = "",
    [ [[
            -- Json解析
            local path = Resources.Load("Configs/json1"):ToString()
		    equipjson = Jsonlist.decode(path)
    ]]] = "",
    [ [[
            ---无限滑动View层
            Table007 = {}

            for i = 1, 100 do
                Table007[i] = {
                    Index = i,
                    Name = i .. "号"
                }
            end
        
            local UIWrapItem = require("UI.WXHDWindow.View.UIWrapItem")
        
            local WXHDWindowView = BaseClass("WXHDWindowView", UIBaseView)
            local base = UIBaseView
            local function OnCreate(self)
            base.OnCreate(self)
            -- 窗口生命周期内保持的成员变量放这
        
            -- 3、服务器列表初始化
            -- A）继承UIWrapComponent去实现子类
            -- B）添加按钮组，area_wrapgroup下所以按钮以UIToggleButton组件实例添加到按钮组
            self.svr_wrapgroup = self:AddComponent(UIWrapGroup, "Panel/Scroll View/Content", UIWrapItem)
            self.svr_wrapgroup:AddButtonGroup(UIToggleButton)
        
        end
        -- 打开
        local function OnEnable(self)
            base.OnEnable(self)
            -- 窗口关闭时可以清理的成员变量放这
        
            self.svr_wrapgroup:SetLength(table.count(Table007)) -- 重新设置数据长度
            self.svr_wrapgroup:SetOriginal(1) ---默认选一个
            self.svr_wrapgroup:ResetToBeginning() ---刷新一下
        
        end
        -- 关闭
        local function OnDestroy(self)
            base.OnDestroy(self)
            -- 清理成员变量
        end
        
        WXHDWindowView.OnCreate = OnCreate
        WXHDWindowView.OnEnable = OnEnable
        WXHDWindowView.OnDestroy = OnDestroy
        
        return WXHDWindowView
    ]]] = "",
	[ [[
        ---无限滑动Item
       
    local UIWrapItem = BaseClass("UIWrapItem", UIWrapComponent)

    local base = UIWrapComponent
    
    -- 创建
    local function OnCreate(self)
        base.OnCreate(self)
        -- 组件初始化
    
        self.FindComponent = FindComponent.ADD(self.gameObject);
        self.Text = self.FindComponent:GetComponent("Text", typeof(Text));
    
        --------
    
    end
    
    -- 组件被复用时回调该函数，执行组件的刷新
    local function OnRefresh(self, real_index, check)
    
        self.Text.text = Table007[real_index + 1].Name
    
    end
    
    -- 组件添加了按钮组，则按钮被点击时回调该函数
    local function OnClick(self, toggle_btn, real_index, check)
        print(toggle_btn, " | ", real_index, " | ", check)
    end
    
    UIWrapItem.OnCreate = OnCreate
    UIWrapItem.OnRefresh = OnRefresh
    UIWrapItem.OnClick = OnClick
    
    return UIWrapItem
    ]]] = "",
	 [ [[
        ---Tips跟随
        local tipsPos=UIManager:GetInstance().UICamera:WorldToScreenPoint(self.root.Tips.gameObject.transform.position)
        local tipsv3=Vector3(CS.UnityEngine.Input.mousePosition.x,CS.UnityEngine.Input.mousePosition.y,tipsPos.z)
        self.root.Tips.transform.position=UIManager:GetInstance().UICamera:ScreenToWorldPoint(tipsv3)
    ]]] = "",
	[ [[
        --点乘正常显示血条
        if Vector3.Dot(self.root.Car.transform.forward,self.gameObject.transform.position - self.root.Car.transform.position) > 0 then
			self.Slider.gameObject:SetActive(true)
			local v3 = Camera.main:WorldToScreenPoint(self.gameObject.transform.position + Vector3(0, 4, 0)); -- 世界转屏幕坐标
			self.Slider.transform.position = v3;
		else
			self.Slider.gameObject:SetActive(false)
		end
    ]]]="",
    [ [[
        --鼠标点击射线nav
        self.Player = GameObject.Find("Car")--寻找主角赛车
        self.FindComponent = FindComponent.ADD(self.Player)
	    self.nav = self.FindComponent:GetComponent(self.Player.name, typeof(NavMeshAgent))
        if Input.GetMouseButtonUp(0) then
            local var = LuaTool.GetMouseRayPos()
            self.nav:SetDestination(var)
        end

    ]]]="",
    [ [[
        --鼠标点击射线
        if Input.GetMouseButtonUp(0) then
            local var = LuaTool.GetMouseRayPos()
            self.pos = var;
        end
        if self.pos then
            self.Player.transform.position = Vector3.Lerp(self.Player.transform.position,self.pos,Time.deltaTime)
        end
    ]]]="",
    [ [[
        -- Etc摇杆
        self.Etc = GameObject.Find("New Joystick"):GetComponent(typeof(CS.ETCJoystick));
        self.Etc.onMove:AddListener(
            function (a)
                self.Player.transform.position = self.Player.transform.position + Vector3(a.x,0,a.y)*0.02;
            end
        )
    ]]]="",
    [ [[
        ---图片加载
        self.Image001 = self.FindComponent:GetComponent("Image001", typeof(Image));
        ResourcesManager:GetInstance():LoadAsync("UI/Atlas/Group/ui_group_02.png", typeof(Sprite), function(go)
            if IsNull(go) then
                return;
            end
            self.Image001.sprite = go;
        end)
    ]]] = "",
    [ [[
        -- 碰撞
        self.FindComponent = FindComponent.ADD(self.gameObject)
        self.FindComponent:ADDCollisionFun(CS.PZenum.OnCollisionEnter, function(My, Other, d)
        local var = Enemyss[Other];
        if var then
            var:Del()
        end
    end)
    ]]] = "",
    [ [[
        -- 加载Update
        if self.Update ~= nil then
            self.__update_handle = BindCallback(self, self.Update)
            UpdateManager:GetInstance():AddUpdate(self.__update_handle)
        end
    ]]] = "",
    [ [[
        -- player move
        if self.Player then
            local h = Input.GetAxis("Horizontal");
            local v = Input.GetAxis("Vertical");
    
            if (h ~= 0 or v ~= 0) then
                local targetDirection = Vector3(h, 0, v);
                self.Player.transform.position = self.Player.transform.position + targetDirection * 0.2;
            else
    
            end
        end
    ]]] = "",
    [ [[
        -- 坐标转换
        local itemPos = UIManager:GetInstance().UICamera:WorldToScreenPoint(self.root.DragImage.gameObject.transform.position);
        local tempV3 = Vector3(CS.UnityEngine.Input.mousePosition.x, CS.UnityEngine.Input.mousePosition.y, itemPos.z);
        self.root.DragImage.transform.position = UIManager:GetInstance().UICamera:ScreenToWorldPoint(tempV3);
    ]]] = "",
    [ [[
        -- 相机跟随
        Camera.gameObject.transform:SetParent(self.Point.gameObject.transform) -- 认父级
        Camera.gameObject.transform.position = self.Point.gameObject.transform.position + Vector3(-0.02, 6, -15); -- 相机位置
    ]]] = "",
    [ [[
        -- 删除Update
        if self.__update_handle ~= nil then
            UpdateManager:GetInstance():RemoveUpdate(self.__update_handle)
            self.__update_handle = nil
        end
    ]]] = "",
    [ [[
        -- 注册消息
        local function OnAddListener(self)
        UIManager:GetInstance():AddListener("得分", self.NowGrade, self)
        end

        -- 注销消息
        local function OnRemoveListener(self)
        UIManager:GetInstance():RemoveListener("得分", self.NowGrade)
        end
    end)
    ]]] = "",
    [ [[
        -- Broadcast
        UIManager:GetInstance():Broadcast("得分")
    end)
    ]]] = "",
    [ [[
        -- 人物移动
        if self.Player then
            local h = Input.GetAxis("Horizontal");
            local v = Input.GetAxis("Vertical");

            if (h ~= 0 or v ~= 0) then
                local targetDirection = Vector3(h, 0, v);
                self.Player.transform.position = self.Player.transform.position + targetDirection * 0.3;
            else

            end
        end
    end)
    ]]] = "",
    [ [[
        --几秒实例化
        Invoke(function()
    
        end, 1, 200)
    
    end)
    ]]] = "",
	[ [[ 
    ----- 拖拽
        self.beginPos = Vector2()
        -- 角色挂载的位置
        self.modelPos = self.FindComponent:GetComponent("modelPos", typeof(Transform))
        self.UIEvent = self.FindComponent:GetComponent(self.gameObject.name, typeof(UIEvent));
        self.UIEvent:AddFunction(EventTriggerType.BeginDrag, function()
         print("开始拖住啊")
          self.beginPos = Input.mousePosition
        end)

        self.UIEvent:AddFunction(EventTriggerType.Drag, function()
        -- print("拖拽中")
        local var = self.beginPos - Input.mousePosition

            self.modelPos.eulerAngles = Vector3(0, var.x, 0)
        end)

        self.UIEvent:AddFunction(EventTriggerType.EndDrag, function()
            print("拖拽结束")
        end)
    ]]]="",
	[ [[
    --平滑移动
        self.MaxTime = 5--最大时间
        self.Speed = 0--速度
        self.state = false--是否停止
    end
    -- Update 角色移动
    function Demo_Scene_1:Update()
        if Input.GetKey(KeyCode.W) then
            self.state = false
            if self.Speed < self.MaxTime then
                self.Speed = self.Speed + Time.deltaTime
            else
                self.Speed = self.MaxTime
            end
            self.Player.transform.position = self.Player.transform.position + self.Player.transform.forward *
                                                 (0.3 * (self.Speed / self.MaxTime))
        end
        if Input.GetKeyUp(KeyCode.W) then
            self.state = true
        end
        if self.state == true then
            if self.Speed > 0 then
                self.Speed = self.Speed - Time.deltaTime
            else
                self.Speed = 0
            end
            self.Player.transform.position = self.Player.transform.position + self.Player.transform.forward *
                                                 (0.3 * (self.Speed / self.MaxTime))
        end
        if Input.GetKey(KeyCode.S) then
            self.state = false
            if self.Speed < self.MaxTime then
                self.Speed = self.Speed + Time.deltaTime
            else
                self.Speed = self.MaxTime
            end
            self.Player.transform.position = self.Player.transform.position - self.Player.transform.forward *
                                                 (0.3 * (self.Speed / self.MaxTime))
        end
        if Input.GetKey(KeyCode.A) then
            self.Player.transform:Rotate(-Vector3.up * Time.deltaTime * 50)
        end
        if Input.GetKey(KeyCode.D) then
            self.Player.transform:Rotate(Vector3.up * Time.deltaTime * 50)
        end
        if Input.GetKey(KeyCode.Space) then
            self.Speed = 0
        end
    end

    ]]]="",
    texture = "",
    sprite = "",
    SetActive = "",
    Instantiate = "",
    GetInstance = "",
    WorldToScreenPoint = "",
    AddComponent = "",
    UIManager_GetInstance__UICamera_ScreenToWorldPoint = {},

    [ [[
    --Dropdown
                    self.FindComponent = FindComponent.ADD(self.gameObject);
                    self.Dropdown = self.FindComponent:GetComponent(self.Text.name, typeof(Dropdown));
                    self.Dropdown.onValueChanged:AddListener(function(str)
                        print(str)
                    end)
        --------
    ]]] = "",

    [ [[
--InputField
                self.FindComponent = FindComponent.ADD(self.gameObject);
                self.InputField = self.FindComponent:GetComponent(self.Text.name, typeof(InputField));
                self.InputField.onValueChanged:AddListener(function(str)
                    print(str)
                end)
--------
    ]]] = "",

    [ [[
--Button
        self.FindComponent = FindComponent.ADD(self.gameObject);
        self.Btn001 = self.FindComponent:GetComponent(self.Text.name, typeof(Button));
        self.Btn001.onClick:AddListener(function()
            print(self.ID, "  点击ID 和 button  ", self.Text.text)
        end)
--------
    ]]] = "",

    [ [[
--Camera.targetTexture
        self.Camera = self.FindComponent:GetComponent("Camera",typeof(Camera));
        self.RawImage = self.FindComponent:GetComponent("RawImage",typeof(RawImage));
        self.Camera.targetTexture = CS.UnityEngine.RenderTexture(300, 300, 0);
        self.RawImage.texture = self.Camera.targetTexture;
--------
    ]]] = "",

    [ [[
--UIEvent
        self.UIEvent = self.FindComponent:GetComponent("UIEvent",typeof(UIEvent));
        self.UIEvent:AddFunction(EventTriggerType.PointerEnter, function()
        end)
---------
    ]]] = "",

    [ [[
--Glob.UIDrag
        -- 找到移动节点
        self.Centre = self.FindComponent:GetComponent("Window", typeof(Transform));
        -- 找到触发面板
        self.UIEvent = self.FindComponent:GetComponent("MouseMove", typeof(UIEvent));
    
        Glob.UIDrag:Open(self.Centre, self.UIEvent);
---------
    ]]] = "",

    [ [[
---UIContentReuseScroll
        ---------------------------------------------------------------

        local wrap_class = Glob.lplus.class(UIContentReuse)
        function wrap_class:Init()
            self.Text = self.transform:GetChild(0):GetComponent(typeof(Text));
        end
        function wrap_class:refresh(ID, userTableList)
            self.Text.text = userTableList[ID].Des;
        end
    
        ---------------------------------------------------------------
    
        local table001 = {};
        for i = 1, 5000 do
            table001[i] = {
                Index = i,
                Des = i .. "号"
            };
        end
    
        local UIWrapGroup = Glob.UIContentReuseScroll.New();
        local gameObject = self.FindComponent:GetComponent("Content", typeof(Transform)).gameObject;
        UIWrapGroup:SetSelf(gameObject)
        UIWrapGroup:Start(wrap_class);
        UIWrapGroup:ResetLength(table001);
------------
    ]]] = "",

    -- 克隆物体
    ["GameObject.Instantiate(self.rootself.itemObj,rootTran)"] = "",

    [ [[
---Json 文件读写
        JsonTool.WriteFileJson("INI/CacheLogicServerIP.json", {
            IP = "",
            Port = ""
        })
        
        local ddd = JsonTool.ReadFileJson("INI/CacheLogicServerIP.json")
        print(ddd.IP)  ;
        print( ddd.Port);
-------
    ]]] = "",

    ---双击
    [ [[
--double-click

    if (Input.GetKeyDown(keyCode)) then
        t2 = Time.realtimeSinceStartup;
        if ((t2 - t1) < 0.2) then
            -- do
        end
        t1 = t2;
    else

    t2 = t2 + Time.deltaTime;
end

    ]]] = "",

    [ [[
        
        --- 显示图标钱的用法
        self.moneyShows = Glob.MoneyShows.New();
        self.moneyShows:SetSelf(self.FindComponent:GetComponent("MoneyShows",typeof(Transform)).gameObject);
        self.moneyShows:Init();
        self.moneyShows:SetMoney(money);

    ]]] = "",

    -- CS_UnityEngine_EventSystems_EventTriggerType=Help.EventTriggerType;
    EventTriggerType = {
        -- //     Intercepts a IPointerEnterHandler.OnPointerEnter.
        PointerEnter = 0,
        -- //     Intercepts a IPointerExitHandler.OnPointerExit.
        PointerExit = 1,
        -- //     Intercepts a IPointerDownHandler.OnPointerDown.
        PointerDown = 2,
        -- //     Intercepts a IPointerUpHandler.OnPointerUp.
        PointerUp = 3,
        -- //     Intercepts a IPointerClickHandler.OnPointerClick.
        PointerClick = 4,
        -- //     Intercepts a IDragHandler.OnDrag.
        Drag = 5,
        -- //     Intercepts a IDropHandler.OnDrop.
        Drop = 6,
        -- //     Intercepts a IScrollHandler.OnScroll.
        Scroll = 7,
        -- //     Intercepts a IUpdateSelectedHandler.OnUpdateSelected.
        UpdateSelected = 8,
        -- //     Intercepts a ISelectHandler.OnSelect.
        Select = 9,
        -- //     Intercepts a IDeselectHandler.OnDeselect.
        Deselect = 10,
        -- //     Intercepts a IMoveHandler.OnMove.
        Move = 11,
        -- //     Intercepts IInitializePotentialDrag.InitializePotentialDrag.
        InitializePotentialDrag = 12,
        -- //     Intercepts IBeginDragHandler.OnBeginDrag.
        BeginDrag = 13,
        -- //     Intercepts IEndDragHandler.OnEndDrag.
        EndDrag = 14,
        -- //     Intercepts ISubmitHandler.Submit.
        Submit = 15,
        -- //     Intercepts ICancelHandler.OnCancel.
        Cancel = 16
    },

    --- 我是矩阵
    transform = {
        localPosition = Help.Vector3,
        eulerAngles = Help.Vector3,
        localEulerAngles = Help.Vector3,
        right = Help.Vector3,
        up = Help.Vector3,
        forward = Help.Vector3,
        rotation = "",
        -- 我是世界为之信息
        position = Help.Vector3,
        localRotation = "",
        parent = Help.transform,
        worldToLocalMatrix = "",
        localToWorldMatrix = "",
        root = "",
        childCount = "",
        lossyScale = "",
        hasChanged = "",
        localScale = Help.Vector3,
        hierarchyCapacity = "",
        hierarchyCount = "",
        DetachChildren = "",
        Find = "",
        FindChild = "",
        GetChild = "",
        GetChildCount = "",
        GetEnumerator = "",
        GetSiblingIndex = "",
        InverseTransformDirection = "",
        InverseTransformPoint = "",
        InverseTransformVector = "",
        IsChildOf = "",
        LookAt = "",
        Rotate = "",
        RotateAround = "",
        SetAsFirstSibling = "",
        SetAsLastSibling = "",
        SetParent = "",
        SetPositionAndRotation = "",
        TransformDirection = "",
        TransformPoint = "",
        TransformVector = "",
        Translate = ""
    },

    HideFlags = {
        -- //
        -- // 摘要:
        -- //     A normal, visible object. This is the default.
        None = 0,
        -- //
        -- // 摘要:
        -- //     The object will not appear in the hierarchy.
        HideInHierarchy = 1,
        -- //
        -- // 摘要:
        -- //     It is not possible to view it in the inspector.
        HideInInspector = 2,
        -- //
        -- // 摘要:
        -- //     The object will not be saved to the Scene in the editor.
        DontSaveInEditor = 4,
        -- //
        -- // 摘要:
        -- //     The object is not be editable in the inspector.
        NotEditable = 8,
        -- //
        -- // 摘要:
        -- //     The object will not be saved when building a player.
        DontSaveInBuild = 16,
        -- //
        -- // 摘要:
        -- //     The object will not be unloaded by Resources.UnloadUnusedAssets.
        DontUnloadUnusedAsset = 32,
        -- //
        -- // 摘要:
        -- //     The object will not be saved to the Scene. It will not be destroyed when a new
        -- //     Scene is loaded. It is a shortcut for HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor
        -- //     | HideFlags.DontUnloadUnusedAsset.
        DontSave = 52,
        -- //
        -- // 摘要:
        -- //     The GameObject is not shown in the Hierarchy, not saved to to Scenes, and not
        -- //     unloaded by Resources.UnloadUnusedAssets.
        HideAndDontSave = 61
    },

    gameObject = {
        name = "",
        hideFlags = Help.HideFlags,
        Destroy = "",
        DestroyImmediate = "",
        DestroyObject = "",
        DontDestroyOnLoad = "",
        FindObjectOfType = "",
        FindObjectsOfType = "",
        FindObjectsOfTypeAll = "",
        FindObjectsOfTypeIncludingAssets = "",
        FindSceneObjectsOfType = "",
        Instantiate = "",
        Equals = "",
        GetHashCode = "",
        GetInstanceID = "",
        ToString = "",
        ----------------------------------------------------------------
        gameObject = Help.gameObject,
        transform = Help.transform,
        layer = "",
        activeInHierarchy = "",
        isStatic = "",
        tag = "",
        scene = "",
        rigidbody = "",
        rigidbody2D = "",
        camera = "",
        light = "",
        animation = "",
        constantForce = "",
        renderer = "",
        audio = "",
        guiText = "",
        networkView = "",
        guiElement = "",
        guiTexture = "",
        collider = "",
        collider2D = "",
        hingeJoint = "",
        particleSystem = "",
        CreatePrimitive = "",
        Find = "",
        FindGameObjectsWithTag = "",
        FindGameObjectWithTag = "",
        FindWithTag = "",
        AddComponent = "",
        BroadcastMessage = "",
        CompareTag = "",
        GetComponent = "",
        GetComponentInChildren = "",
        GetComponentInParent = "",
        GetComponents = "",
        GetComponentsInChildren = "",
        GetComponentsInParent = "",
        PlayAnimation = "",
        SampleAnimation = "",
        SendMessage = "",
        SendMessageUpwards = "",
        SetActive = "",
        SetActiveRecursively = ""

    },

    Vector3 = {
        x = "",
        y = "",
        z = "",
        zero = "",
        forward = "",
        back = "",
        right = "",
        down = "",
        left = "",
        positiveInfinity = "",
        up = "",
        negativeInfinity = "",
        fwd = "",
        sqrMagnitude = "",
        normalized = "",
        magnitude = "",
        Angle = "",
        AngleBetween = "",
        ClampMagnitude = "",
        Cross = "",
        Distance = "",
        Dot = "",
        Exclude = "",
        Lerp = "",
        Magnitude = "",
        Max = "",
        Min = "",
        MoveTowards = "",
        Normalize = "",
        OrthoNormalize = "",
        Project = "",
        ProjectOnPlane = "",
        Reflect = "",
        RotateTowards = "",
        Scale = "",
        SignedAngle = "",
        Slerp = "",
        SlerpUnclamped = "",
        SmoothDamp = "",
        SqrMagnitude = ""

    },

    image = {
        defaultETC1GraphicMaterial = "",
        alphaHitTestMinimumThreshold = "",
        useSpriteMesh = "",
        mainTexture = "",
        hasBorder = "",
        pixelsPerUnit = "",
        material = "",
        minWidth = "",
        preferredWidth = "",
        flexibleWidth = "",
        minHeight = "",
        preferredHeight = "",
        flexibleHeight = "",
        layoutPriority = "",
        fillOrigin = "",
        overrideSprite = "",
        sprite = ""
    },

    [ [[
        self.UIEvent = self.FindComponent:GetComponent(self.go.name,typeof(UIEvent));
    self.UIEvent:AddFunction(EventTriggerType.PointerEnter, function()
        print("进入",self.go);
    end)
    self.UIEvent:AddFunction(EventTriggerType.PointerExit, function()
        print("离开",self.go);
    end)
    self.UIEvent:AddFunction(EventTriggerType.PointerUp, function()
        print("鼠标抬起",self.go);
    end)
    self.UIEvent:AddFunction(EventTriggerType.BeginDrag, function()
        print("拖拽开始",self.go);
    end)
    self.UIEvent:AddFunction(EventTriggerType.Drag, function()
        print("拖拽中",self.go);
    end)
    self.UIEvent:AddFunction(EventTriggerType.EndDrag, function()
        print("拖拽结束",self.go);
    end)
    ]]]="",

    [ [[
        self.Tab={}   
        self.Monster=self:AddComponent(UIBaseContainer,"Monster");
        local child_count = self.ItemManager.transform.childCount
        for i = 0, child_count - 1 do  
            local var =self.Monster:AddComponent(MonsterItem, i)
            table.insert(self.Tab,var);
        end
    ]]]="",
    [ [[   --背包拖拽
        self.UIEvent = self:AddComponent(UIOnClickEvent, self.gameObject.name);
    self.UIEvent:SetOnClick(EventTriggerType.PointerEnter, function()
        print("点击开始");
    end)
    ---------

    self.UIEvent:SetOnClick(EventTriggerType.PointerEnter, function()
        -- print("进入",self.go);
        BagManager:GetInstance().EndItem = self;
    end)
    self.UIEvent:SetOnClick(EventTriggerType.PointerExit, function()
        -- print("离开",self.go);
        BagManager:GetInstance().EndItem = nil;
    end)
    self.UIEvent:SetOnClick(EventTriggerType.PointerUp, function()
        -- print("鼠标抬起",self.go);
    end)
    self.UIEvent:SetOnClick(EventTriggerType.BeginDrag, function()
        -- print("拖拽开始",self.go);
        if self.data.ConfigID ~= 0 then
            BagManager:GetInstance().BeginItem = self;

            if BagManager:GetInstance().DragImage then
                BagManager:GetInstance().DragImage.gameObject:SetActive(true);
                BagManager:GetInstance().DragImage.sprite = self.Icon.sprite;
            end
        else
            BagManager:GetInstance().BeginItem = nil;
            if BagManager:GetInstance().DragImage then
                BagManager:GetInstance().DragImage.gameObject:SetActive(false);
            end
        end
    end)
    self.UIEvent:SetOnClick(EventTriggerType.Drag, function()
        -- print("拖拽中",self.go);
        if BagManager:GetInstance().DragImage then
            local itemPos = UIManager:GetInstance().UICamera:WorldToScreenPoint(
                                BagManager:GetInstance().DragImage.gameObject.transform.position)

            local tempV3 =
                Vector3(CS.UnityEngine.Input.mousePosition.x, CS.UnityEngine.Input.mousePosition.y, itemPos.z)
            BagManager:GetInstance().DragImage.transform.position =
                UIManager:GetInstance().UICamera:ScreenToWorldPoint(tempV3)
        end

    end)
    self.UIEvent:SetOnClick(EventTriggerType.EndDrag, function()
        -- print("拖拽结束",self.go);
        if (BagManager:GetInstance().DragImage) then
            BagManager:GetInstance().DragImage.gameObject:SetActive(false);
        end
        if BagManager:GetInstance().BeginItem == nil then
            return;
        end

        if BagManager:GetInstance().EndItem then
            if BagManager:GetInstance().BeginItem.data.index ~= BagManager:GetInstance().EndItem.data.index then
                BagManager:GetInstance():Exchange(BagManager:GetInstance().BeginItem.data.index,
                    BagManager:GetInstance().EndItem.data.index);
            end
        end
        BagManager:GetInstance().BeginItem = nil;
    end)
    ]]]="",
    [ [[   --字符串切割
        function bagItem:getSplitStr(logStr, breakpointsStr, t)
            local i = 0
            local j = 1
            local z = string.len(breakpointsStr)
            while true do
                i = string.find(logStr, breakpointsStr, i + 1) -- 查找下一行
                if i == nil then
                    table.insert(t, string.sub(logStr, j, -1))
                    break
                end
                table.insert(t, string.sub(logStr, j, i - 1))
                j = i + z
            end
            return t
            
        end
    ]]]="",

}
-- ss = self.transform.position;
return Help;
