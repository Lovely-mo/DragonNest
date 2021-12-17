-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local PVPBASEDATA_PB = require("PvpBaseData_pb")
local PVPHISTORY_PB = require("PvpHistory_pb")
local PVPREQTYPE_PB = require("PvpReqType_pb")
local ERRORCODE_PB = require("ErrorCode_pb")
module('PvpRes_pb')


PVPRES = protobuf.Descriptor();
local PVPRES_BASEDATA_FIELD = protobuf.FieldDescriptor();
local PVPRES_HISTORY_FIELD = protobuf.FieldDescriptor();
local PVPRES_REQTYPE_FIELD = protobuf.FieldDescriptor();
local PVPRES_ERR_FIELD = protobuf.FieldDescriptor();

PVPRES_BASEDATA_FIELD.name = "basedata"
PVPRES_BASEDATA_FIELD.full_name = ".KKSG.PvpRes.basedata"
PVPRES_BASEDATA_FIELD.number = 1
PVPRES_BASEDATA_FIELD.index = 0
PVPRES_BASEDATA_FIELD.label = 1
PVPRES_BASEDATA_FIELD.has_default_value = false
PVPRES_BASEDATA_FIELD.default_value = nil
PVPRES_BASEDATA_FIELD.message_type = PVPBASEDATA_PB.PVPBASEDATA
PVPRES_BASEDATA_FIELD.type = 11
PVPRES_BASEDATA_FIELD.cpp_type = 10

PVPRES_HISTORY_FIELD.name = "history"
PVPRES_HISTORY_FIELD.full_name = ".KKSG.PvpRes.history"
PVPRES_HISTORY_FIELD.number = 2
PVPRES_HISTORY_FIELD.index = 1
PVPRES_HISTORY_FIELD.label = 1
PVPRES_HISTORY_FIELD.has_default_value = false
PVPRES_HISTORY_FIELD.default_value = nil
PVPRES_HISTORY_FIELD.message_type = PVPHISTORY_PB.PVPHISTORY
PVPRES_HISTORY_FIELD.type = 11
PVPRES_HISTORY_FIELD.cpp_type = 10

PVPRES_REQTYPE_FIELD.name = "reqtype"
PVPRES_REQTYPE_FIELD.full_name = ".KKSG.PvpRes.reqtype"
PVPRES_REQTYPE_FIELD.number = 3
PVPRES_REQTYPE_FIELD.index = 2
PVPRES_REQTYPE_FIELD.label = 1
PVPRES_REQTYPE_FIELD.has_default_value = false
PVPRES_REQTYPE_FIELD.default_value = nil
PVPRES_REQTYPE_FIELD.enum_type = PVPREQTYPE_PB.PVPREQTYPE
PVPRES_REQTYPE_FIELD.type = 14
PVPRES_REQTYPE_FIELD.cpp_type = 8

PVPRES_ERR_FIELD.name = "err"
PVPRES_ERR_FIELD.full_name = ".KKSG.PvpRes.err"
PVPRES_ERR_FIELD.number = 4
PVPRES_ERR_FIELD.index = 3
PVPRES_ERR_FIELD.label = 1
PVPRES_ERR_FIELD.has_default_value = false
PVPRES_ERR_FIELD.default_value = nil
PVPRES_ERR_FIELD.enum_type = ERRORCODE_PB.ERRORCODE
PVPRES_ERR_FIELD.type = 14
PVPRES_ERR_FIELD.cpp_type = 8

PVPRES.name = "PvpRes"
PVPRES.full_name = ".KKSG.PvpRes"
PVPRES.nested_types = {}
PVPRES.enum_types = {}
PVPRES.fields = {PVPRES_BASEDATA_FIELD, PVPRES_HISTORY_FIELD, PVPRES_REQTYPE_FIELD, PVPRES_ERR_FIELD}
PVPRES.is_extendable = false
PVPRES.extensions = {}

PvpRes = protobuf.Message(PVPRES)

