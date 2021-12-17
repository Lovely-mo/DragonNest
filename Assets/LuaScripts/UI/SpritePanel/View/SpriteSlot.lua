--[[
-- added by wsh @ 2017-12-11
-- UILogin模块UILoginView窗口中服务器列表的可复用Item
--]]
local SpriteSlot = BaseClass("SpriteSlot", UIWrapComponent)
local ItemEntity=require("UI.BagPanle.View.ComponentBase")
local base = UIWrapComponent

-- 创建
local function OnCreate(self)
    base.OnCreate(self)
    -- 组件初始化

    self.spriteItem = self:AddComponent(require("UI.SpritePanel.View.SpriteItem"), "spriteItem");
    self.spriteItem:SetActive(true);

end

-- 组件被复用时回调该函数，执行组件的刷新
local function OnRefresh(self, real_index, check)
    self.cfgdata = self.view.server_list[real_index + 1]; -- 拿到配置表的数据

    if self.cfgdata~=nil then
        self.spriteItem:SetData(self.cfgdata,real_index)
    end
    
end
local function OnClick(self, real_index, check)
    
    
end




SpriteSlot.OnCreate = OnCreate
SpriteSlot.OnRefresh = OnRefresh
SpriteSlot.OnClick = OnClick

return SpriteSlot

