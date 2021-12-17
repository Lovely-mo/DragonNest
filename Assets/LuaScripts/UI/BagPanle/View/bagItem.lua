local bagItem = BaseClass("bagItem", UIBaseContainer)
local base = UIBaseContainer

local function OnCreate(self)
    base.OnCreate(self);

    self.Item_backgrournd = self:AddComponent(UIImage, "Item_backgrournd", AtlasConfig.AtlasTextures); ---物品品质的背景
    self.Num_Text = self:AddComponent(UIText, "Num_Text"); -- 物品数量的text
    self.Bang_Text = self:AddComponent(UIText, "Bang_Text"); -- 物品是否绑定的文字
    self.Grade_Text = self:AddComponent(UIText, "Grade_Text"); -- 物品等级的文字
    self.item_Image = self:AddComponent(UIImage, "item_Image", AtlasConfig.AtlasTextures); ---物品图片的显示图片
    self.click=self:AddComponent(UIButton,"click");
    self.click:SetOnClick(function()
        ItemPubTips:GetInstance().entity=self.entity
        UIManager:GetInstance():OpenWindow(UIWindowNames.ItemTips);
        
        -- if ItemPubTips:GetInstance().itemtips~=nil then
        --     ItemPubTips:GetInstance():Show(self.entity);
        -- end
        UIManager:GetInstance():Broadcast("打开bagItem介绍");
    end)
end

function bagItem:SetData(entity)

    self.entity = entity;
    self:show();
end

function bagItem:show()
    if self.entity.serverdata ~= nil then
        if math.floor(self.entity.serverdata.ItemCount) ~= 1 then -- 是否开启数量文字的显示
            self.Num_Text.gameObject:SetActive(true);
            self.Num_Text:SetText(math.floor(self.entity.serverdata.ItemCount)) -- 设置物品的数量
        else
            self.Num_Text.gameObject:SetActive(false);
        end

        if self.entity.serverdata.isbind == true then -- 是否开启绑字
            self.Bang_Text.gameObject:SetActive(true); -- 打开绑定文字
        else
            self.Bang_Text.gameObject:SetActive(false);
        end

        if tonumber(self.entity.config.ItemQuality) ~= 0 and tonumber(self.entity.config.ItemType) == 7 then -- 是否开启等级文字显示       
            self.Grade_Text.gameObject:SetActive(true);

            self.Grade_Text:SetText("LV." .. tonumber(self.entity.config.ItemQuality)) -- 设置物品的等级
        end
        -- 设置图片的item信息  
        self.item_Image:SetSpriteName(self.entity.config.ItemAtlas .. "/" .. tostring(self.entity.config.ItemIcon) ..
                                          ".png");

        -- 品质等级

        self.Item_backgrournd:SetSpriteName("common/Universal/kuang_dj_" .. tostring(self.entity.config.ItemQuality) ..
                                                ".png");
    end

end


bagItem.OnCreate = OnCreate;

return bagItem;
