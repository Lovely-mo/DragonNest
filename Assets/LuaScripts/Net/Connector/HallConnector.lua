--[[
-- Author: passion
-- Date: 2019-09-20 12:00:54
-- LastEditors: passion
-- LastEditTime: 2019-09-26 13:38:32
-- Description: 大厅网络连接器，socket长连接
--]]
local HallConnector = BaseClass("HallConnector", Singleton)
local MsgIDMap = require("Net/Config/MsgIDMap")
local MsgIDDefine = require("Net/Config/MsgIDDefine")
local pb = require("pb")
local LoginHandler = require("Net/Handlers/LoginHandler")

local tagID = 0
---每发一个RPC消息，自增一个，从1开始
local tagID_msgID = {} ---保存tagID和消息的对应关系

local ConnStatus = {
    Init = 0,
    Connecting = 1,
    WaitLogin = 2,
    Done = 3
}

local function __init(self)
    self.hallSocket = nil
    --self.handlers = {}

    --local login = LoginHandler.New();
    --login:Init(self.handlers);
end

local function LZG_OnReceivePackage(self, msg_id, tagID, receive_bytes, isPtc)
    local msg = nil
    Logger.Log(
        "*************!!!!!!!!!!!!!!!   msg_id:" ..
            msg_id ..debug.traceback())
    if msg_id == MsgIDDefine.KeepAlivePingAck then ---KEEP alive  心跳包
        Logger.LogError("recv Keep alive Msg from server")
    else
        if (receive_bytes ~= nil) then
            if (isPtc) then
                if MsgIDMap[msg_id] == nil then
                    print("******  不存在的协议   "..msg_id);
                    return;
                    
                end
                msg = MsgIDMap[msg_id].resMsg
                msg:ParseFromString(receive_bytes)
            else
                local tmp = tagID_msgID[tagID]
                assert(tmp ~= nil, "没有发过对应的rpc消息")
                if msg_id == tmp[2] then
                    msg = tmp[1].resMsg
                    msg:ParseFromString(receive_bytes)
                end
            end
        end
    end
    if msg ~= nil then
        ----	收到网络数据进行网络数据的广播
        DataManager:GetInstance():Broadcast(msg_id, msg)
    end
end

--连接服务器
local function Connect(self, host_ip, host_port, on_connect, on_close)
    if not self.hallSocket then
        self.hallSocket = CS.Networks.HjTcpNetwork()
        self.hallSocket.ReceivePkgHandle = Bind(self, LZG_OnReceivePackage)
    end
    self.hallSocket.OnConnect = on_connect
    self.hallSocket.OnClosed = on_close
    self.hallSocket:SetHostPort(host_ip, host_port)
    self.hallSocket:Connect()
    Logger.Log("Connect to " .. host_ip .. ", port : " .. host_port)

    return self.hallSocket
end

local function SendMessage(self, msg_id, msg)
    local msg_bytes = nil
    if (msg) then
        msg_bytes = msg:SerializeToString() --把pb序列化为字节数组
        if not MsgIDMap[msg_id].isRPC then
            self.hallSocket:SendMessage(msg_id, 0, msg_bytes, true)
        else
            ---RPC需要tagID
            tagID = tagID + 1
            tagID_msgID[tagID] = {MsgIDMap[msg_id], msg_id}
            self.hallSocket:SendMessage(msg_id, tagID, msg_bytes, false)
        end

        Logger.Log("send messge：" .. msg_id .. "  tagID = " .. tagID .. "   byte count=" .. #msg_bytes)
    end
end

local function Update(self)
    if self.hallSocket then
        self.hallSocket:UpdateNetwork()
    end
end

local function Disconnect(self)
    if self.hallSocket then
        self.hallSocket:Disconnect()
    end
end

local function Dispose(self)
    if self.hallSocket then
        self.hallSocket:Dispose()
    end
    self.hallSocket = nil
end

HallConnector.__init = __init
HallConnector.Connect = Connect
HallConnector.SendMessage = SendMessage
HallConnector.Update = Update
HallConnector.Disconnect = Disconnect
HallConnector.Dispose = Dispose

return HallConnector
