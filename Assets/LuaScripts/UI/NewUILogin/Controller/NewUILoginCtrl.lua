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
    msg.account = "a456456"
    msg.password = "456456"
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
local function LoginDragonServer(self)
    ConnectServer(self)
end

local function WebRequest(url, callback)
    local co =
        coroutine.create(
        function()
            local request = CS.UnityEngine.Networking.UnityWebRequest.Get(url)
            yield_return(request:SendWebRequest())
            print("连接服务器2")
            if (request.isNetworkError or request.isHttpError) then
                print(request.error)
            else
                callback(request.downloadHandler.text)
                print("" .. request.downloadHandler.text)
            end
        end
    )
    assert(coroutine.resume(co))
end

local function LogindatangServer(self,MySelfName,MySelfPassword)
    --print("登录")
    local appid = ""

    local userId = ""

    local userName = MySelfName--"PC"

    local passWord = MySelfPassword--"123456"

    local tourist = "1"

    local token = ""

    local time = os.time()
    local key = "QYQDGAMEDshEFWOKE7Y6GAEDE-WAN-0668-2625-7DGAMESZEFovDDe777"
    local sign =
        CS.GameUtility.MD5(
        string.format("%s%s%s%s%s%s%s%s", appid, userId, userName, passWord, tourist, token, time, key)
    )

    local url =
        string.format(
        "http://10.161.21.113:8000/login?appid=%s&userId=%s&userName=%s&passWord=%s&tourist=%s&token=%s&time=%s&sign=%s",
        appid,
        userId,
        userName,
        passWord,
        tourist,
        token,
        time,
        sign
    )
    print("url========:" .. url)
    local msg = {}
    WebRequest(
        url,
        function(data)
            print("回来了")
            local jsdata = json.decode(data)
            print(jsdata["userId"])
            self.userId = tonumber(jsdata["userId"])
            self.key = jsdata["key"]
            self.time = tonumber(jsdata["time"])
            self.sign = jsdata["sign"]
            local serList = json.decode(jsdata["serverList"])
            self.serverNo = tonumber(serList[1]["serverNo"])
            self.IP = serList[1]["gameHost"]
            self.port = tonumber(serList[1]["gamePort"])
            local flag = HallConnector:GetInstance():Connect(self.IP, self.port)
            if (flag) then
                print("-----------")
                msg = MsgIDMap[MsgIDDefine.C_LoginGame]
                msg.userId = tonumber(self.userId)
                msg.key = self.key
                msg.time = tostring(self.time)
                msg.sign = self.sign
                msg.serverNo = self.serverNo
                HallConnector:GetInstance():SendMessage(MsgIDDefine.C_LoginGame, msg)
            end
        end
    )
end

NewUILoginCtrl.LoginDragonServer = LoginDragonServer
NewUILoginCtrl.LogindatangServer = LogindatangServer

return NewUILoginCtrl