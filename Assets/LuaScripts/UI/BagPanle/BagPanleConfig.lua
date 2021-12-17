--[[
-- added by passion @ 2021/5/20 14:55:41
-- BagPanle模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local BagPanle= {
	Name = UIWindowNames.BagPanle,
	Layer = UILayers.SceneLayer,
	Model = require "UI.BagPanle.Model.BagPanleModel",
	Ctrl =  require "UI.BagPanle.Controller.BagPanleCtrl",
	View = require "UI.BagPanle.View.BagPanleView",
	PrefabPath = "UI/Prefabs/View/BagPanle.prefab",
}


return {
	BagPanle=BagPanle,
}
