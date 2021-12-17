--[[
-- added by passion @ 2021/5/28 18:42:38
-- SpritePanel视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local SpritePanelView = BaseClass("SpritePanelView", UIBaseView)
local base = UIBaseView
local SpriteTable = require("Config.Data.SpriteTable")
local SpriteSkill=require("Config.Data.SpriteSkill")
local ChangeSpriteInfo = nil
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这

    self.ctrl.AddListener();

    ChangeSpriteInfo = function(serverIndex, index)
        self.index = index;
        self:SetSpriteBasicsData(serverIndex, self.index);
    end

    UIManager:GetInstance():AddListener("ChangeSpriteInfo", ChangeSpriteInfo)

    self.svr_wrapgroup = self:AddComponent(UIWrapGroup, "Sprite_Content", require("UI.SpritePanel.View.SpriteSlot"))

    self.svr_wrapgroup:AddButtonGroup(UIToggleButton)
    self.server_list = {}
    for i = 1, #UserData:GetInstance().SpriteList, 1 do
        self.server_list[i] = UserData:GetInstance().SpriteList[i];
    end

    self.svr_wrapgroup:SetLength(table.count(self.server_list))
    self.svr_wrapgroup:SetOriginal(selected_server_index)
    self.svr_wrapgroup:ResetToBeginning()

    -- ItemPubTips:GetInstance().selectSpriteID=tonumber(UserData:GetInstance().SpriteList[1].SpriteID);
    -- ItemPubTips:GetInstance().selectSprite=self.svr_wrapgroup[1].spriteItem;

    self.Sprite_name = self:AddComponent(UIText, "Sprite_name"); -- 精灵名称
    self.Sprite_military = self:AddComponent(UIText, "Sprite_military"); -- 精灵战斗力
    self.Sprite_Level = self:AddComponent(UIImage, "Sprite_Level", AtlasConfig.AtlasTextures); -- 精灵品质背景

    -------------精灵天资
    self.attr_phyAtk_numImage = self:AddComponent(UIImage, "attr_phyAtk_numImage"); -- 物理攻击背景
    self.attr_phyAtk_Num = self:AddComponent(UIText, "attr_phyAtk_Num"); -- 物理攻击数据
    self.attr_phyAtk_numImage = self:AddComponent(UIImage, "attr_magicAtk_numImage"); -- 魔法攻击背景
    self.attr_phyAtk_Num = self:AddComponent(UIText, "attr_magicAtk_Num"); -- 魔法攻击数据
    self.attr_phyAtk_numImage = self:AddComponent(UIImage, "attr_BiggestLife_numImage"); -- 最大生命背景
    self.attr_phyAtk_Num = self:AddComponent(UIText, "attr_BiggestLife_Num"); -- 最大生命数据
    self.attr_phyAtk_numImage = self:AddComponent(UIImage, "attr_phyDef_numImage"); -- 物理防御背景
    self.attr_phyAtk_Num = self:AddComponent(UIText, "attr_phyDef_Num"); -- 物理防御数据
    self.attr_phyAtk_numImage = self:AddComponent(UIImage, "attr_mgcDef_numImage"); -- 魔法防御背景
    self.attr_phyAtk_Num = self:AddComponent(UIText, "attr_mgcDef_Num"); -- 魔法防御数据

    --------------------------------属性加成
    self.bonuses_phyAtk_num = self:AddComponent(UIText, "bonuses_phyAtk_num"); -- 物理攻击
    self.bonuses_phyDef_num = self:AddComponent(UIText, "bonuses_phyDef_num"); -- 物理防御
    self.bonuses_macAtk_num = self:AddComponent(UIText, "bonuses_macAtk_num"); -- 魔法攻击
    self.bonuses_macDef_num = self:AddComponent(UIText, "bonuses_macDef_num"); -- 魔法防御
    self.bonuses_life_num = self:AddComponent(UIText, "bonuses_life_num"); -- 最大生命


    self.SpriteSkill_Image=self:AddComponent(UIImage,"SpriteSkill_Image", AtlasConfig.AtlasTextures)--精灵技能的图片
    self.FeedBtn=self:AddComponent(UIButton,"FeedBtn");
    self.FeedBtn:SetOnClick(function()
        print("self.server_list[1].uid   ",self.server_list[1].uid)
        for i = 1, #SpriteTable, 1 do
            if tonumber(SpriteTable[i].SpriteID) == tonumber(self.server_list[1].SpriteID)  then
                print(SpriteTable[i].SpriteName)
            end
        end
   
        ---RpcC2G_SpriteOperation type  Sprite_Feed   itemid = 602   uid = 6969123641376178198
        print("spriteUID",self.server_list[1].uid)
        self.ctrl.AskRoleTitle(self.ctrl,self.server_list[1].uid,602)--发送喂养消息
        --self.ctrl.AskRoleBuyShop(self.ctrl);
    end)


    self.AddValueText = {self.bonuses_phyAtk_num, self.bonuses_macAtk_num, self.bonuses_life_num,
                        self.bonuses_phyDef_num, self.bonuses_macDef_num}

    for i = 1, #SpriteTable do
        if tonumber(SpriteTable[i].SpriteID) ==  (self.server_list[1].SpriteID) then
            self.index = i;
            break
        end
    end

    self:SetSpriteBasicsData(1, self.index);

end

function SpritePanelView:SetSpriteBasicsData(serverIndex, cfdIndex)
    self.serverIndex=serverIndex;

    for i = 1, #self.AddValueText, 1 do

        self.AddValueText[i]:SetText(math.floor(tonumber(self.server_list[serverIndex].AddValue[i])*10))
    end

    self.Sprite_name:SetText(SpriteTable[cfdIndex].SpriteName); -- 设置名称
    self.Sprite_Level:SetSpriteName("common/Universal/icondjdj_" .. tonumber(SpriteTable[self.index].SpriteQuality) ..
                                        ".png") -- 设置品质图片

    for i = 1, #SpriteSkill do
        if tonumber(SpriteSkill[i].SkillID) == tonumber(SpriteTable[cfdIndex].SpriteSkillID) then
            self.SpriteSkill_Image:SetSpriteName(tostring(SpriteSkill[i].Atlas).."/" .. tostring(SpriteSkill[i].Icon) ..".png") -- 设置技能图片
        end
    end
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

SpritePanelView.OnCreate = OnCreate
SpritePanelView.OnEnable = OnEnable
SpritePanelView.OnDestroy = OnDestroy

return SpritePanelView

