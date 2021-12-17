--[[
-- added by wsh @ 2017-12-05
-- 客户端数据
--]]
local ClientData = BaseClass("ClientData", Singleton)

local function __init(self)
    self.app_version = CS.GameChannel.ChannelManager.instance.appVersion
    self.res_version = CS.GameChannel.ChannelManager.instance.resVersion
    self.account = CS.UnityEngine.PlayerPrefs.GetString("account")
    self.password = CS.UnityEngine.PlayerPrefs.GetString("password")
    self.login_server_id = CS.UnityEngine.PlayerPrefs.GetInt("login_server_id")
end

local function SetAccountInfo(self, account, password)
    self.account = account
    self.password = password
    CS.UnityEngine.PlayerPrefs.SetString("account", account)
    CS.UnityEngine.PlayerPrefs.SetString("password", password)
    DataManager:GetInstance():Broadcast(DataMessageNames.ON_ACCOUNT_INFO_CHG, account, password)
end

local function SetLoginServerID(self, id)
    self.login_server_id = id
    CS.UnityEngine.PlayerPrefs.SetInt("login_server_id", id)
    DataManager:GetInstance():Broadcast(DataMessageNames.ON_LOGIN_SERVER_ID_CHG, id)
end

local function SetLoginToken(self, tokenBytes)
    --  self.loginToken = CS.System.Convert.ToBase64String(tokenBytes)
    self.loginToken = tokenBytes
    Logger.Log("loginToken = " .. self.loginToken)
end

-- message LoginGateData{
-- 	optional string ip = 1;
-- 	optional string zonename = 2;
-- 	optional string servername = 3;
-- 	optional int32 port = 4;
-- 	optional int32 serverid = 5;
-- 	optional uint32 state = 6;
-- 	optional uint32 flag = 7;
-- 	optional bool isbackflow = 8;
-- 	optional uint32 backflowlevel = 9;
-- }
local function SetServerInfo(self, RecommandGate)
    --- Logger.Log("ServerInfo = " .. self.RecommandGate)
    self.RecommandGate = {}
    self.RecommandGate.ip = RecommandGate.ip
    self.RecommandGate.zonename = RecommandGate.zonename
    self.RecommandGate.servername = RecommandGate.servername
    self.RecommandGate.port = math.tointeger(RecommandGate.port)
    self.RecommandGate.serverid = math.tointeger(RecommandGate.serverid)
    self.RecommandGate.state = math.tointeger(RecommandGate.state)
    self.RecommandGate.flag = RecommandGate.flag
    self.RecommandGate.isbackflow = RecommandGate.isbackflow
    self.RecommandGate.backflowlevel = RecommandGate.backflowlevel

    Logger.Log("RecommandGate  ip= " .. self.RecommandGate.ip .. " port=" .. self.RecommandGate.port)
end

local function SetSessionAndchallenge(self, msg)
    --- Logger.Log("ServerInfo = " .. self.RecommandGate)
    self.challenge = msg.challenge
    self.session = msg.session

    Logger.Log("self.challenge = " .. self.challenge .. "  self.session = " .. self.session)
end

local function Setloginzoneid(self, loginzoneid)
    --- Logger.Log("ServerInfo = " .. self.RecommandGate)
    self.loginzoneid = loginzoneid

    Logger.Log("self.loginzoneid = " .. self.loginzoneid)
end

ClientData.__init = __init
ClientData.SetAccountInfo = SetAccountInfo
ClientData.SetLoginServerID = SetLoginServerID
ClientData.SetLoginToken = SetLoginToken

ClientData.SetServerInfo = SetServerInfo
ClientData.SetSessionAndchallenge = SetSessionAndchallenge
ClientData.Setloginzoneid = Setloginzoneid

return ClientData
