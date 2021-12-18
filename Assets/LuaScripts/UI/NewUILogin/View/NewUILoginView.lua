--[[
-- added by passion @ 2021/12/17 15:17:21
-- NewUILogin视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local NewUILoginView = BaseClass("NewUILoginView", UIBaseView)
local base = UIBaseView

local AccountInput = "ContentRoot/AccountRoot/AccountInput"
local PasswordInput = "ContentRoot/PasswordRoot/PasswordInput"
local SvrSelectBtn = "ContentRoot/SvrRoot/SvrSelectBtn"
local LoginBtn = "ContentRoot/LoginBtn"

local function OnCreate(self)
	base.OnCreate(self)
	-- 窗口生命周期内保持的成员变量放这
	print("进入的是新的UILogin")
	self.AccountInput = self:AddComponent(UIInput, AccountInput)
	self.PasswordInput = self:AddComponent(UIInput, PasswordInput)
	self.SvrSelectBtn = self:AddComponent(UIButton, SvrSelectBtn)
	self.LoginBtn = self:AddComponent(UIButton, LoginBtn)
end
-- 打开
local function OnEnable(self)
	base.OnEnable(self)
	-- 窗口关闭时可以清理的成员变量放这
	self.SvrSelectBtn:SetOnClick(function ()
		print("点击了选服界面")
		UIManager:GetInstance():OpenWindow(UIWindowNames.UIChangeServer)
	end)
	self.LoginBtn:SetOnClick(function ()
		print("点击了登录按钮")
		self.ctrl:LoginDragonServer()
	end)
end
-- 关闭
local function OnDestroy(self)
	base.OnDestroy(self)
	-- 清理成员变量
end


NewUILoginView.OnCreate = OnCreate
NewUILoginView.OnEnable = OnEnable
NewUILoginView.OnDestroy = OnDestroy

return NewUILoginView


