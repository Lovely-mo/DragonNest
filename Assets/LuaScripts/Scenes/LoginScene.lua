--[[
-- added by wsh @ 2017-11-18
-- 登陆场景
--]]

local LoginScene = BaseClass("LoginScene", BaseScene)
local base = BaseScene

-- 创建：准备预加载资源
local function OnCreate(self)
	base.OnCreate(self)
	-- TODO
	print("進入到现在这里")
	-- self:AddPreloadResource(UIConfig[UIWindowNames.UILogin].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
	-- self:AddPreloadResource(UIConfig[UIWindowNames.UILoginServer].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
	self:AddPreloadResource(UIConfig[UIWindowNames.NewUILogin].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
	self:AddPreloadResource(UIConfig[UIWindowNames.UIChangeServer].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
end

-- 准备工作
local function OnComplete(self)
	base.OnComplete(self)
	-- UIManager:GetInstance():OpenWindow(UIWindowNames.UILogin)
	UIManager:GetInstance():OpenWindow(UIWindowNames.NewUILogin)
end

-- 离开场景
local function OnLeave(self)
	-- UIManager:GetInstance():CloseWindow(UIWindowNames.UILogin)
	UIManager:GetInstance():CloseWindow(UIWindowNames.NewUILogin)
	base.OnLeave(self)
end

LoginScene.OnCreate = OnCreate
LoginScene.OnComplete = OnComplete
LoginScene.OnLeave = OnLeave

return LoginScene;