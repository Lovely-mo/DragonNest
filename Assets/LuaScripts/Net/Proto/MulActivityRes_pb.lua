-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local MULACTIVITINFO_PB = require("MulActivitInfo_pb")
local ERRORCODE_PB = require("ErrorCode_pb")
module('MulActivityRes_pb')


MULACTIVITYRES = protobuf.Descriptor();
local MULACTIVITYRES_ACTINFO_FIELD = protobuf.FieldDescriptor();
local MULACTIVITYRES_MYGUILDLEVEL_FIELD = protobuf.FieldDescriptor();
local MULACTIVITYRES_ERRCODE_FIELD = protobuf.FieldDescriptor();

MULACTIVITYRES_ACTINFO_FIELD.name = "actinfo"
MULACTIVITYRES_ACTINFO_FIELD.full_name = ".KKSG.MulActivityRes.actinfo"
MULACTIVITYRES_ACTINFO_FIELD.number = 1
MULACTIVITYRES_ACTINFO_FIELD.index = 0
MULACTIVITYRES_ACTINFO_FIELD.label = 3
MULACTIVITYRES_ACTINFO_FIELD.has_default_value = false
MULACTIVITYRES_ACTINFO_FIELD.default_value = {}
MULACTIVITYRES_ACTINFO_FIELD.message_type = MULACTIVITINFO_PB.MULACTIVITINFO
MULACTIVITYRES_ACTINFO_FIELD.type = 11
MULACTIVITYRES_ACTINFO_FIELD.cpp_type = 10

MULACTIVITYRES_MYGUILDLEVEL_FIELD.name = "myguildlevel"
MULACTIVITYRES_MYGUILDLEVEL_FIELD.full_name = ".KKSG.MulActivityRes.myguildlevel"
MULACTIVITYRES_MYGUILDLEVEL_FIELD.number = 2
MULACTIVITYRES_MYGUILDLEVEL_FIELD.index = 1
MULACTIVITYRES_MYGUILDLEVEL_FIELD.label = 1
MULACTIVITYRES_MYGUILDLEVEL_FIELD.has_default_value = false
MULACTIVITYRES_MYGUILDLEVEL_FIELD.default_value = 0
MULACTIVITYRES_MYGUILDLEVEL_FIELD.type = 5
MULACTIVITYRES_MYGUILDLEVEL_FIELD.cpp_type = 1

MULACTIVITYRES_ERRCODE_FIELD.name = "errcode"
MULACTIVITYRES_ERRCODE_FIELD.full_name = ".KKSG.MulActivityRes.errcode"
MULACTIVITYRES_ERRCODE_FIELD.number = 3
MULACTIVITYRES_ERRCODE_FIELD.index = 2
MULACTIVITYRES_ERRCODE_FIELD.label = 1
MULACTIVITYRES_ERRCODE_FIELD.has_default_value = false
MULACTIVITYRES_ERRCODE_FIELD.default_value = nil
MULACTIVITYRES_ERRCODE_FIELD.enum_type = ERRORCODE_PB.ERRORCODE
MULACTIVITYRES_ERRCODE_FIELD.type = 14
MULACTIVITYRES_ERRCODE_FIELD.cpp_type = 8

MULACTIVITYRES.name = "MulActivityRes"
MULACTIVITYRES.full_name = ".KKSG.MulActivityRes"
MULACTIVITYRES.nested_types = {}
MULACTIVITYRES.enum_types = {}
MULACTIVITYRES.fields = {MULACTIVITYRES_ACTINFO_FIELD, MULACTIVITYRES_MYGUILDLEVEL_FIELD, MULACTIVITYRES_ERRCODE_FIELD}
MULACTIVITYRES.is_extendable = false
MULACTIVITYRES.extensions = {}

MulActivityRes = protobuf.Message(MULACTIVITYRES)
