local UserData = BaseClass("UserData", Singleton)
UserData.Players = {}
UserData.playerCommonMsg = {} --存放金币和元宝
UserData.EnterComplete = {} --进入游戏拿到的相关数据 -- login.proto(S_EnterComplete);
UserData.EnterComplete.listPlayerBags = {} -- 存放背包数据
UserData.listPlayerEquipment = {}--存放装备
UserData.index=0;
UserData.SpriteList={}

--初始化背包
function UserData:Init(len)
    for i = 1, len do
        if self.EnterComplete.listPlayerBags[i] == nil then
            self.EnterComplete.listPlayerBags[i] = {
                playerBagId = 0, -- 玩家背包唯一编号
                goodsType = 0, -- 物品类别 (1:装备 2：药品 3：材料)
                itemId = 0, -- 物品编号 (或装备唯一编号)
                itemIndex = i, -- 物品索引下标 --背包格子
                num = 0, -- 数量
                isBinding = 0, -- 是否绑定(1:是 0：否)
                state = 0 -- 物品状态 (1:背包)
            }
        end
    end
    UIManager:GetInstance():Broadcast("刷新背包")
end

--设置背包数据
function UserData:SetItem(data)
    if data == nil then
        return
    end

    --print(data.itemId,"!!!!!!!!!!!!!!!!!!!!!!")

    --为了显示服务器返回的物品数据
    if self.EnterComplete.listPlayerBags[data.itemIndex] then
        if math.floor(data.goodsType) == 1 then
            for key, value in pairs(self.listPlayerEquipment) do
                if value.playerEquipmentId then
                    if value.playerEquipmentId == data.itemId and value.state == 1 then
                        self.EnterComplete.listPlayerBags[data.itemIndex].itemId = math.floor(value.equipmentId)
                    end
                end
            end
        else
            self.EnterComplete.listPlayerBags[data.itemIndex].itemId = math.floor(data.itemId)
        end
        self.EnterComplete.listPlayerBags[data.itemIndex].playerBagId = math.floor(data.playerBagId)
        self.EnterComplete.listPlayerBags[data.itemIndex].goodsType = math.floor(data.goodsType)
        --self.EnterComplete.listPlayerBags[data.itemIndex].itemId = math.floor(data.itemId)
        self.EnterComplete.listPlayerBags[data.itemIndex].itemIndex = math.floor(data.itemIndex)
        self.EnterComplete.listPlayerBags[data.itemIndex].num = math.floor(data.num)
        self.EnterComplete.listPlayerBags[data.itemIndex].isBinding = math.floor(data.isBinding)
        self.EnterComplete.listPlayerBags[data.itemIndex].state = math.floor(data.state)
    end
   
end

return UserData
