-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('SurviveRecord_pb')


SURVIVERECORD = protobuf.Descriptor();
local SURVIVERECORD_LASTWEEKUPTIME_FIELD = protobuf.FieldDescriptor();
local SURVIVERECORD_POINT_FIELD = protobuf.FieldDescriptor();
local SURVIVERECORD_TOPCOUNT_FIELD = protobuf.FieldDescriptor();
local SURVIVERECORD_GIVEREWARD_FIELD = protobuf.FieldDescriptor();

SURVIVERECORD_LASTWEEKUPTIME_FIELD.name = "lastweekuptime"
SURVIVERECORD_LASTWEEKUPTIME_FIELD.full_name = ".KKSG.SurviveRecord.lastweekuptime"
SURVIVERECORD_LASTWEEKUPTIME_FIELD.number = 1
SURVIVERECORD_LASTWEEKUPTIME_FIELD.index = 0
SURVIVERECORD_LASTWEEKUPTIME_FIELD.label = 1
SURVIVERECORD_LASTWEEKUPTIME_FIELD.has_default_value = false
SURVIVERECORD_LASTWEEKUPTIME_FIELD.default_value = 0
SURVIVERECORD_LASTWEEKUPTIME_FIELD.type = 13
SURVIVERECORD_LASTWEEKUPTIME_FIELD.cpp_type = 3

SURVIVERECORD_POINT_FIELD.name = "point"
SURVIVERECORD_POINT_FIELD.full_name = ".KKSG.SurviveRecord.point"
SURVIVERECORD_POINT_FIELD.number = 2
SURVIVERECORD_POINT_FIELD.index = 1
SURVIVERECORD_POINT_FIELD.label = 1
SURVIVERECORD_POINT_FIELD.has_default_value = false
SURVIVERECORD_POINT_FIELD.default_value = 0
SURVIVERECORD_POINT_FIELD.type = 13
SURVIVERECORD_POINT_FIELD.cpp_type = 3

SURVIVERECORD_TOPCOUNT_FIELD.name = "topcount"
SURVIVERECORD_TOPCOUNT_FIELD.full_name = ".KKSG.SurviveRecord.topcount"
SURVIVERECORD_TOPCOUNT_FIELD.number = 3
SURVIVERECORD_TOPCOUNT_FIELD.index = 2
SURVIVERECORD_TOPCOUNT_FIELD.label = 1
SURVIVERECORD_TOPCOUNT_FIELD.has_default_value = false
SURVIVERECORD_TOPCOUNT_FIELD.default_value = 0
SURVIVERECORD_TOPCOUNT_FIELD.type = 13
SURVIVERECORD_TOPCOUNT_FIELD.cpp_type = 3

SURVIVERECORD_GIVEREWARD_FIELD.name = "givereward"
SURVIVERECORD_GIVEREWARD_FIELD.full_name = ".KKSG.SurviveRecord.givereward"
SURVIVERECORD_GIVEREWARD_FIELD.number = 4
SURVIVERECORD_GIVEREWARD_FIELD.index = 3
SURVIVERECORD_GIVEREWARD_FIELD.label = 1
SURVIVERECORD_GIVEREWARD_FIELD.has_default_value = false
SURVIVERECORD_GIVEREWARD_FIELD.default_value = false
SURVIVERECORD_GIVEREWARD_FIELD.type = 8
SURVIVERECORD_GIVEREWARD_FIELD.cpp_type = 7

SURVIVERECORD.name = "SurviveRecord"
SURVIVERECORD.full_name = ".KKSG.SurviveRecord"
SURVIVERECORD.nested_types = {}
SURVIVERECORD.enum_types = {}
SURVIVERECORD.fields = {SURVIVERECORD_LASTWEEKUPTIME_FIELD, SURVIVERECORD_POINT_FIELD, SURVIVERECORD_TOPCOUNT_FIELD, SURVIVERECORD_GIVEREWARD_FIELD}
SURVIVERECORD.is_extendable = false
SURVIVERECORD.extensions = {}

SurviveRecord = protobuf.Message(SURVIVERECORD)

