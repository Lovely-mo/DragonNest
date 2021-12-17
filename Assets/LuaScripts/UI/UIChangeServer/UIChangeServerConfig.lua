--[[
-- added by passion @ 2021/12/17 19:30:36
-- UIChangeServer模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local UIChangeServer= {
	Name = UIWindowNames.UIChangeServer,
	Layer = UILayers.TipLayer,
	Model = require "UI.UIChangeServer.Model.UIChangeServerModel",
	Ctrl =  require "UI.UIChangeServer.Controller.UIChangeServerCtrl",
	View = require "UI.UIChangeServer.View.UIChangeServerView",
	PrefabPath = "UI/Prefabs/View/UIChangeServer.prefab",
}


return {
	UIChangeServer=UIChangeServer,
}
