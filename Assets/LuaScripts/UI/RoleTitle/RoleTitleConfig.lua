--[[
-- added by passion @ 2021/5/24 21:23:26
-- RoleTitle模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local RoleTitle= {
	Name = UIWindowNames.RoleTitle,
	Layer = UILayers.SceneLayer,
	Model = require "UI.RoleTitle.Model.RoleTitleModel",
	Ctrl =  require "UI.RoleTitle.Controller.RoleTitleCtrl",
	View = require "UI.RoleTitle.View.RoleTitleView",
	PrefabPath = "UI/Prefabs/View/RoleTitle.prefab",
}


return {
	RoleTitle=RoleTitle,
}
