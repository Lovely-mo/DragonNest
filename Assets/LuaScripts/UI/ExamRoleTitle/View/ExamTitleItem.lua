local ExamTitleItem = BaseClass("ExamTitleItem", UIBaseContainer)
local base = UIBaseContainer

local itemlist = require("Config.Data.Itemlist")
local AttributeList = require("Config.Data.AttributeList")

local function OnCreate(self)
    base.OnCreate(self);
    -- 物品选中背景
    self.select_bg = self:AddComponent(UIImage, "select_bg", AtlasConfig.AtlasTextures);
    -- 战力text
    self.fightLabel = self:AddComponent(UIText, "fightLabel");
    -- 标签名称text
    self.AttsLabel = self:AddComponent(UIText, "AttsLabel");
    -- 标签介绍text
    self.DescLabel = self:AddComponent(UIText, "DescLabel");
    -- 图片
    self.sprite = self:AddComponent(UIImage, "sprite", AtlasConfig.AtlasTextures);
    -- 图片名称
    self.sprite_text = self:AddComponent(UIText, "sprite_text");
    -- 锁的图片
    self.UnCompleted = self:AddComponent(UIImage, "UnCompleted")
    -- 称号激活
    self.titleAct = self:AddComponent(UIText, "titleAct");
    -- 属性激活
    self.attrAct = self:AddComponent(UIText, "attrAct");

    self.titleAct.gameObject:SetActive(false); -- 默认关闭选中背景
    self.attrAct.gameObject:SetActive(false); -- 默认关闭选中背景

    self.titleActOpen=false;
    self.attrActOpen=false;

    -- 点击事件
    self.click = self:AddComponent(UIButton, "click");
    self.click:SetOnClick(function()
        if self.isOpen == true then
            if ExamManager:GetInstance().title ~= nil then
                ExamManager:GetInstance().title.select_bg.gameObject:SetActive(false);

            end
            self.select_bg.gameObject:SetActive(true);
            ExamManager:GetInstance().title = self;
            ExamManager:GetInstance().titleID = self.cfgdata.ID;
            UIManager:GetInstance():Broadcast("OpenAttr");
        end

    end)

end

function ExamTitleItem:SetData(cfgdata, serverdata)
    self.serverdata = serverdata;
    self.cfgdata = cfgdata
    self.isOpen = false;
    self:show();
end

function ExamTitleItem:show()
    self.UnCompleted.gameObject:SetActive(true); -- 默认开启黑色背景
    self.select_bg.gameObject:SetActive(false); -- 默认关闭选中背景
    self.attrAct.gameObject:SetActive(false);
    self.titleAct.gameObject:SetActive(false);

    for i = 1, #self.serverdata, 1 do
        if tonumber(self.serverdata[i].designationID) == tonumber(self.cfgdata.ID) then
            self.UnCompleted.gameObject:SetActive(false); -- 当服务器有数据时关闭黑色背景
            self.isOpen=true;
        end
    end

    if self.isOpen==true then
        if  ExamManager:GetInstance().titleID == self.cfgdata.ID then
            self.select_bg.gameObject:SetActive(true);
        end
        if ExamManager:GetInstance().attrActID ==self.cfgdata.ID then
            self.attrAct.gameObject:SetActive(true);
        end
        if ExamManager:GetInstance().titleActID ==self.cfgdata.ID then
            self.titleAct.gameObject:SetActive(true);
        end
    end

    local zhanli = 0
    local typeTab = {}
    typeTab = self:getSplitStr(tostring(self.cfgdata.Attribute), "|", typeTab); ---切割属性的加成
    local AttsLabelStr = ""; -- 接收属性加成的字符串
    for i = 1, #typeTab, 1 do
        local nature = {}; -- 单个属性加成的字符串
        nature = self:getSplitStr(tostring(typeTab[i]), "=", nature); -- 拿到单个数据

        for i = 1, #AttributeList, 1 do
            if AttributeList[i + 1] ~= nil then
                if tonumber(nature[1]) == tonumber(AttributeList[i].AttributeID) and
                    tonumber(AttributeList[i].AttributeID) ~= tonumber(AttributeList[i + 2].AttributeID) then
                    if tonumber(AttributeList[i].Profession) == 7 or tonumber(AttributeList[i].Profession) == 0 then
                        AttsLabelStr = AttsLabelStr .. AttributeList[i].AttributeName .. "+" .. nature[2] .. "  ";

                        if tonumber(AttributeList[i].Weight) == 0 then
                            zhanli = zhanli + tonumber(nature[2]) * 10
                        else
                            zhanli = zhanli + tonumber(nature[2]) * tonumber(AttributeList[i].Weight)
                        end

                        break
                    end

                end
            end

        end
    end
    self.fightLabel:SetText("+" .. math.floor(zhanli)) -- 战斗力的设置
    self.AttsLabel:SetText(AttsLabelStr) -- 加的称号的属性

    self.DescLabel:SetText(self.cfgdata.Explanation); -- 标签介绍赋值
    if self.cfgdata.Effect == "" then -- 设置标签图片
        self.sprite.gameObject:SetActive(false);
        self.sprite_text.gameObject:SetActive(true);
        self.sprite_text:SetText(self.cfgdata.Designation)
    else
        self.sprite.gameObject:SetActive(true);
        self.sprite_text.gameObject:SetActive(false);
        self.sprite:SetSpriteName(self.cfgdata.Atlas .. "/" .. self.cfgdata.Effect .. ".png");
    end

end

function ExamTitleItem:getSplitStr(logStr, breakpointsStr, t)
    local i = 0
    local j = 1
    local z = string.len(breakpointsStr)
    while true do
        i = string.find(logStr, breakpointsStr, i + 1) -- 查找下一行
        if i == nil then
            table.insert(t, string.sub(logStr, j, -1))
            break
        end
        table.insert(t, string.sub(logStr, j, i - 1))
        j = i + z
    end
    return t

end

ExamTitleItem.OnCreate = OnCreate;

return ExamTitleItem;
