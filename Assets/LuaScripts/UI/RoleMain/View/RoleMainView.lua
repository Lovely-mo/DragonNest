--[[
-- added by passion @ 2021/5/24 21:10:48
-- RoleMain视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local RoleMainView = BaseClass("RoleMainView", UIBaseView)
local base = UIBaseView
local function OnCreate(self)
	base.OnCreate(self)
	-- 窗口生命周期内保持的成员变量放这
	self.OpenPanle=UIWindowNames.BagPanle
	UIManager:GetInstance():OpenWindow(UIWindowNames.BagPanle)
	----分类中的toggle组
    self.FindComponent = FindComponent.ADD(self.gameObject);
    -- 显示纹章页面的toggle
    self.Toggle_Gad = self.FindComponent:GetComponent("Toggle_Gad", typeof(Toggle));
    self.Toggle_Gad.onValueChanged:AddListener(function(b)
		if self.OpenPanle~=nil then
			UIManager:GetInstance():CloseWindow(self.OpenPanle)
		end
		UIManager:GetInstance():CloseWindow(UIWindowNames.ItemTips)
		UIManager:GetInstance():OpenWindow(UIWindowNames.SpritePanel)
		self.OpenPanle=UIWindowNames.SpritePanel;
    end)

    self.Toggle_long = self.FindComponent:GetComponent("Toggle_long", typeof(Toggle));
    self.Toggle_long.onValueChanged:AddListener(function(b)
        
    end)

    self.Toggle_bag = self.FindComponent:GetComponent("Toggle_bag", typeof(Toggle));
    self.Toggle_bag.onValueChanged:AddListener(function(b)
		if self.OpenPanle~=nil then
			UIManager:GetInstance():CloseWindow(self.OpenPanle)
		end
		UIManager:GetInstance():OpenWindow(UIWindowNames.BagPanle)
		self.OpenPanle=UIWindowNames.BagPanle;
    end)

    self.Toggle_title = self.FindComponent:GetComponent("Toggle_title", typeof(Toggle));
    self.Toggle_title.onValueChanged:AddListener(function(b)
        if self.OpenPanle~=nil then
			UIManager:GetInstance():CloseWindow(self.OpenPanle)
		end
		UIManager:GetInstance():CloseWindow(UIWindowNames.ItemTips)
		UIManager:GetInstance():OpenWindow(UIWindowNames.RoleTitle)
		self.OpenPanle=UIWindowNames.RoleTitle;
    end)
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


RoleMainView.OnCreate = OnCreate
RoleMainView.OnEnable = OnEnable
RoleMainView.OnDestroy = OnDestroy

return RoleMainView


