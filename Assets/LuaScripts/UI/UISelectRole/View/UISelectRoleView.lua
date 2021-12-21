--[[
-- added by passion @ 2021/12/18 10:00:51
-- UISelectRole视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UISelectRoleView = BaseClass("UISelectRoleView", UIBaseView)
--[[
-- added by passion @ 2021/12/18 10:00:51
-- UISelectRole视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UISelectRoleView = BaseClass("UISelectRoleView", UIBaseView)
local MsgIDDefine = require("Net/Config/MsgIDDefine")
local MsgIDMap = require("Net/Config/MsgIDMap")
local base = UIBaseView
local RoleButtonsParent="RoleButtons"
local HaveaRoleRoot="BeSelectedRoot/HaveaRoleRoot"
local NoRoleRoot="BeSelectedRoot/NoRoleRoot"
local HaveaName=HaveaRoleRoot.."/NameRoot/Name"
local HaveaLevel=HaveaRoleRoot.."/LevelTips/Level"
local HaveaBut=HaveaRoleRoot.."/Submit"
local NoName=NoRoleRoot.."/NoNameRoot/NoName"
local NoBut=NoRoleRoot.."/NoSubmit"

local function OnCreate(self)
	base.OnCreate(self)
	-- 窗口生命周期内保持的成员变量放这
	self.HaveaRoleRoot=self.transform:Find(HaveaRoleRoot)
	self.NoRoleRoot=self.transform:Find(NoRoleRoot)
	self.RoleButtonsParent = self.transform:Find(RoleButtonsParent)
	self.ShowName=self:AddComponent(UIText,HaveaName)
	self.ShowLevel=self:AddComponent(UIText,HaveaLevel)
	self.HaveaBut=self:AddComponent(UIButton,HaveaBut)
	self.NoName=self:AddComponent(UIInput,NoName)
	self.NoBut=self:AddComponent(UIButton,NoBut)
	self.index=1;
	
	for i = 1, self.RoleButtonsParent.transform.childCount, 1 do
		local but=self:AddComponent(UIButton,RoleButtonsParent.."/"..self.RoleButtonsParent.transform:GetChild(i-1).transform.name)
		but:SetOnClick(function ()
			self.index=i
			self:SetActivePanel(i)
			
		end)
	end

	--确定按钮
	self.HaveaBut:SetOnClick(function ()
		local tmpMsg = MsgIDMap[MsgIDDefine.SelectRoleNew].argMsg
    	tmpMsg.index = self.index-1 ---传从0开始的索引
    	HallConnector:GetInstance():SendMessage(MsgIDDefine.SelectRoleNew, tmpMsg)
	end)



	--创建按钮
	self.NoBut:SetOnClick(function ()
		local tmpMsg = MsgIDMap[MsgIDDefine.CreateRoleNew].argMsg
		
     	tmpMsg.type =  RoleBriefInfo_pb[self.index-1]---传从0开始的索引
     	tmpMsg.name = self.NoName:GetText();
     	HallConnector:GetInstance():SendMessage(MsgIDDefine.SelectRoleNew, tmpMsg)
	end)
end
-- 打开
local function OnEnable(self)
	base.OnEnable(self)
	-- 窗口关闭时可以清理的成员变量放这
	self:SetActivePanel(1)
end
-- 关闭
local function OnDestroy(self)
	base.OnDestroy(self)
	-- 清理成员变量
end


local function SetActivePanel(self,index)

	if(self.model.RoleListModel[index]==nil) then
		self.HaveaRoleRoot.transform.gameObject:SetActive(false)
		self.NoRoleRoot.transform.gameObject:SetActive(true)
	else
	
		self.HaveaRoleRoot.transform.gameObject:SetActive(true)
		self.NoRoleRoot.transform.gameObject:SetActive(false)
		self.ShowName:SetText(self.model.RoleListModel[index].name)
		self.ShowLevel:SetText(math.ceil(self.model.RoleListModel[index].level))
		
		
	end
	CS.RoleShow.Init(tonumber(index-1))
end

UISelectRoleView.SetActivePanel = SetActivePanel
UISelectRoleView.OnCreate = OnCreate
UISelectRoleView.OnEnable = OnEnable
UISelectRoleView.OnDestroy = OnDestroy

return UISelectRoleView


