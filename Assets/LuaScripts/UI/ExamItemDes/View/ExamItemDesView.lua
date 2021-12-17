--[[
-- added by passion @ 2021/5/31 8:37:36
-- ExamItemDes视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local ExamItemDesView = BaseClass("ExamItemDesView", UIBaseView)
local base = UIBaseView

local SetUItemDes=nil 

local function OnCreate(self)
	base.OnCreate(self)
	-- 窗口生命周期内保持的成员变量放这

	ItemType={
        [1]="通用礼包",
        [2]="礼包",
        [3]="虚拟道具",
        [4]="材料",
        [5]="时装",
        [6]="纹章",
        [7]="龙玉",
        [8]="时装碎片",
        [9]="宝箱",
        [10]="补给品",
        [11]="钓鱼产出",
        [12]="基础装备模板",
        [13]="坐骑丹",
        [14]="坐骑饲料",
        [15]="门票",
        [16]="金属板",
        [17]="精灵食物",
        [18]="精灵",
        [19]="图鉴",
        [20]="种子",
        [21]="公会种子",
        [22]="食谱",
        [23]="料理",
        [26]="潘多拉遗物",
        [27]="古代人的仪式",
        [29]="称号道具",
        [30]="英雄体验券",
        [31]="龙器",
        [34]="背包扩充券",
        [35]="祝福",
        [36]="铭文",
        [37]="红包",
    }

	SetUItemDes=function(serverData,cfgdata)
		self:SetPanle(serverData,cfgdata)
	end

	self.gameObject.transform.localPosition=Vector3(self.gameObject.transform.localPosition.x,self.gameObject.transform.localPosition.y,-540)

	self.Close_Btn=self:AddComponent(UIButton,"Close_Btn");
	self.Close_Btn:SetOnClick(function()
		UIManager:GetInstance():CloseWindow(UIWindowNames.ExamItemDes)
	end)



	--物品图片
	self.Item_ImageDes=self:AddComponent(UIImage,"Item_ImageDes",AtlasConfig.AtlasTextures);
	--物品名称text
	self.ItemName_text=self:AddComponent(UIText,"ItemName_text");
	--物品等级图片
	self.grade_Image=self:AddComponent(UIImage,"grade_Image",AtlasConfig.AtlasTextures);
	--等级限制text
	self.grade_text=self:AddComponent(UIText,"grade_text");
	--职业限制text
	self.profession_text=self:AddComponent(UIText,"profession_text");
	--物品类型text
	self.itemType_textDes=self:AddComponent(UIText,"itemType_textDes");
	--物品是否绑定text
	self.bang_textDes=self:AddComponent(UIText,"bang_textDes");
	--物品介绍text
	self.ItemDes_Text=self:AddComponent(UIText,"ItemDes_Text");

	UIManager:GetInstance():AddListener("SetUItemDes",SetUItemDes);
	SetUItemDes(ExamManager:GetInstance().serverData,ExamManager:GetInstance().cfgdata)
end
function ExamItemDesView:SetPanle(serverData,cfgdata)
	self.serverData=serverData;
	self.cfgdata=cfgdata
	self.grade_Image:SetSpriteName("common/Universal/icondjdj_"..tonumber(self.cfgdata.ItemQuality) ..".png");--设置品质背景
    self.Item_ImageDes:SetSpriteName(self.cfgdata.ItemAtlas.."/"..self.cfgdata.ItemIcon..".png");--设置图片
	self.ItemName_text:SetText(self.cfgdata.ItemName);
	self.profession_text:SetText("通用");
	self.grade_text:SetText(tonumber(self.cfgdata.ReqLevel) );
	self.itemType_textDes:SetText(ItemType[tonumber(self.cfgdata.ItemType)]);
	if self.serverData.isbind==true then
		self.bang_textDes:SetText("已绑定");
	else
		self.bang_textDes:SetText("未绑定");
	end
	self.ItemDes_Text:SetText(self.cfgdata.ItemDescription);
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




ExamItemDesView.OnCreate = OnCreate
ExamItemDesView.OnEnable = OnEnable
ExamItemDesView.OnDestroy = OnDestroy

return ExamItemDesView


