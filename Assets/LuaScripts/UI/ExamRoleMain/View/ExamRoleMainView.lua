--[[
-- added by passion @ 2021/5/31 8:37:43
-- ExamRoleMain视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local ExamRoleMainView = BaseClass("ExamRoleMainView", UIBaseView)
local base = UIBaseView
local function OnCreate(self)
	base.OnCreate(self)
	-- 窗口生命周期内保持的成员变量放这
	self.OpenPanle=UIWindowNames.ExamBagPanle
	UIManager:GetInstance():OpenWindow(UIWindowNames.ExamBagPanle)
	self.BagBtn=self:AddComponent(UIButton,"BagBtn");
	self.BagBtn:SetOnClick(function()
		UIManager:GetInstance():CloseWindow(self.OpenPanle)
		UIManager:GetInstance():OpenWindow(UIWindowNames.ExamBagPanle)
		self.OpenPanle=UIWindowNames.ExamBagPanle
	end)

	self.TitleBtn=self:AddComponent(UIButton,"TitleBtn");
	self.TitleBtn:SetOnClick(function()
		UIManager:GetInstance():CloseWindow(self.OpenPanle)
		UIManager:GetInstance():CloseWindow(UIWindowNames.ExamItemDes)
		UIManager:GetInstance():OpenWindow(UIWindowNames.ExamRoleTitle)
		self.OpenPanle=UIWindowNames.ExamRoleTitle
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


ExamRoleMainView.OnCreate = OnCreate
ExamRoleMainView.OnEnable = OnEnable
ExamRoleMainView.OnDestroy = OnDestroy

return ExamRoleMainView


