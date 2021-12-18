--[[
-- added by passion @ 2021/12/18 10:00:51
-- UISelectRole模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local UISelectRole= {
	Name = UIWindowNames.UISelectRole,
	Layer = UILayers.SceneLayer,
	Model = require "UI.UISelectRole.Model.UISelectRoleModel",
	Ctrl =  require "UI.UISelectRole.Controller.UISelectRoleCtrl",
	View = require "UI.UISelectRole.View.UISelectRoleView",
	PrefabPath = "UI/Prefabs/View/UISelectRole.prefab",
}


return {
	UISelectRole=UISelectRole,
}
