--[[
-- added by passion @ 2021/5/31 8:37:25
-- ExamBagPanle视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local ExamBagPanleView = BaseClass("ExamBagPanleView", UIBaseView)
local base = UIBaseView

ExamManager = require("UI.ExamBagPanle.ExamManager")

local itemlist = require("Config.Data.Itemlist")

local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这

    -- 无限滑动
    self.svr_wrapgroup = self:AddComponent(UIWrapGroup, "BagList_Content", require("UI.ExamBagPanle.View.ExamSlotItem"))

    self.svr_wrapgroup:AddButtonGroup(UIToggleButton)
    self.server_list = {}
    for i = 1, 200 do
        self.server_list[i] = {}
    end
    self.BagDataList = {}
    for i = 1, #UserData:GetInstance().EnterComplete.listPlayerBags do
        self.BagDataList[i] = UserData:GetInstance().EnterComplete.listPlayerBags[i];
    end

    self.svr_wrapgroup:SetLength(table.count(self.server_list))
    self.svr_wrapgroup:SetOriginal(-1)
    self.svr_wrapgroup:ResetToBeginning()

    self.FindComponent = FindComponent.ADD(self.gameObject);
    -- 显示全部物品的toggle
    self.Toggle_All = self.FindComponent:GetComponent("Toggle_All", typeof(Toggle));
    self.Toggle_grow = self.FindComponent:GetComponent("Toggle_grow", typeof(Toggle));
    self.Toggle_gift = self.FindComponent:GetComponent("Toggle_gift", typeof(Toggle));
    self.Toggle_home = self.FindComponent:GetComponent("Toggle_home", typeof(Toggle));

    -- 将所有的分类按钮添加到表中  一起添加点击事件
    self.ToggleTab = {self.Toggle_All, self.Toggle_grow, self.Toggle_gift, self.Toggle_home};
    for i = 1, #self.ToggleTab, 1 do
        self.ToggleTab[i].onValueChanged:AddListener(function(a)
            self:Sort(i - 1);
        end)
    end

    self.BagDilatation_text = self:AddComponent(UIText, "BagDilatation_text")
    self.BagDilatation_text:SetText(#self.BagDataList .. "/200")

    self.Player = self.FindComponent:GetComponent("Player", typeof(Transform))
    self.UIEvent = self.FindComponent:GetComponent(self.gameObject.name, typeof(UIEvent));
	
    self.UIEvent:AddFunction(EventTriggerType.Drag, function()
        local x = Input.GetAxis("Mouse X")
		self.Player.gameObject.transform:Rotate(Vector3(0,x,0)*5)
    end)

end

function ExamBagPanleView:Sort(type) -- 分类的方法
    self.server_list = {}
    for i = 1, 200 do
        self.server_list[i] = {}
    end

    self.BagDataList = {}
    if type == 0 then
        for i = 1, #UserData:GetInstance().EnterComplete.listPlayerBags do
            self.BagDataList[i] = UserData:GetInstance().EnterComplete.listPlayerBags[i];
        end
    else
        local nums = 1
        for i = 1, #UserData:GetInstance().EnterComplete.listPlayerBags do
            local num = 0;
            for j = 1, #itemlist, 1 do
                if tonumber(itemlist[j].ItemID) ==
                    math.floor(UserData:GetInstance().EnterComplete.listPlayerBags[i].ItemID) then
                    num = j;
                    break
                end
            end
            if tonumber(itemlist[num].BagType) == type then
                self.BagDataList[nums] = UserData:GetInstance().EnterComplete.listPlayerBags[i];
                nums = nums + 1;
            end
        end
    end
    self.svr_wrapgroup:SetLength(table.count(self.server_list))
    self.svr_wrapgroup:SetOriginal(-1)
    self.svr_wrapgroup:ResetToBeginning()

end
-- 打开
local function OnEnable(self)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这

end
-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

ExamBagPanleView.OnCreate = OnCreate
ExamBagPanleView.OnEnable = OnEnable
ExamBagPanleView.OnDestroy = OnDestroy

return ExamBagPanleView

