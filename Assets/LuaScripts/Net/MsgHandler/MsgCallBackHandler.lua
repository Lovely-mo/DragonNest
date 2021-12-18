---------------------------------------------------------------------
-- new_xlua-framework (C) CompanyName, All Rights Reserved
-- Created by: AuthorName
-- Date: 2020-04-29 22:12:39
---------------------------------------------------------------------
-- To edit this template in: Data/Config/Template.lua
-- To disable this template, check off menuitem: Options-Enable Template File
---@class MsgCallBackHandler
local MsgCallBackHandler = {}
local MsgIDDefine = require("Net/Config/MsgIDDefine")
local MsgIDMap = require("Net/Config/MsgIDMap")

local SpriteTable=require("Config.Data.SpriteTable")




---------------连接成功的回调函数-------------------
local function OnConnect(self, sender, result, msg)
    Logger.Log("连接结果" .. result)
    -- if result < 0 then
    --     Logger.LogError("Connect err : " )
    --     return
    -- end
end
-------------------------连接失败的回调函数----------------------
local function OnClose(self, sender, result, msg)
    Logger.Log("连接关闭 result:" .. result)
end
-- -- message QueryGateRes{
-- -- 	optional bytes loginToken = 1;
-- -- 	optional bytes gateconfig = 2;
-- -- 	optional string userphone = 3;
-- -- 	optional LoginGateData RecommandGate = 4;
-- -- 	repeated SelfServerData servers = 5;
-- -- 	optional uint32 loginzoneid = 6;
-- -- 	repeated LoginGateData allservers = 7;
-- -- 	optional bool in_white_list = 8;
-- -- 	optional PlatNotice notice = 9;
-- -- 	optional ErrorCode error = 10;
-- -- 	optional PlatBanAccount baninfo = 11;
-- -- 	optional bool freeflow = 12;  //是否免流量
-- -- 	optional int32 cctype = 13;
-- -- 	repeated PlatFriendServer platFriendServers = 14;
-- -- 	repeated uint32 bespeakserverids = 15;
-- -- }

-- 处理授权消息的返回
local function Handle_QueryGateArg(msg)
    Logger.Log("龙之谷的消息，切换下场景")
    print("连接新的服务器")
    ClientData:GetInstance():SetLoginToken(msg.loginToken)
    ClientData:GetInstance():SetServerInfo(msg.RecommandGate)
    ClientData:GetInstance():Setloginzoneid(msg.loginzoneid)
    local server = ClientData:GetInstance().RecommandGate
    print(server.ip.."连接信息"..server.port)
    HallConnector:GetInstance():Connect(server.ip, server.port, Bind(self, OnConnect), Bind(self, OnClose))

    ---	SceneManager:GetInstance():SwitchScene(SceneConfig.HomeScene)
end

-- -- message ErrorInfo{
-- -- 	optional uint32 errorno = 1;
-- -- 	repeated uint32 param = 2;
-- -- 	optional uint64 param64 = 3;
-- -- 	optional bool istip = 4;
-- -- }
---------------------------处理服务器有错误等情况时，返回的消息--------------------------------
local function Handle_ErrorInfo(msg)
    Logger.Log("服务器 error = " .. tostring(msg.errorno) .. "   param=" .. tostring(msg.param))
end

-- -- message LoginChallenge{
-- -- 	optional string challenge = 1;
-- -- 	optional uint64 session = 2;
-- -- }

-- -- message LoginArg{
-- -- 	optional uint32 gameserverid = 1;
-- -- 	optional bytes token = 2;
-- -- 	optional string ios = 3;
-- -- 	optional string android = 4;
-- -- 	optional string pc = 5;
-- -- 	optional string openid = 6;
-- -- 	optional ClientInfo clientInfo = 7;
-- -- 	optional uint32 loginzoneid = 8;
-- -- }

