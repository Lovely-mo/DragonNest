--[[
-- added by passion @2021/12/17 15:17:21
-- +NewUILogin控制层
--]]
local NewUILoginCtrl = BaseClass("NewUILoginCtrl", UIBaseCtrl)
local MsgIDDefine = require "Net.Config.MsgIDDefine"
local json = require("rapidjson")
local util = require("xlua.util")
local yield_return = (require "cs_coroutine").yield_return
local MsgIDMap = require("Net/Config/MsgIDMap")

local MySelfName = "a456456"
local MySelfPassword = "456456"


local function OnConnect(self, sender, result, msg)
    Logger.Log("连接结果" .. result .. msg)
    if result < 0 then
        Logger.LogError("Connect err : " .. msg)
        return
    end

    msg = MsgIDMap[MsgIDDefine.QueryGateArg].argMsg
    msg.type = LoginType_pb.LOGIN_PASSWORD
    msg.platid = PlatType_pb.PLAT_ANDROID
    msg.version = "0.0.0"
    msg.account = MySelfName --"a456456"
    msg.password = MySelfPassword --"456456"
    msg.openid = "a456456"
    msg.token = ""
    msg.pf = ""

    HallConnector:GetInstance():SendMessage(MsgIDDefine.QueryGateArg, msg)
    print("现在是运行完这个了")
end
local function OnClose(self, sender, result, msg)
    Logger.Log("连接关闭 result:" .. result .. "msg:" .. msg)
end
local function ConnectServer(self)
    HallConnector:GetInstance():Connect("10.161.21.113", 25001, Bind(self, OnConnect), Bind(self, OnClose))
end
local function LoginDragonServer(self,selfname,selfpassword)
    ConnectServer(self)
    MySelfName = selfname
    MySelfPassword = selfpassword
end

NewUILoginCtrl.LoginDragonServer = LoginDragonServer

return NewUILoginCtrl