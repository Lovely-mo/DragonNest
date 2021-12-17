--使用方法  testSingleton1:GetInstance():Show(entity)
local ExamManager = BaseClass("ExamManager",Singleton)

function ExamManager:__init()
    self.cfgdata=nil 
    self.serverData=nil

    self.title=nil
    self.titleID=nil
    self.titleActID=nil
    self.attrActID=nil
end



return ExamManager