--[[
-- added by wsh @ 2017-12-01
-- UILogin控制层
--]]
local UILoginCtrl = BaseClass("UILoginCtrl", UIBaseCtrl)
local MsgIDDefine = require "Net.Config.MsgIDDefine"

local json = require("rapidjson")
local util = require("xlua.util")
local yield_return = (require "cs_coroutine").yield_return
local MsgIDMap = require("Net/Config/MsgIDMap")

local function OnConnect(self, sender, result, msg)
    Logger.Log("连接结果" .. result .. msg)
    if result < 0 then
        Logger.LogError("Connect err : " .. msg)
        return
    end

    -- -- TODO：
    -- local msd_id = MsgIDDefine.LOGIN_REQ_GET_UID
    -- local msg = {}
    -- msg.plat_account = "455445"
    -- msg.from_svrid = 4001
    -- msg.device_id = ""
    -- msg.device_model = "All Series (ASUS)"
    -- msg.mobile_type = ""
    -- msg.plat_token = ""
    -- msg.app_ver = ""
    -- msg.package_id = ""
    -- msg.res_ver = ""
    -- HallConnector:GetInstance():SendMessage(msd_id, msg)

    msg = MsgIDMap[MsgIDDefine.QueryGateArg].argMsg
    msg.type = LoginType_pb.LOGIN_PASSWORD
    msg.platid = PlatType_pb.PLAT_ANDROID
    msg.version = "0.0.0"
    msg.account = "a456456"
    msg.password = "456456"
    msg.openid = "a456456"
    msg.token = ""
    msg.pf = ""

    HallConnector:GetInstance():SendMessage(MsgIDDefine.QueryGateArg, msg)
end

local function OnClose(self, sender, result, msg)
    Logger.Log("连接关闭 result:" .. result .. "msg:" .. msg)
end


local function ConnectServer(self)
    HallConnector:GetInstance():Connect("10.161.21.113", 25001, Bind(self, OnConnect), Bind(self, OnClose))
end
local function LoginDragonServer(self)
    ConnectServer(self)
end

local function LoginServer(self, name, password)
    -- 合法性检验
    if string.len(name) > 20 or string.len(name) < 1 then
        -- TODO：错误弹窗
        Logger.LogError("name length err!")
        return
    end
    if string.len(password) > 20 or string.len(password) < 1 then
        -- TODO：错误弹窗
        Logger.LogError("password length err!")
        return
    end
    -- 检测是否有汉字
    for i = 1, string.len(name) do
        local curByte = string.byte(name, i)
        if curByte > 127 then
            -- TODO：错误弹窗
            Logger.LogError("name err : only ascii can be used!")
            return
        end
    end

    ClientData:GetInstance():SetAccountInfo(name, password)

    -- TODO start socket
    --ConnectServer(self)
    SceneManager:GetInstance():SwitchScene(SceneConfig.HomeScene)
end

local function ChooseServer(self)
    UIManager:GetInstance():OpenWindow(UIWindowNames.UILoginServer)
end
local function SendSelectRoleNew(self)
    local tmpMsg = MsgIDMap[MsgIDDefine.SelectRoleNew].argMsg
    tmpMsg.index = 6---传从0开始的索引
end






UILoginCtrl.LoginServer = LoginServer
UILoginCtrl.ChooseServer = ChooseServer
UILoginCtrl.LoginDragonServer = LoginDragonServer
UILoginCtrl.SendSelectRoleNew = SendSelectRoleNew

return UILoginCtrl
