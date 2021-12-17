--使用方法  testSingleton1:GetInstance():Show(entity)
local ItemPubTips = BaseClass("ItemPubTips",Singleton)

function ItemPubTips:__init()
    self.entity=nil;--背包物品的单个数据
    self.TitleID=nil;
    self.Title=nil
    self.selectSprite=nil
    self.selectSpriteID=nil
end



return ItemPubTips