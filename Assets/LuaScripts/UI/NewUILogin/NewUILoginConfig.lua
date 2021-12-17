--[[
-- added by passion @ 2021/12/17 15:17:21
-- NewUILogin模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local NewUILogin= {
	Name = UIWindowNames.NewUILogin,
	Layer = UILayers.SceneLayer,
	Model = require "UI.NewUILogin.Model.NewUILoginModel",
	Ctrl =  require "UI.NewUILogin.Controller.NewUILoginCtrl",
	View = require "UI.NewUILogin.View.NewUILoginView",
	PrefabPath = "UI/Prefabs/View/NewUILogin.prefab",
}


return {
	NewUILogin=NewUILogin,
}
