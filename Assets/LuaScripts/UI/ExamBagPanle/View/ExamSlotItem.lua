--[[
-- added by wsh @ 2017-12-11
-- UILogin模块UILoginView窗口中服务器列表的可复用Item
--]]
local ExamSlotItem = BaseClass("ExamSlotItem", UIWrapComponent)

local base = UIWrapComponent

-- 创建
local function OnCreate(self)
    base.OnCreate(self)
    -- 组件初始化

    self.bagItem = self:AddComponent(require("UI.ExamBagPanle.View.ExamBagItem"), "pubitem");
    self.bagItem:SetActive(false);
    self.cfgdata = nil

end

-- 组件被复用时回调该函数，执行组件的刷新
local function OnRefresh(self, real_index, check)

    self.cfgdata = self.view.BagDataList[real_index + 1]; -- 无限滑动刷新数据拿到服务器发来的背包数据

    self.bagItem:SetActive(self.cfgdata~=nil);--根据数据来显示

    self.bagItem:SetData(self.cfgdata);

end



ExamSlotItem.OnCreate = OnCreate
ExamSlotItem.OnRefresh = OnRefresh


return ExamSlotItem

