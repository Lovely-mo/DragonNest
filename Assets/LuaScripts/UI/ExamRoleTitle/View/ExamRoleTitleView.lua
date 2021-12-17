--[[
-- added by passion @ 2021/5/31 8:37:52
-- ExamRoleTitle视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local ExamRoleTitleView = BaseClass("ExamRoleTitleView", UIBaseView)
local base = UIBaseView
local TitleReturnView = nil
local  OpenAttr = nil

Designation=require("Config.Data.Designation")


local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这

	OpenAttr=function()
		self.zhanli_text:SetText(ExamManager:GetInstance().title.fightLabel:GetText());
		self.zhanli_des:SetText(ExamManager:GetInstance().title.AttsLabel:GetText());
		if ExamManager:GetInstance().title.attrActOpen==true then
			self.actArr_text:SetText("隐藏属性")
		else
			self.actArr_text:SetText("激活属性")
		end

		self.showtitle_des:SetText(ExamManager:GetInstance().title.cfgdata.Designation);
		if ExamManager:GetInstance().title.titleActOpen==true then
			self.showtitle_text:SetText("隐藏称号")
		else
			self.showtitle_text:SetText("激活称号")
		end
	end

    TitleReturnView = function(index)
        self:ShowTitle(index);
    end

	UIManager:GetInstance():AddListener("TitleReturnView",TitleReturnView)
	UIManager:GetInstance():AddListener("OpenAttr",OpenAttr)

	self.zhanli_text=self:AddComponent(UIText,"zhanli_text")
	self.zhanli_des=self:AddComponent(UIText,"zhanli_des")
	self.actArr_text=self:AddComponent(UIText,"actArr_text")
	self.actAttr_btn=self:AddComponent(UIButton,"actAttr_btn");
	self.actAttr_btn:SetOnClick(function()
		if ExamManager:GetInstance().title.attrActOpen==true then
			ExamManager:GetInstance().title.attrActOpen=false;
			ExamManager:GetInstance().title.attrAct.gameObject:SetActive(false)
			self.actArr_text:SetText("激活属性")
			ExamManager:GetInstance().attrActID=nil
		else
			ExamManager:GetInstance().title.attrActOpen=true;
			ExamManager:GetInstance().title.attrAct.gameObject:SetActive(true)
			self.actArr_text:SetText("隐藏属性")
			ExamManager:GetInstance().attrActID=(ExamManager:GetInstance().title.cfgdata.ID) 
		end
		
	end)


	self.showtitle_des=self:AddComponent(UIText,"showtitle_des")
	self.showtitle_text=self:AddComponent(UIText,"showtitle_text")
	self.showtitle_btn=self:AddComponent(UIButton,"showtitle_btn");
	self.showtitle_btn:SetOnClick(function()
		if ExamManager:GetInstance().title.titleActOpen==true then
			ExamManager:GetInstance().title.titleActOpen=false;
			ExamManager:GetInstance().title.titleAct.gameObject:SetActive(false)
			self.showtitle_text:SetText("激活称号")
			ExamManager:GetInstance().titleActID=nil
		else
			ExamManager:GetInstance().title.titleActOpen=true;
			ExamManager:GetInstance().title.titleAct.gameObject:SetActive(true)
			self.showtitle_text:SetText("隐藏称号")
			ExamManager:GetInstance().titleActID=(ExamManager:GetInstance().title.cfgdata.ID) 
		end
	end)

    self.ctrl.AddListener()--绑定事件
    self.ctrl.AskTitleList(1);--刚打开面板请求第一个列表

    self.FindComponent = FindComponent.ADD(self.gameObject);
    -- 显示全部物品的toggle
    self.Toggle1 = self.FindComponent:GetComponent("Toggle1", typeof(Toggle));
    self.Toggle2 = self.FindComponent:GetComponent("Toggle2", typeof(Toggle));
    self.Toggle3 = self.FindComponent:GetComponent("Toggle3", typeof(Toggle));
    self.Toggle4 = self.FindComponent:GetComponent("Toggle4", typeof(Toggle));
    self.Toggle5 = self.FindComponent:GetComponent("Toggle5", typeof(Toggle));
    self.Toggle6 = self.FindComponent:GetComponent("Toggle6", typeof(Toggle));

    self.ToggleTab = {self.Toggle1, self.Toggle2, self.Toggle3, self.Toggle4, self.Toggle5, self.Toggle6}
    -- 侦听点击事件
    for i = 1, #self.ToggleTab, 1 do
        self.ToggleTab[i].onValueChanged:AddListener(function(a)
            self.ctrl.AskTitleList(i)
        end)
    end

    self.svr_wrapgroup = self:AddComponent(UIWrapGroup, "Title_Content", require("UI.ExamRoleTitle.View.ExamTitleSlot"))

    self.svr_wrapgroup:AddButtonGroup(UIToggleButton)


	

end

function ExamRoleTitleView:ShowTitle(index)
    self.list = {}

    for i = 1, #Designation do
        if tonumber(Designation[i].Type)==index then
			table.insert(self.list,Designation[i]);
		end
    end

	self.server_list={}
	self.lists={}
	for i = 1, #self.list do
		local  num = 0
		for j = 1, #self.model.titleList do
			if tonumber(self.list[i].ID) ==tonumber(self.model.titleList[j].designationID) then
				table.insert(self.server_list,self.list[i])
				num=num+1;
				break;
			end
		end
		if num==0 then
			table.insert(self.lists,self.list[i])
		end
	end

	table.sort(self.server_list,function(a,b)
		return tonumber(a.SortID) < tonumber(b.SortID)
	end)
	table.sort(self.lists,function(a,b)
		return tonumber(a.SortID) < tonumber(b.SortID)
	end)
	for i = 1, #self.lists, 1 do
		table.insert(self.server_list,self.lists[i]);
	end



    self.svr_wrapgroup:SetLength(table.count(self.server_list))
    self.svr_wrapgroup:SetOriginal(-1)
    self.svr_wrapgroup:ResetToBeginning()
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

ExamRoleTitleView.OnCreate = OnCreate
ExamRoleTitleView.OnEnable = OnEnable
ExamRoleTitleView.OnDestroy = OnDestroy

return ExamRoleTitleView

