--[[
-- added by wsh @ 2017-12-11
-- UILogin模块UILoginView窗口中服务器列表的可复用Item
--]]
local TitleSlot = BaseClass("TitleSlot", UIWrapComponent)
local ItemEntity=require("UI.BagPanle.View.ComponentBase")
local base = UIWrapComponent

-- 创建
local function OnCreate(self)
    base.OnCreate(self)
    -- 组件初始化

    self.titleItem = self:AddComponent(require("UI.RoleTitle.View.TitleItem"), "TitileItem");
    self.titleItem:SetActive(true);
    self.cfgdata = nil   --配置表数据
    self.serverdata=nil  --服务器数据

end

-- 组件被复用时回调该函数，执行组件的刷新
local function OnRefresh(self, real_index, check)
    self.cfgdata = self.view.cfg_data[real_index + 1]; -- 拿到配置表的数据

    self.serverdata=self.view.model.server_data

    self.titleItem:SetData(self.cfgdata,self.serverdata);

end



TitleSlot.OnCreate = OnCreate
TitleSlot.OnRefresh = OnRefresh


return TitleSlot

