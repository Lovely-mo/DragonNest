--[[
-- added by passion @ 2021/12/17 19:30:36
-- UIChangeServer视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UIChangeServerView = BaseClass("UIChangeServerView", UIBaseView)
local base = UIBaseView

local ServerListItem = require("UI.UIChangeServer.Component.UIServerListWrapItem")
local ServerInfoItem = require("UI.UIChangeServer.Component.UIServerInfoWrapItem")

local ServerList_content_Path="ServerList/ServerListViewport/ServerListContent"
local ServerInfo_content_Path="ServerInfo/ServerInfoViewport/ServerInfoContent"
local Close_Button_Path = "Close"

local function OnCreate(self)
	base.OnCreate(self)
	self.ServerList_content=self:AddComponent(UIWrapGroup,ServerList_content_Path,ServerListItem)
	self.ServerInfo_content=self:AddComponent(UIWrapGroup,ServerInfo_content_Path,ServerInfoItem)
	self.Close_Button = self:AddComponent(UIButton,Close_Button_Path)

	self.Close_Button:SetOnClick(self,function()
		self.ctrl:CloseWindow()
	end)
	-- 窗口生命周期内保持的成员变量放这
end

local function FreshServerList(self)
	
end

local function FreshServerInfo(self)
	
end

-- 打开
local function OnEnable(self)
	base.OnEnable(self)
	
	-- 窗口关闭时可以清理的成员变量放这
end
-- 关闭
local function OnDestroy(self)
	base.OnDestroy(self)
	-- 清理成员变量
end


UIChangeServerView.OnCreate = OnCreate
UIChangeServerView.OnEnable = OnEnable
UIChangeServerView.OnDestroy = OnDestroy
UIChangeServerView.FreshServerList = FreshServerList
UIChangeServerView.FreshServerInfo = FreshServerInfo

return UIChangeServerView


