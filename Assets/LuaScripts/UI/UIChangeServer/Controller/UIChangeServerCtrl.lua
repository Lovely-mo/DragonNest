--[[
-- added by passion @2021/12/17 19:30:36
-- +UIChangeServer控制层
--]]
local UIChangeServerCtrl = BaseClass("UIChangeServerCtrl", UIBaseCtrl)

local function CloseWindow(self)
    -- body
    UIManager:GetInstance():CloseWindow(UIWindowNames.UIChangeServer)
end

UIChangeServerCtrl.CloseWindow = CloseWindow

return UIChangeServerCtrl