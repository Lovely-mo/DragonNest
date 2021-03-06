-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local WEEKLYTASKINFO_PB = require("WeeklyTaskInfo_pb")
local ERRORCODE_PB = require("ErrorCode_pb")
local TASKHELPINFO_PB = require("TaskHelpInfo_pb")
module('GetWeeklyTaskInfoRes_pb')


GETWEEKLYTASKINFORES = protobuf.Descriptor();
local GETWEEKLYTASKINFORES_RESULT_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_TASK_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_SCORE_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_HELPINFO_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_LEFTTIME_FIELD = protobuf.FieldDescriptor();
local GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD = protobuf.FieldDescriptor();

GETWEEKLYTASKINFORES_RESULT_FIELD.name = "result"
GETWEEKLYTASKINFORES_RESULT_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.result"
GETWEEKLYTASKINFORES_RESULT_FIELD.number = 1
GETWEEKLYTASKINFORES_RESULT_FIELD.index = 0
GETWEEKLYTASKINFORES_RESULT_FIELD.label = 1
GETWEEKLYTASKINFORES_RESULT_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_RESULT_FIELD.default_value = nil
GETWEEKLYTASKINFORES_RESULT_FIELD.enum_type = ERRORCODE_PB.ERRORCODE
GETWEEKLYTASKINFORES_RESULT_FIELD.type = 14
GETWEEKLYTASKINFORES_RESULT_FIELD.cpp_type = 8

GETWEEKLYTASKINFORES_TASK_FIELD.name = "task"
GETWEEKLYTASKINFORES_TASK_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.task"
GETWEEKLYTASKINFORES_TASK_FIELD.number = 2
GETWEEKLYTASKINFORES_TASK_FIELD.index = 1
GETWEEKLYTASKINFORES_TASK_FIELD.label = 3
GETWEEKLYTASKINFORES_TASK_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_TASK_FIELD.default_value = {}
GETWEEKLYTASKINFORES_TASK_FIELD.message_type = WEEKLYTASKINFO_PB.WEEKLYTASKINFO
GETWEEKLYTASKINFORES_TASK_FIELD.type = 11
GETWEEKLYTASKINFORES_TASK_FIELD.cpp_type = 10

GETWEEKLYTASKINFORES_SCORE_FIELD.name = "score"
GETWEEKLYTASKINFORES_SCORE_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.score"
GETWEEKLYTASKINFORES_SCORE_FIELD.number = 3
GETWEEKLYTASKINFORES_SCORE_FIELD.index = 2
GETWEEKLYTASKINFORES_SCORE_FIELD.label = 1
GETWEEKLYTASKINFORES_SCORE_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_SCORE_FIELD.default_value = 0
GETWEEKLYTASKINFORES_SCORE_FIELD.type = 13
GETWEEKLYTASKINFORES_SCORE_FIELD.cpp_type = 3

GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.name = "askhelp_num"
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.askhelp_num"
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.number = 4
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.index = 3
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.label = 1
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.default_value = 0
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.type = 13
GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD.cpp_type = 3

GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.name = "rewarded_box"
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.rewarded_box"
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.number = 5
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.index = 4
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.label = 3
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.default_value = {}
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.type = 13
GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD.cpp_type = 3

GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.name = "accept_level"
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.accept_level"
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.number = 6
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.index = 5
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.label = 1
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.default_value = 0
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.type = 13
GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD.cpp_type = 3

GETWEEKLYTASKINFORES_HELPINFO_FIELD.name = "helpinfo"
GETWEEKLYTASKINFORES_HELPINFO_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.helpinfo"
GETWEEKLYTASKINFORES_HELPINFO_FIELD.number = 7
GETWEEKLYTASKINFORES_HELPINFO_FIELD.index = 6
GETWEEKLYTASKINFORES_HELPINFO_FIELD.label = 3
GETWEEKLYTASKINFORES_HELPINFO_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_HELPINFO_FIELD.default_value = {}
GETWEEKLYTASKINFORES_HELPINFO_FIELD.message_type = TASKHELPINFO_PB.TASKHELPINFO
GETWEEKLYTASKINFORES_HELPINFO_FIELD.type = 11
GETWEEKLYTASKINFORES_HELPINFO_FIELD.cpp_type = 10

GETWEEKLYTASKINFORES_LEFTTIME_FIELD.name = "lefttime"
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.lefttime"
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.number = 8
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.index = 7
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.label = 1
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.default_value = 0
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.type = 13
GETWEEKLYTASKINFORES_LEFTTIME_FIELD.cpp_type = 3

GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.name = "remain_free_refresh_count"
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.full_name = ".KKSG.GetWeeklyTaskInfoRes.remain_free_refresh_count"
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.number = 9
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.index = 8
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.label = 1
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.has_default_value = false
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.default_value = 0
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.type = 13
GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD.cpp_type = 3

GETWEEKLYTASKINFORES.name = "GetWeeklyTaskInfoRes"
GETWEEKLYTASKINFORES.full_name = ".KKSG.GetWeeklyTaskInfoRes"
GETWEEKLYTASKINFORES.nested_types = {}
GETWEEKLYTASKINFORES.enum_types = {}
GETWEEKLYTASKINFORES.fields = {GETWEEKLYTASKINFORES_RESULT_FIELD, GETWEEKLYTASKINFORES_TASK_FIELD, GETWEEKLYTASKINFORES_SCORE_FIELD, GETWEEKLYTASKINFORES_ASKHELP_NUM_FIELD, GETWEEKLYTASKINFORES_REWARDED_BOX_FIELD, GETWEEKLYTASKINFORES_ACCEPT_LEVEL_FIELD, GETWEEKLYTASKINFORES_HELPINFO_FIELD, GETWEEKLYTASKINFORES_LEFTTIME_FIELD, GETWEEKLYTASKINFORES_REMAIN_FREE_REFRESH_COUNT_FIELD}
GETWEEKLYTASKINFORES.is_extendable = false
GETWEEKLYTASKINFORES.extensions = {}

GetWeeklyTaskInfoRes = protobuf.Message(GETWEEKLYTASKINFORES)

