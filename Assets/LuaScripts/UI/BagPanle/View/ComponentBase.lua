----------------------------基类-----------------------------
local ActionBase = BaseClass("ActionBase");

function ActionBase:__init(_config, _server)
    self.data = _config
    self._server = _server
end

function ActionBase:Do()

end

function ActionBase:Can()
    return ""
end

function ActionBase:GetBtnName()
    return ""
end

function ActionBase:BtnEnable()
    return false
end

function ActionBase:RefreshData(_config, _server)
    self.data = _config
    self.server = _server
end

------------------------------物品使用的类-------------------------------------
local UseAction = BaseClass("UseAction", ActionBase)
local base = ActionBase

function UseAction:Do()
    base:Do();
    local notify = self:Can();
    if notify == "" then
        -- 发送协议
    end
end

function UseAction:Can()
    local str = "此物品不可使用"
    if tonumber(self.data.CanTrade) ==1 then
        str = ""
    end
    return str
end

function UseAction:GetBtnName()
    return "btn_use"
end
-- 1 2 9 10 11 13
function UseAction:BtnEnable()
    return self.data.ItemType == 1 or self.data.ItemType == 2 or self.data.ItemType == 9 or self.data.ItemType == 10 or
               self.data.ItemType == 11 or self.data.ItemType == 13
end

-----------------------------------物品分解的类------------------------------------------
local DecomposeAction = BaseClass("DecomposeAction", ActionBase)
local base = ActionBase

function DecomposeAction:Do()
    base:Do();
    local notify = self:Can();
    if notify == "" then
        -- 发送协议
    end
end

function DecomposeAction:Can()
    local str = "不能分解"
    if tonumber(self.data.IsCanRecycle) ==1  then
        str = ""
    end
    return str
end

function DecomposeAction:GetBtnName()
    return "btn_resolve"
end

function DecomposeAction:BtnEnable()
    return self.data.ItemType == 1 or self.data.ItemType == 2 or self.data.ItemType == 9 or self.data.ItemType == 10 or
               self.data.ItemType == 11 or self.data.ItemType == 13
end

-------------------------------Item数据----------------------------

local ItemList = require("Config.Data.Itemlist")

local ItemEntity = BaseClass("ItemEntity")

function ItemEntity:__init(serverdata)

    self.config = nil -- 接收具体数据的属性
    self.serverdata = serverdata;

    if serverdata~=nil then
        for i = 1, #ItemList, 1 do
            if tonumber(ItemList[i].ItemID) == math.floor(serverdata.ItemID) then
                self.config = ItemList[i]; -- 拿到配置表的具体数据
                break
            end
        end
    end
    

    self.components = 
                     {
                        UseAction.New(self.config, self.serverdata), 
                        DecomposeAction.New(self.config, self.serverdata)
                     }
end

function ItemEntity:Refresh(serverData)
    self.serverdata = serverData;
    if self.serverdata ~= nil then
        for i = 1, #ItemList, 1 do
            if tonumber(ItemList[i].ItemID) == math.floor(serverData.ItemID) then
                self.config = ItemList[i]; -- 拿到配置表的具体数据
                break
            end
        end
        for i = 1, #self.components, 1 do
            self.components[i]:RefreshData(self.config, serverData)
        end
    end
end

return ItemEntity;

