-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('TeamCountInfo_pb')


TEAMCOUNTINFO = protobuf.Descriptor();
local TEAMCOUNTINFO_TEAMTYPE_FIELD = protobuf.FieldDescriptor();
local TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD = protobuf.FieldDescriptor();
local TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD = protobuf.FieldDescriptor();
local TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD = protobuf.FieldDescriptor();
local TEAMCOUNTINFO_HELPCOUNT_FIELD = protobuf.FieldDescriptor();

TEAMCOUNTINFO_TEAMTYPE_FIELD.name = "teamType"
TEAMCOUNTINFO_TEAMTYPE_FIELD.full_name = ".KKSG.TeamCountInfo.teamType"
TEAMCOUNTINFO_TEAMTYPE_FIELD.number = 1
TEAMCOUNTINFO_TEAMTYPE_FIELD.index = 0
TEAMCOUNTINFO_TEAMTYPE_FIELD.label = 1
TEAMCOUNTINFO_TEAMTYPE_FIELD.has_default_value = false
TEAMCOUNTINFO_TEAMTYPE_FIELD.default_value = 0
TEAMCOUNTINFO_TEAMTYPE_FIELD.type = 5
TEAMCOUNTINFO_TEAMTYPE_FIELD.cpp_type = 1

TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.name = "finishCountToday"
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.full_name = ".KKSG.TeamCountInfo.finishCountToday"
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.number = 2
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.index = 1
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.label = 1
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.has_default_value = false
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.default_value = 0
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.type = 5
TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD.cpp_type = 1

TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.name = "buyCountToday"
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.full_name = ".KKSG.TeamCountInfo.buyCountToday"
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.number = 3
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.index = 2
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.label = 1
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.has_default_value = false
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.default_value = 0
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.type = 5
TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD.cpp_type = 1

TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.name = "extraAddCount"
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.full_name = ".KKSG.TeamCountInfo.extraAddCount"
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.number = 4
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.index = 3
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.label = 1
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.has_default_value = false
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.default_value = 0
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.type = 5
TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD.cpp_type = 1

TEAMCOUNTINFO_HELPCOUNT_FIELD.name = "helpcount"
TEAMCOUNTINFO_HELPCOUNT_FIELD.full_name = ".KKSG.TeamCountInfo.helpcount"
TEAMCOUNTINFO_HELPCOUNT_FIELD.number = 5
TEAMCOUNTINFO_HELPCOUNT_FIELD.index = 4
TEAMCOUNTINFO_HELPCOUNT_FIELD.label = 1
TEAMCOUNTINFO_HELPCOUNT_FIELD.has_default_value = false
TEAMCOUNTINFO_HELPCOUNT_FIELD.default_value = 0
TEAMCOUNTINFO_HELPCOUNT_FIELD.type = 13
TEAMCOUNTINFO_HELPCOUNT_FIELD.cpp_type = 3

TEAMCOUNTINFO.name = "TeamCountInfo"
TEAMCOUNTINFO.full_name = ".KKSG.TeamCountInfo"
TEAMCOUNTINFO.nested_types = {}
TEAMCOUNTINFO.enum_types = {}
TEAMCOUNTINFO.fields = {TEAMCOUNTINFO_TEAMTYPE_FIELD, TEAMCOUNTINFO_FINISHCOUNTTODAY_FIELD, TEAMCOUNTINFO_BUYCOUNTTODAY_FIELD, TEAMCOUNTINFO_EXTRAADDCOUNT_FIELD, TEAMCOUNTINFO_HELPCOUNT_FIELD}
TEAMCOUNTINFO.is_extendable = false
TEAMCOUNTINFO.extensions = {}

TeamCountInfo = protobuf.Message(TEAMCOUNTINFO)

