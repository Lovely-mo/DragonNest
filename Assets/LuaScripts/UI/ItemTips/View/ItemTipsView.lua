--[[
-- added by passion @ 2021/5/25 20:25:09
-- ItemTips视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local ItemTipsView = BaseClass("ItemTipsView", UIBaseView)
local base = UIBaseView

local Show = nil

local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    -- ItemPubTips:GetInstance().itemtips = self;
	-- print("ItemPubTips:GetInstance().itemtips   ",ItemPubTips:GetInstance().itemtips)
	-- print("self            ",self)
    Show = function()
        local entity=ItemPubTips:GetInstance().entity;
        self.Item_Image:SetSpriteName(entity.config.ItemAtlas .. "/" .. tostring(entity.config.ItemIcon) .. ".png");
        self.ItemName_text:SetText(entity.config.ItemName)
        self.grade_Image:SetSpriteName("common/Universal/icondjdj_" .. tonumber(entity.config.ItemQuality) .. ".png");
        self.grade_text:SetText(tonumber(entity.config.ReqLevel));
        self.profession_text:SetText("通用")
        self.itemType_textDes:SetText(ItemType[tonumber(entity.config.ItemType)])
        if entity.serverdata.isbind == true and entity.serverdata~=nil  then
            self.bang_textDes:SetText("已绑定");
        else
            self.bang_textDes:SetText("未绑定");
        end
        self.ItemDes_Text:SetText(entity.config.ItemDescription);

        for i = 1, #entity.components, 1 do
            local num=1;
            for j = 1, #self.btnTab, 1 do
                if self.btnTab[j].gameObject.name==entity.components[i]:GetBtnName() then
                    num=j;
                    break;
                end
            end
            if entity.components[i]:Can()=="" then 
                
                self.btnTab[num].gameObject:SetActive(true);
            else
                self.btnTab[num].gameObject:SetActive(false);
            end
        end

    end

    self.gameObject.transform.localPosition =Vector3(-279,0,0)
    self.Image_background = self:AddComponent(UIImage, "Image_background");
    self.Item_Image = self:AddComponent(UIImage, "Item_ImageDes", AtlasConfig.AtlasTextures); -- Item图片的位置
    self.ItemName_text = self:AddComponent(UIText, "ItemName_text"); -- Item图片名称
    self.grade_Image = self:AddComponent(UIImage, "grade_Image", AtlasConfig.AtlasTextures); -- item的品质
    self.grade_text = self:AddComponent(UIText, "grade_text"); -- 等级限制的文字
    self.profession_text = self:AddComponent(UIText, "profession_text"); -- 职业限制文字
    self.itemType_textDes = self:AddComponent(UIText, "itemType_textDes"); -- 物品类型的文字
    self.bang_textDes = self:AddComponent(UIText, "bang_textDes"); -- 绑定状态
    self.ItemDes_Text = self:AddComponent(UIText, "ItemDes_Text"); -- 道具说明

    self.btn_use=self:AddComponent(UIButton,"btn_use");--使用按钮
    self.btn_recycle=self:AddComponent(UIButton,"btn_recycle");--回收按钮
    self.btn_resolve=self:AddComponent(UIButton,"btn_resolve");--分解按钮
    self.btn_call=self:AddComponent(UIButton,"btn_call");--召唤按钮

    self.btnTab={self.btn_use,self.btn_recycle,self.btn_resolve,self.btn_call}


    UIManager:GetInstance():AddListener("打开bagItem介绍",Show);
    Show();
end


-- 打开
local function OnEnable(self)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这
end
-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

-- 注册消息
local function OnAddListener(self)
    
end

ItemTipsView.OnAddListener = OnAddListener
ItemTipsView.OnCreate = OnCreate
ItemTipsView.OnEnable = OnEnable
ItemTipsView.OnDestroy = OnDestroy

return ItemTipsView

