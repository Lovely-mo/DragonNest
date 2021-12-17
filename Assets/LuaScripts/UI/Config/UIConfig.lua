--[[
-- added by wsh @ 2017-11-30
-- UI模块配置表，添加新UI模块时需要在此处加入
--]]

local UIModule = {
	-- 模块 = 模块配置表
	UILogin = require "UI.UILogin.UILoginConfig",
	UILoading = require "UI.UILoading.UILoadingConfig",
	UINoticeTip = require "UI.UINoticeTip.UINoticeTipConfig",
	UITestMain = require "UI.UITestMain.UITestMainConfig",
	UIBattle = require "UI.UIBattle.UIBattleConfig",
	UIBoard = require "UI.UIBoard.UIBoardConfig",
	--模块描述
	BagPanle=require "UI.BagPanle.BagPanleConfig",

	
	--RoleMain
	RoleMain=require "UI.RoleMain.RoleMainConfig",
	--RoleMain
	RoleTitle=require "UI.RoleTitle.RoleTitleConfig",
	--模块描述
	ItemTips=require "UI.ItemTips.ItemTipsConfig",

	--模块描述
	SpritePanel=require "UI.SpritePanel.SpritePanelConfig",
	--模块描述
	ExamBagPanle=require "UI.ExamBagPanle.ExamBagPanleConfig",
	--模块描述
	ExamItemDes=require "UI.ExamItemDes.ExamItemDesConfig",
	--模块描述
	ExamRoleMain=require "UI.ExamRoleMain.ExamRoleMainConfig",
	--模块描述
	ExamRoleTitle=require "UI.ExamRoleTitle.ExamRoleTitleConfig",
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