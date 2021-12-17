local SpriteItem = BaseClass("SpriteItem", UIBaseContainer)
local SpriteTable = require("Config.Data.SpriteTable")

local base = UIBaseContainer

local function OnCreate(self)
    base.OnCreate(self);

    self.Item_backgrournd = self:AddComponent(UIImage, "Item_backgrournd", AtlasConfig.AtlasTextures); -- common/Universal
    self.item_Image = self:AddComponent(UIImage, "item_Image", AtlasConfig.AtlasTextures) -- Wing
    self.item_selectBg = self:AddComponent(UIImage, "item_selectBg") -- 背景选中
    self.click = self:AddComponent(UIButton, "click");
    self.item_selectBg.gameObject:SetActive(false);
    self.click:SetOnClick(function()
        if ItemPubTips:GetInstance().selectSprite ~= nil then
            ItemPubTips:GetInstance().selectSprite.item_selectBg.gameObject:SetActive(false);
        end
        self.item_selectBg.gameObject:SetActive(true);
        ItemPubTips:GetInstance().selectSprite = self;
        ItemPubTips:GetInstance().selectSpriteID = tonumber(self.cfgdata.SpriteID);
        UIManager:GetInstance():Broadcast("ChangeSpriteInfo", self.real_index+1, self.index);
    end)
end

function SpriteItem:SetData(cfgdata, real_index)
    self.cfgdata = cfgdata;
    self.real_index = real_index;
    self:show();
end

function SpriteItem:show()
    self.item_selectBg.gameObject:SetActive(false);
    if ItemPubTips:GetInstance().selectSpriteID == self.cfgdata.SpriteID then -- 判断单例是否是上次记录的        
        self.item_selectBg.gameObject:SetActive(true);
    end

    if tonumber(self.cfgdata.SpriteID) == ItemPubTips:GetInstance().selectSpriteID then
        self.click.gameObject:SetActive(true);
    end
    self.index = 0

    for i = 1, #SpriteTable do
        if tonumber(SpriteTable[i].SpriteID) == tonumber(self.cfgdata.SpriteID) then
            self.index = i;
            break
        end
    end
    self.Item_backgrournd:SetSpriteName(
        "common/Universal/kuang_dj_" .. tonumber(SpriteTable[self.index].SpriteQuality) .. ".png");
    self.item_Image:SetSpriteName("Wing/" .. SpriteTable[self.index].SpriteIcon .. ".png");

end

SpriteItem.OnCreate = OnCreate;

return SpriteItem;