-- -- rpcC2T_ClientLoginRequest.oArg.token = Convert.FromBase64String(XSingleton<XClientNetwork>.singleton.XLoginToken);
-- -- rpcC2T_ClientLoginRequest.oArg.gameserverid = XSingleton<XClientNetwork>.singleton.ServerID;
-- -- rpcC2T_ClientLoginRequest.oArg.openid = XSingleton<XLoginDocument>.singleton.OpenID;
-- -- rpcC2T_ClientLoginRequest.oArg.loginzoneid = XSingleton<XLoginDocument>.singleton.LoginZoneID;
-- -- rpcC2T_ClientLoginRequest.oArg.pc = "0.0.0";

-- -- UserData = require("DataCenter.UserData.UserData")

----============ 拿到授权后，连接上游戏服务器后，由服务器主动下发的第一个消息，保存里面的session，用于以后的短线重连   ============
local function Handle_LoginChallenge(msg)
    Logger.Log("LoginChallenge 消息   challenge= " .. msg.challenge .. "   session=" .. tostring(msg.session))
    print("现在是在登录进行登录")
    ClientData:GetInstance():SetSessionAndchallenge(msg)

    local clientdata = ClientData:GetInstance()
    if ClientData:GetInstance().loginToken ~= nil then
        local tmpMsg = MsgIDMap[MsgIDDefine.LoginArg].argMsg

        -----向游戏服务器发送LoginArg 登录消息，带着token等信息
        -- tmpMsg.token = CS.System.Convert.FromBase64String(clientdata.loginToken)
        tmpMsg.token = clientdata.loginToken
        tmpMsg.gameserverid = clientdata.RecommandGate.serverid
        tmpMsg.openid = "a456456" ---openid必须需要使用这个，内部授权账户
        tmpMsg.loginzoneid = clientdata.loginzoneid
        tmpMsg.pc = "0.0.0"

        HallConnector:GetInstance():SendMessage(MsgIDDefine.LoginArg, tmpMsg)
    end
end

-----------=======  loginArg的对应返回消息，登录返回消息，里面包含角色信息，=======================================
local function Handle_LoginRes(msg)

    UIManager:GetInstance():OpenWindow(UIWindowNames.UISelectRole)
    Logger.Log("msg.accountData.account = " .. msg.accountData.account .. "  selectSlot =  " ..
                   msg.accountData.selectSlot .. " result = " .. msg.result .. "  is_backflow_server =" ..
                   tostring(msg.data.is_backflow_server) .. " backflow_level =" .. msg.data.backflow_level)

    for i = 1, 9 do
        local role1bytes = msg.accountData["role" .. i]
        ----accountData里包含的是拥有的角色的具体信息。里面包含role1 ,role2，role3 ,…………,role9
        if #role1bytes > 0 then
            local m = RoleBriefInfo_pb.RoleBriefInfo()
            ----由于role1  到role9都是bytes类型，即c#的byte[] 。  需要使用  RoleBriefInfo_pb.RoleBriefInfo反序列化成角色信息
            m:ParseFromString(role1bytes)
            ------打印每个角色信息
            Logger.Log(i.."  角色  name= " .. m.name .. "  type = " .. m.type .. "  roleID=" .. tostring(m.roleID) ..
                           "   level = " .. m.level)
        end
    end

    -- ------------------------发送选角色的消息给服务器----------------------------
    -- local tmpMsg = MsgIDMap[MsgIDDefine.SelectRoleNew].argMsg
    -- tmpMsg.index = 6 ---传从0开始的索引
    -- HallConnector:GetInstance():SendMessage(MsgIDDefine.SelectRoleNew, tmpMsg)

    --------------发送创建角色的消息----------------------------
    --  local tmpMsg = MsgIDMap[MsgIDDefine.CreateRoleNew].argMsg
    --  tmpMsg.type = RoleBriefInfo_pb.Role_Archer ---传从0开始的索引
    --  tmpMsg.name = "测试的名字";
    --  HallConnector:GetInstance():SendMessage(MsgIDDefine.SelectRoleNew, tmpMsg)
end

