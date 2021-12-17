--[[
-- added by passion @ 2021/5/31 8:37:36
-- ExamItemDes模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local ExamItemDes= {
	Name = UIWindowNames.ExamItemDes,
	Layer = UILayers.SceneLayer,
	Model = require "UI.ExamItemDes.Model.ExamItemDesModel",
	Ctrl =  require "UI.ExamItemDes.Controller.ExamItemDesCtrl",
	View = require "UI.ExamItemDes.View.ExamItemDesView",
	PrefabPath = "UI/Prefabs/View/ExamItemDes.prefab",
}


return {
	ExamItemDes=ExamItemDes,
}
