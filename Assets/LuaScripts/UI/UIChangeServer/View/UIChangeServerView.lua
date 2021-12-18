--[[
-- added by passion @ 2021/12/17 19:30:36
-- UIChangeServer视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UIChangeServerView = BaseClass("UIChangeServerView", UIBaseView)
local base = UIBaseView


local Left_Scoll_content_Path=""
local Right_Scoll_content_Path=""




local function OnCreate(self)
	base.OnCreate(self)
	self.left_Scoll=self:AddComponent(UIWrapGroup,Left_Scoll_content_Path)
	self.right_scoll=self:AddComponent(UIWrapGroup,Right_Scoll_content_Path)

	-- 窗口生命周期内保持的成员变量放这
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


UIChangeServerView.OnCreate = OnCreate
UIChangeServerView.OnEnable = OnEnable
UIChangeServerView.OnDestroy = OnDestroy

return UIChangeServerView


