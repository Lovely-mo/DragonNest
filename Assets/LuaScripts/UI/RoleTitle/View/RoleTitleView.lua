--[[
-- added by passion @ 2021/5/24 21:23:26
-- RoleTitle视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local RoleTitleView = BaseClass("RoleTitleView", UIBaseView)
local base = UIBaseView
local Designation =require("Config.Data.Designation")  --对接服务器的称号配置表
local AttributeList=require("Config.Data.AttributeList")  --称号加强属性的配置表



local SetSerTitleView=nil;


local function OnCreate(self)
	base.OnCreate(self)
	-- 窗口生命周期内保持的成员变量放这


	
	self.ctrl.AddListener();---初始化的时候去侦听消息
	self.ctrl.AskRoleTitle(self.ctrl,1);

	SetSerTitleView=function(index)
		self:TitleShow(index);
	end

	UIManager:GetInstance():AddListener("SetSerTitleView",SetSerTitleView);--绑定设置view层的方法

	----分类中的toggle组
    self.FindComponent = FindComponent.ADD(self.gameObject);
    -- 显示全部物品的toggle
    self.Toggle1 = self.FindComponent:GetComponent("Toggle1", typeof(Toggle));
    self.Toggle1.onValueChanged:AddListener(function(b)
        self.ctrl.AskRoleTitle(self.ctrl,1);
    end)

    self.Toggle2 = self.FindComponent:GetComponent("Toggle2", typeof(Toggle));
    self.Toggle2.onValueChanged:AddListener(function(b)
        self.ctrl.AskRoleTitle(self.ctrl,2);
    end)

    self.Toggle3 = self.FindComponent:GetComponent("Toggle3", typeof(Toggle));
    self.Toggle3.onValueChanged:AddListener(function(b)
        self.ctrl.AskRoleTitle(self.ctrl,3);
    end)

    self.Toggle4 = self.FindComponent:GetComponent("Toggle4", typeof(Toggle));
    self.Toggle4.onValueChanged:AddListener(function(b)
        self.ctrl.AskRoleTitle(self.ctrl,4);
    end)
	
	self.Toggle5 = self.FindComponent:GetComponent("Toggle5", typeof(Toggle));
    self.Toggle5.onValueChanged:AddListener(function(b)
        self.ctrl.AskRoleTitle(self.ctrl,5);
    end)

	self.Toggle6 = self.FindComponent:GetComponent("Toggle6", typeof(Toggle));
    self.Toggle6.onValueChanged:AddListener(function(b)
        self.ctrl.AskRoleTitle(self.ctrl,6);
    end)


	self.svr_wrapgroup = self:AddComponent(UIWrapGroup, "Title_Content", require("UI.RoleTitle.View.TitleSlot"))

    self.svr_wrapgroup:AddButtonGroup(UIToggleButton)
	

end

function RoleTitleView:TitleShow(index)  
	self.list = {}
	local num=1;
	for i = 1, #Designation, 1 do
		if tonumber(Designation[i].Type) ==index then
			self.list[num] = Designation[i]
			num=num+1;
		end
	end

    self.cfg_data={}  --view层type的数据

    self.lists={}--排除服务器数据后剩下的数据

    for i = 1, #self.list, 1 do
        local num=0;
        for j = 1, #self.model.server_data, 1 do
            if tonumber(self.list[i].ID) == tonumber(self.model.server_data[j].designationID)  then
                table.insert(self.cfg_data,self.list[i]);
                num=num+1;
                break;
            end
        end
        if num==0 then
            table.insert(self.lists,self.list[i]);
        end
    end

    table.sort(self.cfg_data,function(a,b)
        return tonumber(a.SortID) < tonumber(b.SortID)
    end)

    table.sort(self.lists,function(a,b)
        return tonumber(a.SortID) < tonumber(b.SortID)
    end)

    for i = 1, #self.lists do
        table.insert(self.cfg_data,self.lists[i]);
    end

    

	self.svr_wrapgroup:SetLength(table.count(self.cfg_data))
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


RoleTitleView.OnCreate = OnCreate
RoleTitleView.OnEnable = OnEnable
RoleTitleView.OnDestroy = OnDestroy

return RoleTitleView