-----------选角色信息的返回消息，里面的result是我们要的，代表结果，具体值的含义 看ErrorCode.proto的pb文件，  0为成功, 211代表服务器没有这个角色----------
local function Handle_SelectRoleNewRes(msg)
    local str = '有这个角色';
    if math.modf(msg.result) == 211 then
        str = "服务器没有这个角色";
    end
    Logger.Log("msg.result = " .. tostring(msg.result) .. "   reason = " .. msg.reason .. "   " .. str)

end

------------含义暂时未知，，发完SelectRoleNewArg后，由服务器回传。------------------------
local function Handle_IBGiftIcon(msg)
    Logger.Log("msg.status = " .. tostring(msg.status))
end

-----------------含义暂时未知，，发完SelectRoleNewArg后，由服务器回传。-------------------------------
local function Handle_NotifyStartUpTypeToClient(msg)
    Logger.Log("Handle_NotifyStartUpTypeToClient msg.type = " .. tostring(msg.type))
end


----------非常重要的消息，进游戏后所有的初始数据都在这里，，
----------涉及的pb协议很多，搞了很久才把它的.proto文件挨个找出生成.lua的，
----------因为。lua有local变量不能超过200个的限制，需把协议拆要分
local function Handle_SelectRoleNtfData(msg)
    local roleData = msg.roleData
    Logger.Log("Handle_SelectRoleNtfData   lv = " .. tostring(roleData.Brief.level) .. "   name = " ..
                   roleData.Brief.name .. "  serverId = " .. msg.serverid .. "  is frist= " ..
                   tostring(msg.backflow_firstenter) .. "  bag" .. roleData.Bag.Equips[1].uid)

    UserData:GetInstance().EnterComplete.listPlayerBags = roleData.Bag.Items;

    


    print("精灵的第一个                 ",roleData.SpriteRecord.SpriteData[1].SpriteID)
    print("精灵的第一个uid                 ",roleData.SpriteRecord.SpriteData[1].uid)
    print("精灵的第一个  AttrID                 ",roleData.SpriteRecord.SpriteData[1].AttrID)
    for i = 1, #roleData.SpriteRecord.SpriteData[1].AttrID, 1 do
        print("roleData.SpriteRecord.SpriteData[1].AttrID  ",roleData.SpriteRecord.SpriteData[1].AttrID[i])
    end
    print("精灵的第一个  AttrValue                 ",roleData.SpriteRecord.SpriteData[1].AttrValue)
    for i = 1, #roleData.SpriteRecord.SpriteData[1].AttrValue, 1 do
        print("roleData.SpriteRecord.SpriteData[1].AttrValue  ",roleData.SpriteRecord.SpriteData[1].AttrValue[i])
    end
    print("精灵的第一个  AddValue                 ",roleData.SpriteRecord.SpriteData[1].AddValue)
    for i = 1, #roleData.SpriteRecord.SpriteData[1].AddValue, 1 do
        print("roleData.SpriteRecord.SpriteData[1].AddValue  ",roleData.SpriteRecord.SpriteData[1].AddValue[i])
    end
    print("精灵的第一个  SkillID                 ",roleData.SpriteRecord.SpriteData[1].SkillID)
    print("精灵的第一个  PassiveSkillID                 ",roleData.SpriteRecord.SpriteData[1].PassiveSkillID)
    print("精灵的第一个  Level                 ",roleData.SpriteRecord.SpriteData[1].Level)
    print("精灵的第一个  EvolutionLevel                 ",roleData.SpriteRecord.SpriteData[1].EvolutionLevel)
    print("精灵的第一个  Exp                 ",roleData.SpriteRecord.SpriteData[1].Exp)
    print("精灵的第一个  PowerPoint                 ",roleData.SpriteRecord.SpriteData[1].PowerPoint)
    print("精灵的第一个  TrainExp                 ",roleData.SpriteRecord.SpriteData[1].TrainExp)
    print("精灵的第一个  EvoAttrID                 ",roleData.SpriteRecord.SpriteData[1].EvoAttrID)
    print("精灵的第一个  EvoAttrValue                 ",roleData.SpriteRecord.SpriteData[1].EvoAttrValue)
    print("精灵的第一个  ThisLevelEvoAttrID                 ",roleData.SpriteRecord.SpriteData[1].ThisLevelEvoAttrID)
    print("精灵的第一个  ThisLevelEvoAttrValue                 ",roleData.SpriteRecord.SpriteData[1].ThisLevelEvoAttrValue)
    UserData:GetInstance().SpriteList=roleData.SpriteRecord.SpriteData;

 -----------进场景前先发几个消息--------------------

    




 -----------------------------------------------


    ---------------------可以进入游戏场景了。------------------
    SceneManager:GetInstance():SwitchScene(SceneConfig.Test)

