--[[
-- added by wsh @ 2017-11-30
-- UI模块配置表，添加新UI模块时需要在此处加入
--]]

local UIModule = {
	-- 模块 = 模块配置表
	UILogin = require "UI.UILogin.UILoginConfig",
	UILoading = require "UI.UILoading.UILoadingConfig",
	UINoticeTip = require "UI.UINoticeTip.UINoticeTipConfig",
	--选服界面
	 UIChangeServer=require "UI.UIChangeServer.UIChangeServerConfig",
	--新的登录界面
	NewUILogin=require "UI.NewUILogin.NewUILoginConfig",
	--选择角色界面
	UISelectRole=require "UI.UISelectRole.UISelectRoleConfig",
	--AppendCode
}

local UIConfig = {}
for _,ui_module in pairs(UIModule) do 
	for _,ui_config in pairs(ui_module) do
		local ui_name = ui_config.Name
		assert(UIConfig.ui_name == nil, "Aready exsits : "..ui_name)
		if ui_config.View then
			assert(ui_config.PrefabPath ~= nil and #ui_config.PrefabPath > 0, ui_name.." PrefabPath empty.")
		end
		UIConfig[ui_name] = ui_config
	end
end

return ConstClass("UIConfig", UIConfig)