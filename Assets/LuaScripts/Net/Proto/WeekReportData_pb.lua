-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local ENUM_PB = require("Enum_pb")
module('WeekReportData_pb')


WEEKREPORTDATA = protobuf.Descriptor();
local WEEKREPORTDATA_TYPE_FIELD = protobuf.FieldDescriptor();
local WEEKREPORTDATA_JOINCOUNT_FIELD = protobuf.FieldDescriptor();
local WEEKREPORTDATA_LASTJOINTIME_FIELD = protobuf.FieldDescriptor();

WEEKREPORTDATA_TYPE_FIELD.name = "type"
WEEKREPORTDATA_TYPE_FIELD.full_name = ".KKSG.WeekReportData.type"
WEEKREPORTDATA_TYPE_FIELD.number = 1
WEEKREPORTDATA_TYPE_FIELD.index = 0
WEEKREPORTDATA_TYPE_FIELD.label = 1
WEEKREPORTDATA_TYPE_FIELD.has_default_value = false
WEEKREPORTDATA_TYPE_FIELD.default_value = nil
WEEKREPORTDATA_TYPE_FIELD.enum_type = ENUM_PB.WEEKREPORTDATATYPE
WEEKREPORTDATA_TYPE_FIELD.type = 14
WEEKREPORTDATA_TYPE_FIELD.cpp_type = 8

WEEKREPORTDATA_JOINCOUNT_FIELD.name = "joincount"
WEEKREPORTDATA_JOINCOUNT_FIELD.full_name = ".KKSG.WeekReportData.joincount"
WEEKREPORTDATA_JOINCOUNT_FIELD.number = 2
WEEKREPORTDATA_JOINCOUNT_FIELD.index = 1
WEEKREPORTDATA_JOINCOUNT_FIELD.label = 1
WEEKREPORTDATA_JOINCOUNT_FIELD.has_default_value = false
WEEKREPORTDATA_JOINCOUNT_FIELD.default_value = 0
WEEKREPORTDATA_JOINCOUNT_FIELD.type = 5
WEEKREPORTDATA_JOINCOUNT_FIELD.cpp_type = 1

WEEKREPORTDATA_LASTJOINTIME_FIELD.name = "lastjointime"
WEEKREPORTDATA_LASTJOINTIME_FIELD.full_name = ".KKSG.WeekReportData.lastjointime"
WEEKREPORTDATA_LASTJOINTIME_FIELD.number = 3
WEEKREPORTDATA_LASTJOINTIME_FIELD.index = 2
WEEKREPORTDATA_LASTJOINTIME_FIELD.label = 1
WEEKREPORTDATA_LASTJOINTIME_FIELD.has_default_value = false
WEEKREPORTDATA_LASTJOINTIME_FIELD.default_value = 0
WEEKREPORTDATA_LASTJOINTIME_FIELD.type = 13
WEEKREPORTDATA_LASTJOINTIME_FIELD.cpp_type = 3

WEEKREPORTDATA.name = "WeekReportData"
WEEKREPORTDATA.full_name = ".KKSG.WeekReportData"
WEEKREPORTDATA.nested_types = {}
WEEKREPORTDATA.enum_types = {}
WEEKREPORTDATA.fields = {WEEKREPORTDATA_TYPE_FIELD, WEEKREPORTDATA_JOINCOUNT_FIELD, WEEKREPORTDATA_LASTJOINTIME_FIELD}
WEEKREPORTDATA.is_extendable = false
WEEKREPORTDATA.extensions = {}

WeekReportData = protobuf.Message(WEEKREPORTDATA)

