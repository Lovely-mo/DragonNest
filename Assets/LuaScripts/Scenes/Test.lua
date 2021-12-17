--[[
-- added by wsh @ 2017-11-18
-- 登陆场景
--]]

local Test = BaseClass("Test", BaseScene)
local base = BaseScene

-- 创建：准备预加载资源
local function OnCreate(self)
	base.OnCreate(self)
	-- TODO
	
end

-- 准备工作
local function OnComplete(self)
	base.OnComplete(self)
	UIManager:GetInstance():OpenWindow(UIWindowNames.RoleMain)
end

-- 离开场景
local function OnLeave(self)
	UIManager:GetInstance():CloseWindow(UIWindowNames.UILogin)
	base.OnLeave(self)
end

Test.OnCreate = OnCreate
Test.OnComplete = OnComplete
Test.OnLeave = OnLeave

return Test;