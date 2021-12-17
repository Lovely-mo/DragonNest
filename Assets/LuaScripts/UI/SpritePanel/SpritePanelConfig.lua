--[[
-- added by passion @ 2021/5/28 18:42:38
-- SpritePanel模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local SpritePanel= {
	Name = UIWindowNames.SpritePanel,
	Layer = UILayers.SceneLayer,
	Model = require "UI.SpritePanel.Model.SpritePanelModel",
	Ctrl =  require "UI.SpritePanel.Controller.SpritePanelCtrl",
	View = require "UI.SpritePanel.View.SpritePanelView",
	PrefabPath = "UI/Prefabs/View/SpritePanel.prefab",
}


return {
	SpritePanel=SpritePanel,
}
