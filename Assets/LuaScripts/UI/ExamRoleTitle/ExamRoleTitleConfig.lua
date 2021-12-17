--[[
-- added by passion @ 2021/5/31 8:37:52
-- ExamRoleTitle模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local ExamRoleTitle= {
	Name = UIWindowNames.ExamRoleTitle,
	Layer = UILayers.SceneLayer,
	Model = require "UI.ExamRoleTitle.Model.ExamRoleTitleModel",
	Ctrl =  require "UI.ExamRoleTitle.Controller.ExamRoleTitleCtrl",
	View = require "UI.ExamRoleTitle.View.ExamRoleTitleView",
	PrefabPath = "UI/Prefabs/View/ExamRoleTitle.prefab",
}


return {
	ExamRoleTitle=ExamRoleTitle,
}
