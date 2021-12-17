local bagItem = BaseClass("bagItem", UIBaseContainer)
local Designation = require("Config.Data.Designation") -- 对接服务器的称号配置表
local AttributeList = require("Config.Data.AttributeList") -- 称号加强属性的配置表
local base = UIBaseContainer

local function OnCreate(self)
    base.OnCreate(self);

    self.select_bg = self:AddComponent(UIImage, "select_bg"); ---选中的背景
    self.select_bg.gameObject:SetActive(false);
    self.fightLabel = self:AddComponent(UIText, "fightLabel"); -- 称号加的战斗力
    self.AttsLabel = self:AddComponent(UIText, "AttsLabel"); -- 称号加的属性
    self.DescLabel = self:AddComponent(UIText, "DescLabel"); -- 称号获取的方法
    self.sprite = self:AddComponent(UIImage, "sprite", AtlasConfig.AtlasTextures); ---有图片时显示图片
    self.sprite_text = self:AddComponent(UIText, "sprite_text"); -- 没有图片显示称号名称
    self.UnCompleted = self:AddComponent(UIImage, "UnCompleted") -- 当玩家没有这个称号开启
    self.UnCompleted.gameObject:SetActive(true) -- 默认关闭
    self.click = self:AddComponent(UIButton, "click");

    self.click:SetOnClick(function() -- 点击称号的方法
        if self.IsOpen == true then -- 是否开启了这个称号
            if ItemPubTips:GetInstance().Title ~= nil then
                ItemPubTips:GetInstance().Title.select_bg.gameObject:SetActive(false);--关闭上次选择的
            end
            self.select_bg.gameObject:SetActive(true);
            ItemPubTips:GetInstance().TitleID = self.cfgdata.ID   --选择的ID
            ItemPubTips:GetInstance().Title = self   --选择的对象
        else
            print("您还没有获得该称号")
        end
    end)

end

function bagItem:SetData(cfgdata, serverdata)

    self.cfgdata = cfgdata;
    self.serverdata = serverdata
    self.IsOpen=false;
    self:show();
end

function bagItem:show()
    self.select_bg.gameObject:SetActive(false);   --选中背景全部默认关闭
    self.UnCompleted.gameObject:SetActive(true)   --开启锁的背景
    for i = 1, #self.serverdata, 1 do
        if tonumber(self.cfgdata.ID) == tonumber(self.serverdata[i].designationID) then
            self.UnCompleted.gameObject:SetActive(false) -- 当服务器反馈回来列表没有数据  开启锁
            self.IsOpen=true;  
        end
    end

    if self.IsOpen == true then -- 选中背景
        if ItemPubTips:GetInstance().TitleID == self.cfgdata.ID then    --判断单例是否是上次记录的        
            self.select_bg.gameObject:SetActive(true);
        end
    end

    if self.cfgdata ~= nil then
        print("self.cfgdata       ",self.cfgdata.Designation)
        local zhanli=0
        local typeTab = {}
        typeTab = self:getSplitStr(tostring(self.cfgdata.Attribute), "|", typeTab); ---切割属性的加成
        local AttsLabelStr = ""; -- 接收属性加成的字符串
        for i = 1, #typeTab, 1 do
            local nature = {}; -- 单个属性加成的字符串
            nature = self:getSplitStr(tostring(typeTab[i]), "=", nature); -- 拿到单个数据

            for i = 1, #AttributeList, 1 do
                if AttributeList[i+1]~=nil then
                    if tonumber(nature[1]) == tonumber(AttributeList[i].AttributeID) and tonumber(AttributeList[i].AttributeID)~=tonumber(AttributeList[i+2].AttributeID) then
                        if tonumber(AttributeList[i].Profession)==7 or tonumber(AttributeList[i].Profession)==0 then
                            AttsLabelStr = AttsLabelStr .. AttributeList[i].AttributeName .. "+" .. nature[2] .. "  ";
    
                            if tonumber(AttributeList[i].Weight)==0 then
                                zhanli=zhanli+tonumber(nature[2]) *10
                            else
                                zhanli=zhanli+tonumber(nature[2]) *tonumber(AttributeList[i].Weight)
                            end
                            
                            break
                        end   
                        
                    end
                end
                
            end
        end
        self.fightLabel:SetText("+"..math.floor(zhanli) )   --战斗力的设置
        self.AttsLabel:SetText(AttsLabelStr) -- 加的称号的属性
        self.DescLabel:SetText(self.cfgdata.Explanation); -- 根据配置表判断是否需要加载图片还是文字
        if self.cfgdata.Effect ~= '' then
            self.sprite.gameObject:SetActive(true)
            self.sprite:SetSpriteName(tostring(self.cfgdata.Atlas) .. "/" .. tostring(self.cfgdata.Effect) .. ".png")
            self.sprite_text.gameObject:SetActive(false)
        else
            self.sprite_text.gameObject:SetActive(true)
            self.sprite_text:SetText(self.cfgdata.Designation)
            self.sprite.gameObject:SetActive(false)
        end
    end
end

function bagItem:getSplitStr(logStr, breakpointsStr, t)
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

bagItem.OnCreate = OnCreate;

return bagItem;
