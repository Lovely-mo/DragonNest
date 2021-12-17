--[[
-- added by passion @ 2021/5/31 8:37:43
-- ExamRoleMain模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local ExamRoleMain= {
	Name = UIWindowNames.ExamRoleMain,
	Layer = UILayers.SceneLayer,
	Model = require "UI.ExamRoleMain.Model.ExamRoleMainModel",
	Ctrl =  require "UI.ExamRoleMain.Controller.ExamRoleMainCtrl",
	View = require "UI.ExamRoleMain.View.ExamRoleMainView",
	PrefabPath = "UI/Prefabs/View/ExamRoleMain.prefab",
}


return {
	ExamRoleMain=ExamRoleMain,
}
