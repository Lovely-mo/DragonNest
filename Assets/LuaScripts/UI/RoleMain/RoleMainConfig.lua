--[[
-- added by passion @ 2021/5/24 21:10:48
-- RoleMain模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local RoleMain= {
	Name = UIWindowNames.RoleMain,
	Layer = UILayers.SceneLayer,
	Model = require "UI.RoleMain.Model.RoleMainModel",
	Ctrl =  require "UI.RoleMain.Controller.RoleMainCtrl",
	View = require "UI.RoleMain.View.RoleMainView",
	PrefabPath = "UI/Prefabs/View/RoleMain.prefab",
}


return {
	RoleMain=RoleMain,
}
