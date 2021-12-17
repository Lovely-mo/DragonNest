--[[
-- added by passion @ 2021/5/20 14:55:41
-- BagPanle视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local BagPanleView = BaseClass("BagPanleView", UIBaseView)
local base = UIBaseView
itemlist = require("Config.Data.Itemlist")
ItemType={}
ItemPubTips=require("UI.BagPanle.View.ItemPubTips")
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    ---无限滑动
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


    self.svr_wrapgroup = self:AddComponent(UIWrapGroup, "BagList_Content", require("UI.BagPanle.View.SlotItem"))

    self.svr_wrapgroup:AddButtonGroup(UIToggleButton)
    self.server_list = {}
    for i = 1,200 do
        self.server_list[i] = {}
    end

	self.BagDataList={}
	for i = 1, #UserData:GetInstance().EnterComplete.listPlayerBags do
		self.BagDataList[i]=UserData:GetInstance().EnterComplete.listPlayerBags[i];
	end
    print(" #UserData:GetInstance().EnterComplete.listPlayerBags              ", #UserData:GetInstance().EnterComplete.listPlayerBags)

    print("self.BagDataList          ",#self.BagDataList)
    self.svr_wrapgroup:SetLength(table.count(self.server_list))
    self.svr_wrapgroup:SetOriginal(selected_server_index)
    self.svr_wrapgroup:ResetToBeginning()

    ----设置背包容量的text
    self.BagDilatation_Btn = self:AddComponent(UIText, "BagDilatation_Btn");
    self.BagDilatation_Btn:SetText(#self.BagDataList .. "/" .. "200")

    ----分类中的toggle组
    self.FindComponent = FindComponent.ADD(self.gameObject);
    -- 显示全部物品的toggle
    self.Toggle_All = self.FindComponent:GetComponent("Toggle_All", typeof(Toggle));
    self.Toggle_All.onValueChanged:AddListener(function(b)
        self:SortBagItem(0);
    end)

    self.Toggle_grow = self.FindComponent:GetComponent("Toggle_grow", typeof(Toggle));
    self.Toggle_grow.onValueChanged:AddListener(function(b)
        self:SortBagItem(1);
    end)

    self.Toggle_gift = self.FindComponent:GetComponent("Toggle_gift", typeof(Toggle));
    self.Toggle_gift.onValueChanged:AddListener(function(b)
        self:SortBagItem(2);
    end)

    self.Toggle_home = self.FindComponent:GetComponent("Toggle_home", typeof(Toggle));
    self.Toggle_home.onValueChanged:AddListener(function(b)
        self:SortBagItem(3);
    end)

end

function BagPanleView:SortBagItem(index)  --排序的方法
    self.server_list = {}
    for i = 1,200 do
        self.server_list[i] = {}
    end

	self.BagDataList={}

    if index == 0 then
        for i = 1, #UserData:GetInstance().EnterComplete.listPlayerBags do
            self.BagDataList[i] = UserData:GetInstance().EnterComplete.listPlayerBags[i];
        end
	else
		local nums = 1
		for i = 1, #UserData:GetInstance().EnterComplete.listPlayerBags do
			local num = 0;
			for j = 1, #itemlist, 1 do
				if tonumber(itemlist[j].ItemID) ==
					math.floor(UserData:GetInstance().EnterComplete.listPlayerBags[i].ItemID) then
					num = j;
					break
				end
			end
			if tonumber(itemlist[num].BagType) == index then
				self.BagDataList[nums] = UserData:GetInstance().EnterComplete.listPlayerBags[i];
				nums = nums + 1;
			end
		end
    end	
    self.svr_wrapgroup:SetLength(table.count(self.server_list))
    self.svr_wrapgroup:SetOriginal(selected_server_index)
    self.svr_wrapgroup:ResetToBeginning()
end



-- 注册消息
local function OnAddListener(self)

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

BagPanleView.OnAddListener = OnAddListener
BagPanleView.OnCreate = OnCreate
BagPanleView.OnEnable = OnEnable
BagPanleView.OnDestroy = OnDestroy

return BagPanleView

