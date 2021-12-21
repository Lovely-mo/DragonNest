local UIServerInfoWrapItem = BaseClass("UIServerWrapItem", UIWrapComponent)
local base = UIWrapComponent

-- 创建
local function OnCreate(self)
	base.OnCreate(self)
end

-- 组件被复用时回调该函数，执行组件的刷新
local function OnRefresh(self, real_index, check)
end

-- 组件添加了按钮组，则按钮被点击时回调该函数
local function OnClick(self, toggle_btn, real_index, check)
end

UIServerInfoWrapItem.OnCreate = OnCreate
UIServerInfoWrapItem.OnRefresh = OnRefresh
UIServerInfoWrapItem.OnClick = OnClick

return UIServerInfoWrapItem