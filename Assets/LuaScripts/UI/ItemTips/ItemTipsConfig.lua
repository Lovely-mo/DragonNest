--[[
-- added by passion @ 2021/5/25 20:25:09
-- ItemTips模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local ItemTips= {
	Name = UIWindowNames.ItemTips,
	Layer = UILayers.SceneLayer,
	Model = require "UI.ItemTips.Model.ItemTipsModel",
	Ctrl =  require "UI.ItemTips.Controller.ItemTipsCtrl",
	View = require "UI.ItemTips.View.ItemTipsView",
	PrefabPath = "UI/Prefabs/View/ItemTips.prefab",
}


return {
	ItemTips=ItemTips,
}
