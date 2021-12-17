--[[
-- added by passion @2021/5/24 21:23:26
-- +RoleTitle控制层
--]]
local RoleTitleCtrl = BaseClass("RoleTitleCtrl", UIBaseCtrl)
local MsgIDMap = require("Net/Config/MsgIDMap")
local MsgIDDefine = require "Net.Config.MsgIDDefine"

local index=1;

local function AddListener()
    --侦听服务器反馈回的事件
    DataManager:GetInstance():AddListener(MsgIDDefine.GetClassifyDesignation, RoleTitleCtrl.Handle_GetClassifyDesignationRes)
end

local function AskRoleTitle(self, type) -- 向服务器请求称号列表
    index=type;
    local msg = {}

    print("开始向服务器发送请求称号列表的消息 ")
    msg = MsgIDMap[MsgIDDefine.GetClassifyDesignation].argMsg
    msg.type=type;
    print("开始向服务器发送请求称号列表的消息    type类型为   ",msg.type);
    HallConnector:GetInstance():SendMessage(MsgIDDefine.GetClassifyDesignation, msg)


    
end

local function Handle_GetClassifyDesignationRes (msg)
    -- body

    print("接收了服务器反馈的消息");
    print("称号列表返回了    ",#msg.dataList)

    UIManager:GetInstance():Broadcast("SetSerTitleModel",msg.dataList,index);
end



RoleTitleCtrl.Handle_GetClassifyDesignationRes = Handle_GetClassifyDesignationRes
RoleTitleCtrl.AskRoleTitle = AskRoleTitle
RoleTitleCtrl.AddListener = AddListener

return RoleTitleCtrl
