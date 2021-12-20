--[[
-- added by wsh @ 2017-11-19
-- 选角场景
--]]

local SelectRoleScence = BaseClass("SelectRoleScence", BaseScene)
local base = BaseScene

-- 创建：准备预加载资源
local function OnCreate(self)
	base.OnCreate(self)
	-- TODO
	self:AddPreloadResource(UIConfig[UIWindowNames.UISelectRole].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
end

-- 准备工作
local function OnComplete(self)
	base.OnComplete(self)
	UIManager:GetInstance():OpenWindow(UIWindowNames.UISelectRole)
end

-- 离开场景
local function OnLeave(self)
	UIManager:GetInstance():CloseWindow(UIWindowNames.UISelectRole)
	base.OnLeave(self)
end

SelectRoleScence.OnCreate = OnCreate
SelectRoleScence.OnComplete = OnComplete
SelectRoleScence.OnLeave = OnLeave

return SelectRoleScence;