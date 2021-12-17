--[[
-- added by passion @2021/5/31 8:37:52
-- +ExamRoleTitle控制层
--]]
local ExamRoleTitleCtrl = BaseClass("ExamRoleTitleCtrl", UIBaseCtrl)

local MsgIDDefine = require("Net/Config/MsgIDDefine")
local MsgIDMap = require("Net/Config/MsgIDMap")

local index=1

local function AddListener()
    DataManager:GetInstance():AddListener(MsgIDDefine.GetClassifyDesignation, ExamRoleTitleCtrl.Handle_GetClassifyDesignationRes)
end

local function AskTitleList(type)
    index=type
    local msg={};
    msg=MsgIDMap[MsgIDDefine.GetClassifyDesignation].argMsg
    msg.type=type;

    print("开始发送消息");
    HallConnector:GetInstance():SendMessage(MsgIDDefine.GetClassifyDesignation, msg);
end

local function Handle_GetClassifyDesignationRes(msg)
    UIManager:GetInstance():Broadcast("TitleReturnModel",msg.dataList,index);
end

ExamRoleTitleCtrl.Handle_GetClassifyDesignationRes=Handle_GetClassifyDesignationRes
ExamRoleTitleCtrl.AskTitleList=AskTitleList
ExamRoleTitleCtrl.AddListener=AddListener

return ExamRoleTitleCtrl