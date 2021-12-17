local bagItem = BaseClass("bagItem", UIBaseContainer)
local base = UIBaseContainer

local itemlist = require("Config.Data.Itemlist")

local function OnCreate(self)
    base.OnCreate(self);
    -- 物品品质背景
    self.Item_backgrournd = self:AddComponent(UIImage, "Item_backgrournd", AtlasConfig.AtlasTextures);
    -- 物品图片
    self.item_Image = self:AddComponent(UIImage, "item_Image", AtlasConfig.AtlasTextures);
    -- 物品数量text
    self.Num_Text = self:AddComponent(UIText, "Num_Text");
    -- 物品是否绑定text
    self.Bang_Text = self:AddComponent(UIText, "Bang_Text");
    -- 物品等级的显示
    self.Grade_Text = self:AddComponent(UIText, "Grade_Text");
    -- 物品点击
    self.click = self:AddComponent(UIButton, "click");
    self.click:SetOnClick(function()

        if self.serverData ~= nil then
            ExamManager:GetInstance().serverData = self.serverData;
            ExamManager:GetInstance().cfgdata = self.cfgdata;
            UIManager:GetInstance():OpenWindow(UIWindowNames.ExamItemDes);
            UIManager:GetInstance():Broadcast("SetUItemDes", self.serverData, self.cfgdata);
        end
    end)

end

function bagItem:SetData(serverData)
    self.serverData = serverData;
    if self.serverData ~= nil then
        self:show();
    end
end

function bagItem:show()
    for i = 1, #itemlist, 1 do -- 在配置表中拿到这条数据
        if tonumber(self.serverData.ItemID) == tonumber(itemlist[i].ItemID) then
            self.cfgdata = itemlist[i];
            break
        end
    end

    self.Item_backgrournd:SetSpriteName("common/Universal/kuang_dj_" .. tonumber(self.cfgdata.ItemQuality) .. ".png"); -- 设置品质背景
    self.item_Image:SetSpriteName(self.cfgdata.ItemAtlas .. "/" .. self.cfgdata.ItemIcon .. ".png"); -- 设置图片
    if tonumber(self.serverData.ItemCount) ~= 1 then -- 是否开启数量的text
        self.Num_Text.gameObject:SetActive(true);
        self.Num_Text:SetText(math.floor(self.serverData.ItemCount)) -- 设置数量
    else
        self.Num_Text.gameObject:SetActive(false);
    end

    if self.serverData.isbind == true then -- 是否开启邦字
        self.Bang_Text.gameObject:SetActive(true);
    else
        self.Bang_Text.gameObject:SetActive(false);
    end

    if tonumber(self.cfgdata.ItemType) == 4 or tonumber(self.cfgdata.ItemType) == 7 then -- 是否开启等级显示
        self.Grade_Text.gameObject:SetActive(true);
        self.Grade_Text:SetText("LV." .. tonumber(self.cfgdata.ItemQuality))
    else
        self.Grade_Text.gameObject:SetActive(false);
    end

end

bagItem.OnCreate = OnCreate;

return bagItem;
