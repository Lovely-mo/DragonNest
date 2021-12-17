--[[
-- added by wsh @ 2017-12-11
-- UILogin模块UILoginView窗口中服务器列表的可复用Item
--]]
local ExamTitleSlot = BaseClass("ExamTitleSlot", UIWrapComponent)

local base = UIWrapComponent

-- 创建
local function OnCreate(self)
    base.OnCreate(self)
    -- 组件初始化

    self.examTitleItem = self:AddComponent(require("UI.ExamRoleTitle.View.ExamTitleItem"), "TitileItem");


end

-- 组件被复用时回调该函数，执行组件的刷新
local function OnRefresh(self, real_index, check)

    self.cfgdata = self.view.server_list[real_index + 1]; -- 无限滑动刷新数据拿到服务器发来的背包数据

    self.serverdata=self.view.model.titleList;

    self.examTitleItem:SetData(self.cfgdata,self.serverdata);

end



ExamTitleSlot.OnCreate = OnCreate
ExamTitleSlot.OnRefresh = OnRefresh


return ExamTitleSlot

