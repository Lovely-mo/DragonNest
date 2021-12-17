---------------------------------------------------------------------
-- new_xlua-framework (C) CompanyName, All Rights Reserved
-- Created by: AuthorName
-- Date: 2020-04-27 16:39:45
---------------------------------------------------------------------

-- To edit this template in: Data/Config/Template.lua
-- To disable this template, check off menuitem: Options-Enable Template File

---@class ConfigMgr
local ConfigMgr = BaseClass("UIManager", Singleton)

--根据配置文件名称，加载配置表数据
local function GetCfgData(name)
	return  require(string.format("Config/Data/cfg_{0}",name))
end

return ConfigMgr