end

----------------发送完选角色消息由服务器返回的，看情况里面包含的是场景的配置。----------------------------
local function Handle_SceneCfg(msg)
    Logger.Log("msg changdu " .. #msg)
    Logger.Log("Handle_SceneCfg msg.SceneID = " .. tostring(msg.SceneID))
  
    if #msg.enemyWaves > 0 then
        Logger.Log("enemyWaves.unitName = " .. msg.enemyWaves[1].unitName)
    end

    -----发下这个消息，服务器在喂养消息发的时候，才能给回ItemChanged等背包物品改变的消息
    local tmpMsg = MsgIDMap[MsgIDDefine.DoEnterSceneArg].argMsg
    tmpMsg.sceneid = msg.SceneID;
    HallConnector:GetInstance():SendMessage(MsgIDDefine.DoEnterSceneArg, tmpMsg)
end

--------------------------里面包含的是，已经开启的系统id和已经关闭的系统id----------------------
local function Handle_Systems(msg)
    Logger.Log("Handle_Systems msg.sysIDs Length = " .. tostring(#msg.sysIDs) .. "closeSysIDs len = " ..
                   tostring(#msg.closeSysIDs) .. " closeSysIDs[1] =" .. msg.closeSysIDs[1])
end

-----------------------服务器下发的聊天的消息-------------------------------
local function Handle_ChatNotify(msg)
    local chatInfo = msg.chatinfo
    Logger.Log(
        "收到4256聊天消息   source = " .. chatInfo.source.name .. "  desname =" .. chatInfo.source.desname ..
            "目标 = " .. chatInfo.dest.roleid .. "   内容 =" .. chatInfo.info)
end

local function Handle_WorldChannelLeftTimesNtf(msg)
    local leftTimes = msg.leftTimes
    Logger.Log("收到37503  消息  leftTimes = " .. leftTimes)
end

---------服务器下发的称号列表

local function Handle_GetDesignationReq(msg)
    -- body
    Logger.Log("收到40256  消息  leftTimes = " .. leftTimes)
end

 MsgCallBackHandler.Handle_QueryGateArg = Handle_QueryGateArg
 MsgCallBackHandler.Handle_ErrorInfo = Handle_ErrorInfo
 MsgCallBackHandler.Handle_LoginChallenge = Handle_LoginChallenge
 MsgCallBackHandler.Handle_LoginRes = Handle_LoginRes
 MsgCallBackHandler.Handle_SelectRoleNewRes = Handle_SelectRoleNewRes
 MsgCallBackHandler.Handle_IBGiftIcon = Handle_IBGiftIcon
 MsgCallBackHandler.Handle_NotifyStartUpTypeToClient = Handle_NotifyStartUpTypeToClient
 MsgCallBackHandler.Handle_SceneCfg = Handle_SceneCfg
 MsgCallBackHandler.Handle_Systems = Handle_Systems
 MsgCallBackHandler.Handle_SelectRoleNtfData = Handle_SelectRoleNtfData
 MsgCallBackHandler.Handle_ChatNotify = Handle_ChatNotify

 MsgCallBackHandler.Handle_WorldChannelLeftTimesNtf = Handle_WorldChannelLeftTimesNtf

 MsgCallBackHandler.Handle_GetDesignationReq = Handle_GetDesignationReq

return MsgCallBackHandler
