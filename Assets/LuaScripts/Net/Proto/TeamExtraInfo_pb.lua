-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('TeamExtraInfo_pb')


TEAMEXTRAINFO = protobuf.Descriptor();
local TEAMEXTRAINFO_PPTLIMIT_FIELD = protobuf.FieldDescriptor();
local TEAMEXTRAINFO_COSTINDEX_FIELD = protobuf.FieldDescriptor();
local TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD = protobuf.FieldDescriptor();

TEAMEXTRAINFO_PPTLIMIT_FIELD.name = "pptlimit"
TEAMEXTRAINFO_PPTLIMIT_FIELD.full_name = ".KKSG.TeamExtraInfo.pptlimit"
TEAMEXTRAINFO_PPTLIMIT_FIELD.number = 1
TEAMEXTRAINFO_PPTLIMIT_FIELD.index = 0
TEAMEXTRAINFO_PPTLIMIT_FIELD.label = 1
TEAMEXTRAINFO_PPTLIMIT_FIELD.has_default_value = false
TEAMEXTRAINFO_PPTLIMIT_FIELD.default_value = 0
TEAMEXTRAINFO_PPTLIMIT_FIELD.type = 13
TEAMEXTRAINFO_PPTLIMIT_FIELD.cpp_type = 3

TEAMEXTRAINFO_COSTINDEX_FIELD.name = "costindex"
TEAMEXTRAINFO_COSTINDEX_FIELD.full_name = ".KKSG.TeamExtraInfo.costindex"
TEAMEXTRAINFO_COSTINDEX_FIELD.number = 2
TEAMEXTRAINFO_COSTINDEX_FIELD.index = 1
TEAMEXTRAINFO_COSTINDEX_FIELD.label = 1
TEAMEXTRAINFO_COSTINDEX_FIELD.has_default_value = false
TEAMEXTRAINFO_COSTINDEX_FIELD.default_value = 0
TEAMEXTRAINFO_COSTINDEX_FIELD.type = 13
TEAMEXTRAINFO_COSTINDEX_FIELD.cpp_type = 3

TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.name = "league_teamname"
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.full_name = ".KKSG.TeamExtraInfo.league_teamname"
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.number = 3
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.index = 2
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.label = 1
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.has_default_value = false
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.default_value = ""
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.type = 9
TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD.cpp_type = 9

TEAMEXTRAINFO.name = "TeamExtraInfo"
TEAMEXTRAINFO.full_name = ".KKSG.TeamExtraInfo"
TEAMEXTRAINFO.nested_types = {}
TEAMEXTRAINFO.enum_types = {}
TEAMEXTRAINFO.fields = {TEAMEXTRAINFO_PPTLIMIT_FIELD, TEAMEXTRAINFO_COSTINDEX_FIELD, TEAMEXTRAINFO_LEAGUE_TEAMNAME_FIELD}
TEAMEXTRAINFO.is_extendable = false
TEAMEXTRAINFO.extensions = {}

TeamExtraInfo = protobuf.Message(TEAMEXTRAINFO)
