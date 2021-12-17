--[[
-- added by passion @2021/5/24 21:23:26
-- +RoleTitle控制层
--]]
local SpritePanleCtrl = BaseClass("SpritePanleCtrl", UIBaseCtrl)
local MsgIDMap = require("Net/Config/MsgIDMap")
local MsgIDDefine = require "Net.Config.MsgIDDefine"

local index = 1

local function AddListener()
    --侦听服务器反馈回的事件
    DataManager:GetInstance():AddListener(MsgIDDefine.SpriteOperation, SpritePanleCtrl.Handle_SpriteOperationRes)
    DataManager:GetInstance():AddListener(MsgIDDefine.ItemChanged, SpritePanleCtrl.Handle_ItemChangedRes)
end

local function AskRoleTitle(self, spriteid, itemid) -- 向服务器请求称号列表
    local msg = {}

    print("开始向服务器发送请求称号列表的消息 ")
    msg = MsgIDMap[MsgIDDefine.SpriteOperation].argMsg
    msg.uid = spriteid
    msg.FeedItemID = itemid
    msg.Type = SpriteType_pb.Sprite_Feed

    print("*************   spriteid   " .. spriteid)
    print("开始向服务器发送请求称号列表的消息    喂养食物的ID   ", msg.FeedItemID)
    HallConnector:GetInstance():SendMessage(MsgIDDefine.SpriteOperation, msg)
end

local function AskRoleBuyShop(self) -- 向服务器购买物品
    local msg = {}

    msg = MsgIDMap[MsgIDDefine.IBBuyItem].argMsg
    msg.goodsid = 1
    msg.count = 1
    print("开始向服务器发送请求称号列表的消息    购买物品   ")
    HallConnector:GetInstance():SendMessage(MsgIDDefine.IBBuyItem, msg)
end

local function Handle_SpriteOperationRes(msg)
    -- body

    print("接收了服务器反馈的消息精灵改变的消息   ", msg)

    --UIManager:GetInstance():Broadcast("SetSerTitleModel",msg.dataList,index);
end

local function Handle_ItemChangedRes(msg)
    -- body
    print(
        string.format(
            "接收itemchanged的消息   %s  %s  %s  %s %s   %s   %s",
            #msg.RemoveItems,
            #msg.SwapItems,
            #msg.ChangeItems,
            #msg.VirtualItemID,
            #msg.VirtualItemCount,
            #msg.recylechangeitems,
            #msg.NewItems
        )
    )
    for i = 1, #msg.ChangeItems do
        print(msg.RemoveItems[i])
    end
print("1111111111111111111111 RemoveItems111111");
    for i = 1, #msg.RemoveItems do
        print(msg.RemoveItems[i])
    end
end

SpritePanleCtrl.AskRoleBuyShop = AskRoleBuyShop
SpritePanleCtrl.Handle_SpriteOperationRes = Handle_SpriteOperationRes
SpritePanleCtrl.AskRoleTitle = AskRoleTitle
SpritePanleCtrl.AddListener = AddListener
SpritePanleCtrl.Handle_ItemChangedRes = Handle_ItemChangedRes

return SpritePanleCtrl
