--[[
-- added by passion @ 2021/5/31 8:37:25
-- ExamBagPanle模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local ExamBagPanle= {
	Name = UIWindowNames.ExamBagPanle,
	Layer = UILayers.SceneLayer,
	Model = require "UI.ExamBagPanle.Model.ExamBagPanleModel",
	Ctrl =  require "UI.ExamBagPanle.Controller.ExamBagPanleCtrl",
	View = require "UI.ExamBagPanle.View.ExamBagPanleView",
	PrefabPath = "UI/Prefabs/View/ExamBagPanle.prefab",
}


return {
	ExamBagPanle=ExamBagPanle,
}
